using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBankLib {
    public class AccountClient : RESTClient {
        private Models.Account me;

        public AccountClient(string baseUrl) : base(baseUrl) {

        }

        public async Task<Models.Account> Login(string accountId, string password) {
            me = await Post<Models.Account>("/api/login/account", new Dictionary<string, object>() {
                {"accountId", accountId },
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
        public async Task<Models.ATMInventory> Withdraw(Models.ATM atm, long amount)
        {
            return await Post<Models.ATMInventory>("/api/account/withdraw", new Dictionary<string, object>() {
                {"atmId", atm.Id },
                {"amount", amount }
            });
        }

        public async Task<Models.Account> Deposit(Models.ATM atm, Models.ATMInventory inventory)
        {
            return await Post<Models.Account>("/api/account/deposit", new Dictionary<string, object>() {
                {"atmId", atm.Id },
                {"items", inventory }
            });
        }
    }

}
