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
            string url = "http://35.221.88.74/agent/query?" + email;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpclient.SendAsync(requestMessage);
            var result = await response.Content.ReadAsStringAsync();
            Agent responseObject = JsonConvert.DeserializeObject<Agent>(result);
            return responseObject;
        }

    }
}
