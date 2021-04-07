using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Linq;
using Vita_Book_Edit.Entities;
using System.Collections.Generic;

namespace Vita_Book_Edit
{
    public partial class AddBook : Form
    {
        Point CoverBackSize = new Point { X = 177, Y = 250 };
        Point SpineSize = new Point { X = 30, Y = 250 };
        string BookPath = null;
        readonly BackgroundWorker worker = new BackgroundWorker();
        string EPUBfilename;
        int ImageCounter;
        int SizeLimit = 1200000;
        bool EditMode = true;
        bool MultiEdit = false;
        bool NewCover = true;
        bool NewBack = true;
        bool NewSpine = true;
        bool SizeWarning = true;
        string tempPath = string.Empty;
        public Book bookCtrl = null;
        public List<Book> booksToSave = new List<Book>();
        public AddBook(string path)
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
            worker.RunWorkerCompleted += OnWorkerCompleted;
            button1.Enabled = true;
            bookCtrl = new Book { 
                Authors = new List<Authors> { 
                    new Authors() 
                }, 
                Links = new List<Links> { 
                    new Links { 
                        Link = new Link() 
                    } 
                } 
            };
        }
        public AddBook(Book book)
        {
            InitializeComponent();
            bookCtrl = book;
            BookPath = book.BookPath;
            BookPathLabel.Text = BookPath + "\\";
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            //Fill in Book Details
            BookTitleText.Text = book.Title;
            SeriesNameText.Text = book.SeriesName;
            SeriesOrdinalText.Text = book.SeriesOrdinal;
            AuthorText.Text = book.Authors[0].Author;
            PublisherText.Text = book.Publisher;
            DescriptionText.Text = book.Description;
            WebsiteText.Text = book.Links[0].Link == null ? string.Empty : book.Links[0].Link.Target;
            WebInfoText.Text = book.Links[0].Link == null ? string.Empty : book.Links[0].Link.WebLinkDescription;
            AddNoteText.Text = string.IsNullOrEmpty(book.Links[0].Note) ? string.Empty : book.Links[0].Note;
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
            ModelType.SelectedIndex = book.Model;
            ModelType.Enabled = false;
            EditMode = true;
            checkBox1.Checked = !EditMode;
            checkBox1.Enabled = !EditMode;
        }

        public AddBook(List<Book> selectedBooks)
        {
            InitializeComponent();
            booksToSave = selectedBooks;
            this.Width = 397;
            BookTitleText.Enabled = false;
            SeriesOrdinalText.Enabled = false;
            selectEPUB.Enabled = false;
            button1.Enabled = true;
            MultiEdit = true;
            checkBox1.Checked = !MultiEdit;
            checkBox1.Enabled = !MultiEdit;
        }

