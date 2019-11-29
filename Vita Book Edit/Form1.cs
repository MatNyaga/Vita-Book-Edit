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
        String DLCBookTitle = "00000000BLKSTOK1";
        int BookNumber = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void library_Click(object sender, EventArgs e)
        {
            int BookNumber = 2;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibraryLocation = folderBrowserDialog1.SelectedPath;
                System.IO.Directory.CreateDirectory(LibraryLocation+"\\"+ReaderTitle+"\\"+DLCBookTitle);
                librarypathlabel.Text = LibraryLocation + "\\";

                //Find the Number of Books (Count folders with book in them)
                const string searchQuery = "*" + "book" + "*";
                var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle);
                var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
                foreach (var d in directories)
                {
                    BookNumber+=1;
                }
                
                LibraryBookNumber.Text = "( "+(BookNumber-1)+" Books )";
            }

        }

        //Needs fixing
        private void AddBook_Click(object sender, EventArgs e)
        {
            if (BookNumber < 10)
            {
                System.IO.Directory.CreateDirectory(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle + "\\" + ("book0" + BookNumber));
                BookNumber += 1;
                LibraryBookNumber.Text = "( " + (BookNumber-1) + " Books )";
                BookNumber -= 1;
                AddBook mybook = new AddBook(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle + "\\" + ("book0" + BookNumber));
                mybook.ShowDialog();
                //Find the Number of Books (Count folders with book in them)
                BookNumber = 2;
                const string searchQuery = "*" + "book" + "*";
                var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle);
                var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
                foreach (var d in directories)
                {
                    BookNumber += 1;
                }

                LibraryBookNumber.Text = "( " + (BookNumber - 1) + " Books )";
        }
            else {
                System.IO.Directory.CreateDirectory(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle + "\\" + ("book" + BookNumber));
                BookNumber += 1;
                LibraryBookNumber.Text = "( " + (BookNumber-1) + " Books )";
                BookNumber -= 1;
                AddBook mybook = new AddBook(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle + "\\" + ("book" + BookNumber));
                mybook.ShowDialog();
                //Find the Number of Books (Count folders with book in them)
                BookNumber = 2;
                const string searchQuery = "*" + "book" + "*";
                var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitle);
                var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
                foreach (var d in directories)
                {
                    BookNumber += 1;
                }

                LibraryBookNumber.Text = "( " + (BookNumber - 1) + " Books )";
            
        }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
