using Core.Models;
using Interfaces.Core.DataSources;
using Interfaces.Core.Transformations;

namespace Core.Pipelines
{
    public class EtlPipeline
    {
        private readonly IEnumerable<IDataSource<TransactionModel>> _dataSources;
        private readonly IEnumerable<ITransformationRule<TransactionModel>> _transformationRules;

        public EtlPipeline(IEnumerable<IDataSource<TransactionModel>> dataSources, IEnumerable<ITransformationRule<TransactionModel>> transformationRules)
        {
            _dataSources = dataSources;
            _transformationRules = transformationRules;
        }

        public async Task<IEnumerable<TransactionModel>> RunAsync()
        {
            var allTransactions = new List<TransactionModel>();

            // Extract data from all sources in parallel
            var extractTransactions = _dataSources.Select(source => source.ExtractAsync());
            var extractedData = await Task.WhenAll(extractTransactions);

            foreach (var transactions in extractedData)
            {
                allTransactions.AddRange(transactions);
            }

            // Apply transformation rules
            foreach (var rule in _transformationRules)
            {
                allTransactions = rule.Apply(allTransactions).ToList();
            }

            return allTransactions;
        }       
    }  
}
