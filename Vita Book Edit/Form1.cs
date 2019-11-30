﻿using System;
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
            var directory = new DirectoryInfo(LibraryLocation + "\\" + ReaderTitle + "\\" + DLCBookTitleBase + DLCIndex);
            var directories = directory.GetDirectories(searchQuery, SearchOption.AllDirectories);
            foreach (var d in directories)
            {
                if (Directory.GetFiles(d.FullName + "\\", "metadata.xml").Length != 0) { BookNumber += 1; }
            }

            LibraryBookNumber.Text = "( " + (BookNumber) + " Books )";
            cleanfolders.Enabled = true;
            AddBook.Enabled = true;
            library.Text = "Change reAddcont Folder Location";
            ErrorNoPath:;
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
            //Format controls. Note: Controls inherit color from parent form.
            this.btnAdd.BackColor = Color.Gray;
            this.btnAdd.Text = "Book";
            this.btnAdd.Location = new System.Drawing.Point(90, 25);
            this.btnAdd.Size = new System.Drawing.Size(50, 25);
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
    }
}
