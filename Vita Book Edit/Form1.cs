using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vita_Book_Edit.Entities;
namespace Vita_Book_Edit
{
    public partial class Form1 : Form
    {
        string LibraryLocation = null;
        string baseval1 = "book";
        string baseval2 = "u";
        string ReaderTitle = "PCSC80012";
        string DLCBookTitleBase = "00000000BLKSTOK";
        string PatchFolder = "reAddcont";
        int DLCIndex = 1;
        Serializer ser = new Serializer();
        string xmlInputData = string.Empty;
        List<Book> allbooks = new List<Book>();
        int DeleteID;
        List<DLC> DLCs = new List<DLC>();
        //Universal library format function
        private void libcheck(bool showLast = false)
        {
            DLCs = new List<DLC>();
            try
            {
                if (!Directory.Exists(Path.Combine(LibraryLocation, PatchFolder, ReaderTitle, DLCBookTitleBase + DLCIndex)))
                    Directory.CreateDirectory(Path.Combine(LibraryLocation, PatchFolder, ReaderTitle, DLCBookTitleBase + DLCIndex));
                if (!Directory.Exists(Path.Combine(LibraryLocation, baseval1, baseval2, DLCBookTitleBase + DLCIndex)))
                    Directory.CreateDirectory(Path.Combine(LibraryLocation, baseval1, baseval2, DLCBookTitleBase + DLCIndex));
            }
            catch (Exception ex)
            {
                File.WriteAllText("settings.ini", String.Empty);
                MessageBox.Show("Saved ux0:/ location not found! Please select it again.");
                goto ErrorNoPath;
            }
            librarypathlabel.Text = LibraryLocation;
            //Get all DLLs
            DLCs = Directory.GetDirectories(Path.Combine(LibraryLocation, baseval1, baseval2)).OrderBy(o => o)
                .Select(d => new DLC { 
                    RootPath = d, DLCName = Path.GetFileName(d), Index = Convert.ToInt32(Path.GetFileName(d).Replace(DLCBookTitleBase, string.Empty))
                }).ToList();
            DLCs.ForEach(b =>
            {
                string[] booksTmp = Directory.GetDirectories(b.RootPath).OrderBy(o => o).ToArray();
                b.Books = booksTmp.ToList()
                    .Select(bk => {
                        xmlInputData = File.Exists(Path.Combine(bk, "metadata.xml")) ? File.ReadAllText(Path.Combine(bk, "metadata.xml")) : string.Empty;
                        Book mybook = string.IsNullOrEmpty(xmlInputData) ? new Book() : ser.Deserialize<Book>(xmlInputData);
                        mybook.BookPath = bk;
                        mybook.DLCIndex = b.Index;
                        mybook.DLCPath = b.RootPath;
                        mybook.FilePath = bk;
                        mybook.FileExists = File.Exists(Path.Combine(bk, "book.epub"));
                        mybook.Index = Convert.ToInt32(Path.GetFileName(bk).Replace(baseval1, string.Empty));
                        return mybook;
                    }).ToList();
                b.DLCName = string.Concat(b.DLCName, " (", b.Books.Where(f => f.FileExists).ToList().Count().ToString(), ") books");
                allbooks.AddRange(b.Books);
            });
            LibraryBookNumber.Text = "( " + (allbooks.Count().ToString()) + " Books )";
            cleanfolders.Enabled = true;
            multiModMeta.Enabled = true;
            AddBook.Enabled = true;
            library.Text = "Change ux0:/ Folder Location";
            if (DLCs.Any())
            {
                cmbDLL.DataSource = DLCs;
                cmbDLL.DisplayMember = "DLCName";
                cmbDLL.ValueMember = "Index";
                cmbDLL.SelectedIndex = showLast ? cmbDLL.Items.Count - 1 : 0;
            }            
        ErrorNoPath:;
        }
        private void ShowBooks(DLC dLC)
        {
            List<Control> bookCovers = new List<Control>();
            flpCategories.Controls.Clear();
            dLC.Books.Where(b => b.FileExists).ToList().ForEach(b =>
            {                
                Button btn = new Button();
                btn.Text = b.Title + " - " + b.Authors[0].Author;
                if (b.Model == 0)
                    btn.Size = new Size(177, 250);
                else if (b.Model == 1)
                    btn.Size = new Size(350, 250);
                btn.ForeColor = Color.BlueViolet;
                try
                {
                    using (FileStream stream = new FileStream(Path.Combine(b.FilePath, "cover.jpg"), FileMode.Open, FileAccess.Read))
                    {
                        btn.Image = Image.FromStream(stream);
                        stream.Dispose();
                        btn.Image = ResizeImage(btn.Image, btn.Size);
                    }
                }
                catch { }
                btn.Tag = b.Index;

                CheckBox chb = new CheckBox();
                chb.Parent = btn;
                chb.Size = new Size(15, 14);
                chb.Location = new Point(5, (btn.Height - chb.Height) - 3);
                chb.TextAlign = ContentAlignment.MiddleLeft;
                chb.TextAlign = ContentAlignment.MiddleRight;
                chb.Tag = b.Index;
                chb.CheckedChanged += chb_CheckedChanged;

                bookCovers.Add(btn);
                btn.Click += btn_Click;
                btn.MouseDown += new MouseEventHandler(this.btn_Click);
            });
            flpCategories.Controls.AddRange(bookCovers.ToArray());
            this.Controls.Add(flpCategories);
        }
        void chb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                DLCs.Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                    .FirstOrDefault().Books.Where(bk => bk.Index == (int)b.Tag)
                        .ToList().ForEach(bk =>
                        {
                            bk.Selected = b.Checked;
                        });
            }
            catch { }            
        }
        void btn_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button b = (Button)sender; 
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        {
                            DeleteID = (int)b.Tag;
                            DeleteMenuStrip.Show(this, e.Location);
                            DeleteMenuStrip.Show(Cursor.Position);//places the menu at the pointer position
                        }
                        break;
                }
            }
            catch { }
        }
        void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                MessageBox.Show(
                    DLCs.Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index).FirstOrDefault().Books.Where(bk => bk.Index == (int)b.Tag).FirstOrDefault().Title
                );
            }
            catch { }
        }
        private static Image ResizeImage(Image image, Size size)
        {
            Image img = new Bitmap(image, size);
            return img;
        }
        public Form1()
        {
            InitializeComponent();
            //Check if library path was saved and act accordingly
            if (File.ReadLines("settings.ini").ElementAtOrDefault(0) != null)
            {
                LibraryLocation = File.ReadLines("settings.ini").ElementAtOrDefault(0);
                libcheck();
            }
        }
        private void library_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            //Checks patch folder            
            if (result == DialogResult.OK)
            {
                DLCIndex = 1;
                flpCategories.Controls.Clear();
                allbooks.Clear();
                LibraryLocation = folderBrowserDialog1.SelectedPath;
                if (string.IsNullOrEmpty(File.ReadLines("settings.ini").ElementAtOrDefault(0)))
                    File.AppendAllText("settings.ini", LibraryLocation);
                if (Directory.Exists(Path.Combine(LibraryLocation, PatchFolder, ReaderTitle)))
                {
                    MessageBox.Show(this, "Please ensure you DELETE the contents in: " + Environment.NewLine +
                        Environment.NewLine +
                        Path.Combine(LibraryLocation, PatchFolder, ReaderTitle) + Environment.NewLine +
                        Environment.NewLine +
                        "or MOVE them to " + Environment.NewLine +
                        Environment.NewLine +
                        Path.Combine(LibraryLocation, baseval1, baseval2) + Environment.NewLine +
                        Environment.NewLine +
                        "for the Library to be recognized in the Vita", "PLEASE NOTE!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                libcheck();                
            }
        }
        private Book CheckNewBookEntry()
        {
            DLC dlcSaveTo = null;
            Book NewBook = null;
            try
            {
                string basePath = Path.Combine(LibraryLocation, baseval1, baseval2);
                if (DLCs.Any())
                {
                    Book[] emptyBooks = DLCs.SelectMany(o => 
                        o.Books.Where(b => !b.FileExists).Select(i => i)).ToArray();
                    if (emptyBooks.Any())
                        NewBook = emptyBooks.FirstOrDefault();
                    else
                    {
                        dlcSaveTo = DLCs.OrderByDescending(d => d.RootPath).FirstOrDefault();
                        int maxBookIndex = dlcSaveTo.Books.Any() ? dlcSaveTo.Books.Max(m => m.Index) : 0;
                        if (dlcSaveTo.Index == 7 && maxBookIndex == 7)
                        {
                            MessageBox.Show("Reader Book Limit has been reached!");
                            return null;
                        }
                        if (maxBookIndex == 99)
                        {
                            int newDLCIndex = dlcSaveTo.Index + 1;
                            dlcSaveTo = new DLC {
                                Index = newDLCIndex,
                                RootPath = Path.Combine(Path.GetDirectoryName(dlcSaveTo.RootPath), string.Concat(DLCBookTitleBase, newDLCIndex.ToString())),
                                Books = new List<Book>()
                            };
                            NewBook = new Book { Index = 1, DLCPath = dlcSaveTo.RootPath, DLCIndex = dlcSaveTo.Index };
                        } else
                        {
                            if (dlcSaveTo.Index == 1 && maxBookIndex == 0) maxBookIndex = 1;
                            NewBook = new Book { Index = maxBookIndex + 1, DLCPath = dlcSaveTo.RootPath, DLCIndex = dlcSaveTo.Index };
                        }
                    }
                }
                return NewBook;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private void AddBook_Click(object sender, EventArgs e)
        {
            if (librarypathlabel.Text == "\\")
            {
                MessageBox.Show("Please select the Vita root directory (ux0:).");
                return;
            }
            Book newBook = CheckNewBookEntry();
            if (newBook != null)
            {
                AddBook mybook = new AddBook(
                    Path.Combine(
                        LibraryLocation, baseval1, baseval2, DLCBookTitleBase + newBook.DLCIndex.ToString(), "book" + newBook.Index.ToString().PadLeft(2, '0')
                    )
                );
                mybook.ShowDialog();
                DLCIndex = 1;
                flpCategories.Controls.Clear();
                allbooks.Clear();
                LibraryLocation = folderBrowserDialog1.SelectedPath;
                if (File.ReadLines("settings.ini").ElementAtOrDefault(0) == null)
                    File.AppendAllText("settings.ini", LibraryLocation);
                libcheck(true);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private static void CleanDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                CleanDirectory(directory);
                if (Directory.GetFiles(directory,"book.epub").Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, true);
                }
            }
        }
        private void cleanfolders_Click(object sender, EventArgs e)
        {
            CleanDirectory(LibraryLocation +  baseval1 + "\\" + baseval2 + "\\"  + DLCBookTitleBase + DLCIndex);
            MessageBox.Show("Library Cleaned");
            DLCIndex = 1;
            flpCategories.Controls.Clear();
            allbooks.Clear();
            LibraryLocation = folderBrowserDialog1.SelectedPath;
            if (File.ReadLines("settings.ini").ElementAtOrDefault(0) == null)
                File.AppendAllText("settings.ini", LibraryLocation);
            libcheck();
        }
        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book selBook = DLCs
                .Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                .FirstOrDefault().Books
                    .Where(bk => bk.Index == DeleteID)
                    .FirstOrDefault();

            DialogResult result = MessageBox.Show(
                "Are you sure you want to Delete this book? " 
                    + selBook.Title, 
                "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(selBook.BookPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                flpCategories.Controls.Clear();
                allbooks.Clear();
                libcheck();
                MessageBox.Show("Book Deleted!");
            }
            else
            {
                DeleteID = 0;
                return;
            }            
            DeleteID = 0;
        }
        private void bookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book selBook = DLCs
                .Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                .FirstOrDefault().Books
                    .Where(bk => bk.Index == DeleteID)
                    .FirstOrDefault();
            MessageBox.Show("Book Path:"+Environment.NewLine +
                selBook.BookPath +Environment.NewLine + Environment.NewLine+
                "Author: "+ selBook.Authors[0].Author + Environment.NewLine+
                "Series Name: " + selBook.SeriesName + Environment.NewLine+
                "Volume No: " + selBook.SeriesOrdinal + Environment.NewLine+
                "Title: " + selBook.Title + Environment.NewLine+
                "Publisher: " + selBook.Publisher + Environment.NewLine+
                "Description: " + selBook.Description + Environment.NewLine);
        }
        private void editBookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book selBook = DLCs
                .Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                .FirstOrDefault().Books
                    .Where(bk => bk.Index == DeleteID)
                    .FirstOrDefault();
            AddBook mybook = new AddBook(selBook);
            mybook.ShowDialog();
            selBook = mybook.bookCtrl;
            DLCs.Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                .FirstOrDefault().Books
                    .Where(bk => bk.Index == DeleteID)
                        .ToList().ForEach(b =>
                        {
                            b = selBook;
                        });
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            DLCIndex = 1;
            flpCategories.Controls.Clear();
            allbooks.Clear();
            LibraryLocation = folderBrowserDialog1.SelectedPath;
            if (File.ReadLines("settings.ini").ElementAtOrDefault(0) == null)
                File.AppendAllText("settings.ini", LibraryLocation);
            libcheck();
        }
        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            flpCategories.Controls.OfType<Button>().ToList().ForEach(c => { 
                c.Visible = string.IsNullOrEmpty(searchTxt.Text) || c.Text.ToLower().Contains(searchTxt.Text.ToLower());
                c.BackColor = !string.IsNullOrEmpty(searchTxt.Text) && c.Visible ? Color.Yellow : Color.White;
            });
        }
        private void cmbDLL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDLL.SelectedIndex != -1)
                ShowBooks((DLC)cmbDLL.SelectedItem);
        }

        private void multiModMeta_Click(object sender, EventArgs e)
        {
            var selectedBooks = DLCs
                .Where(d => d.Index == ((DLC)cmbDLL.SelectedItem).Index)
                .FirstOrDefault().Books
                    .Where(bk => bk.Selected).ToList();
            if (selectedBooks.Any())
            {
                AddBook mybook = new AddBook(selectedBooks);
                mybook.ShowDialog();
            } else
            {
                MessageBox.Show("Select one ore more books to continue.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var d in flpCategories.Controls)
            {
                Button b = (Button)d;
                CheckBox c = (CheckBox)b.GetChildAtPoint(new Point(5, (b.Height - 14) - 3));
                c.Checked = checkBox1.Checked;
            }
        }
    }
}
