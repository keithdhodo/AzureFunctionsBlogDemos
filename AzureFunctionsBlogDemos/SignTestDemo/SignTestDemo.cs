using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Code originally written by Dr. James McCaffrey; published by MSDN magazine here: https://msdn.microsoft.com/en-us/magazine/mt793273.aspx
/// Available for download from here: https://msdn.microsoft.com/magazine/0217magcode
/// </summary>
namespace AzureFunctionsBlogDemos.SignTest
{
    public class SignTestDemo
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log,
            IAsyncCollector<SignTest> outputTable)
        {
            log.Info("\nBegin Sign Test demo \n");
            log.Info("Goal is to determine if weight loss program has an effect \n");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var signTest = JsonConvert.DeserializeObject<SignTest>(jsonContent);
            log.Info($"Inputs: {signTest.Before.Select(s => s.ToString())}, {signTest.After.Select(s => s.ToString())}");

            if (signTest.Before.Length != signTest.After.Length)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Number of items to compare do not match."
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            log.Info("The weight data is: \n");
            SignTest.ShowVector("Before:  ", signTest.Before, 0, "", log);
            SignTest.ShowVector("After :  ", signTest.After, 0, "\n", log);

            int[] counts = SignTest.DoCounts(signTest.Before, signTest.After);
            log.Info("Num success = " + counts[2]);
            log.Info("Num failure = " + counts[0]);

            log.Info("\nCalling program-defined BinomRightTail() function \n");
            int k = counts[2];  // successes
            int n = counts[0] + counts[2];  // success + fail
            log.Info("k = " + k + " n = " + n + " p = 0.5");
            signTest.PValue = SignTest.BinomRightTail(k, n, 0.5);

            signTest.ProbabilityOfEffect = 1 - signTest.PValue;
            signTest.ProbabilityOfNoEffect = signTest.PValue;

            log.Info("\nProbability of 'no effect' is " + signTest.ProbabilityOfNoEffect.ToString("F4"));
            log.Info("Probability of 'an effect' is " + signTest.ProbabilityOfEffect.ToString("F4"));

            log.Info("\n\nEnd Sign Test demo \n");

            stopwatch.Stop();
            signTest.Runtime = stopwatch.Elapsed;

            signTest.AlgorithmName = "SignTestDemo";
            signTest.PartitionKey = "SignTest";
            signTest.RowKey = Guid.NewGuid().ToString();

            await outputTable.AddAsync(signTest);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(signTest)
            });
        }
    }
}