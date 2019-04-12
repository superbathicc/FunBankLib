using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Account : Model {
        [JsonProperty("accountId", Required = Required.Always)]
        public string AccountNumber { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string PasswordHash { get; set; }

        [JsonProperty("balance", Required = Required.Always)]
        public long Balance { get; set; }

        [JsonProperty("hash", Required = Required.Default)]
        public string Hash { get; set; }

        [JsonProperty("customer", Required = Required.Always)]
        public string CustomerId { get; set; }
    }
}
