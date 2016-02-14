using MySeriesService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySeriesService.SeriesLogic
{
    public class SeriesLogic : ISeriesLogic
    {
        private readonly IEnumerable<ISeriesEvaluator> _evaluators;
        private readonly ISeriesCache _cache;

        public SeriesLogic(IEnumerable<ISeriesEvaluator> evaluators, ISeriesCache cache)
        {
            if (evaluators == null)
            {
                throw new ArgumentNullException(nameof(evaluators));
            }
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            _evaluators = evaluators;
            _cache = cache;
        }

        public async Task<long> Evaluate(string series, int index)
        {
            var result = await _cache.GetAsync(series, index);
            if (result != null)
            {
                return result.Value;
            }

            var evaluator = _evaluators.FirstOrDefault(x => x.SeriesName.Equals(series, StringComparison.OrdinalIgnoreCase));

            if (evaluator == null)
            {
                throw new ArgumentNullException($"Series {series} does not have a registered evaluator");
            }

            result = evaluator.Evaluate(index);

            _cache.SetAsync(series, index, result.Value);

            return await Task.FromResult(result.Value);
        }

        public async Task<IEnumerable<string>> GetSeriesEvaluators()
        {
            return await Task.FromResult(_evaluators.Select(x => x.SeriesName));
        }
    }
}
