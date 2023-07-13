using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace hmacUI
{
    public partial class Main : Form
    {

        //https://www.codeproject.com/Articles/11114/Move-window-form-without-Titlebar-in-C
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            drag(e);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag(e);
        }
        private void drag( MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }



        public Main()
        {
            InitializeComponent();
        }





        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Data is empty");
            }
            else
            {
                string s = HMAC.ComputeHMAC(comboBox1.Text, textBox1.Text, textBox2.Text);

                Result r  = new Result(s);
                r.Show();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
