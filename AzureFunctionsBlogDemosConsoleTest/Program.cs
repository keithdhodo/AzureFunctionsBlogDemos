using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    class Program
    {
        static HttpClient client = new HttpClient();
        

        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var debug = bool.Parse(ConfigurationManager.AppSettings["Debug"]);
            string baseUrl = debug == true ? ConfigurationManager.AppSettings["DebugBaseUrl"]  : ConfigurationManager.AppSettings["BaseUrl"];

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new Array
                int arraySize = 5000;
                var inputArray = new AzureFunctionsBlogDemos.Merging.Array();
                inputArray.NumberToUnionFrom = CreateIntegers(arraySize);
                inputArray.NumberToUnionTo = CreateIntegers(arraySize);

                var url = await CreateProductAsync(inputArray);
                Console.WriteLine($"Created at {url}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static async Task<Uri> CreateProductAsync(AzureFunctionsBlogDemos.Merging.Array inputArray)
        {
            var routeAndKey = new Uri(client.BaseAddress + "api/MergeTrigger?code=" + ConfigurationManager.AppSettings["ApiKey"]);

            HttpResponseMessage response = await client.PostAsJsonAsync(routeAndKey, inputArray);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        private static int[] CreateIntegers(int input)
        {
            int[] returnArray = new int[input];

            Random r = new Random();

            for (int i = 0; i < input; i++)
            {
                int number = r.Next(0, input * 4);

                returnArray[i] = number;
            }

            return returnArray;
        }
    }
}