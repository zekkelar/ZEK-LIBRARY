using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZekLIB
{
    public partial class Login : Form
    {
        private LoginInformation dataInstance;
        private FetchItem fetchItem;
        private string apiEndpoint = "";
        public Login()
        {
            InitializeComponent();
            dataInstance = LoginInformation.GetInstance();
            fetchItem = FetchItem.GetInstance();
            richTextBox1.Visible = false;
            senterRichbox();
            apiEndpoint = $"{fetchItem.Myurl}account";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private Form activeForm = null;
        private void formkecil(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Form1.instance.PanelChildForm.Controls.Add(childForm);
            Form1.instance.PanelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static readonly HttpClient httpClient = new HttpClient();
        
        private async Task StartLogin()
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("login"), "id");
                formData.Add(new StringContent(guna2TextBox1.Text), "username");
                formData.Add(new StringContent(guna2TextBox2.Text), "password");
                using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            dataInstance.Username = guna2TextBox1.Text;
                            dataInstance.Password = guna2TextBox2.Text;
                            dataInstance.IsLoggin = true;
                            richTextBox1.Text = "Success Login!";
                            richTextBox1.Visible = true;
                        }
                        else
                        {
                            dataInstance.IsLoggin = false;
                            richTextBox1.Text = "Failed Login!";
                            richTextBox1.Visible = true;
                        }
                    }
                }
            }
            senterRichbox();
        }

        private void senterRichbox()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

            StartLogin();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            formkecil(new Register());
        }

        private void guna2TextBox1_Enter(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Username")
            {
                guna2TextBox1.Text = "";
            }
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Username";
            }
        }

        private void guna2TextBox2_Enter(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "Password")
            {
                guna2TextBox2.Text = "";
            }
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                guna2TextBox2.Text = "Password";
            }
        }
    }
}
