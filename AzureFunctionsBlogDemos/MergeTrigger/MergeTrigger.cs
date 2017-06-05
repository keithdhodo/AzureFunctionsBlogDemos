using Microsoft.Azure;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Merging
{
    public class MergeTrigger
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
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
                CloudConfigurationManager.GetSetting("AzureWebJobsServiceBus");

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            var topicName = "AlgorithmsMerge";

            // Configure Topic Settings.
            TopicDescription td = new TopicDescription(topicName);
            td.MaxSizeInMegabytes = 5120;
            td.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);

            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopic(td);
            }

            foreach (string algorithmName in Enum.GetNames(typeof(MergeAlgorithms)))
            {
                if (!namespaceManager.SubscriptionExists(topicName, algorithmName))
                {
                    namespaceManager.CreateSubscription(topicName, algorithmName);
                }
            }

            TopicClient Client =
            TopicClient.CreateFromConnectionString(connectionString, topicName);

            Client.Send(new BrokeredMessage(input));
        }
    }
}