using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mp3Tomp4
{
    
    public partial class mainOne : Form
    {
        int mov, movX, movY;

        string videoPath, videoName, musicPath, musicName;
        public mainOne()
        {

            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to QUIT?", "Leaving App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Multiselect=false,Filter="MP4 File|*.mp4" };
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                videoPath = ofd.FileName;
                videoName = ofd.SafeFileName;
            }
            txtPathVideo.Text = videoPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                musicPath = fbd.SelectedPath;
                musicName = videoName.Substring(0, videoName.Length-4);
                musicPath += ("\\" + musicName + ".mp3");
            }
            txtSaveTo.Text = musicPath;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (!txtPathVideo.Text.Trim().Equals("") || txtSaveTo.Text.Trim().Equals(""))
            {


                var convert = new NReco.VideoConverter.FFMpegConverter();
                try
                {
                    convert.ConvertMedia(txtPathVideo.Text.Trim(), txtSaveTo.Text.Trim(), "mp3");
                    MessageBox.Show("Converted successfully!");
                }
                catch (NReco.VideoConverter.FFMpegException ee) { }
                
            }
            else
            {
                MessageBox.Show("Empty Inputs");
            }
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void txtPathVideo_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SaveButton.BackColor = Color.Silver;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }else if (WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
       
    }
}
