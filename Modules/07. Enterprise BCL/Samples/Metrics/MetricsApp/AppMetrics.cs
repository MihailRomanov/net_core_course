using System.Diagnostics.Metrics;

namespace MetricsApp
{
    public class AppMetrics
    {
        public const string MeterName = "MetricsApp";
        public const string IterationsCounterName = "metrics.app.iterations";
        public const string WorkerNumberTagName = "worker.number";

        private readonly Counter<int> iterations;

        public AppMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);
            iterations = meter.CreateCounter<int>(IterationsCounterName);
        }

        public void AddIteration() => iterations.Add(1);
        public void AddIteration(int workerNumber)
            => iterations.Add(
                1,
                new KeyValuePair<string, object?>(
                        WorkerNumberTagName, workerNumber));

    }
}
