using MySeriesService.Interfaces;

namespace MySeriesService.SeriesEvaluator.Fibonacci
{
    public class FibonacciSeriesEvaluator : ISeriesEvaluator
    {
        public string SeriesName => "Fibonacci";

        public long? Evaluate(int index)
        {
            return index == 1 || index == 2 ? index : Evaluate(index - 1) + Evaluate(index - 2);
        }
    }
}
