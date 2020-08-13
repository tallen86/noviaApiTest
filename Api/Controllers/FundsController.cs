namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.IO;
    using Api.DataFiles;
    using System.Net;

    public class FundsController : Controller
    {
        /// <summary>
        /// Return fund with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return fund element</returns>
        [Route("get-funds")]
        public IActionResult GetFunds(string id)
        {
            var funds = readFundsFile();
            if (id != null)
            {
                return this.Ok(funds.Single(x => x.Id.ToString() == id));
            }
            
            return this.Ok(funds);
        }

        /// <summary>
        /// Return fund with marketcode
        /// </summary>
        /// <param name="marketcode"></param>
        /// <returns>return fund element</returns>
        [Route("get-marketcode")]
        public IActionResult GetFundsMarketcode(string marketcode)
        {
            var funds = readFundsFile();
            if (marketcode != null)
            {
                return this.Ok(funds.Where(x => x.MarketCode.ToLower() == marketcode.ToLower()));
            }
            
            return this.Ok(funds);
        }

        /// <summary>
        /// Get all fund elements belonging to a manger name
        /// </summary>
        /// <param name="manager"></param>
        /// <returns>List of funds belonging to a manager</returns>
        [Route("get-manager")]
        public IActionResult GetManagerFunds(string manager)
        {
            var funds = readFundsFile();
            if (manager != null)
            {
                return this.Ok(funds.Where(x => x.FundManager.ToLower() == manager.ToLower()));
            }

            return this.Ok(funds);
        }

        /// <summary>
        /// Read Funds.json and return all elements
        /// </summary>
        /// <returns>list returning all funds</returns>
        private List<FundDetails> readFundsFile()
        {
            var file = System.IO.File.ReadAllTextAsync("./DataFiles/funds.json").Result;
            return JsonConvert.DeserializeObject<List<FundDetails>>(file);

        }
    }
}