using Interfaces.Core.DataSources;
using Interfaces.Core.Transformations;
using Interfaces.Sql.Entities;

namespace Core.Pipelines
{
    public class EtlPipeline
    {
        private readonly IEnumerable<IDataSource> _dataSources;
        private readonly IEnumerable<ITransformationRule> _transformationRules;

        public EtlPipeline(IEnumerable<IDataSource> dataSources, IEnumerable<ITransformationRule> transformationRules)
        {
            _dataSources = dataSources;
            _transformationRules = transformationRules;
        }

        public async Task<IEnumerable<ITransaction>> RunAsync()
        {
            var allTransactions = new List<ITransaction>();

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
