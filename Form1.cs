using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMIP
{
    public partial class Form1 : Form , IDisposable
    {

        ImageInfo imgInfo;
        Path p=new Path();
        Connection conn;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            conn = new Connection("DESKTOP-4E401T4", "Images");
            dt = conn.SelectQuery("ImageBlobs", null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadImagesFromDB();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {

                InitialDirectory = @"E:\ProiectMIP\",
                Title = "Browse Pictures",

                CheckFileExists = true,
                CheckPathExists = true,

                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Load(openFileDialog1.FileName);
                //imgInfo = new ImageInfo(openFileDialog1.SafeFileName, openFileDialog1.GetType().Name);
                List<string> fields = new List<string>();
                fields.Add("ImageName");
                fields.Add("Blob");
                conn.InsertBlobQuery("ImageBlobs", fields,openFileDialog1.FileName);
                LoadImagesFromDB();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = picturesListBox.SelectedIndex;
            string imgName = picturesListBox.Items[index].ToString();

            List<string> filters = new List<string>();
            filters.Add("ImageName='" + imgName + "'");
            conn.DeleteQuery("ImageBlobs", filters);
            LoadImagesFromDB();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    Title = "Browse Pictures",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    //DefaultExt = "txt",
                    //Filter = "txt files (*.txt)|*.txt",
                    DefaultExt = "jpg",
                    Filter = "JPG File (*.jpg)|*.jpg",
                    //DefaultExt = "png",
                    //Filter = "PNG File (*.png)|*.png",
                    FilterIndex = 2,
                    RestoreDirectory = false,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //pictureBox1.Load(openFileDialog1.FileName);
                    imgInfo = new ImageInfo(openFileDialog1.SafeFileName);
                

                    //var selItems = from ListItem li in picturesListBox.Items
                    //               where li.Select == true
                    //               select li.Text;

                    List<string> listItems = new List<string>();
                    for (int i = 0; i < picturesListBox.Items.Count; i++)
                        listItems.Add(picturesListBox.Items[i].ToString());

                    string fileName = openFileDialog1.FileName;

                    string selectedItemName = fileName.Substring(fileName.LastIndexOf(@"\") + 1, fileName.LastIndexOf(@".") - fileName.LastIndexOf(@"\") - 1);

                    var foundItem = listItems.Where(item => item == fileName);

                    if (foundItem != null)
                        System.Windows.Forms.MessageBox.Show("There is already an image with the same name i the list! ");
                    else
                        picturesListBox.Items.Add(imgInfo.getName());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void picturesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = picturesListBox.SelectedIndex;
            string imgName = picturesListBox.Items[index].ToString();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["ImageName"].ToString() == imgName)
                    {
                        try
                        {
                            Byte[] byteBLOBData = new Byte[0];
                            byteBLOBData = (Byte[])(dt.Rows[i]["Blob"]);
                            MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                            pictureBox1.Image = Image.FromStream(stmBLOBData);
                        }
                        catch(Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Failed to load image into picture box! " + ex.Message);
                        }
                    }
                }
            }
        }

        private void LoadImagesFromDB()
        {
            dt = conn.SelectQuery("ImageBlobs", null, null);

            if (picturesListBox.Items.Count > 0)
                picturesListBox.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    picturesListBox.Items.Add(dt.Rows[i]["ImageName"]);
                }

                picturesListBox.SelectedIndex = 0;
                picturesListBox_SelectedIndexChanged(null, null);
            }

        }
    }
}
