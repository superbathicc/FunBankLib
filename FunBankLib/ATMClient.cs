using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBankLib {
    public class ATMClient : RESTClient {
        Models.ATM me;

        public ATMClient(string baseUrl) : base(baseUrl) {
            
        }

        public async Task<Models.ATM> Login(string id, string password) {
            me = await Post<Models.ATM>("/api/login/atm", new Dictionary<string, object>() {
                {"id", id },
                {"password", id }
            });
            return me;
        }

        public async Task<Models.ATM> GetATM(string id) {
            return await Get<Models.ATM>($"/api/atm/{id}", null);
        }

        public async Task<List<Models.ATM>> GetATMs() {
            return await Get<List<Models.ATM>>("/api/atm", null);
        }
    }
}
