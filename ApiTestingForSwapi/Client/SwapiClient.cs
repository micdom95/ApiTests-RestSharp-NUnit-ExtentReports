using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingForSwapi.Client
{
    public class SwapiClient : SwapiApiConfig
    {
        public IRestResponse GetResponse(RestRequest request)
        {
            RestClient client = new RestClient(ApiUrl);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse GetPeople(int id)
        {
            var request = new RestRequest($"people/{id}")
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("accept", "application/json");

            var response = GetResponse(request);

            return response;
        }

        public IRestResponse GetPlanet(int id)
        {
            var request = new RestRequest($"planets/{id}")
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("accept", "application/json");

            var response = GetResponse(request);
            return response;
        }
    }
}
