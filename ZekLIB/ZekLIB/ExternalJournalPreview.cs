using AltoHttp;
using Guna.UI.WinForms;
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
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;
using ZekLIB.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ZekLIB
{
    public partial class ExternalJournalPreview : Form
    {
        public static ExternalJournalPreview instance;
        private FetchItem dataInstance;
        private LoginInformation loginInformation;

        private string addeddate_;
        private string title_;
        private string author_;
        private string description_;
        private string publication_date;
        private string language;
        private string source;
        private string link_download;
        private string image_url;


        private bool isBookmark;
        private bool isDownload;
        public ExternalJournalPreview()
        {
            InitializeComponent();
            dataInstance = FetchItem.GetInstance();
            loginInformation = LoginInformation.GetInstance();
            instance = this;
            addeddate_ += dataInstance.Addeddate;
            title_ += dataInstance.Title;
            author_ += dataInstance.Author;
            description_ += dataInstance.Description;
            publication_date += dataInstance.Publication_date;
            language += dataInstance.Language_;
            source += dataInstance.Sources;
            link_download += dataInstance.Link_Download;
            image_url += dataInstance.Image_url;

            DisplayMenu();

            _ = check_isBookmark();
            _ = check_isDownload();
            
        }

      

        private async Task check_isBookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                { 
                    {"source", source},
                    {"username", loginInformation.Username},
                    {"id", "isBookmark_external"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("True")){
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
                    {"source", title_},
                    {"username", loginInformation.Username},
                    {"id", "isDownload_external"}
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
        private void DisplayMenu()
        {
            pictureBox1.ImageLocation = image_url;
            title.Text = title_;
            uploaded.Text = author_;
            description.Text = description_;
            author.Text = author_;
            PublicationDate.Text = publication_date;
            Language.Text = language;
            Source.Text = source;

        }

        private GunaProgressBar[] progressBars;
        private Label[] progressLabels;
        private Label[] speedLabels;
        private Label[] downloadedLabels;
        private Label[] statusLabel;
        private Label[] NamaFileLabel;

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            _ = anjing();
        }
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

            int count_progress = FindAvailableProgressBar();
            if (count_progress != -1)
            {
                DownloadFileAsync(link_download, progressBars[count_progress], progressLabels[count_progress],
                       speedLabels[count_progress], downloadedLabels[count_progress], statusLabel[count_progress], NamaFileLabel[count_progress]);
            }


            /*
            foreach (var progressBar in progressBars)
            {
                if (progressBar.Value == 0 || progressBar.Value == 100)
                {
                   _ =  DownloadFileAsync(link_download, progressBar, progressLabels[count_progress],
                        speedLabels[count_progress], downloadedLabels[count_progress], statusLabel[count_progress], NamaFileLabel[count_progress]);
                    break;
                }
                count_progress++;
            }
            */
        }

     
        private async Task DownloadFileAsync(string fileUrl, GunaProgressBar progressBar, Label labelProgress,
                                        Label labelSpeed, Label downloadedLabel, Label statusLabel, Label NamaFile)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(link_download, HttpCompletionOption.ResponseHeadersRead))
                using (var stream = await response.Content.ReadAsStreamAsync())
                {

                    var fileName = Path.GetFileName(link_download);
                    NamaFile.Text = $"Name File : {fileName}";
                    using (var fileStream = new FileStream($"{Application.StartupPath}\\Downloads\\{fileName}", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var buffer = new byte[8192];
                        var totalBytesRead = 0;
                        var totalBytes = response.Content.Headers.ContentLength ?? -1L;

                        while (true)
                        {
                            var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                break;
                            }

                            await fileStream.WriteAsync(buffer, 0, bytesRead);

                            if (totalBytes > 0)
                            {
                                double speedInKBps = totalBytesRead / 1024.0 / 1024.0;
                                totalBytesRead += bytesRead;
                                var percentage = (int)((double)totalBytesRead / totalBytes * 100);
                                progressBar.Value = percentage;
                                labelProgress.Text = percentage.ToString();
                                statusLabel.Text = $"Downloading... {percentage}%";
                                labelSpeed.Text = $"Speed : {Math.Round(speedInKBps, 1).ToString()} Mb/s";
                                downloadedLabel.Text = $"Downloaded: {totalBytesRead / 1024} KB";
                            }
                        }
                    }
                }

            

                /*
                string name = "";
                int last = title_.Length / 3;
                for (int i = 0; i < title_.Length; i++)
                {
                    name += title_[i];
                    if (i == last)
                    {
                        break;
                    }
                }
                NamaFile.Text = $"Nama File : {name}.pdf";
                httpDownloader = new HttpDownloader(link_download, $"{Application.StartupPath}\\Downloads\\{Path.GetFileName(title_)}.pdf");
                httpDownloader.ProgressChanged += (sender, e) =>
                {

                    progressBar.Value = (int)e.Progress;
                    labelSpeed.Text = string.Format("{0} MB/s", (e.SpeedInBytes / 1024d / 1024d).ToString("0.00"));
                    labelProgress.Text = $"{e.Progress}%";
                    downloadedLabel.Text = $"Downloaded: {(httpDownloader.TotalBytesReceived / 1024d / 1024d).ToString("0.00")} KB";

                };
                httpDownloader.DownloadCompleted += (sender, e) =>
                {

                };
                httpDownloader.Start();
               */

            }
        }


        private int FindAvailableProgressBar()
        {
            progressBars = new GunaProgressBar[] { DownloadList.instance.gunaProgressBar1, DownloadList.instance.gunaProgressBar2,
                                                    DownloadList.instance.gunaProgressBar3, DownloadList.instance.gunaProgressBar4,
                                                    DownloadList.instance.gunaProgressBar5 };

            for (int i = 0; i < progressBars.Length; i++)
            {
                if (progressBars[i].Value == 0 || progressBars[i].Value == 100)
                    return i;
            }
            return -1;
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (loginInformation.IsLoggin)
            {
                if (isBookmark)
                {
                    remove_bookmark();
                }
                else
                {
                    add_bookmark();
                }
            }
            else
            {
                formkecil(new Login());
            }
            
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
        private async Task remove_bookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"source", source},
                    {"username", loginInformation.Username},
                    {"id", "remove_bookmark_external"}
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
                    {"source", title_},
                    {"username", loginInformation.Username},
                    {"id", "remove_bookmark_external"}
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
        private static readonly HttpClient httpClient = new HttpClient();
        private async Task add_bookmark()
        {
            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"added_date", addeddate_},
                    {"title", title_},
                    {"author", author_},
                    {"description", description_},
                    {"publication_date", publication_date},
                    {"language", language},
                    {"source", source},
                    {"link_download", link_download},
                    {"img_url", image_url},
                    {"username", loginInformation.Username},
                    {"id", "bookmark_journal_external"}
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
                    {"added_date", addeddate_},
                    {"title", title_},
                    {"author", author_},
                    {"description", description_},
                    {"publication_date", publication_date},
                    {"language", language},
                    {"source", source},
                    {"link_download", link_download},
                    {"img_url", image_url},
                    {"username", loginInformation.Username},
                    {"id", "download_journal_external"}
                };

                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent.Contains("Success"))
                {
                    isBookmark = true;
                    gunaButton2.Image = Resources.Love_pressed;
                }
                else
                {
                    isBookmark = false;
                    gunaButton2.Image = Resources.Love;
                }
            }
        }


    }
}
