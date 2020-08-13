using System;
using System.Runtime.Serialization;

namespace Api.DataFiles
{
    public class FundDetails
    {
        [IgnoreDataMember]
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public decimal CurrentUnitPrice { get; set; }

        public string FundManager { get; set; }

        public string Name { get; set; }

        public string MarketCode { get; set; }
    }
}