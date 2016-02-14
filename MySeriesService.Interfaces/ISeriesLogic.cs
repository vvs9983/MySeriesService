using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySeriesService.Interfaces
{
    public interface ISeriesLogic
    {
        Task<long> Evaluate(string series, int index);
        Task<IEnumerable<string>> GetSeriesEvaluators();
    }
}
