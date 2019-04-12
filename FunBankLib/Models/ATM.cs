using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class ATM : Model {
        [JsonProperty("password", Required = Required.Always)]
        public string PasswordHash { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("status")]
        public ATMStatus Status { get; set; }

        [JsonProperty("inventory")]
        public ATMInventory Inventory { get; set; }
    }

    public class ATMStatus {
        [JsonProperty("maintenanceRequired")]
        public bool MaintenanceRequired { get; set; }
    }

    public class ATMInventory {
        [JsonProperty("1ct")]
        public int OneCent { get; set; }

        [JsonProperty("2ct")]
        public int TwoCents { get; set; }

        [JsonProperty("5ct")]
        public int FiveCents { get; set; }

        [JsonProperty("10ct")]
        public int TenCents { get; set; }

        [JsonProperty("20ct")]
        public int TwentyCents { get; set; }

        [JsonProperty("50ct")]
        public int FifyCents { get; set; }

        [JsonProperty("1eur")]
        public int OneEuro { get; set; }

        [JsonProperty("2eur")]
        public int TwoEuro { get; set; }

        [JsonProperty("5eur")]
        public int FiveEuro { get; set; }

        [JsonProperty("10eur")]
        public int TenEuro { get; set; }

        [JsonProperty("20eur")]
        public int TwentyEuro { get; set; }

        [JsonProperty("50eur")]
        public int FiftyEuro { get; set; }

        [JsonProperty("100eur")]
        public int OneHundredEuro { get; set; }

        [JsonProperty("200eur")]
        public int TwoHundredEuro { get; set; }
    }
}
