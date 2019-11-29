namespace Vita_Book_Edit
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.library = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AddBook = new System.Windows.Forms.Button();
            this.librarypathlabel = new System.Windows.Forms.Label();
            this.LibraryBookNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cleanfolders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // library
            // 
            this.library.Location = new System.Drawing.Point(39, 23);
            this.library.Name = "library";
            this.library.Size = new System.Drawing.Size(137, 48);
            this.library.TabIndex = 0;
            this.library.Text = "reAddcont Folder Location";
            this.library.UseVisualStyleBackColor = true;
            this.library.Click += new System.EventHandler(this.library_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(655, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Vita Book Library";
            // 
            // AddBook
            // 
            this.AddBook.Enabled = false;
            this.AddBook.Location = new System.Drawing.Point(39, 111);
            this.AddBook.Name = "AddBook";
            this.AddBook.Size = new System.Drawing.Size(90, 65);
            this.AddBook.TabIndex = 2;
            this.AddBook.Text = "+ (Add Book)";
            this.AddBook.UseVisualStyleBackColor = true;
            this.AddBook.Click += new System.EventHandler(this.AddBook_Click);
            // 
            // librarypathlabel
            // 
            this.librarypathlabel.AutoSize = true;
            this.librarypathlabel.Location = new System.Drawing.Point(244, 28);
            this.librarypathlabel.Name = "librarypathlabel";
            this.librarypathlabel.Size = new System.Drawing.Size(16, 13);
            this.librarypathlabel.TabIndex = 3;
            this.librarypathlabel.Text = "...";
            // 
            // LibraryBookNumber
            // 
            this.LibraryBookNumber.AutoSize = true;
            this.LibraryBookNumber.Location = new System.Drawing.Point(655, 58);
            this.LibraryBookNumber.Name = "LibraryBookNumber";
            this.LibraryBookNumber.Size = new System.Drawing.Size(58, 13);
            this.LibraryBookNumber.TabIndex = 4;
            this.LibraryBookNumber.Text = "( 0 Books )";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(753, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ver 0.1";
            // 
            // cleanfolders
            // 
            this.cleanfolders.Enabled = false;
            this.cleanfolders.Location = new System.Drawing.Point(192, 111);
            this.cleanfolders.Name = "cleanfolders";
            this.cleanfolders.Size = new System.Drawing.Size(90, 65);
            this.cleanfolders.TabIndex = 6;
            this.cleanfolders.Text = "Clean Empty Folders";
            this.cleanfolders.UseVisualStyleBackColor = true;
            this.cleanfolders.Click += new System.EventHandler(this.cleanfolders_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cleanfolders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LibraryBookNumber);
            this.Controls.Add(this.librarypathlabel);
            this.Controls.Add(this.AddBook);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.library);
            this.Name = "Form1";
            this.Text = "Vita Book Library";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button library;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddBook;
        private System.Windows.Forms.Label librarypathlabel;
        private System.Windows.Forms.Label LibraryBookNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cleanfolders;
    }
}

