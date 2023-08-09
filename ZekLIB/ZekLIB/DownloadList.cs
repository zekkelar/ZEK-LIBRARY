using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZekLIB
{
    public partial class DownloadList : Form
    {
        public static DownloadList instance;
        public static DownloadList instance_two;
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


        public static DownloadList Instance_two
        {
            get
            {
                if (instance_two == null || instance_two.IsDisposed)
                {
                    instance = new DownloadList();
                }
                return instance;
            }
        }

        public DownloadList()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized; // Set Form 3 ke minimized saat pertama kali dibuat
            this.ShowInTaskbar = false; // Sembunyikan Form 3 dari taskbar
            instance = this;
            labelStatus1.Visible = true;
            labelStatus2.Visible = true; labelStatus2.Visible = true; labelStatus3.Visible = true; labelStatus4.Visible = true; labelStatus5.Visible = true;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DownloadList_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DownloadList_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Download List";
            notifyIcon1.BalloonTipText = "Download List";
            notifyIcon1.Text = "Download List";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;

           
            
        }

        private void DownloadList_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;

            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }



        //download menggunakan async
   

        private void DownloadList_Move(object sender, EventArgs e)
        {
            
        }
    }
}
