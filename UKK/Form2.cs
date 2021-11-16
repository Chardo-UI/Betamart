using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UKK
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtPassword.Focus();

            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                AppDataTableAdapters.LoginTableAdapter user = new AppDataTableAdapters.LoginTableAdapter();
                AppData.LoginDataTable dt = user.GetDataByUsernamePassword(txtUsername.Text, txtPassword.Text);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("You have been successfully logged in ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();


                }
                else
                {
                    MessageBox.Show("Your username or password is incorrect", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
