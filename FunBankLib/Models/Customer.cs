using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Customer : Model {
        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("username", Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string PasswordHash { get; set; }

        [JsonProperty("name")]
        public CustomerName Name { get; set; }

        [JsonProperty("dateOfBirth")]
        public long DateOfBirthMS { get; set; }

        [JsonIgnore()]
        public DateTime DateOfBirth {
            get {
                return new DateTime(1970, 1, 1).AddMilliseconds(Convert.ToDouble(DateOfBirthMS));
            }
        }

        [JsonProperty("address")]
        public CustomerAddress Address { get; set; }
    }

    public class CustomerName {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }

    public class CustomerAddress {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("houseNumber")]
        public string HouseNumber { get; set; }
    }
}
