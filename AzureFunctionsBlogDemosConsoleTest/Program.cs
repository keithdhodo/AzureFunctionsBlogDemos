using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// Client code adapted from the following example: https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
namespace HttpClientSample
{
    class Program
    {
        static Random r = new Random();
        static HttpClient client = new HttpClient();


        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var debug = bool.Parse(ConfigurationManager.AppSettings["Debug"]);
            string baseUrl = debug == true ? ConfigurationManager.AppSettings["DebugBaseUrl"] : ConfigurationManager.AppSettings["BaseUrl"];

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var numberOfRequests = 1000;

            for (int i = 0; i < numberOfRequests; i++)
            {

                try
                {
                    // Create a new Array
                    int arraySize = 2000;
                    var inputArray = new AzureFunctionsBlogDemos.Merging.MergingArray();
                    inputArray.NumberToUnionFrom = CreateIntegers(arraySize);
                    inputArray.NumberToUnionTo = CreateIntegers(arraySize);

                    var url = await SendArrayAsync(inputArray);
                    Console.WriteLine($"Created at {url}");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.ReadLine();
        }

        static async Task<Uri> SendArrayAsync(AzureFunctionsBlogDemos.Merging.MergingArray inputArray)
        {
            var routeAndKey = new Uri(client.BaseAddress + "api/MergeTrigger?code=" + ConfigurationManager.AppSettings["ApiKey"]);

            HttpResponseMessage response = await client.PostAsJsonAsync(routeAndKey, inputArray);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.RequestMessage.RequestUri;
        }

        private static int[] CreateIntegers(int input)
        {
            int[] returnArray = new int[input * 2];

            for (int i = 0; i < input * 2; i++)
            {
                int number = r.Next(0, input);

                returnArray[i] = number;
            }

            return returnArray;
        }
    }
}