using AltoHttp;
using Guna.UI.WinForms;
using Guna.UI2.WinForms;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ZekLIB
{
    public partial class bookmarkList : Form
    {

        private LoginInformation loginInformation;
        private FetchItem dataInstance;

        private string source_now = "internal source";
        public bookmarkList()
        {
            InitializeComponent();
            loginInformation = LoginInformation.GetInstance();
            dataInstance = FetchItem.GetInstance();
            gunaComboBox1.Items.Add("internal source");
            gunaComboBox1.Items.Add("external source");
            gunaComboBox1.SelectedItem = "internal source";
            setting_up();
            _ = get_all_bookmark_internal();
        }

        private void setting_up()
        {
            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;    
            pictureBox3.Visible = false;
            description_bookmark1.Visible = false;
            description_bookmark2.Visible = false;
            description_bookmark3.Visible = false;
            remove_bookmark1.Visible = false;
            remove_bookmark2.Visible = false;
            remove_bookmark3.Visible = false;
            view_bookmark1.Visible = false;
            view_bookmark2.Visible = false;
            view_bookmark3.Visible = false;

        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private RichTextBox[] richboxTitle;
        private RichTextBox[] richboxDescription;
        private PictureBox[] item_picture;
        private GunaButton[] view_button;
        private GunaButton[] remove_button;

        private int currentPage = 0;
        private int itemsPerPage = 3;
        List<List<String>> items = new List<List<String>>();
        private async Task get_all_bookmark_internal()
        {
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3};
            richboxDescription = new RichTextBox[] {description_bookmark1, description_bookmark2, description_bookmark3};
            item_picture = new PictureBox[] {pictureBox1, pictureBox2, pictureBox3};
            view_button = new GunaButton[] { view_bookmark1, view_bookmark2, view_bookmark3 };
            remove_button = new GunaButton[] {remove_bookmark1, remove_bookmark2, remove_bookmark3};


            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"username", loginInformation.Username},
                    {"id", "get_all_bookmark_internal"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();


                if (responseContent.Contains("False"))
                {
                    
                }
                else
                {
                    getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(responseContent);
                    label1.Text = $"Total : {_getbooks.data.Count.ToString()}";
                    int cont = 0;
                    items = _getbooks.data;
                    int page = ((_getbooks.data.Count + 3) - 1) / 3;
                    label3.Text = $"Total Page : {page}";
                    gunaNumeric1.Value = 0;
                    gunaNumeric1.Maximum = page - 1;
                    foreach (var  book in _getbooks.data) {
                        string[] name_image = book[0].Split('/');
                        richboxTitle[cont].Text = book[3];
                        richboxTitle[cont].Visible = true;
                        richboxDescription[cont].Text = book[5];
                        richboxDescription[cont].Visible = true;
                        item_picture[cont].ImageLocation = book[0];
                        item_picture[cont].Visible = true;
                        remove_button[cont].Visible = true;
                        view_button[cont].Visible = true;
                        view_button[cont].Click += Button_Click;
                        view_button[cont].Tag = name_image[4]+"|"+"view_product";
                        remove_button[cont].Click += Button_Click;
                        remove_button[cont].Tag = name_image[4] + "|" + "remove_product";

                        cont++;
                    }
                }
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
        private void Button_Click(object sender, EventArgs e)
        {
            item_picture = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3 };
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richboxDescription = new RichTextBox[] { description_bookmark1, description_bookmark2, description_bookmark3 };
            view_button = new GunaButton[] { view_bookmark1, view_bookmark2, view_bookmark3 };
            remove_button = new GunaButton[] { remove_bookmark1, remove_bookmark2, remove_bookmark3 };

            if (source_now == "internal source")
            {
                GunaButton clickedButton = (GunaButton)sender;
                object data = clickedButton.Tag;
                string dataString = data.ToString();

                string[] data_parser = dataString.Split('|');
                if (dataString.Contains("view_product"))
                {
                    dataInstance.Data = data_parser[0];
                    formkecil(new BookPreview());
                }
                else
                {
                    _ = remove_bookmark(data_parser[0]);
                }
            }
            else
            {
                GunaButton clickedButton = (GunaButton)sender;
                object data = clickedButton.Tag;
                string dataString = data.ToString();

                string[] data_parser = dataString.Split('|');
                if (dataString.Contains("view_product"))
                {
                    int clickedImageIndex = Array.IndexOf(view_button, clickedButton);
                    dataInstance.Addeddate = added_date[clickedImageIndex];
                    dataInstance.Title = title_[clickedImageIndex];
                    dataInstance.Author = author_list[clickedImageIndex];
                    dataInstance.Description = description[clickedImageIndex];
                    dataInstance.Publication_date = added_date[clickedImageIndex];
                    dataInstance.Language_ = language__[clickedImageIndex];
                    dataInstance.Sources = source[clickedImageIndex];
                    dataInstance.Link_Download = link_download[clickedImageIndex];
                    dataInstance.Image_url = img_urls[clickedImageIndex];
                    formkecil(new ExternalJournalPreview());
                }
                else
                {
                    _ = remove_bookmark(data_parser[0]);
                }

               
            }

        }

        private async Task remove_bookmark(string name_img)
        {
            string url = $"{dataInstance.Myurl}bookmark";
            if (source_now == "internal source")
            {
                using (var httpClient = new HttpClient())
                {


                    Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"name",  name_img},
                    {"username", loginInformation.Username},
                    {"id", "remove_bookmark_internal"}
                };

                    var content = new FormUrlEncodedContent(data);

                    var response = await httpClient.PostAsync(url, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent.Contains("Success"))
                    {
                        setting_up();
                        _ = get_all_bookmark_internal();
                    }
                    else
                    {
                        _ = get_all_bookmark_internal();
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {


                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"source",  name_img},
                        {"username", loginInformation.Username},
                        {"id", "remove_bookmark_external"}
                    };

                    var content = new FormUrlEncodedContent(data);

                    var response = await httpClient.PostAsync(url, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent.Contains("Success"))
                    {
                        setting_up();
                        _ = get_all_bookmark_external();
                    }
                    else
                    {
                        _ = get_all_bookmark_external();
                    }
                }
            }

            
        }

        private void bookmarkList_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            _ = download();
        }

        Queue<string> downloadQueue = new Queue<string>();
        private GunaProgressBar[] progressBars;
        private Label[] progressLabels;
        private Label[] speedLabels;
        private Label[] downloadedLabels;
        private Label[] statusLabel;
        private Label[] NamaFileLabel;

        private WebClient[] webClients;
        private async Task download()
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
            webClients = new WebClient[5];
            string url = $"{dataInstance.Myurl}bookmark";
            if (source_now == "internal source")
            {
                using (var httpClient = new HttpClient())
                {


                    Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"username", loginInformation.Username},
                    {"id", "get_all_bookmark_internal"}
                };

                    var content = new FormUrlEncodedContent(data);
                    var response = await httpClient.PostAsync(url, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (responseContent.Contains("False"))
                    {

                    }
                    else
                    {
                        getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(responseContent);
                        int cont = 0;
                        foreach (var book in _getbooks.data)
                        {
                            downloadQueue.Enqueue("http://"+book[8]);
                            cont++;
                        }
           
                        var HC = new HttpClient();
                        var tasks = new List<Task>();

                        while (downloadQueue.Count > 0)
                        {
                            if (tasks.Count < 5)
                            {

                                int i = FindAvailableProgressBar();
                                if (i != -1)
                                {
                                    string downloadLink = downloadQueue.Dequeue();
                                    progressBars[i].Value = 1;
                                    var task = DownloadFileAsync(httpClient, progressBars[i], downloadLink, _getbooks.data[i][1], progressLabels[i], speedLabels[i], downloadedLabels[i], statusLabel[i], NamaFileLabel[i]);
                                    tasks.Add(task);
                                }
                            }
                            else
                            {
                                await Task.WhenAny(tasks);
                                tasks.RemoveAll(t => t.IsCompleted);
                            }
                        }
                        await Task.WhenAll(tasks);
                        MessageBox.Show("DOWNLOAD DONE!");

                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {


                    Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"username", loginInformation.Username},
                    {"id", "get_all_bookmark_external"}
                };

                    var content = new FormUrlEncodedContent(data);
                    var response = await httpClient.PostAsync(url, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (responseContent.Contains("False"))
                    {

                    }
                    else
                    {
                        getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(responseContent);
                        int cont = 0;
                        foreach (var book in _getbooks.data)
                        {
                            downloadQueue.Enqueue(book[7]);
                            cont++;
                        }
                        var HC = new HttpClient();
                        var tasks = new List<Task>();
                        
                        while (downloadQueue.Count > 0)
                        {
                            if (tasks.Count < 5)
                            {
                                
                                int i = FindAvailableProgressBar();
                                if (i != -1){
                                    string downloadLink = downloadQueue.Dequeue();
                                    progressBars[i].Value = 1;
                                    var task = DownloadFileAsync(httpClient, progressBars[i], downloadLink, _getbooks.data[i][1], progressLabels[i], speedLabels[i], downloadedLabels[i], statusLabel[i], NamaFileLabel[i]);
                                    tasks.Add(task);       
                                }                               
                            }
                            else
                            {
                                await Task.WhenAny(tasks);
                                tasks.RemoveAll(t => t.IsCompleted);
                            }
                        }
                        await Task.WhenAll(tasks);
                        MessageBox.Show("DOWNLOAD DONE!");
                        

                    }
                }
            }
                 
        }

        private async Task DownloadFileAsync(HttpClient httpClient, GunaProgressBar progressBar, string downloadLink, string title_, Label labelProgress,
                                        Label labelSpeed, Label downloadedLabel, Label statusLabel, Label NamaFile)
        {
            using (var response = await httpClient.GetAsync(downloadLink, HttpCompletionOption.ResponseHeadersRead))
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
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
                var fileName = Path.GetFileName(downloadLink);
                NamaFile.Text = $"Name File : {name}.pdf";
                using (var fileStream = new FileStream($"{Application.StartupPath}\\BookmarkDownloads\\{fileName}", FileMode.Create, FileAccess.Write, FileShare.None))
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

            // Unduhan selesai
            progressBar.Value = 100;
            statusLabel.Text = "Download Complete";
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


        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = gunaComboBox1.SelectedItem.ToString();
            if (selectedItem == "internal source")
            {
                source_now = "internal source";
                setting_up();
                items.Clear();
                currentPage = 0;
                _ = get_all_bookmark_internal();
            }
            else
            {
                source_now = "external source";
                setting_up();
                items.Clear();
                currentPage = 0;
                _ = get_all_bookmark_external();
            }
        }


        private List<String> link_download = new List<String>();
        private List<String> description = new List<String>();
        private List<String> author_list = new List<String>();
        private List<String> added_date = new List<String>();
        private List<String> language__ = new List<String>();
        private List<String> source = new List<String>();
        private List<String> title_ = new List<String>();
        private List<String> img_urls = new List<String>();

        private async Task get_all_bookmark_external()
        {

            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richboxDescription = new RichTextBox[] { description_bookmark1, description_bookmark2, description_bookmark3 };
            item_picture = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3 };
            view_button = new GunaButton[] { view_bookmark1, view_bookmark2, view_bookmark3 };
            remove_button = new GunaButton[] { remove_bookmark1, remove_bookmark2, remove_bookmark3 };


            string url = $"{dataInstance.Myurl}bookmark";

            using (var httpClient = new HttpClient())
            {


                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"username", loginInformation.Username},
                    {"id", "get_all_bookmark_external"}
                };

                var content = new FormUrlEncodedContent(data);

                var response = await httpClient.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();


                if (responseContent.Contains("False"))
                {

                }
                else
                {
                    getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(responseContent);

                    label1.Text = $"Total : {_getbooks.data.Count.ToString()}";
                    int cont = 0;
                    items = _getbooks.data;
                    int page = ((_getbooks.data.Count + 3) - 1) / 3;
                    label3.Text = $"Total Page : {page}";
                    gunaNumeric1.Value = 0;
                    gunaNumeric1.Maximum = page - 1;
                    foreach (var book in _getbooks.data)
                    {

                        link_download.Add(book[7]);
                        description.Add(book[3]);
                        added_date.Add(book[0]);
                        title_.Add(book[1]);
                        language__.Add("English");
                        author_list.Add(book[2]);
                        source.Add(book[6]);
                        img_urls.Add(book[8]);

                        richboxTitle[cont].Text = book[1];
                        richboxTitle[cont].Visible = true;

                        richboxDescription[cont].Text = book[3];
                        richboxDescription[cont].Visible = true;

                        item_picture[cont].ImageLocation = book[8];
                        item_picture[cont].Visible = true;
                    
                        remove_button[cont].Visible = true;
                        view_button[cont].Visible = true;

                        view_button[cont].Click += Button_Click;
                        view_button[cont].Tag = book[6] + "|" + "view_product";

                        remove_button[cont].Click += Button_Click;
                        remove_button[cont].Tag = book[6] + "|" + "remove_product";
                        
                        cont++;
                    }
                    
                }
            }
        }

        private void display_item_external()
        {
            setting_up();
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richboxDescription = new RichTextBox[] { description_bookmark1, description_bookmark2, description_bookmark3 };
            item_picture = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3 };
            view_button = new GunaButton[] { view_bookmark1, view_bookmark2, view_bookmark3 };
            remove_button = new GunaButton[] { remove_bookmark1, remove_bookmark2, remove_bookmark3 };

            link_download.Clear();
            description.Clear();
            added_date.Clear();
            title_.Clear();
            language__.Clear();
            author_list.Clear();
            source.Clear();
            img_urls.Clear();
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, items.Count);
            List<List<string>> currentPageItems = items.GetRange(startIndex, endIndex - startIndex);
            int cont = 0;
            foreach (var book in  currentPageItems)
            {
                link_download.Add(book[7]);
                description.Add(book[3]);
                added_date.Add(book[0]);
                title_.Add(book[1]);
                language__.Add("English");
                author_list.Add(book[2]);
                source.Add(book[6]);
                img_urls.Add(book[8]);
                richboxTitle[cont].Text = book[1];
                richboxTitle[cont].Visible = true;

                richboxDescription[cont].Text = book[3];
                richboxDescription[cont].Visible = true;

                item_picture[cont].ImageLocation = book[8];
                item_picture[cont].Visible = true;

                remove_button[cont].Visible = true;
                view_button[cont].Visible = true;

                view_button[cont].Click += Button_Click;
                view_button[cont].Tag = book[6] + "|" + "view_product";

                remove_button[cont].Click += Button_Click;
                remove_button[cont].Tag = book[6] + "|" + "remove_product";

                cont++;
            }

        }

        private void display_item_internal()
        {
            setting_up();
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richboxDescription = new RichTextBox[] { description_bookmark1, description_bookmark2, description_bookmark3 };
            item_picture = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3 };
            view_button = new GunaButton[] { view_bookmark1, view_bookmark2, view_bookmark3 };
            remove_button = new GunaButton[] { remove_bookmark1, remove_bookmark2, remove_bookmark3 };
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, items.Count);
            List<List<string>> currentPageItems = items.GetRange(startIndex, endIndex - startIndex);
            int cont = 0;
            foreach(var book in  currentPageItems)
            {
                string[] name_image = book[0].Split('/');
                richboxTitle[cont].Text = book[3];
                richboxTitle[cont].Visible = true;

                richboxDescription[cont].Text = book[5];
                richboxDescription[cont].Visible = true;

                item_picture[cont].ImageLocation = book[0];
                item_picture[cont].Visible = true;
                remove_button[cont].Visible = true;
                view_button[cont].Visible = true;


                view_button[cont].Click += Button_Click;
                view_button[cont].Tag = name_image[4] + "|" + "view_product";
                remove_button[cont].Click += Button_Click;
                remove_button[cont].Tag = name_image[4] + "|" + "remove_product";
                cont++;
            }
           
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            int maxPage = (items.Count - 1) / itemsPerPage;
            if (currentPage < maxPage)
            {
                currentPage++;
                gunaNumeric1.Value = currentPage;
                if (source_now == "external source")
                {
                    display_item_external();
                }
                else
                {
                    display_item_internal();
                }
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                gunaNumeric1.Value = currentPage;
                if (source_now == "external source")
                {
                    display_item_external();
                }
                else
                {
                    display_item_internal();
                }
            }
        }

        private void RemoveAllBookmark_Click(object sender, EventArgs e)
        {
            _ = remove_all_download();
        }


        private async Task remove_all_download()
        {
            string url = $"{dataInstance.Myurl}bookmark";
            if (source_now == "internal source")
            {
                using (var httpClient = new HttpClient())
                {


                    Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"username", loginInformation.Username},
                    {"id", "remove_all_bookmark_internal"}
                };

                    var content = new FormUrlEncodedContent(data);

                    var response = await httpClient.PostAsync(url, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent.Contains("Success"))
                    {
                        setting_up();
                        _ = get_all_bookmark_internal();
                    }
                    else
                    {
                        _ = get_all_bookmark_internal();
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"username", loginInformation.Username},
                        {"id", "remove_all_bookmark_external"}
                    };

                    var content = new FormUrlEncodedContent(data);

                    var response = await httpClient.PostAsync(url, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent.Contains("Success"))
                    {
                        setting_up();
                        _ = get_all_bookmark_external();
                    }
                    else
                    {
                        _ = get_all_bookmark_external();
                    }
                }
            }
        }
    }
}
