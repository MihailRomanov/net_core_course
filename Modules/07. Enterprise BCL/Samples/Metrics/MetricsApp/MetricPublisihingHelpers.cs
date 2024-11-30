using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;

namespace MetricsApp
{
    internal static class MetricPublisihingHelpers
    {
        private static void SetMeasurement<T>(
            Instrument instrument,
            T measurement,
            ReadOnlySpan<KeyValuePair<string, object?>> tags,
            object? state)
        {
            var tagString = string.Join(" ",
                tags.ToImmutableArray().Select(t => $"{t.Key}:{t.Value}"));

            Console.WriteLine($"{instrument.Name} {measurement} {tagString}");
        }

        public static MeterListener GetMeterListener(string meterName)
        {
            MeterListener meterListener = new()
            {
                InstrumentPublished = (instrument, listener) =>
                {
                    if (instrument.Meter.Name == meterName)
                        listener.EnableMeasurementEvents(instrument);
                }
            };

            meterListener.SetMeasurementEventCallback<int>(SetMeasurement);
            meterListener.Start();
            return meterListener;
        }

        public static MeterProvider GetMeterProvider(
            string meterName, string endpointUri)
        {
            return Sdk
                .CreateMeterProviderBuilder()
                .AddMeter(meterName)
                .AddPrometheusHttpListener(
                    options => options.UriPrefixes = [endpointUri])
                .Build();
        }
    }
}