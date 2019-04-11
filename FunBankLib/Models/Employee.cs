using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Employee : Model {
        [JsonProperty("username", Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string PasswordHash { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("name")]
        public EmployeeName Name { get; set; }
    }

    public class EmployeeName {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
