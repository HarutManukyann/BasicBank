using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEqualizerWorkerService.Models;

namespace UserEqualizerWorkerService.Data
{
    public class PlaceHolderClient
    {
        private readonly HttpClient _httpClient;

        public PlaceHolderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlaceHolderUser>> GetPlaceHolderUsers()
        {
            var uri = "/users";

            var responseString = await _httpClient.GetStringAsync(uri);

            var placeHolederUsers = JsonConvert.DeserializeObject<List<PlaceHolderUser>>(responseString);

            return placeHolederUsers ?? new List<PlaceHolderUser>();
        }
    }
}
