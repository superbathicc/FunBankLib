using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBankLib {
    class AccountClient : RESTClient {
        private Models.Account me;

        public AccountClient(string baseUrl) : base(baseUrl) {

        }

        public async Task<Models.Account> Login(string username, string password) {
            me = await Post<Models.Account>("/api/login/account", new Dictionary<string, object>() {
                {"username", username },
                {"password", password }
            });
            SetAuthorization("Account", me.Hash);
            return me;
        }

        public async Task<Models.Transaction> Transaction(string accountNumber, long amount) {
            return await Post<Models.Transaction>("/api/account/transaction", new Dictionary<string, object>() {
                {"accountNumber", accountNumber },
                {"amount", amount}
            });
        }
    }
}
