using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading;
using static Guna.UI2.Native.WinApi;

namespace ZekLIB
{
    public partial class Form1 : Form
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

        public static Form1 instance;
        public Panel panelchild;

        private LoginInformation dataInstance;
        private static DownloadList downloadlistform;
        private FetchItem fetchItem;

        private bool isUpdate;

        private string url_temp = "";
        public Form1()
        {

            InitializeComponent();
            check_folder();
            gunaTextBox1.Text = "Search by Title";
            instance = this;
            panelchild = PanelChildForm;
            notifyIcon1.Text = "My App";
            dataInstance = LoginInformation.GetInstance();
            fetchItem = FetchItem.GetInstance();
            
            _ = get_url();
            FetchCpuIdInternal();
           
            
            if (isUpdate)
            {
                MessageBox.Show("The update has been released, please check the following website: zekkelar.github.io");
            }

        }

    

        private async Task get_url()
        {

            string text;
            var fileStream = new FileStream(@"config.json", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            Geturl myData = JsonConvert.DeserializeObject<Geturl>(text);
            url_temp = myData.url.ToString();
            fetchItem.Myurl = myData.url;
            if (myData.update == "yes")
            {
                isUpdate = true;
            }
            else
            {
                isUpdate = false;
            }
            check_registered(FetchCpuIdInternal());




        }

        private async Task check_registered(string machine_id)
        {
            
            string apiEndpoint = $"{fetchItem.Myurl}/account";
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("machine_login"), "id");
                formData.Add(new StringContent(machine_id), "machine_id");

                using (var response = await httpClient.PostAsync(apiEndpoint, formData))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            dataInstance.IsLoggin = true;
                            Profile.getitem getitem = JsonConvert.DeserializeObject<Profile.getitem>(responseContent);
                            dataInstance.Username = getitem.status.username;
                        }
                        else
                        {
                            dataInstance.IsLoggin = false;
                        }
                    }
                }
            }
        }

        private String[] folder;
        
        private void check_folder()
        {
            folder = new string[] { "BulkDownloads", "Downloads", "BookmarkDownloads" };
            foreach(var folders in folder)
            {
                if (Directory.Exists(folders))
                {
                    
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(folders);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            
        }
        private string FetchCpuIdInternal()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                
            }
            string drive = "C";
            ManagementObject dsk = new ManagementObject(
                @"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            string uniqueId = cpuInfo + volumeSerial;
            dataInstance.MachineID = uniqueId;
            return uniqueId;
            
           
        }

        private static readonly HttpClient httpClient = new HttpClient();




        private Form activeForm = null;
        private void formkecil(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelChildForm.Controls.Add(childForm);
            PanelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formkecil(new Home());
            downloadlistform = new DownloadList();
            downloadlistform.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            formkecil(new Home());
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            if (dataInstance.IsLoggin)
            {
                formkecil(new Profile());
            }
            else
            {
                formkecil(new Login());
            }
            
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (downloadlistform == null || downloadlistform.IsDisposed)
            {
                downloadlistform = new DownloadList();
                downloadlistform.Show();
            }
            else
            {
                downloadlistform.Show();
                downloadlistform.WindowState= FormWindowState.Normal;
                downloadlistform.ShowInTaskbar = true;
                DownloadList.instance.notifyIcon1.Visible = false;
                
            }
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            fetchItem.Query = gunaTextBox1.Text;
            formkecil(new ExternalJournal());

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            fetchItem.Query = gunaTextBox1.Text;
            formkecil(new MenuForm());
        }

    

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void gunaTextBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaTextBox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void gunaTextBox1_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void gunaTextBox1_Enter(object sender, EventArgs e)
        {
            // Hapus hint atau placeholder saat TextBox mendapatkan fokus
            if (gunaTextBox1.Text == "Search by Title")
            {
                gunaTextBox1.Text = "";
            }
        }

        private void gunaTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gunaTextBox1.Text))
            {
                gunaTextBox1.Text = "Search by Title";
            }
        }

        private void PanelChildForm_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
    public class Geturl
    {
        public string url { get; set; }
        public string update { get; set; }
    }

}
