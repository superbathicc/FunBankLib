using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FunBankLib;

namespace FunBankInit {
    class Program {
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        static void Main(string[] args) {

            GetBaseUrl();
            manualResetEvent.WaitOne();
            Console.CancelKeyPress += delegate {
                manualResetEvent.Set();
            };
        }

        static void GetBaseUrl() {
            Prompt baseUrlPrompt = new Prompt("enter base url:", new string[0]);
            baseUrlPrompt.Answered += (sender, e) => {
                try {
                    Uri uri = new Uri(e.answer);

                    AdminClient adminClient = new AdminClient(uri.ToString());
                    Console.WriteLine("Login as Admin:");
                    adminClient.Login(new Prompt("Username:").Execute(), new Prompt("Password:").Execute()).Wait();
                    CreateCustomer(adminClient);
                } catch (UriFormatException) {
                    e.invalid = true;
                }
            };
            baseUrlPrompt.Execute();
        }

        static void CreateCustomer(AdminClient adminClient) {
            Prompt createCustomerPrompt = new Prompt("Create a new customer?", new string[] { "Y", "n" });
            createCustomerPrompt.Answered += (sender, e) => {
                if (e.answer.Equals("Y")) {
                    new Task(async () => {
                        var customer = await adminClient.CreateCustomer(new FunBankLib.Models.Customer() {
                            Id = null,
                            Hash = null,
                            Username = new Prompt("Username:").Execute(),
                            PasswordHash = new Prompt("Password:").Execute(),
                            Name = new FunBankLib.Models.CustomerName() {
                                First = new Prompt("First Name:").Execute(),
                                Last = new Prompt("Last Name:").Execute()
                            },
                            Address = new FunBankLib.Models.CustomerAddress() {
                                Country = new Prompt("Country: ").Execute(),
                                Postcode = new Prompt("Postcode:").Execute(),
                                City = new Prompt("City:").Execute(),
                                Street = new Prompt("Street:").Execute(),
                                HouseNumber = new Prompt("House Number:").Execute()
                            }
                        });

                        CreateAccounts(adminClient, customer);
                    }).Start();
                } else {
                    CreateEmployee(adminClient);
                }
            };
            createCustomerPrompt.Execute();
        }

        static void CreateAccounts(AdminClient adminClient, FunBankLib.Models.Customer customer) {
            Prompt createAccountPrompt = new Prompt($"Create a new account for {customer.Username} ({customer.Name.First} {customer.Name.Last}", new string[] { "Y", "n" });
            createAccountPrompt.Answered += (sender, e) => {
                if(e.answer == "Y") {
                    new Task(async () => {
                        var account = await adminClient.CreateAccount(customer, new Prompt("Enter a new Password:").Execute());

                        createAccountPrompt.Execute();
                    }).Start();
                } else {
                    CreateEmployee(adminClient);
                }
            };
            createAccountPrompt.Execute();
        }

        static void CreateATM(AdminClient adminClient) {
            Prompt createATMPrompt = new Prompt($"Create a new ATM?", new string[] { "Y", "n" });
            createATMPrompt.Answered += (sender, e) => {
                if (e.answer == "Y") {
                    new Task(async () => {
                        var atm = await adminClient.CreateATM(new Prompt("enter a password:").Execute(), new Prompt("description:").Execute());

                        createATMPrompt.Execute();
                    }).Start();
                } else {
                    manualResetEvent.Set();
                }
            };
            createATMPrompt.Execute();
        }

        static void CreateEmployee(AdminClient adminClient) {
            Prompt createEmployeePrompt = new Prompt($"Create new employee?", new string[] { "Y", "n" });
            createEmployeePrompt.Answered += (sender, e) => {
                if (e.answer == "Y") {
                    new Task(async () => {
                        var employee = await adminClient.CreateEmployee(new FunBankLib.Models.Employee() {
                            Id = null,
                            Hash = null,
                            Username = new Prompt("username:").Execute(),
                            Name = new FunBankLib.Models.EmployeeName() {
                                First = new Prompt("first name:").Execute(),
                                Last = new Prompt("last name:").Execute()
                            },
                            PasswordHash = new Prompt("password:").Execute()
                        });

                        createEmployeePrompt.Execute();
                    }).Start();
                } else {
                    CreateATM(adminClient);
                }
            };
            createEmployeePrompt.Execute();
        }
    }

    public class Prompt {
        private string question;
        public string Question {
            get {
                return question;
            }
            private set {
                question = value;
            }
        }

        public List<string> Answers { get; private set; }

        public Prompt(string question, string[] answers) {
            Question = question;
            Answers = new List<string>();
            Answers.AddRange(answers);
        }

        public Prompt(string question) {
            Question = question;
        }

        public string Execute() {
            if(Answers != null && Answers.Count > 0) {
                string answer = Answers[0];
                do {
                    Console.Write($"{Question} ({String.Join(", ", Answers.ToArray())}) ");
                    answer = Console.ReadLine();
                } while (Answers.Where(answ => answ.Equals(answer)).Count() <= 0);
                OnAnswered(new AnswerEventArgs(answer));
                return answer;
            } else {
                Console.Write($"{Question} ");
                string answer = Console.ReadLine();
                OnAnswered(new AnswerEventArgs(answer));
                return answer;
            }
        }

        public event EventHandler<AnswerEventArgs> Answered;
        protected virtual void OnAnswered(AnswerEventArgs e) {
            Answered?.Invoke(this, e);

            if (e.invalid) {
                Console.WriteLine("Invalid Answer");
                Execute();
            }
        }

        public class AnswerEventArgs : EventArgs {
            public string answer;
            public bool invalid = false;

            public AnswerEventArgs(string answer) {
                this.answer = answer;
            }
        }
    }
}
