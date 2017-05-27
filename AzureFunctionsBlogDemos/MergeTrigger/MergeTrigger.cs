using AzureFunctionsBlogDemos.Merging;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzureFunctionsBlogDemos.Merging
{
    public class MergeTrigger
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("QuickUnionTrigger processed a request.");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var inputArrays = JsonConvert.DeserializeObject<Merging.Array>(jsonContent);
            log.Info($"Inputs: {inputArrays.NumberToUnionFrom.ToString()}, {inputArrays.NumberToUnionTo.ToString()}");

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
            inputArrays.Runtime = stopwatch.Elapsed;

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(inputArrays)
            });
        }

        public static void AddMessageToTopic(Merging.Array input)
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

            List<string> paths = new List<string>{ "QuickUnion", "WeightedQuickUnionWithPathCompression" };

            foreach (string path in paths)
            {
                if (!namespaceManager.SubscriptionExists(topicName, path))
                {
                    namespaceManager.CreateSubscription(topicName, path);
                }
            }

            TopicClient Client =
            TopicClient.CreateFromConnectionString(connectionString, topicName);

            Client.Send(new BrokeredMessage(input));
        }
    }
}