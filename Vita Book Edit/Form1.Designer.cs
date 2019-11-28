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
            this.SuspendLayout();
            // 
            // library
            // 
            this.library.Location = new System.Drawing.Point(39, 23);
            this.library.Name = "library";
            this.library.Size = new System.Drawing.Size(137, 23);
            this.library.TabIndex = 0;
            this.library.Text = "Library Location";
            this.library.UseVisualStyleBackColor = true;
            this.library.Click += new System.EventHandler(this.library_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Vita Book Library";
            // 
            // AddBook
            // 
            this.AddBook.Location = new System.Drawing.Point(39, 104);
            this.AddBook.Name = "AddBook";
            this.AddBook.Size = new System.Drawing.Size(90, 105);
            this.AddBook.TabIndex = 2;
            this.AddBook.Text = "+";
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
            this.LibraryBookNumber.Location = new System.Drawing.Point(513, 58);
            this.LibraryBookNumber.Name = "LibraryBookNumber";
            this.LibraryBookNumber.Size = new System.Drawing.Size(58, 13);
            this.LibraryBookNumber.TabIndex = 4;
            this.LibraryBookNumber.Text = "( 0 Books )";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LibraryBookNumber);
            this.Controls.Add(this.librarypathlabel);
            this.Controls.Add(this.AddBook);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.library);
            this.Name = "Form1";
            this.Text = "Vita Book Library";
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
    }
}

