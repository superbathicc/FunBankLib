using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Admin : Model {
        [JsonProperty("username", Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string PasswordHash { get; set; }

        [JsonProperty("hash", Required = Required.Default)]
        public string Hash { get; set; }
    }
}
