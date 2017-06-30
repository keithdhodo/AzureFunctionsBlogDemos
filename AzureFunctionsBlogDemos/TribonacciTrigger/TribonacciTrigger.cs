using AzureFunctionsBlogDemos.Merging;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Tribonacci
{
    public class TribonacciTrigger
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("TribonacciTrigger has processed a request.");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var tribonnaciInput = JsonConvert.DeserializeObject<Shared.Tribonnaci>(jsonContent);
            log.Info($"Inputs are: {string.Join(",", tribonnaciInput.StartingSet)}. Functions will find Tribonnaci at the {tribonnaciInput.Iterations} iteration.");

            AddMessageToTopic(tribonnaciInput);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(tribonnaciInput)
            });
        }

        public static void AddMessageToTopic(Shared.Tribonnaci input)
        {
            // Create the topic if it does not exist already.
            string connectionString =
                CloudConfigurationManager.GetSetting("AzureWebJobsServiceBus");

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            var topicName = "TribonacciAlgorithms";

            // Configure Topic Settings.
            TopicDescription td = new TopicDescription(topicName);
            td.MaxSizeInMegabytes = 5120;
            td.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);

            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopic(td);
            }

            foreach (string algorithmName in Enum.GetNames(typeof(TribonacciAlgorithms)))
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
