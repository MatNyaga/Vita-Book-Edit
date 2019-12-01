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

namespace Vita_Book_Edit
{
    public partial class Form1 : Form
    {
        String LibraryLocation = null;
        String ReaderTitle = "PCSC80012";
        String DLCBookTitleBase = "00000000BLKSTOK";
        int DLCIndex = 1;
        int BookNumber = 0;
        int Bookindex = 2;
        Boolean NewBook = false;
        //XML Stream Data
        Serializer ser = new Serializer();
        string path = string.Empty;
        string xmlInputData = string.Empty;
        string xmlOutputData = string.Empty;
        List<book> allbooks = new List<book>();
        int DeleteID;

        private Button btnAdd = new Button();

        //Universal library format function
        private void libcheck()
        {
            int BookNumber = 0;
            try
            {
                System.IO.Directory.CreateDirectory(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex);
            }
            catch (Exception ex)
            {
                File.WriteAllText("settings.ini", String.Empty);
                MessageBox.Show("Saved reAddcont location not found! Please select it again.");
                goto ErrorNoPath;
            }
            librarypathlabel.Text = LibraryLocation + "\\";

            //Find the Number of Books (Count folders with book in them)
            const string searchQuery = "*" + "book" + "*";
            for (int i = 0; i < 7; i++) {
                if (Directory.Exists(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + i)){DLCIndex = i;
                
                    var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex);
                    var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
                    foreach (var d in directories)
                    {
                        if (Directory.GetFiles(d.FullName + "\\", "metadata.xml").Length != 0)
                        {
                            BookNumber += 1;
                            xmlInputData = File.ReadAllText(d.FullName + "\\" + "metadata.xml");
                            book mybook = ser.Deserialize<book>(xmlInputData);
                            Button btn = new Button();
                            btn.Text = mybook.title;
                            mybook.bookpath = d.FullName;
                            btn.Size = new System.Drawing.Size(177, 250);
                            btn.ForeColor = Color.BlueViolet;
                            try
                            {
                                using (FileStream stream = new FileStream(d.FullName + "\\" + "cover.jpg", FileMode.Open, FileAccess.Read))
                                {
                                    btn.Image = Image.FromStream(stream);
                                    stream.Dispose();
                                    btn.Image = ResizeImage(btn.Image, btn.Size);
                                }
                            }
                            catch (Exception ex) { }

                            allbooks.Add(mybook);
                            btn.Tag = allbooks.Count() - 1;
                            flpCategories.Controls.Add(btn);
                            this.Controls.Add(flpCategories);
                            btn.Click += btn_Click;
                            btn.MouseDown += new MouseEventHandler(this.btn_Click);
                        }

                }
                }
                else
                {
                    //return;
                }
            }
            LibraryBookNumber.Text = "( " + (BookNumber) + " Books )";
            cleanfolders.Enabled = true;
            AddBook.Enabled = true;
            library.Text = "Change reAddcont Folder Location";
            ErrorNoPath:;
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
                int ListID = (int)b.Tag;
                MessageBox.Show(allbooks[ListID].title);
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
            if (result == DialogResult.OK)
            {
                DLCIndex = 1;
                BookNumber = 0;
                Bookindex = 2;
                flpCategories.Controls.Clear();
                allbooks.Clear();
                LibraryLocation = folderBrowserDialog1.SelectedPath;
                if (File.ReadLines("settings.ini").ElementAtOrDefault(0) == null)
                    File.AppendAllText("settings.ini", LibraryLocation);
                libcheck();
                
            }

        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            Bookindex = 2;
            string bkindex = null;
                while (!NewBook)
                {
                    if (DLCIndex > 6)
                    {
                        MessageBox.Show("Reader Book Limit has been reached!");
                        NewBook = true;
                        return;
                    }
                    if (Bookindex > 99)
                    {
                    DLCIndex += 1;
                    System.IO.Directory.CreateDirectory(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex);
                    Bookindex = 2;
                    }
                    if (Bookindex < 10){bkindex = "0" + Bookindex.ToString();}
                    else{bkindex = Bookindex.ToString();}
                
                if (!Directory.Exists(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex + "\\" + ("book" + bkindex)))
                    {
                        System.IO.Directory.CreateDirectory(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex + "\\" + ("book" + bkindex));
                        AddBook mybook = new AddBook(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex + "\\" + ("book" + bkindex));
                        mybook.ShowDialog();
                        NewBook = true;
                    }   
                    else
                    {
                        Bookindex += 1;
                    }
                }
                BookNumber = 0;
                //Find the Number of Books (Count folders with book in them)
                const string searchQuery = "*" + "book" + "*";
                var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex);
                var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
                foreach (var d in directories)
                {
                    if (Directory.GetFiles(d.FullName + "\\", "metadata.xml").Length != 0) { BookNumber += 1; }
                }

                LibraryBookNumber.Text = "( " + (BookNumber) + " Books )";
                NewBook = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private static void CleanDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                CleanDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        private void cleanfolders_Click(object sender, EventArgs e)
        {
            CleanDirectory(LibraryLocation + "\\" + ReaderTitle);
            MessageBox.Show("Library Cleaned");
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to Delete this book?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(allbooks[DeleteID].bookpath);

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
            MessageBox.Show("Book Path:"+Environment.NewLine +
                allbooks[DeleteID].bookpath +Environment.NewLine + Environment.NewLine+
                "Author: "+ allbooks[DeleteID].authors[0].author + Environment.NewLine+
                "Series Name: " + allbooks[DeleteID].seriesName + Environment.NewLine+
                "Volume No: " + allbooks[DeleteID].seriesOrdinal + Environment.NewLine+
                "Title: " + allbooks[DeleteID].title + Environment.NewLine+
                "Publisher: " + allbooks[DeleteID].publisher + Environment.NewLine+
                "Description: " + allbooks[DeleteID].description + Environment.NewLine);
        }

        private void editBookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBook mybook = new AddBook(allbooks[DeleteID]);
            mybook.ShowDialog();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            DLCIndex = 1;
            BookNumber = 0;
            Bookindex = 2;
            flpCategories.Controls.Clear();
            allbooks.Clear();
            LibraryLocation = folderBrowserDialog1.SelectedPath;
            if (File.ReadLines("settings.ini").ElementAtOrDefault(0) == null)
                File.AppendAllText("settings.ini", LibraryLocation);
            libcheck();
        }
    }
}
