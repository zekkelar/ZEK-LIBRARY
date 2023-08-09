using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Newtonsoft.Json;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ZekLIB
{
    public partial class MenuForm : Form
    {
        HttpClient requests = new HttpClient();

        public static MenuForm instance;

        private FetchItem dataInstance;

        public MenuForm()
        {
            InitializeComponent();
            setting_up();
       
            currentPage = FetchItem.GetInstance().LastPageViewed;
            
         
            instance = this;
            dataInstance = FetchItem.GetInstance();
            getData();

        }

        private void centerRich()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();

            richTextBox2.SelectAll();
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.DeselectAll();

            richTextBox3.SelectAll();
            richTextBox3.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox3.DeselectAll();

            richTextBox4.SelectAll();
            richTextBox4.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox4.DeselectAll();

            richTextBox5.SelectAll();
            richTextBox5.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox5.DeselectAll();

            richTextBox6.SelectAll();
            richTextBox6.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox6.DeselectAll();
        }

        private async Task getData()
        {
            await fetchJson();
        }

       


        string name_img_1 = "";
        string name_img_2 = "";
        string name_img_3 = "";

        private RichTextBox[] richboxTitle;
        private GunaPictureBox[] image;
        private RichTextBox[] richBoxAuthor;

        private String[] name_img_;

        private List<String> img_urls = new List<String>();
        private List<String> titles = new List<String>();
        private void setting_up()
        {
            image = new GunaPictureBox[] { gunaPictureBox1, gunaPictureBox2, gunaPictureBox3 };
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richBoxAuthor = new RichTextBox[] { richTextBox4, richTextBox5, richTextBox6 };
            int count = 0;
            foreach(var images in image)
            {
                image[count].Visible = false;
                richBoxAuthor[count].Visible = false;
                richboxTitle[count].Visible = false;
                count++;
            }
        }


        private async Task fetchJson()
        {

            image = new GunaPictureBox[] { gunaPictureBox1, gunaPictureBox2, gunaPictureBox3};
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richBoxAuthor = new RichTextBox[] {richTextBox4, richTextBox5, richTextBox6 };
            name_img_ = new string[] {name_img_1, name_img_2, name_img_3};

            string uri = $"{dataInstance.Myurl}search?id=searchInternal&query={dataInstance.Query}";
            string response = await requests.GetStringAsync(uri);
            getBooks _getbooks = JsonConvert.DeserializeObject<getBooks>(response);
            TotalResult.Text = _getbooks.data.Count.ToString();
            int page = ((_getbooks.data.Count + 3) - 1) / 3;
            totalPage.Text = $"Total Page : {(page).ToString()}";
            items = _getbooks.data;
            gunaNumeric1.Value = 0;
            gunaNumeric1.Maximum = page-1;
           
            int count = 0;
            foreach (var _book in _getbooks.data)
            {
                string[] name_image = _book[0].Split('/');
                image[count].ImageLocation = _book[0];
                richboxTitle[count].Text = _book[3];
                name_img_[count] = name_image[4];
                img_urls.Add(name_image[4]);
                titles.Add(_book[3]);
                richBoxAuthor[count].Text = _book[2];

                image[count].Visible = true;
                richboxTitle[count].Visible = true;
                richBoxAuthor[count].Visible = true;

                image[count].Click += GunaPictureBox_Click;
                image[count].Cursor = Cursors.Hand;
                centerRich();
                count++;

            }
            centerRich();

        }



        private void MenuForm_Load(object sender, EventArgs e)
        {

            
        }


        private int currentPage = 0;
        private int itemsPerPage = 3;
        List<List<String>> items = new List<List<String>>();


        private void display_item()
        {
            setting_up();
            img_urls.Clear();
            titles.Clear();
            image = new GunaPictureBox[] { gunaPictureBox1, gunaPictureBox2, gunaPictureBox3 };
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            richBoxAuthor = new RichTextBox[] { richTextBox4, richTextBox5, richTextBox6 };
            name_img_ = new string[] { name_img_1, name_img_2, name_img_3 };

            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, items.Count);
            List<List<string>> currentPageItems = items.GetRange(startIndex, endIndex - startIndex);

            
            int count = 0;
            foreach (var list in currentPageItems)
            {
                string[] name_image = list[0].Split('/');
                image[count].ImageLocation = list[0];
                richboxTitle[count].Text = list[3];
                name_img_[count] = name_image[4];
                richBoxAuthor[count].Text = list[2];
                img_urls.Add(name_image[4]);
                titles.Add(list[3]);

                image[count].Visible = true;
                richboxTitle[count].Visible = true;
                richBoxAuthor[count].Visible = true;

                image[count].Click += GunaPictureBox_Click;
                image[count].Cursor = Cursors.Hand;
                centerRich();
                count++;
            }
            centerRich();

        }

        private void GunaPictureBox_Click(object sender, EventArgs e)
        {
            name_img_ = new string[] { name_img_1, name_img_2, name_img_3 };
            richboxTitle = new RichTextBox[] { richTextBox1, richTextBox2, richTextBox3 };
            image = new GunaPictureBox[] { gunaPictureBox1, gunaPictureBox2, gunaPictureBox3 };
            PictureBox pictureBox = (PictureBox)sender;
            int clickedImageIndex = Array.IndexOf(image, pictureBox);
            dataInstance.Data = img_urls[clickedImageIndex];
            dataInstance.NamaFile = getNameFile(titles[clickedImageIndex]);
            formkecil(new BookPreview());
        }

        //this next button
        private void button8_Click(object sender, EventArgs e)
        {
            int maxPage = (items.Count - 1) / itemsPerPage;
            if (currentPage < maxPage)
            {
                currentPage++;
                gunaNumeric1.Value = currentPage;
                display_item();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                gunaNumeric1.Value = currentPage;
                display_item();
            }
        }

        private void showItem1_Click(object sender, EventArgs e)
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
            Form1.instance.panelchild.Controls.Add(childForm);
            Form1.instance.panelchild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

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
        private void showItem1_Click_1(object sender, EventArgs e)
        {
            dataInstance.Data = name_img_1;
            dataInstance.NamaFile = getNameFile(richTextBox1.Text);
            formkecil(new BookPreview());
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            dataInstance.Data = name_img_2;
            dataInstance.NamaFile = getNameFile(richTextBox2.Text);
            formkecil(new BookPreview());
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            dataInstance.Data = name_img_3;
            dataInstance.NamaFile = getNameFile(richTextBox3.Text);
            formkecil(new BookPreview());
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            Form formExternal = new ExternalJournal();
            formExternal.Show();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {

        }

        //search by page
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(gunaNumeric1.Value);
            display_item();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    class getBooks
    {
        public List<List<string>> data { get; set; }
    }
}