        void copyfile(string src, string dest)
        {
            FileStream fsOut = new FileStream(dest, FileMode.Create);
            FileStream fsIn = new FileStream(src, FileMode.Open);
            byte[] bt = new byte[1048756];
            int readybyte;
            while ((readybyte = fsIn.Read(bt, 0, bt.Length)) > 0)
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
            }
        }
        private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {            
            worker.Dispose();
            if (checkBox1.Checked)
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                    EPUBfilename, 
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, 
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                );
            MessageBox.Show("Book Added to Library", "Book saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        private void AddBook_Load(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                SpinepictureBox.Image = Properties.Resources.spine;
                BackpictureBox.Image = Properties.Resources.back;
                OpenSelectBookDialog();
            }
        }
        private string SetNewValues()
        {
            bookCtrl.Model = ModelType.SelectedIndex == -1 ? bookCtrl.Model : ModelType.SelectedIndex;
            bookCtrl.Title = string.IsNullOrEmpty(BookTitleText.Text) ? bookCtrl.Title : BookTitleText.Text;
            bookCtrl.SeriesName = string.IsNullOrEmpty(SeriesNameText.Text) ? bookCtrl.SeriesName : SeriesNameText.Text;
            bookCtrl.SeriesOrdinal = string.IsNullOrEmpty(SeriesOrdinalText.Text) ? bookCtrl.SeriesOrdinal : SeriesOrdinalText.Text;
            bookCtrl.Authors[0].Author = string.IsNullOrEmpty(AuthorText.Text) ? bookCtrl.Authors[0].Author : AuthorText.Text;
            bookCtrl.Publisher = string.IsNullOrEmpty(PublisherText.Text) ? bookCtrl.Publisher : PublisherText.Text;
            bookCtrl.Description = string.IsNullOrEmpty(DescriptionText.Text) ? bookCtrl.Description : DescriptionText.Text;
            bookCtrl.Links[0].Link.Target = string.IsNullOrEmpty(WebsiteText.Text) ? bookCtrl.Links[0].Link.Target : WebsiteText.Text;
            bookCtrl.Links[0].Link.WebLinkDescription = string.IsNullOrEmpty(WebInfoText.Text) ? bookCtrl.Links[0].Link.WebLinkDescription : WebInfoText.Text;
            bookCtrl.Links[0].Note = string.IsNullOrEmpty(AddNoteText.Text) ? bookCtrl.Links[0].Note : AddNoteText.Text;

            string str = File.ReadAllText("metadata.xml");
            str = str.Replace("id_model", bookCtrl.Model.ToString());
            str = str.Replace("id_title", bookCtrl.Title);
            str = str.Replace("id_series_sorting", bookCtrl.SeriesName);
            str = str.Replace("id_series", bookCtrl.SeriesName);
            str = str.Replace("id_ordinal", bookCtrl.SeriesOrdinal);
            str = str.Replace("id_author_sorting", bookCtrl.Authors[0].Author);
            str = str.Replace("id_author", bookCtrl.Authors[0].Author);
            str = str.Replace("id_publisher_sorting", bookCtrl.Publisher);
            str = str.Replace("id_publisher", bookCtrl.Publisher);
            str = str.Replace("id_description", bookCtrl.Description);
            str = str.Replace("id_link", bookCtrl.Links[0].Link.Target);
            str = str.Replace("id_name_link", bookCtrl.Links[0].Link.WebLinkDescription);
            str = str.Replace("id_note_link", bookCtrl.Links[0].Note);

            return str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            //booksToSave = new List<Book>();
            if (!MultiEdit) booksToSave.Add(bookCtrl);

            booksToSave.ForEach(b =>
            {
                bookCtrl = b;
                BookPath = bookCtrl.BookPath;
                Directory.CreateDirectory(BookPath);
                //Save to file
                File.WriteAllText(BookPath + "\\" + "metadata.xml", SetNewValues());
                if (!MultiEdit)
                {
                    if (EditMode)
                    {
                        if (NewCover)
                            CoverpictureBox.Image.Save(BookPath + "\\" + "cover.jpg", ImageFormat.Jpeg);
                        if (NewBack)
                            BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
                        if (NewSpine)
                            SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);
                        MessageBox.Show("Book has been updated", "Book saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        if (CoverpictureBox.Image != null)
                            CoverpictureBox.Image.Save(BookPath + "\\" + "cover.jpg", ImageFormat.Jpeg);
                        if (BackpictureBox.Image != null)
                            BackpictureBox.Image.Save(BookPath + "\\" + "back.jpg", ImageFormat.Jpeg);
                        if (SpinepictureBox.Image != null)
                            SpinepictureBox.Image.Save(BookPath + "\\" + "spine.jpg", ImageFormat.Jpeg);
                        worker.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Books have been updated", "Books saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            });
            
        }
        private void SetImageCover(string fileName)
        {
            Image img = Image.FromFile(fileName);
            img = resizeImage(img, new Size(CoverBackSize));
            CoverpictureBox.Image = img;
        }
        private void CoverpictureBox_Click(object sender, EventArgs e)
        {
            string filename;
            try
            {
                openFileDialog1.Filter = " JPEG files| *.jpg| PNG files | *.png| GIF Files | *.gif| TIFF Files | *.tif| BMP Files | *.bmp";
                openFileDialog1.ShowDialog();
                filename = openFileDialog1.FileName;
                SetImageCover(filename);
                filename = null;
                NewCover = true;
            }
            catch (Exception ex) { }
            openFileDialog1.Reset();
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
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
        private void ExtractTempCover(ZipArchive file)
        {
            tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            ZipArchiveEntry firstFile = file.Entries
                .Where(f => f.FullName.Contains("Images"))
                .OrderBy(f => f.Name).FirstOrDefault();
            string firstImage = Path.Combine(tempPath, firstFile.Name);
            firstFile.ExtractToFile(firstImage);
            SetImageCover(firstImage);
        }
        private void selectEPUB_Click(object sender, EventArgs e)
        {
            OpenSelectBookDialog();
        }
        private void OpenSelectBookDialog()
        {
        EPUBincompatible:
            ImageCounter = 0;
            openFileDialog1.Filter = " EPUB files| *.epub";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //EPUB compatibility check
                EPUBfilename = openFileDialog1.FileName;
                BookTitleText.Text = Path.GetFileNameWithoutExtension(EPUBfilename);
                using (ZipArchive archive = ZipFile.Open(EPUBfilename, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                        {
                            if (ImageCounter == 0)
                            {
                                ExtractTempCover(archive);
                            }
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
