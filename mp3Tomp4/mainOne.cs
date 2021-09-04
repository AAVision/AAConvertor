using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace mp3Tomp4
{
    
    public partial class mainOne : Form
    {
        int mov, movX, movY;
        Thread mainThread;
        string videoPath, videoName, musicPath, musicName;
        public mainOne()
        {

            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //QUIT Question
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
            //SELECT MP4 file.
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
            //CHOOSE the path of the saved MP3
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                musicPath = fbd.SelectedPath;
                musicName = videoName.Substring(0, videoName.Length-4);
                musicPath += ("\\" + musicName + ".mp3");
            }
            txtSaveTo.Text = musicPath;
        }

        

        private void process()
        {
            //Save button check if 2 inputs are not empty
            if (!txtPathVideo.Text.Trim().Equals("") || txtSaveTo.Text.Trim().Equals(""))
            {

                //Use FFMpeg to convert from MP4 to MP3
                var convert = new NReco.VideoConverter.FFMpegConverter();
                try
                {
                    convert.ConvertMedia(txtPathVideo.Text.Trim(), txtSaveTo.Text.Trim(), "mp3");
                    MessageBox.Show("Converted successfully!");
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        SaveButton.Enabled = true;
                    }));
                    
                }
                catch (NReco.VideoConverter.FFMpegException ee)
                {
                    MessageBox.Show("Error in processing!");
                }

            }
            else
            {
                MessageBox.Show("Empty Inputs");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //call thread
            mainThread = new Thread(process);
            mainThread.Start();
            SaveButton.Enabled = false;

        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void txtPathVideo_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SaveButton.BackColor = Color.Silver;
        }

        //GUI movement and position
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }
        //Minimize
        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Minimize and Maximize button
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
        //GUI movement and position
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        //GUI movement and position
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
       
    }
}
