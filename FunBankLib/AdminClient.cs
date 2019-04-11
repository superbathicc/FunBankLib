using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBankLib {
    public class AdminClient : RESTClient {
        private Models.Admin me;

        public AdminClient(string baseUrl) : base(baseUrl) {

        }

        public async Task<Models.Admin> Login(string username, string password) {
            me = await Post<Models.Admin>("/api/login/admin", new Dictionary<string, object>() {
                {"username", username },
                {"password", password }
            });
            // httpClient.DefaultRequestHeaders.Add("Authorization", $"Admin {me.Hash}");
            return me;
        }

        public async Task<Models.Admin> CreateAdmin(string username, string password) {
            return await Post<Models.Admin>("/api/admin", new Dictionary<string, object>() {
                {"username", username },
                {"password", password }
            });
        }

        public async Task<Models.Account> CreateAccount(Models.Customer owner, string password) {
            return await Post<Models.Account>("/api/account", new Dictionary<string, object>() {
                {"customer", owner.Username },
                {"password", password }
            });
        }

        public async Task<Models.ATM> CreateATM(string password, string description) {
            return await Post<Models.ATM>("/api/atm", new Dictionary<string, object>() {
                {"password", password },
                {"description", description }
            });
        }

        public async Task<Models.Customer> CreateCustomer(Models.Customer customer) {
            return await Post<Models.Customer>("/api/customer", customer);
        }

        public async Task<Models.Employee> CreateEmployee(Models.Employee employee) {
            if (employee.Id != null) employee.Id = null;
            return await Post<Models.Employee>("/api/employee", employee);
        }
    }
}
