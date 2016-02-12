namespace MySeriesService.Interfaces
{
    public interface ISeriesEvaluator
    {
        string SeriesName { get; }
        long? Evaluate(int index);
    }
}
