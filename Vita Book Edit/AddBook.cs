using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
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
        int ImageCounter;
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
                progresstextlbl.Text = "0%";
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
            str = str.Replace("id_title", BookTitleText.Text);
            str = str.Replace("id_series_sorting", SeriesNameText.Text);
            str = str.Replace("id_series", SeriesNameText.Text);
            str = str.Replace("id_ordinal", SeriesOrdinalText.Text);
            str = str.Replace("id_author_sorting", AuthorText.Text);
            str = str.Replace("id_author", AuthorText.Text);
            str = str.Replace("id_publisher_sorting", PublisherText.Text);
            str = str.Replace("id_publisher", PublisherText.Text);
            str = str.Replace("id_description", DescriptionText.Text);
            str = str.Replace("id_link", WebsiteText.Text);
            str = str.Replace("id_link_name", WebInfoText.Text);
            str = str.Replace("id_link_note", AddNoteText.Text);
            //Save to file
            File.WriteAllText(BookPath+"\\"+"metadata.xml", str);
            CoverpictureBox.Image.Save(BookPath + "\\" +"cover.jpg", ImageFormat.Jpeg);
            BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
            SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);

            MessageBox.Show("Book Added");
            this.Close();
        }

        private void CoverpictureBox_Click(object sender, EventArgs e)
        {
            string filename;
            try
            {
                openFileDialog1.Filter = " JPEG files| *.jpg| PNG files | *.png| GIF Files | *.gif| TIFF Files | *.tif| BMP Files | *.bmp";
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
                openFileDialog1.Filter = " JPEG files| *.jpg| PNG files | *.png| GIF Files | *.gif| TIFF Files | *.tif| BMP Files | *.bmp";
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
                openFileDialog1.Filter = " JPEG files| *.jpg| PNG files | *.png| GIF Files | *.gif| TIFF Files | *.tif| BMP Files | *.bmp";
                openFileDialog1.ShowDialog();
                filename = openFileDialog1.FileName;
                Image img = Image.FromFile(filename);
                img = resizeImage(img, new Size(30, 300));
                SpinepictureBox.Image = img;
                filename = null;
            }
            catch (Exception ex) { }
            openFileDialog1.Reset();
        }

        private void selectEPUB_Click(object sender, EventArgs e)
        {

        EPUBincompatible:
            ImageCounter = 0;
            openFileDialog1.Filter = " EPUB files| *.epub";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //EPUB compatibility check
                EPUBfilename = openFileDialog1.FileName;
                using (ZipArchive archive = ZipFile.Open(EPUBfilename, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                        ImageCounter += 1;
                    }
                }

                if (ImageCounter > 2)
                {
                    MessageBox.Show(EPUBfilename);
                    ImageCounter = 0;
                }
                else if (ImageCounter <= 2)
                {
                    DialogResult SelectedOption = MessageBox.Show("EPUB file is either incompatible with the Reader or contains less than 2 pages.", "Warning", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
                    if (SelectedOption == DialogResult.Retry)
                        goto EPUBincompatible;
                    else if (SelectedOption == DialogResult.Abort)
                        goto EndWithoutCopy;
                }
                else if (ImageCounter == 0)
                {
                    DialogResult SelectedOption = MessageBox.Show("EPUB file is incompatible with the Reader.", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    if (SelectedOption == DialogResult.Retry)
                        goto EPUBincompatible;
                    else
                        goto EndWithoutCopy;
                }
            }
            worker.RunWorkerAsync();
        EndWithoutCopy:;  
        }
    }
}
