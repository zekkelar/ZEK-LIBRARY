using Guna.UI.WinForms;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZekLIB
{
    public partial class UploadSkripsi : Form
    {
        private LoginInformation dataInstance;
        private FetchItem fetchItem;

        private string apiEndpoint = "";
        public UploadSkripsi()
        {
            InitializeComponent();
            dataInstance = LoginInformation.GetInstance();
            fetchItem = FetchItem.GetInstance();
            apiEndpoint = $"{fetchItem.Myurl}upload_books";

        }

        private string _imagePath = "";
        private string _pdfpath = "";
        private void gunaButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                pictureBox1.ImageLocation = imagePath;
                _imagePath = imagePath;

            }
        }


        private static readonly HttpClient httpClient = new HttpClient();
        

        private async void gunaButton1_Click(object sender, EventArgs e)
        {
            if(gunaTextBox1.Text == "File")
            {
                MessageBox.Show("Please upload your journal file");
            }
            else
            {
                string response = await upload();

            }

        }
        public async Task<string> upload()
        {
            if (File.Exists(_imagePath))
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] imageBytes = File.ReadAllBytes(_imagePath);
                    var imageContent = new ByteArrayContent(imageBytes);

                    byte[] pdfBytes = File.ReadAllBytes(_pdfpath);
                    var pdfContent = new ByteArrayContent(pdfBytes);

                    formData.Add(pdfContent, "pdf", Path.GetFileName(_pdfpath));
                    formData.Add(imageContent, "photo", Path.GetFileName(_imagePath));
                    formData.Add(new StringContent(title.Text), "title");
                    formData.Add(new StringContent(years.Text), "years");
                    formData.Add(new StringContent(author.Text), "author");
                    formData.Add(new StringContent(deskripsi.Text), "deskripsi");
                    formData.Add(new StringContent(dataInstance.Username), "username");
                    formData.Add(new StringContent("Metallurgy"), "category");
                    try
                    {
                        int.Parse(years.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Years must be a number format!");
                    }

                    using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            if (responseContent.Contains("Success"))
                            {
                                MessageBox.Show("Success!!");
                                title.Text = "";
                                years.Text = "Years";
                                author.Text = "Author";
                                deskripsi.Text = "Description";
                                gunaTextBox1.Text = "file";
                                pictureBox1.Image = Properties.Resources.Account;
                            }
                        }
                    }
                }
            }

            return "Gagal mengunggah gambar.";
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _pdfpath = openFileDialog.FileName;
                gunaTextBox1.Text = _pdfpath;
            }
        }

        private void title_Enter(object sender, EventArgs e)
        {
            if (title.Text == "Title")
            {
                title.Text = "";
            }
        }

        private void title_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                title.Text = "Title";
            }
        }

        private void years_Enter(object sender, EventArgs e)
        {
            if (years.Text == "Years")
            {
                years.Text = "";
            }
        }

        private void years_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(years.Text))
            {
                years.Text = "Years";
            }
        }

        private void author_Enter(object sender, EventArgs e)
        {
            if (author.Text == "Author")
            {
                author.Text = "";
            }
        }

        private void author_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(author.Text))
            {
                author.Text = "Author";
            }
        }

        private void deskripsi_Enter(object sender, EventArgs e)
        {
            if (deskripsi.Text == "Abstract / Description ")
            {
                deskripsi.Text = "";
            }
        }

        private void deskripsi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(deskripsi.Text))
            {
                deskripsi.Text = " Abstract / Description";
            }
           
        }
    }
}
