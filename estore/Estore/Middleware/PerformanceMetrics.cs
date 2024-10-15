using System;
using System.Text;
using Estore.DAL;

namespace Estore.Middleware
{
	public class PerformanceMetrics
	{
		private readonly RequestDelegate next;

        public PerformanceMetrics(RequestDelegate next)
		{
			this.next = next;
        }

		public async Task  Invoke(HttpContext context, IMetricDAL metricDAL)
		{

			await this.next(context);

			var metricData = metricDAL.GetMetrics();
			if (metricData != null && !context.Request.Path.ToString().Contains("ajax"))
			{
				await context.Response.WriteAsync(
					$@"
					<div class='dev-performance'>
						{MetricToHtml(metricData)}
					</div>
					"
					);
            }
        }

		string MetricToHtml(List<MetricData> metrics)
		{
			int index = 1;
			StringBuilder sb = new StringBuilder();
			sb.Append("<table class='table'>");

			foreach(var m in metrics)
			{
                sb.Append($"<tr><td>{index}</td><td>{m.SQL}</td><td>{m.Parameters}</td><td>{m.Elapsed}</td></tr>");
				index++;
            }

            sb.Append("</table>");
            return sb.ToString();
		}
	}
}

