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

        [JsonProperty("1€")]
        public int OneEuro { get; set; }

        [JsonProperty("2€")]
        public int TwoEuro { get; set; }

        [JsonProperty("5€")]
        public int FiveEuro { get; set; }

        [JsonProperty("10€")]
        public int TenEuro { get; set; }

        [JsonProperty("20€")]
        public int TwentyEuro { get; set; }

        [JsonProperty("50€")]
        public int FiftyEuro { get; set; }

        [JsonProperty("100€")]
        public int OneHundredEuro { get; set; }

        [JsonProperty("200€")]
        public int TwoHundredEuro { get; set; }
    }
}
