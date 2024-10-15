namespace Estore.DAL
{
	public interface IMetricDAL
	{
        void Add(MetricData metric);

        List<MetricData>? GetMetrics();
    }
}

