using System.Threading.Tasks;

namespace MySeriesService.Interfaces
{
    public interface ISeriesCache
    {
        string Host { get; }

        Task<long?> GetAsync(string series, int index);
        Task<bool> SetAsync(string series, int index, long value);
    }
}
