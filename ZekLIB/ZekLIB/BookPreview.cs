using AltoHttp;
using Guna.UI.WinForms;
using Guna.UI2.WinForms.Suite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZekLIB.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;

namespace ZekLIB
{
    public partial class BookPreview : Form
    {
        public static BookPreview instance;

        private FetchItem dataInstance;

        private Form1 form1;

        HttpClient requests = new HttpClient();
        private string name_img = "";

        private bool isBookmark;
        private bool isDownload;

        private LoginInformation loginInformation;
        public BookPreview()
        {
            InitializeComponent();
            instance = this;

            dataInstance = FetchItem.GetInstance();
            name_file += dataInstance.NamaFile;
            name_img += $"{dataInstance.Myurl}read_books?id=get_by_name&name={dataInstance.Data}";
            loginInformation = LoginInformation.GetInstance();
            _ = getData();
            _ = check_isBookmark();
            _ = check_isDownload();
            

        }

        
        private string name_file = "";
        private static DownloadList downloadlistform;

        private async Task getData()
        {
            await fetchJson();
        }


        private string url_download = "";
        private async Task fetchJson()
        {
            string response = await requests.GetStringAsync(name_img);
            get_preview preview = JsonConvert.DeserializeObject<get_preview>(response);
            
            title.Text = preview.Data[3];
            uploaded.Text = preview.Data[2];
            description.Text = preview.Data[5];    
            author.Text = preview.Data[1];
            tahun.Text = preview.Data[4];
            pictureBox1.ImageLocation = preview.Data[0];
            TotalViews.Text = preview.Data[10];
            url_download = preview.Data[8];

            //UPDATE VIEWS
            string[] name_image = preview.Data[0].Split('/');
            int view_update = int.Parse(preview.Data[10]) + 1;
            string url_update_views = $"{dataInstance.Myurl}read_books?id=update_views&name={name_image[4]}&views={view_update.ToString()}";
            string response2 = await requests.GetStringAsync(url_update_views);

        }


        private HttpDownloader httpDownloader = null;
        private void gunaButton1_Click(object sender, EventArgs e, HttpDownloader httpDownloader)
        {
            

        }

       
        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            _ = anjing();
           
            
        }


        /*
         * 
         * 
         * test menggunakan async dan cek progressbar
         * 
         */
        private GunaProgressBar[] progressBars;
        private List<GunaProgressBar> activeProgressBars = new List<GunaProgressBar>();
        private Label[] progressLabels;
        private Label[] speedLabels;
        private Label[] downloadedLabels;
        private Label[] statusLabel;
        private Label[] NamaFileLabel;



        private int count_progress;
        private async Task anjing()
        {
           
         
            progressBars = new GunaProgressBar[] { DownloadList.instance.gunaProgressBar1, DownloadList.instance.gunaProgressBar2,
                                                   DownloadList.instance.gunaProgressBar3, DownloadList.instance.gunaProgressBar4,
                                                   DownloadList.instance.gunaProgressBar5 };
            progressLabels = new Label[] { DownloadList.instance.labelProgress1, DownloadList.instance.labelProgress2,
                                           DownloadList.instance.labelProgress3, DownloadList.instance.labelProgress4,
                                           DownloadList.instance.labelProgress5 };
            speedLabels = new Label[] { DownloadList.instance.LabelSpeed1,DownloadList.instance.LabelSpeed2,DownloadList.instance.LabelSpeed3,
                                        DownloadList.instance.LabelSpeed4, DownloadList.instance.LabelSpeed5
                                        };
            downloadedLabels = new Label[] { DownloadList.instance.LabelDownload1,DownloadList.instance.LabelDownload2,DownloadList.instance.LabelDownload3,
                                        DownloadList.instance.LabelDownload4, DownloadList.instance.LabelDownload5
                                        };
            statusLabel = new Label[] { DownloadList.instance.labelStatus1,DownloadList.instance.labelStatus2,DownloadList.instance.labelStatus3,
                                        DownloadList.instance.labelStatus4, DownloadList.instance.labelStatus5
                                        };
            NamaFileLabel = new Label[] { DownloadList.instance.NameFile1,DownloadList.instance.NameFile2,DownloadList.instance.NameFile3,
                                        DownloadList.instance.NameFile4, DownloadList.instance.NameFile5
                                        };
            foreach (var progressBar in progressBars)
            {
                if (progressBar.Value == 0 || progressBar.Value == 100)
                {
                    await DownloadFileAsync("http://"+url_download, progressBar, progressLabels[count_progress], 
                        speedLabels[count_progress], downloadedLabels[count_progress], statusLabel[count_progress], NamaFileLabel[count_progress]);
                    break;
                }
                count_progress++;
            }
        }


