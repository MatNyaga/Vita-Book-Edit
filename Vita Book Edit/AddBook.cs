using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Vita_Book_Edit
{
    public partial class AddBook : Form
    {
        String BookPath = null;
        BackgroundWorker worker = new BackgroundWorker();
        string EPUBfilename;
        public AddBook(String path)
        {
            InitializeComponent();
            BookPath = path;
            BookPathLabel.Text = BookPath + "\\";
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
        }

        void copyfile(string src, string dest)
        {
            FileStream fsOut = new FileStream(dest, FileMode.Create);
            FileStream fsIn = new FileStream(src, FileMode.Open);
            byte[] bt = new byte[1048756];
            int readybyte;
            while((readybyte = fsIn.Read(bt,0,bt.Length)) > 0)
            {
                fsOut.Write(bt, 0, readybyte);
                worker.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length));
            }
            fsIn.Close();
            fsOut.Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            copyfile(EPUBfilename, BookPath+"\\"+"book.epub");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progresstextlbl.Text = progressBar1.Value.ToString() + "%";

            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                button1.Enabled = true;
            }

        }

        private void AddBook_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //Save the Metadata File
            string str = File.ReadAllText("metadata.xml");
            str = str.Replace("SERIES NAME FOR SORTING", BookTitleText.Text);
            str = str.Replace("SERIES NAME", BookTitleText.Text);
            str = str.Replace("AUTHOR FOR SORTING", AuthorText.Text);
            str = str.Replace("AUTHOR", AuthorText.Text);
            str = str.Replace("PUBLISHER FOR SORTING", PublisherText.Text);
            str = str.Replace("PUBLISHER", PublisherText.Text);
            str = str.Replace("DESCRIPTION HERE", DescriptionText.Text);
            str = str.Replace("https://www.google.com/", WebsiteText.Text);
            str = str.Replace("MORE INFO LINK NAME", WebInfoText.Text);
            str = str.Replace("NOTE FOR LINKS", AddNoteText.Text);
            //Save to file
            File.WriteAllText(BookPath+"\\"+"metadata.xml", str);
            CoverpictureBox.Image.Save(BookPath + "\\" +"cover.jpg", ImageFormat.Jpeg);
            BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
            SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);

            MessageBox.Show("Book Added");
        }

        private void CoverpictureBox_Click(object sender, EventArgs e)
        {
            string filename;
            try
            {
                openFileDialog1.Filter = " JPEG files| *.jpg | PNG files | *.png | GIF Files | *.gif | TIFF Files | *.tif | BMP Files | *.bmp";
                openFileDialog1.ShowDialog();
                filename = openFileDialog1.FileName;
                Image img = Image.FromFile(filename);
                img = resizeImage(img, new Size(177, 250));
                CoverpictureBox.Image = img;
                filename = null;
            }
            catch (Exception ex) { }
            openFileDialog1.Reset();
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void BackpictureBox_Click(object sender, EventArgs e)
        {
            string filename;
            try
            {
                openFileDialog1.Filter = " JPEG files| *.jpg | PNG files | *.png | GIF Files | *.gif | TIFF Files | *.tif | BMP Files | *.bmp";
                openFileDialog1.ShowDialog();
                filename = openFileDialog1.FileName;
                Image img = Image.FromFile(filename);
                img = resizeImage(img, new Size(177, 250));
                BackpictureBox.Image = img;
                filename = null;
            }
            catch (Exception ex) { }
            openFileDialog1.Reset();
        }

        private void SpinepictureBox_Click(object sender, EventArgs e)
        {
            string filename;
            try
            {
                openFileDialog1.Filter = " JPEG files| *.jpg | PNG files | *.png | GIF Files | *.gif | TIFF Files | *.tif | BMP Files | *.bmp";
                openFileDialog1.ShowDialog();
                filename = openFileDialog1.FileName;
                Image img = Image.FromFile(filename);
                img = resizeImage(img, new Size(177, 250));
                SpinepictureBox.Image = img;
                filename = null;
            }
            catch (Exception ex) { }
            openFileDialog1.Reset();
        }

        private void selectEPUB_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = " EPUB files| *.epub";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                EPUBfilename = openFileDialog1.FileName;
                worker.RunWorkerAsync();
            }
        }
    }
}
