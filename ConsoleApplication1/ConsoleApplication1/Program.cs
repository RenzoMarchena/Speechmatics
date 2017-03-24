using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace DeepGramRecognition
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Recognize();

            Console.ReadLine();

        }

        private static async void Recognize()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            // Do the actual request and await the response
            var httpResponse = await httpClient.GetAsync("https://api.speechmatics.com/v1.0/user/19227/jobs/2238807/transcript?auth_token=MGJiNzU4MzItOTFkNi00MGRjLThmZDMtNDFjZDY5ODg3ZDYx");

            // If the response contains content we want to read it!
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                dynamic jObj = (JObject)JsonConvert.DeserializeObject(responseContent);

                foreach (var word in jObj["words"]) {
                    Console.WriteLine(TimeSpan.FromSeconds(Math.Truncate(Convert.ToDouble(word["time"]))).ToString() + " " + TimeSpan.FromSeconds(Math.Truncate(Convert.ToDouble(word["time"]) + Convert.ToDouble(word["duration"]))).ToString() + " " +word["name"]);
                }
                

                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
            }


        }
    }
}