        private async Task DownloadFileAsync(string fileUrl, GunaProgressBar progressBar, Label labelProgress,
                                        Label labelSpeed, Label downloadedLabel, Label statusLabel, Label NamaFile)
        {
            using (WebClient webClient = new WebClient())
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                webClient.DownloadProgressChanged += (s, e) =>
                {
                    double speedInKBps = e.BytesReceived / 1024.0 / 1024.0;
                    labelSpeed.Text = $"Speed : {Math.Round(speedInKBps, 1).ToString()} Mb/s";
                    progressBar.Value = e.ProgressPercentage;
                    labelProgress.Text = $"{e.ProgressPercentage}%";
                    downloadedLabel.Text = $"Downloaded: {e.BytesReceived / 1024} KB";
                };

                await webClient.DownloadFileTaskAsync(new Uri(fileUrl), $"{Application.StartupPath}\\Downloads\\{name_file}.pdf");
                statusLabel.Text = "Finished";
                string name = "";
                int last = name_file.Length/3;
                for (int i = 0; i < name_file.Length; i++) {
                    name += name_file[i];
                    if (i == last)
                    {
                        break;
                    }
                }
                NamaFile.Text = $"Nama File : {name}.pdf";
            }
            
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
            Form1.instance.panelchild.Controls.Add(childForm);
            Form1.instance.panelchild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void gunaButton6_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (loginInformation.IsLoggin)
            {
                if (isDownload)
                {
                    _ = remove_download();
                }
                else
                {
                    _ = add_download();
                }
            }
            else
            {
                formkecil(new Login());
            }
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (loginInformation.IsLoggin)
            {
                if (isBookmark)
                {
                    _ = remove_bookmark();
                }
                else
                {
                    _ = add_bookmark();
                }
            }
            else
            {
                formkecil(new Login());
            }
           
            
        }


        private async Task check_isBookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name", dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "isBookmark_internal"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("True"))
                {
                    gunaButton3.Image = Resources.Bookmark_pressed;
                    isBookmark = true;

                }
                else
                {
                    gunaButton3.Image = Resources.Bookmark;
                    isBookmark = false;
                }
            }
        }

        private async Task check_isDownload()
        {
            string url = $"{dataInstance.Myurl}download";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name", dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "isDownload_internal"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("True"))
                {
                    gunaButton2.Image = Resources.Love_pressed;
                    isDownload = true;

                }
                else
                {
                    gunaButton2.Image = Resources.Love;
                    isDownload = false;
                }
            }
        }

        private async Task add_bookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name",  dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "add_bookmark_internal"}
                };

                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("Success"))
                {
                    isBookmark = true;
                    gunaButton3.Image = Resources.Bookmark_pressed;
                }
                else
                {
                    isBookmark = false;
                    gunaButton3.Image = Resources.Bookmark;
                }
            }
        }

        private async Task add_download()
        {
            string url = $"{dataInstance.Myurl}download";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name",  dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "add_download_internal"}
                };

                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("Success"))
                {
                    isDownload = true;
                    gunaButton2.Image = Resources.Love_pressed;
                }
                else
                {
                    isDownload = false;
                    gunaButton2.Image = Resources.Love;
                }
            }
        }

        private async Task remove_bookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name",  dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "remove_bookmark_internal"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (responseContent.Contains("Success"))
                {
                    isBookmark = false;
                    gunaButton3.Image = Properties.Resources.Bookmark;
                }
                else
                {
                    isBookmark = true;
                }
            }
        }

        private async Task remove_download()
        {
            string url = $"{dataInstance.Myurl}download";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name",  dataInstance.Data},
                    {"username", loginInformation.Username},
                    {"id", "remove_download_internal"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (responseContent.Contains("Success"))
                {
                    isDownload = false;
                    gunaButton2.Image = Properties.Resources.Love;
                }
                else
                {
                    isDownload = true;
                }
            }
        }
    }
    class get_preview
    {
        public List<string> Data { get; set; }
    }
}
