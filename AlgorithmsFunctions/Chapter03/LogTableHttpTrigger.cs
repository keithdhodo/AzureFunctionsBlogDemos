using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using AlgorithmsFunctions.Shared.Chapter03;

namespace AlgorithmsFunctions.Chapter03
{
    public static class LogTableHttpTrigger
    {
        [FunctionName("LogTableHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("LogTable HTTP trigger function processed a request.");

            // parse query parameter
            string logarithmNumber = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "input", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            logarithmNumber = logarithmNumber ?? data?.input;

            int numberToCalculateLogarithmFor = 0;

            var inputIsInt = int.TryParse(logarithmNumber, out numberToCalculateLogarithmFor);

            int output = 0;

            if (inputIsInt)
            {
                output = new LogBaseTwo().Log(numberToCalculateLogarithmFor);
            }

            return logarithmNumber == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, $"Logarithm Base 2 for {logarithmNumber}: " + output);
        }
    }
}
