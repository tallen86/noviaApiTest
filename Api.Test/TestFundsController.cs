using Api.DataFiles;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetAllFunds_ReturnAllFunds()
        {
            //Get expected result
            var expectedResult = GetTestFunds();

            //Get response from API
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58987/get-funds");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var objects = JsonConvert.DeserializeObject<List<FundDetails>>(responseBody);

            //Compare with expected result
            Assert.AreEqual(expectedResult.Count, objects.Count);
        }

        [Test]
        public async Task TestGetByMarketcode_ReturnFundsWithMarketcode()
        {
            //Get expected result
            var expectedResult = GetTestFunds().Where(x => x.MarketCode == "EA").ToList();

            //Get response from API
            string URL = "http://localhost:58987/get-marketcode?marketcode=EA";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var objects = JsonConvert.DeserializeObject<List<FundDetails>>(responseBody);

            //Compare with expected result
            Assert.AreEqual(expectedResult.Count, objects.Count);
        }
        
        [Test]
        public async Task TestGetByManager_ReturnFundsWithManger()
        {
            //Get expected result
            var expectedResult = GetTestFunds().Where(x => x.FundManager == "Comtract").ToList();

            //Get response from API
            string URL = "http://localhost:58987/get-manager?manager=Comtract";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var objects = JsonConvert.DeserializeObject<List<FundDetails>>(responseBody);

            //Compare with expected result
            Assert.AreEqual(expectedResult.Count, objects.Count);
        }

        private List<FundDetails> GetTestFunds()
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;
            var funds = JsonConvert.DeserializeObject<List<FundDetails>>(file);
            
            return funds;
        }
    }
}