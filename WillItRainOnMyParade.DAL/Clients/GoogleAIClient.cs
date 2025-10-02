using GenerativeAI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.DAL.Clients
{
    public class GoogleAIClient : IGoogleAIClient
    {
        private readonly IConfiguration config;

        public GoogleAIClient(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> AskGemini(string message)
        {

            // Specify the model to use (e.g., "gemini-1.5-flash")

            var generativeModel = new GenerativeModel(apiKey: config["GoogleAI:ApiKey"], model: "gemini-2.5-flash");

            // Send the prompt to the model
            var response = await generativeModel.GenerateContentAsync(message);

            return response.Text;

        }
    }
}
