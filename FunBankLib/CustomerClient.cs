using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBankLib {
    public class CustomerClient : RESTClient {
        Models.Customer me;

        public CustomerClient(string baseUrl) : base(baseUrl) {

        }

        public async Task<Models.Customer> Login(string username, string password) {
            me = await Post<Models.Customer>("/api/customer", new Dictionary<string, object>() {
                {"username", username },
                {"password", password }
            });
            return me;
        }

        public async Task<Models.Customer> GetCustomer(string id) {
            return await Get<Models.Customer>($"/api/customer/{id}", null);
        }

        public async Task<List<Models.Customer>> GetCustomers() {
            return await Get<List<Models.Customer>>("/api/customer", null)l
        }

        public async Task<List<Models.Account>> GetAccounts(Models.Customer customer) {
            return await Get<List<Models.Account>>("/api/account", null);
        }

        public async Task<Models.ATMInventory> Withdraw(Models.ATM atm, long amount) {
            return await Post<Models.ATMInventory>("/api/account/withdraw", new Dictionary<string, object>() {
                {"atmId", atm.Id },
                {"amount", amount }
            });
        }

        public async Task<Models.Account> Deposit(Models.ATM atm, Models.ATMInventory inventory) {
            return await Post<Models.Account>("/api/account/deposit", new Dictionary<string, object>() {
                {"atmId", atm.Id },
                {"items", inventory }
            });
        }
    }
}
