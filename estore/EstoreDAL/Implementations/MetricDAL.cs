namespace Estore.DAL
{
	public class MetricData
	{
		public string SQL { get; set; } = null!;

		public string Parameters { get; set; } = null!;

		public TimeSpan Elapsed { get; set; }
    }

	public class MetricDAL: IMetricDAL
    {
        private List<MetricData> metricDatas = new List<MetricData>();

		public void Add(MetricData metric)
		{
			metricDatas.Add(metric);
        }

		public List<MetricData>? GetMetrics()
		{
			return metricDatas;
        }
    }
}

