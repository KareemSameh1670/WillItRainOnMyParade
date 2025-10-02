using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.DAL.Clients
{
    public interface IGoogleAIClient
    {
        public Task<string> AskGemini(string message);
    }
}
