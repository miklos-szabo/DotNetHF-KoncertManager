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
        public static string ResponseString { get; set; }

        public static async Task<List<Band>> GetBandsAsync()
        {
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/bands");
            return JsonConvert.DeserializeObject<List<Band>>(ResponseString);
        }

        public static async Task<List<Venue>> GetVenuesAsync()
        {
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/venues");
            return JsonConvert.DeserializeObject<List<Venue>>(ResponseString);
        }

        public static async Task<List<Concert>> GetConcertsAsync()
        {
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/concerts");
            return JsonConvert.DeserializeObject<List<Concert>>(ResponseString);
        }

        public static async Task CreateBandAsync(Band band)
        {
            var jstr = JsonConvert.SerializeObject(band);
            var response = await client.PostAsync("http://localhost:53501/api/bands",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
        }

        public static async Task CreateVenueAsync(Venue venue)
        {
            var jstr = JsonConvert.SerializeObject(venue);
            var response = await client.PostAsync("http://localhost:53501/api/venues",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
        }

        public static async Task DeleteBandAsync(int id)
        {
            var response = await client.DeleteAsync($"http://localhost:53501/api/bands/{id}");
            ResponseString = response.StatusCode == HttpStatusCode.NoContent ? "Successful delete!" : "Delete failed!";
        }

        public static async Task DeleteVenueAsync(int id)
        {
            var response = await client.DeleteAsync($"http://localhost:53501/api/venues/{id}");
            ResponseString = response.StatusCode == HttpStatusCode.NoContent ? "Successful delete!" : "Delete failed!";
        }

        public static async Task UpdateBandAsync(int id, Band band)
        {
            var jstr = JsonConvert.SerializeObject(band);
            var response = await client.PutAsync($"http://localhost:53501/api/bands/{id}",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            ResponseString = response.StatusCode == HttpStatusCode.NoContent ? "Successful update!" : "Update failed!";
        }

        public static async Task UpdateVenueAsync(int id, Venue venue)
        {
            var jstr = JsonConvert.SerializeObject(venue);
            var response = await client.PutAsync($"http://localhost:53501/api/venues/{id}",
                new StringContent(jstr, Encoding.UTF8, "application/json"));
            ResponseString = response.StatusCode == HttpStatusCode.NoContent ? "Successful update!" : "Update failed!";
        }
    }
}
