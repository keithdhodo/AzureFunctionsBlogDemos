using AlgorithmsFunctions.Shared;
using AlgorithmsFunctions.Shared.Chapter01;
using AlgorithmsFunctions.Shared.Chapter01.Enums;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlgorithmsFunctions.Chapter01
{
    public static class MergeTrigger
    {
        [FunctionName("MergeTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "api/chapter01/merging")]
            HttpRequestMessage req, 
            TraceWriter log)
        {
            log.Info("MergeTrigger has processed a request.");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var inputArrays = JsonConvert.DeserializeObject<MergingArray>(jsonContent);
            log.Info($"Inputs: {string.Join(",", inputArrays.NumberToUnionFrom)}, {string.Join(",", inputArrays.NumberToUnionTo)}");

            if (inputArrays.NumberToUnionFrom.Length != inputArrays.NumberToUnionTo.Length)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Number of items to union do not match."
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            AddMessageToTopic(inputArrays);

            stopwatch.Stop();

            var performance = new MergePerformance();

            performance.Runtime = stopwatch.Elapsed;

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(inputArrays)
            });
        }

        public static void AddMessageToTopic(MergingArray input)
        {
            // Create the topic if it does not exist already.
            string connectionString =
                CloudConfigurationManager.GetSetting(Constants.AzureServiceBusConnectionString);

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            var topicName = Constants.Chapter01.AzureServiceBusTopicNameChapter01;

            // Configure Topic Settings.
            TopicDescription td = new TopicDescription(topicName);
            td.MaxSizeInMegabytes = 5120;
            td.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);

            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopicAsync(td);
            }

            foreach (string algorithmName in Enum.GetNames(typeof(MergeAlgorithms)))
            {
                if (!namespaceManager.SubscriptionExists(topicName, algorithmName))
                {
                    namespaceManager.CreateSubscriptionAsync(topicName, algorithmName);
                }
            }

            TopicClient Client =
            TopicClient.CreateFromConnectionString(connectionString, topicName);

            Client.SendAsync(new BrokeredMessage(input));
        }
    }
}