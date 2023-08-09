using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static ZekLIB.Profile;

namespace ZekLIB
{
    public partial class EditProfile : Form
    {
        private LoginInformation dataInstance;
        private FetchItem fetchitem;

        private string apiEndpoint = "";
        public EditProfile()
        {
            InitializeComponent();
            dataInstance = LoginInformation.GetInstance();
            fetchitem = FetchItem.GetInstance();
            apiEndpoint = $"{fetchitem.Myurl}account";
            _ = get_info_user();
            richTextBox2.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            _ = upload();

        }

        private static readonly HttpClient httpClient = new HttpClient();
        private string _imagePath = "";
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                gunaPictureBox1.Image = new Bitmap(openFileDialog.FileName);
                _imagePath = imagePath;

            }
        }
        
        private string url_image = "";
        private async Task get_info_user()
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("machine_login"), "id");
                formData.Add(new StringContent(dataInstance.MachineID), "machine_id");
                using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            getitem status = JsonConvert.DeserializeObject<getitem>(responseContent);
                            string res = status.status.name;
                            NameEditProfile.Text = res;
                            BioEditProfile.Text = status.status.bio;
                            PasswordEditProfile.Text = status.status.password;
                            gunaPictureBox1.ImageLocation = status.status.profile_image;
                            url_image = status.status.profile_image;

                        }
                        else
                        {

                        }
                    }
                }
            }

        }

        private void senterRichbox()
        {
            richTextBox2.SelectAll();
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.DeselectAll();
        }
        private async Task upload()
        {
            using (var formData = new MultipartFormDataContent())
            {
                if (_imagePath != "")
                {
                    byte[] imageBytes = File.ReadAllBytes(_imagePath);
                    var imageContent = new ByteArrayContent(imageBytes);
                    formData.Add(imageContent, "photo", Path.GetFileName(_imagePath));
                }

                formData.Add(new StringContent(url_image), "url_photo");
                formData.Add(new StringContent(NameEditProfile.Text), "name");
                formData.Add(new StringContent(PasswordEditProfile.Text), "password");
                formData.Add(new StringContent(dataInstance.Username), "username");
                formData.Add(new StringContent(BioEditProfile.Text), "bio");
                formData.Add(new StringContent("update_profile"), "id");
           

                using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                {

                    if (response.IsSuccessStatusCode)
                    {

                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            richTextBox2.Text = "Success Edit Profile";
                            richTextBox2.ForeColor = Color.Green;
                            richTextBox2.Visible = true;
                     
                            senterRichbox();
                        }
                        else
                        {
                            richTextBox2.Text = "Failed Edit Profile";
                            richTextBox2.ForeColor = Color.Red;
                            richTextBox2.Visible = true;
                       
                            senterRichbox();
                        }
                    }
                    else
                    {

                        richTextBox2.Text = "Failed Edit Profile";
                        richTextBox2.ForeColor = Color.Red;
                        richTextBox2.Visible = true;
                       
                        senterRichbox();
                    }
                }

            }
        }
    }
}
