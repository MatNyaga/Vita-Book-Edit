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
        Point CoverBackSize = new Point { X = 177, Y = 250 };
        Point SpineSize = new Point { X = 30, Y = 250 };
        String BookPath = null;
        BackgroundWorker worker = new BackgroundWorker();
        string EPUBfilename;
        int ImageCounter;
        int SizeLimit = 1200000;
        Boolean EditMode = true;
        Boolean NewCover = true;
        Boolean NewBack = true;
        Boolean NewSpine = true;
        Boolean SizeWarning = true;

        public AddBook(String path)
        {
            InitializeComponent();
            ModelType.SelectedIndex = 0;
            EditMode = false;
            BookPath = path;
            BookPathLabel.Text = BookPath + "\\";
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
        }

        public AddBook(book book)
        {
            InitializeComponent();
            BookPath = book.bookpath;
            BookPathLabel.Text = BookPath + "\\";
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            //Fill in Book Details
            BookTitleText.Text = book.title;
            SeriesNameText.Text = book.seriesName;
            SeriesOrdinalText.Text = book.seriesOrdinal;
            AuthorText.Text = book.authors[0].author;
            PublisherText.Text = book.publisher;
            DescriptionText.Text = book.description;
            WebsiteText.Text = book.links[0].link.target;
            WebInfoText.Text = book.links[0].link.weblinkdescription;
            AddNoteText.Text = book.links[0].note;

            //Load the Cover Image
            try
            {
                CoverpictureBox.Load(BookPath + "\\" + "cover.jpg");
                NewCover = false;
            }
            catch (Exception ex)
            {
                NewCover = false;
            }

            //Load the Back Image
            try
            {
                BackpictureBox.Load(BookPath + "\\" + "back.jpg");
                NewBack = false;
            }
            catch (Exception ex) 
            {
                NewBack = false;
            }

            //Load the Spine Image
            try
            {
                SpinepictureBox.Load(BookPath + "\\" + "spine.jpg");
                NewSpine = false;
            }
            catch (Exception ex) 
            { 
                NewSpine = false; 
            }

            selectEPUB.Enabled = false;
            button1.Enabled = true;
            ModelType.SelectedIndex = book.model;
            ModelType.Enabled = false;
            EditMode = true;

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
            copyfile(EPUBfilename, BookPath + "\\" + "book.epub");
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
            if (!EditMode)
            {
                SpinepictureBox.Image = Properties.Resources.spine;
                BackpictureBox.Image = Properties.Resources.back;
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Save the Metadata File
            string str = File.ReadAllText("metadata.xml");
            str = str.Replace("id_model", ModelType.SelectedIndex.ToString());
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
            str = str.Replace("id_name_link", WebInfoText.Text);
            str = str.Replace("id_note_link", AddNoteText.Text);
            //Save to file
            File.WriteAllText(BookPath + "\\" + "metadata.xml", str);
            if (EditMode)
            {
                if (NewCover)
                    CoverpictureBox.Image.Save(BookPath + "\\" + "cover.jpg", ImageFormat.Jpeg);
                if (NewBack)
                    BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
                if (NewSpine)
                    SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);
            }
            else
            {
                if (CoverpictureBox.Image != null)
                    CoverpictureBox.Image.Save(BookPath + "\\" + "cover.jpg", ImageFormat.Jpeg);
                if (BackpictureBox.Image != null)
                    BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
                if (SpinepictureBox.Image != null)
                    SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);
            }
            if (EditMode)
                MessageBox.Show("Book has been updated");
            else
                MessageBox.Show("Book Added to Library");
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
                img = resizeImage(img, new Size(CoverBackSize));
                CoverpictureBox.Image = img;
                filename = null;
                NewCover = true;
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
                img = resizeImage(img, new Size(CoverBackSize));
                BackpictureBox.Image = img;
                filename = null;
                NewBack = true;
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
                img = resizeImage(img, new Size(SpineSize));
                SpinepictureBox.Image = img;
                filename = null;
                NewSpine = true;
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
                        {
                            ImageCounter += 1;
                            if (SizeWarning && entry.Length > SizeLimit)
                            {
                                MessageBox.Show("EPUB file contains images greater than 1.20MB. Images should be under 1.20MB to achieve best Reader performance.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                SizeWarning = false;
                            }
                        }
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
                worker.RunWorkerAsync();
            }
        EndWithoutCopy:;  
        }

        //Book model type selection
        private void ModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModelType.SelectedIndex == 0)
            {
                CoverBackSize = new Point { X = 177, Y = 250 };
                SpineSize = new Point { X = 30, Y = 250 };
                CoverpictureBox.Size = new System.Drawing.Size(177, 250);
                BackpictureBox.Size = new System.Drawing.Size(177, 250);
                SpinepictureBox.Size = new System.Drawing.Size(30, 250);
            }
            else if (ModelType.SelectedIndex == 1)
            {
                CoverBackSize = new Point { X = 350, Y = 250 };
                SpineSize = new Point { X = 30, Y = 250 };
                CoverpictureBox.Size = new System.Drawing.Size(350, 250);
                BackpictureBox.Size = new System.Drawing.Size(350, 250);
                SpinepictureBox.Size = new System.Drawing.Size(30, 250);
            }
            if (EditMode) { }
            else
            {
                CoverpictureBox.Image = null;
                BackpictureBox.Image = null;
                SpinepictureBox.Image = null;
            }
        }
    }
}
