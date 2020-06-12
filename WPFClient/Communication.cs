using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.BLL.DTOs;
using Newtonsoft.Json;

namespace WPFClient
{
    /**
     * A szerverrel való kommunikációért felelős statikus osztály
     */
    public static class Communication
    {
        private static readonly HttpClient client = new HttpClient();

        /**
         * Visszaadja az összes együttest - GET
         */
        public static async Task<List<Band>> GetBandsAsync()
        {
            string responseString = await client.GetStringAsync("http://localhost:53501/bands");
            //ODataResponse egy alább levő internal osztály
            return JsonConvert.DeserializeObject<ODataResponse<Band>>(responseString).Values;
        }

        /**
         * Visszaadja az összes helyszínt - GET
         */
        public static async Task<List<Venue>> GetVenuesAsync()
        {
            string responseString = await client.GetStringAsync("http://localhost:53501/venues");
            return JsonConvert.DeserializeObject<ODataResponse<Venue>>(responseString).Values;
        }

        /**
         * Visszaadja az összes koncertet - GET
         */
        public static async Task<List<Concert>> GetConcertsAsync()
        {
            //Az expand fontos, anélkül nem küldi el az együtteseket
            string responseString = await client.GetStringAsync("http://localhost:53501/concerts?$expand=bands");
            return JsonConvert.DeserializeObject<ODataResponse<Concert>>(responseString).Values;
        }

        /**
         * Létrrehoz egy együttest a paraméter alapján, visszaad egy boolt a sikerességről - POST
         */
        public static async Task<bool> CreateBandAsync(Band band)
        {
            var jstr = JsonConvert.SerializeObject(band);
            var response = await client.PostAsync("http://localhost:53501/bands",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.Created;
        }

        /**
         * Létrrehoz egy helyszínt a paraméter alapján, visszaad egy boolt a sikerességről - POST
         */
        public static async Task<bool> CreateVenueAsync(Venue venue)
        {
            var jstr = JsonConvert.SerializeObject(venue);
            var response = await client.PostAsync("http://localhost:53501/venues",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.Created;
        }

        /**
         * Létrehoz egy koncertet a paraméter alapján, visszaad egy boolt a sikerességről - POST
         */
        public static async Task<bool> CreateConcertAsync(Concert concert)
        {
            var jstr = JsonConvert.SerializeObject(concert);
            var response = await client.PostAsync("http://localhost:53501/concerts",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.Created;
        }

        /**
         * Törli az adott Id-jű együttest, visszaad egy boolt a sikerességéről - DELETE
         */
        public static async Task<bool> DeleteBandAsync(int id)
        {
            var response = await client.DeleteAsync($"http://localhost:53501/bands/{id}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * Törli az adott Id-jű helyszínt, visszaad egy boolt a sikerességről - DELETE
         */
        public static async Task<bool> DeleteVenueAsync(int id)
        {
            var response = await client.DeleteAsync($"http://localhost:53501/venues/{id}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * Törli az adott ID-jű koncertet, visszaad egy boolt a sikerességről - DELETE
         */
        public static async Task<bool> DeleteConcertAsync(int id)
        {
            var response = await client.DeleteAsync($"http://localhost:53501/concerts/{id}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * Frissíti az adott Id-jű együttest a paraméterben levő együttesre,
         * visszaad egy boolt a sikerességről - PUT
         */
        public static async Task<bool> UpdateBandAsync(int id, Band band)
        {
            var jstr = JsonConvert.SerializeObject(band);
            var response = await client.PutAsync($"http://localhost:53501/bands/{id}",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * Frissíti az adott Id-jű helyszínt a paraméterben levő helyszínre,
         * visszaad egy boolt a sikerességről - PUT
         */
        public static async Task<bool> UpdateVenueAsync(int id, Venue venue)
        {
            var jstr = JsonConvert.SerializeObject(venue);
            var response = await client.PutAsync($"http://localhost:53501/venues/{id}",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * Frissíti az adott Id-jű koncertet a paraméterben levő koncertre,
         * visszaad egy boolt a sikerességről - PUT
         */
        public static async Task<bool> UpdateConcertAsync(int id, Concert concert)
        {
            var jstr = JsonConvert.SerializeObject(concert);
            var response = await client.PutAsync($"http://localhost:53501/concerts/{id}",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /**
         * A kapott OData json válaszban az első elem a metadata, a második elem pedig a keresett értékek
         */
        internal class ODataResponse<T>
        {
            [JsonProperty("odata.context")]
            public string MetaData { get; set; }
            [JsonProperty("value")]
            public List<T> Values { get; set; }
        }
    }
}
