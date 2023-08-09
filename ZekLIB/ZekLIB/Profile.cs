using Guna.UI.WinForms;
using Newtonsoft.Json;
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
    public partial class Profile : Form
    {
        private LoginInformation dataInstance;
        public Profile instance;
        private FetchItem fetchItem;
        public Profile()
        {
            InitializeComponent();
            gunaButton2.Visible = false;
            gunaButton3.Visible = false;
            gunaButton1.Visible = false;
            visible_item();
            dataInstance = LoginInformation.GetInstance();
            fetchItem = FetchItem.GetInstance();
            instance = this;
            _ = get_info_user();
            _ = get_books_uploaded();
        }

        private void visible_item()
        {
            gunaPictureBox2.Visible = false;
            gunaPictureBox3.Visible = false;
            gunaPictureBox4.Visible = false;
            gunaPictureBox5.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            richTextBox5.Visible = false;
        }
        private void Profile_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            formkecil(new EditProfile());
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
        private void gunaButton10_Click(object sender, EventArgs e)
        {
            formkecil(new bookmarkList());
        }

        private static readonly HttpClient httpClient = new HttpClient();
        private async Task get_info_user()
        {
            string apiEndpoint = $"{fetchItem.Myurl}/account";
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
                            if( status.status.username == "zekkelar"){
                                gunaButton1.Visible = true;
                                gunaButton2.Visible = true;
                                gunaButton3.Visible = true;
                            }
                            else
                            {
                                TypeProfile.Text = $"Type : {status.status.type}";
                            }
                            string res = status.status.name;
                            NameProfile.Text = res;
                            Total_download.Text = status.status.total_downloads;
                            Books_upload.Text = status.status.books_upload;
                            Total_likes.Text = status.status.total_likes;
                            BioProfile.Text = status.status.bio;
                            gunaPictureBox1.ImageLocation = status.status.profile_image;

                        }
                        else
                        {

                        }
                    }
                }
            }

        }

        private GunaPictureBox[] image;
        private RichTextBox[] title_items;

        private List<String> name_img = new List<String>();
        private List<String> title_ = new List<String>();

        private string getNameFile(string title)
        {
            string[] pisah = title.Split(' ');
            string name = "";
            foreach (string s in pisah)
            {
                name += '-' + s;
            }
            return name;
        }

        private async Task get_books_uploaded()
        {

            image = new GunaPictureBox[] { gunaPictureBox2, gunaPictureBox3, gunaPictureBox4, gunaPictureBox5 };
            title_items = new RichTextBox[] {richTextBox2, richTextBox3, richTextBox4, richTextBox5};
            HttpClient requests = new HttpClient();
            string response = await requests.GetStringAsync($"{fetchItem.Myurl}/read_books?id=get_book_uploaded&username="+dataInstance.Username);
            getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(response);
            int page = ((_getbooks.data.Count + 4) - 1) / 4;
            gunaNumeric1.Value = 0;
            gunaNumeric1.Maximum = page-1;
            label2.Text = $"Total Page : {(page).ToString()}";
            label1.Text = $"Total Result : {(_getbooks.data.Count).ToString()}";

            int count = 0;
            items = _getbooks.data;
            foreach (var _book in _getbooks.data)
            {
                string[] name_image = _book[0].Split('/');
                image[count].ImageLocation = _book[0];
                image[count].Visible = true;
                title_items[count].Text = _book[3];
                title_items[count].Visible = true;

                name_img.Add(name_image[4]);
                title_.Add(getNameFile(_book[3]));

                image[count].Click += pictureBox_Click;
                image[count].Cursor = Cursors.Hand;
                count++;
               

            }
        }


        private int currentPage = 0;
        private int itemsPerPage = 3;
        List<List<String>> items = new List<List<String>>();

        private void pictureBox_Click(object sender, EventArgs e)
        {
            image = new GunaPictureBox[] { gunaPictureBox2, gunaPictureBox3, gunaPictureBox4, gunaPictureBox5 };
            PictureBox pictureBox = (PictureBox)sender;
            int clickedImageIndex = Array.IndexOf(image, pictureBox);

            fetchItem.Data = name_img[clickedImageIndex];
            fetchItem.NamaFile = title_[clickedImageIndex];
            formkecil(new BookPreview());



        }

        private void display_item()
        {
            visible_item();
            image = new GunaPictureBox[] { gunaPictureBox2, gunaPictureBox3, gunaPictureBox4, gunaPictureBox5 };
            title_items = new RichTextBox[] { richTextBox2, richTextBox3, richTextBox4, richTextBox5 };
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, items.Count);
            List<List<string>> currentPageItems = items.GetRange(startIndex, endIndex - startIndex);


            int count = 0;
            foreach (var list in currentPageItems)
            {
                string[] name_image = list[0].Split('/');
                image[count].ImageLocation = list[0];
                image[count].Visible = true;
                title_items[count].Text = list[3];
                title_items[count].Visible = true;
                count++;
            }

        }
        private void gunaButton7_Click(object sender, EventArgs e)
        {
            int maxPage = (items.Count - 1) / itemsPerPage;
            if (currentPage < maxPage)
            {
                currentPage++;
                //gunaNumeric1.Value = currentPage;
                display_item();
            }
        }
        private void gunaButton8_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                //gunaNumeric1.Value = currentPage;
                display_item();
            }
        }
        private void gunaButton4_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(gunaNumeric1.Value);
            display_item();
        }
        public class getitem
        {

            public Status status { get; set; }

        }

        public class Status
        {
            public string bio { get; set; }
            public string books_upload { get; set; }
            public string machine_id { get; set; }
            public string name { get; set; }
            public string password { get; set; }
            public string profile_image { get; set; }
            public string register_date { get; set; }
            public string total_downloads { get; set; }
            public string type { get; set; }
            public string username { get; set; }
            public string total_likes { get; set; } 
        }

        private void gunaButton11_Click(object sender, EventArgs e)
        {
            formkecil(new BulkDownload());
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            formkecil(new UploadSkripsi());
        }
    }
}
