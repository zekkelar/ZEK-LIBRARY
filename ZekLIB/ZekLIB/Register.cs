using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZekLIB
{
    public partial class Register : Form
    {
        private LoginInformation dataInstance;
        private FetchItem fetchItem;

        private string apiEndpoint = "";
        public Register()
        {
            InitializeComponent();
            dataInstance = LoginInformation.GetInstance();
            fetchItem = FetchItem.GetInstance();
            apiEndpoint = $"{fetchItem.Myurl}/account";
            richTextBox1.Visible = false;
            senterRichbox();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            startRegist();
        }
        private static readonly HttpClient httpClient = new HttpClient();
       
        private async Task startRegist()
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("register"), "id");
                formData.Add(new StringContent(guna2TextBox1.Text), "username");
                formData.Add(new StringContent(guna2TextBox2.Text), "password");
                formData.Add(new StringContent(dataInstance.MachineID), "machine_id");
                using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            richTextBox1.Text = "Success Register!";
                            richTextBox1.Visible = true;
                            dataInstance.Username = guna2TextBox1.Text;
                            dataInstance.Password = guna2TextBox2.Text;

                        }
                        else if (responseContent.Contains("user are exist"))
                        {
                            richTextBox1.Text = "User are exist, please input unique username!";
                            richTextBox1.Visible = true;
                        }
                        else
                        {
                            richTextBox1.Text = "Failed to regist";
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
        private void label1_Click(object sender, EventArgs e)
        {
            formkecil(new Login());
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            formkecil(new MenuForm());
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
