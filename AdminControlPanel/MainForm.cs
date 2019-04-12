using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunBankLib;

namespace AdminControlPanel {
    public partial class MainForm : Form {
        FunBankLib.AdminClient adminClient;

        public MainForm() {
            InitializeComponent();

            button_login.Click += new EventHandler(async (sender, e) => {
                adminClient = new AdminClient(textBox_address.Text);
                var admin = await adminClient.Login(textBox_uname.Text, textBox_pw.Text);
                (sender as Button).Text = $"Logged in as {admin.Username}";
            });

            comboBox_createType.Items.AddRange(new object[] {
                typeof(FunBankLib.Models.Admin),
                typeof(FunBankLib.Models.ATM),
                typeof(FunBankLib.Models.Customer),
                typeof(FunBankLib.Models.Employee),
                typeof(FunBankLib.Models.Transaction)
            });
        }

        private void ComboBox_createType_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }

    public static class ListViewUtils {
        public class KeyAttribute : Attribute {

        }

        public static void Clear(ListView lv) {
            lv.Items.Clear();
            lv.Groups.Clear();
            lv.Columns.Clear();
        }

        public static void PrepareForType<T>(ListView lv) {
            foreach(var prop in typeof(T).GetProperties()) {
                lv.Columns.Add(new ColumnHeader() {
                    Text = $"{prop.Name}:{prop.PropertyType.Name}",
                    TextAlign = HorizontalAlignment.Center,
                    Width = 100
                });
            }
        }

        public static void AddItem<T>(ListView lv, T item) {
            var lvi = new ListViewItem();
            foreach (var prop in typeof(T).GetProperties()) {

            }
        }
    }
}
