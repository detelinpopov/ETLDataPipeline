using Core.Models;
using Core.Models.Operations;
using Interfaces.Core;
using Interfaces.Core.Transformations;

namespace Core.Pipelines
{
    public class EtlPipeline
    {
        private readonly IEnumerable<IDataSource<ExtractTransactionsResult>> _dataSources;
        private readonly IEnumerable<ITransformationRule<TransactionModel>> _transformationRules;

        public EtlPipeline(IEnumerable<IDataSource<ExtractTransactionsResult>> dataSources, IEnumerable<ITransformationRule<TransactionModel>> transformationRules)
        {
            _dataSources = dataSources;
            _transformationRules = transformationRules;
        }

        public async Task<ExtractTransactionsResult> RunAsync()
        { 
            var extractedDataResult = new ExtractTransactionsResult();

            // Extract data from all sources in parallel
            var extractTransactionsResult = _dataSources.Select(source => source.ExtractAsync());
            var extractedDataFromAllSources = await Task.WhenAll(extractTransactionsResult);

            foreach (var extractedResultFromSource in extractedDataFromAllSources)
            {
                ((List<TransactionModel>)extractedDataResult.Transactions).AddRange(extractedResultFromSource.Transactions);
            }

            // Apply transformation rules
            foreach (var rule in _transformationRules)
            {
                extractedDataResult.Transactions = rule.Apply(extractedDataResult.Transactions).ToList();
            }

            return extractedDataResult;
        }       
    }  
}
