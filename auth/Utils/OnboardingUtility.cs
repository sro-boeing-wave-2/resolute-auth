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
            string url = Constants.BASE_URL + Constants.GET_AGENT_BY_EMAIL + email;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Access", "Allow_Service");
            var response = await httpclient.SendAsync(requestMessage);
            var result = await response.Content.ReadAsStringAsync();
            Agent responseObject = JsonConvert.DeserializeObject<Agent>(result);
            return responseObject;
        }

    }
}
