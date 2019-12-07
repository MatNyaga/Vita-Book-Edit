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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.library = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AddBook = new System.Windows.Forms.Button();
            this.librarypathlabel = new System.Windows.Forms.Label();
            this.LibraryBookNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cleanfolders = new System.Windows.Forms.Button();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DeleteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editBookDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refresh = new System.Windows.Forms.Button();
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.DeleteMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // library
            // 
            this.library.Location = new System.Drawing.Point(39, 23);
            this.library.Name = "library";
            this.library.Size = new System.Drawing.Size(90, 68);
            this.library.TabIndex = 0;
            this.library.Text = "ux0: Location (Memory Card)";
            this.library.UseVisualStyleBackColor = true;
            this.library.Click += new System.EventHandler(this.library_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(381, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Vita Book Library";
            // 
            // AddBook
            // 
            this.AddBook.Enabled = false;
            this.AddBook.Location = new System.Drawing.Point(39, 113);
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
            this.librarypathlabel.Location = new System.Drawing.Point(212, 28);
            this.librarypathlabel.Name = "librarypathlabel";
            this.librarypathlabel.Size = new System.Drawing.Size(16, 13);
            this.librarypathlabel.TabIndex = 3;
            this.librarypathlabel.Text = "...";
            // 
            // LibraryBookNumber
            // 
            this.LibraryBookNumber.AutoSize = true;
            this.LibraryBookNumber.Location = new System.Drawing.Point(511, 60);
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
            this.label2.Text = "ver 0.5";
            // 
            // cleanfolders
            // 
            this.cleanfolders.Enabled = false;
            this.cleanfolders.Location = new System.Drawing.Point(39, 200);
            this.cleanfolders.Name = "cleanfolders";
            this.cleanfolders.Size = new System.Drawing.Size(90, 65);
            this.cleanfolders.TabIndex = 6;
            this.cleanfolders.Text = "Clean Empty Folders";
            this.cleanfolders.UseVisualStyleBackColor = true;
            this.cleanfolders.Click += new System.EventHandler(this.cleanfolders_Click);
            // 
            // flpCategories
            // 
            this.flpCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCategories.AutoScroll = true;
            this.flpCategories.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpCategories.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpCategories.Location = new System.Drawing.Point(160, 86);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(628, 339);
            this.flpCategories.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Authors";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "GrapheneCt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "MatNyaga";
            // 
            // DeleteMenuStrip
            // 
            this.DeleteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBookDetailsToolStripMenuItem,
            this.bookDetailsToolStripMenuItem,
            this.deleteBookToolStripMenuItem});
            this.DeleteMenuStrip.Name = "DeleteMenuStrip";
            this.DeleteMenuStrip.Size = new System.Drawing.Size(163, 70);
            // 
            // editBookDetailsToolStripMenuItem
            // 
            this.editBookDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editBookDetailsToolStripMenuItem.Image")));
            this.editBookDetailsToolStripMenuItem.Name = "editBookDetailsToolStripMenuItem";
            this.editBookDetailsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.editBookDetailsToolStripMenuItem.Text = "Edit Book Details";
            this.editBookDetailsToolStripMenuItem.Click += new System.EventHandler(this.editBookDetailsToolStripMenuItem_Click);
            // 
            // bookDetailsToolStripMenuItem
            // 
            this.bookDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bookDetailsToolStripMenuItem.Image")));
            this.bookDetailsToolStripMenuItem.Name = "bookDetailsToolStripMenuItem";
            this.bookDetailsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.bookDetailsToolStripMenuItem.Text = "Book Details";
            this.bookDetailsToolStripMenuItem.Click += new System.EventHandler(this.bookDetailsToolStripMenuItem_Click);
            // 
            // deleteBookToolStripMenuItem
            // 
            this.deleteBookToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteBookToolStripMenuItem.Image")));
            this.deleteBookToolStripMenuItem.Name = "deleteBookToolStripMenuItem";
            this.deleteBookToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteBookToolStripMenuItem.Text = "Delete Book";
            this.deleteBookToolStripMenuItem.Click += new System.EventHandler(this.deleteBookToolStripMenuItem_Click);
            // 
            // refresh
            // 
            this.refresh.Image = ((System.Drawing.Image)(resources.GetObject("refresh.Image")));
            this.refresh.Location = new System.Drawing.Point(160, 16);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(46, 38);
            this.refresh.TabIndex = 12;
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // searchTxt
            // 
            this.searchTxt.Location = new System.Drawing.Point(160, 60);
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(213, 20);
            this.searchTxt.TabIndex = 13;
            this.searchTxt.TextChanged += new System.EventHandler(this.searchTxt_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchTxt);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flpCategories);
            this.Controls.Add(this.cleanfolders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LibraryBookNumber);
            this.Controls.Add(this.librarypathlabel);
            this.Controls.Add(this.AddBook);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.library);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Vita Book Library";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DeleteMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.FlowLayoutPanel flpCategories;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip DeleteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBookDetailsToolStripMenuItem;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.TextBox searchTxt;
    }
}

