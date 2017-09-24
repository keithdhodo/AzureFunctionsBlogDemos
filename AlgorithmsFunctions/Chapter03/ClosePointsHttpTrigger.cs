using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsFunctions.Chapter03
{
    public class ClosePointsHttpTrigger
    {
        [FunctionName("ClosePointsHttpTrigger")]
        public static async System.Threading.Tasks.Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/chapter03/closepoints")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("ClosePointsHttpTrigger function processed a request.");

            // Parse request input
            dynamic data = await req.Content.ReadAsAsync<object>();
            // Set name to body data
            int distance = data?.distance;

            if (distance <= 0)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Distance needs to be greater than zero. Please enter a value greater than zero and retry the request."
                });
            }

            log.Info($"Creating {distance * 4} points and finding number of points within distance.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var closePoints = new ClosePoints(distance * 4);
            closePoints.GenerateRandomPoints();
            var count = closePoints.FindPointsWithinDistance(distance);

            stopwatch.Stop();

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(count)
            });
        }
    }
}
