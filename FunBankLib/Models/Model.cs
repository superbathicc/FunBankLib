using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunBankLib.Models {
    public class Model {
        [JsonProperty("_id", Required = Required.AllowNull)]
        public string Id { get; set; }
    }
}
