using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ZekLIB
{
    public partial class ExternalJournal : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
       );
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        public static ExternalJournal instance;

        HttpClient requests = new HttpClient();

        private string journal_type = "";

        private string url_journal_1 = "";
        private string url_journal_2 = "";
        private string url_journal_3 = "";
        private string url_journal_4 = "";
        private string url_journal_5 = "";
        private string url_journal_6 = "";
        private string url_journal_7 = "";
        private string url_journal_8 = "";
        private string url_journal_9 = "";
        private string url_journal_10 = "";
        private string url_journal_11 = "";
        private string url_journal_12 = "";


        private FetchItem dataInstance;
        public ExternalJournal()
        {
            InitializeComponent();
            setting_startup();
            AlignAllRichTextBoxesCenter();
            instance = this;
            dataInstance = FetchItem.GetInstance();
            comboBox1.Items.Add("Journal 1");
            comboBox1.Items.Add("Journal 2");
            comboBox1.SelectedItem = "Journal 1";
            journal_type = "Journal 1";
            _ = getData();
        }

        private void setting_startup()
        {
            richboxTitle = new RichTextBox[] { title_1, title_2, title_3, title_4, title_5, title_6, title_7, title_8, title_9, title_10, title_11, title_12 };
            image = new PictureBox[] { image_1, image_2, image_3, image_4, image_5, image_6, image_7, image_8, image_9, image_10, image_11, image_12 };
            urljournal = new String[] { url_journal_1, url_journal_2, url_journal_3, url_journal_4, url_journal_5, url_journal_6, url_journal_7, url_journal_8, url_journal_9, url_journal_10, url_journal_11, url_journal_12 };
            richboxAuthor = new RichTextBox[] { author_1, author_2, author_3, author_4, author_5, author_6, author_7, author_8, author_9, author_10, author_11, author_12 };
            int cnt = 0;
            foreach(var images in image)
            {
                richboxTitle[cnt].Visible = false;
                richboxAuthor[cnt].Visible = false;
                image[cnt].Visible = false;
                cnt++;
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ExternalJournal_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void ExternalJournal_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox14_TextChanged(object sender, EventArgs e)
        {

        }
        private void AlignAllRichTextBoxesCenter()
        {
            foreach (Control control in this.Controls)
            {
                if (control is RichTextBox richTextBox)
                {
                    richTextBox.SelectionAlignment = HorizontalAlignment.Center;
                }
            }
        }

        private int currentPage = 1;
        private int currentPageIndex = 1;
        List<Item> items = new List<Item>();
        private int itemsPerPage = 12;
        private int total_result;
        private int totalPages;

        private RichTextBox[] richboxTitle;
        private PictureBox[] image;
        private String[] urljournal;
        private RichTextBox[] richboxAuthor;


        private List<String> url_journal_temp = new List<String>();
        private List<String> link_download = new List<String>();
        private List<String> description = new List<String>();
        private List<String> author_list = new List<String>();
        private List<String> added_date = new List<String>();
        private List<String> language__ = new List<String>();
        private List<String> source = new List<String>();
        private List<String> title_ = new List<String>();
        private List<String> img_urls = new List<String>();
        private void change_label(int count, Item item, string img_url)
        {
            richboxTitle = new RichTextBox[] { title_1, title_2, title_3, title_4, title_5, title_6, title_7 , title_8, title_9, title_10, title_11, title_12};
            image = new PictureBox[] { image_1, image_2, image_3, image_4, image_5, image_6, image_7, image_8, image_9, image_10, image_11, image_12 };
            urljournal = new String[] { url_journal_1, url_journal_2, url_journal_3, url_journal_4, url_journal_5, url_journal_6, url_journal_7, url_journal_8, url_journal_9, url_journal_10, url_journal_11, url_journal_12 };
            richboxAuthor = new RichTextBox[] { author_1, author_2, author_3, author_4, author_5, author_6, author_7, author_8, author_9, author_10, author_11, author_12 };
            int cntL = 0;

            richboxTitle[count].Visible = true;
            richboxAuthor[count].Visible = true;
            image[count].Visible = true;

            richboxTitle[count].Text = item.fields.title.ToUpper();
            image[count].ImageLocation = img_url;
            urljournal[count] = item.fields.identifier;
            richboxAuthor[count].MaxLength = 10;
            richboxAuthor[count].ScrollBars = RichTextBoxScrollBars.None;
            img_urls.Add(img_url);

            image[count].Click += pictureBox_Click;
            image[count].Cursor = Cursors.Hand;


            url_journal_temp.Add(item.fields.identifier);
            string generate_url_download = $"https://archive.org/download/{item.fields.identifier}/{item.fields.identifier.Replace("arxiv-", "")}.pdf";
            link_download.Add(generate_url_download);
            description.Add(item.fields.description);
            added_date.Add(item.fields.addeddate);
            title_.Add(item.fields.title);  


            string lang = "";
           

            
            language__.Add(lang);
            source.Add(item.fields.source);
            

            richboxAuthor[count].Text = "";
            if (item.fields.creator != null)
            {
                foreach (var authorlist in item.fields.creator)
                {
                    richboxAuthor[count].Text += authorlist;
                }
            }
            else
            {
                richboxAuthor[count].Text = "";
            }
            author_list.Add(richboxAuthor[count].Text);



        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            image = new PictureBox[] { image_1, image_2, image_3, image_4, image_5, image_6, image_7, image_8, image_9, image_10, image_11, image_12 };
            urljournal = new String[] { url_journal_1, url_journal_2, url_journal_3, url_journal_4, url_journal_5, url_journal_6, url_journal_7, url_journal_8, url_journal_9, url_journal_10, url_journal_11, url_journal_12 };
            PictureBox pictureBox = (PictureBox)sender;
            int clickedImageIndex = Array.IndexOf(image, pictureBox);
            
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

        private async Task getData()
        {
            await fetchJson();
        }
        private async Task fetchJson()
        {
            string uri = "";
            if (journal_type == "Journal 1")
            {
                uri = $"{dataInstance.Myurl}search?id=searchExternal1&query={dataInstance.Query}&page={currentPage}";
            }
            else
            {
                uri = $"{dataInstance.Myurl}search?id=searchExternal2&query={dataInstance.Query}&page={currentPage}";
            }
            url_journal_temp.Clear();
            string response = await requests.GetStringAsync(uri);
            
            DataModel data = JsonConvert.DeserializeObject<DataModel>(response);
            
            Data data_total_result = JsonConvert.DeserializeObject<Data>(response);
            total_result = int.Parse(data_total_result.total_result);
            if (total_result > 0)
            {
                totalPages = (total_result / 12) - 12;
                if (totalPages < 0)
                {
                    totalPages = (total_result / 12);
                }
               
                items = data.Data;
                TotalResult.Text = total_result.ToString();
                totalPagesLabel.Text = totalPages.ToString();
                int count = 0;
                url_journal_temp.Clear();
                link_download.Clear();
                description.Clear();
                author_list.Clear();
                added_date.Clear();
                language__.Clear();
                source.Clear();
                title_.Clear();
                img_urls.Clear();
                foreach (var item in items)
                {
                    string img_url = "https://archive.org/services/img/" + item.fields.identifier;

                    //change_information(count, item, img_url);
                    change_label(count, item, img_url);
                    AlignAllRichTextBoxesCenter();

                    count++;

                }
            }
            else
            {
                TotalResult.Text = "0";
                totalPagesLabel.Text = "0";
            }
            
            /*
            string response = await requests.GetStringAsync("http://127.0.0.1:5000/archieve_journal?id=all_journal&page="+ currentPage.ToString());
            DataModel data = JsonConvert.DeserializeObject<DataModel>(response);
            Data data_total_result = JsonConvert.DeserializeObject<Data>(response);
            total_result = int.Parse(data_total_result.total_result);
            totalPages = (total_result / 12)-12;
            items = data.Data;
            TotalResult.Text = total_result.ToString();
            totalPagesLabel.Text = totalPages.ToString();
            int count = 0;
       
            url_journal_temp.Clear();
            link_download.Clear();
            description.Clear();
            author_list.Clear();
            added_date.Clear();
            language__.Clear();
            source.Clear();
            title_.Clear();
            img_urls.Clear();
            foreach (var item in items)
            {
                string img_url = "https://archive.org/services/img/" + item.fields.identifier;

                //change_information(count, item, img_url);
                change_label(count, item, img_url);
                AlignAllRichTextBoxesCenter();

                count++;

            }
            */

        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            
            currentPage++;
            gunaNumeric1.Value = currentPage;
            _ = getData();

        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                gunaNumeric1.Value = currentPage;
                _ = getData();
                //InitializeComboBox();
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(gunaNumeric1.Value);
            _ = getData();
        }

        private void image_1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            journal_type = comboBox1.SelectedItem.ToString();
            items.Clear();
            _ = getData();
        }
    }
    public class Data
    {
        public string total_result { get; set; }
    }
    public class DataModel
    {
        public List<Item> Data { get; set; }
    }
    public class Item
    {
       
        public Fields fields { get; set; }
        public string index { get; set; }
    }

    public class Fields
    {
        public string addeddate { get; set; }
        public List<string> creator { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string identifier { get; set; }
        public long item_size { get; set; }
        public List<string> language { get; set; }
        public string mediatype { get; set; }
        public int month { get; set; }
        public string publicdate { get; set; }
        public string source { get; set; }
        public string title { get; set; }
        public int year { get; set; }
    }
}
