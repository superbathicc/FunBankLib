using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Transaction : Model {
        [JsonProperty("sender")]
        public Account Sender { get; set; }

        [JsonProperty("target")]
        public Account Target { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("time")]
        public long TimeMS { get; set; }

        public DateTime Time {
            get {
                return new DateTime(1970, 1, 1).AddMilliseconds(TimeMS);
            }
        }
    }
}
