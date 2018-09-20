using auth.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace auth.Utils
{
    class OnboardingUtility
    {

        public async Task<Agent> GetAgentDetails(String email)
        {
            HttpClient httpclient = new HttpClient();
            string url = "http://localhost:8082/api/agent/query?" + email;
            var response = await httpclient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            Agent responseObject = JsonConvert.DeserializeObject<Agent>(result);
            return responseObject;
        }

    }
}
