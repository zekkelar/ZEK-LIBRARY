namespace ZekLIB
{
    partial class UploadSkripsi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadSkripsi));
            this.title = new Guna.UI.WinForms.GunaTextBox();
            this.years = new Guna.UI.WinForms.GunaTextBox();
            this.author = new Guna.UI.WinForms.GunaTextBox();
            this.deskripsi = new System.Windows.Forms.RichTextBox();
            this.gunaButton5 = new Guna.UI.WinForms.GunaButton();
            this.gunaButton1 = new Guna.UI.WinForms.GunaButton();
            this.gunaComboBox2 = new Guna.UI.WinForms.GunaComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gunaTextBox1 = new Guna.UI.WinForms.GunaTextBox();
            this.gunaButton2 = new Guna.UI.WinForms.GunaButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.BaseColor = System.Drawing.Color.White;
            this.title.BorderColor = System.Drawing.Color.Silver;
            this.title.BorderSize = 1;
            this.title.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.title.FocusedBaseColor = System.Drawing.Color.White;
            this.title.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.title.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.title.Location = new System.Drawing.Point(161, 35);
            this.title.Name = "title";
            this.title.PasswordChar = '\0';
            this.title.Radius = 8;
            this.title.SelectedText = "";
            this.title.Size = new System.Drawing.Size(1028, 51);
            this.title.TabIndex = 0;
            this.title.Text = "Title";
            this.title.Enter += new System.EventHandler(this.title_Enter);
            this.title.Leave += new System.EventHandler(this.title_Leave);
            // 
            // years
            // 
            this.years.BackColor = System.Drawing.Color.Transparent;
            this.years.BaseColor = System.Drawing.Color.White;
            this.years.BorderColor = System.Drawing.Color.Silver;
            this.years.BorderSize = 1;
            this.years.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.years.FocusedBaseColor = System.Drawing.Color.White;
            this.years.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.years.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.years.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.years.Location = new System.Drawing.Point(161, 108);
            this.years.Name = "years";
            this.years.PasswordChar = '\0';
            this.years.Radius = 8;
            this.years.SelectedText = "";
            this.years.Size = new System.Drawing.Size(1028, 51);
            this.years.TabIndex = 1;
            this.years.Text = "Years";
            this.years.Enter += new System.EventHandler(this.years_Enter);
            this.years.Leave += new System.EventHandler(this.years_Leave);
            // 
            // author
            // 
            this.author.BackColor = System.Drawing.Color.Transparent;
            this.author.BaseColor = System.Drawing.Color.White;
            this.author.BorderColor = System.Drawing.Color.Silver;
            this.author.BorderSize = 1;
            this.author.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.author.FocusedBaseColor = System.Drawing.Color.White;
            this.author.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.author.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.author.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.author.Location = new System.Drawing.Point(161, 184);
            this.author.Name = "author";
            this.author.PasswordChar = '\0';
            this.author.Radius = 8;
            this.author.SelectedText = "";
            this.author.Size = new System.Drawing.Size(1028, 51);
            this.author.TabIndex = 2;
            this.author.Text = "Author";
            this.author.Enter += new System.EventHandler(this.author_Enter);
            this.author.Leave += new System.EventHandler(this.author_Leave);
            // 
            // deskripsi
            // 
            this.deskripsi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.deskripsi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.deskripsi.Location = new System.Drawing.Point(161, 648);
            this.deskripsi.MaxLength = 703;
            this.deskripsi.Name = "deskripsi";
            this.deskripsi.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.deskripsi.Size = new System.Drawing.Size(1028, 96);
            this.deskripsi.TabIndex = 20;
            this.deskripsi.Text = "Abstract / Description ";
            this.deskripsi.Enter += new System.EventHandler(this.deskripsi_Enter);
            this.deskripsi.Leave += new System.EventHandler(this.deskripsi_Leave);
            // 
            // gunaButton5
            // 
            this.gunaButton5.AnimationHoverSpeed = 0.07F;
            this.gunaButton5.AnimationSpeed = 0.03F;
            this.gunaButton5.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaButton5.BorderColor = System.Drawing.Color.Black;
            this.gunaButton5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton5.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton5.ForeColor = System.Drawing.Color.White;
            this.gunaButton5.Image = null;
            this.gunaButton5.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton5.Location = new System.Drawing.Point(414, 575);
            this.gunaButton5.Name = "gunaButton5";
            this.gunaButton5.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton5.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton5.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton5.OnHoverImage = null;
            this.gunaButton5.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton5.Radius = 7;
            this.gunaButton5.Size = new System.Drawing.Size(294, 42);
            this.gunaButton5.TabIndex = 21;
            this.gunaButton5.Text = "UPLOAD IMAGE";
            this.gunaButton5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton5.Click += new System.EventHandler(this.gunaButton5_Click);
            // 
            // gunaButton1
            // 
            this.gunaButton1.AnimationHoverSpeed = 0.07F;
            this.gunaButton1.AnimationSpeed = 0.03F;
            this.gunaButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(221)))), ((int)(((byte)(154)))));
            this.gunaButton1.BorderColor = System.Drawing.Color.Black;
            this.gunaButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton1.ForeColor = System.Drawing.Color.White;
            this.gunaButton1.Image = null;
            this.gunaButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton1.Location = new System.Drawing.Point(895, 764);
            this.gunaButton1.Name = "gunaButton1";
            this.gunaButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton1.OnHoverImage = null;
            this.gunaButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton1.Radius = 7;
            this.gunaButton1.Size = new System.Drawing.Size(294, 42);
            this.gunaButton1.TabIndex = 22;
            this.gunaButton1.Text = "UPLOAD SKRIPSI";
            this.gunaButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton1.Click += new System.EventHandler(this.gunaButton1_Click);
            // 
            // gunaComboBox2
            // 
            this.gunaComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.gunaComboBox2.BaseColor = System.Drawing.Color.White;
            this.gunaComboBox2.BorderColor = System.Drawing.Color.Silver;
            this.gunaComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunaComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunaComboBox2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaComboBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gunaComboBox2.ForeColor = System.Drawing.Color.Black;
            this.gunaComboBox2.FormattingEnabled = true;
            this.gunaComboBox2.Items.AddRange(new object[] {
            "journal"});
            this.gunaComboBox2.Location = new System.Drawing.Point(244, 251);
            this.gunaComboBox2.Name = "gunaComboBox2";
            this.gunaComboBox2.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaComboBox2.OnHoverItemForeColor = System.Drawing.Color.White;
            this.gunaComboBox2.Size = new System.Drawing.Size(211, 35);
            this.gunaComboBox2.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Category :";
            // 
            // gunaTextBox1
            // 
            this.gunaTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.gunaTextBox1.BaseColor = System.Drawing.Color.White;
            this.gunaTextBox1.BorderColor = System.Drawing.Color.Silver;
            this.gunaTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gunaTextBox1.FocusedBaseColor = System.Drawing.Color.White;
            this.gunaTextBox1.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaTextBox1.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.gunaTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaTextBox1.Location = new System.Drawing.Point(161, 308);
            this.gunaTextBox1.Name = "gunaTextBox1";
            this.gunaTextBox1.PasswordChar = '\0';
            this.gunaTextBox1.Radius = 8;
            this.gunaTextBox1.SelectedText = "";
            this.gunaTextBox1.Size = new System.Drawing.Size(728, 51);
            this.gunaTextBox1.TabIndex = 29;
            this.gunaTextBox1.Text = "File";
            // 
            // gunaButton2
            // 
            this.gunaButton2.AnimationHoverSpeed = 0.07F;
            this.gunaButton2.AnimationSpeed = 0.03F;
            this.gunaButton2.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaButton2.BorderColor = System.Drawing.Color.Black;
            this.gunaButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton2.ForeColor = System.Drawing.Color.White;
            this.gunaButton2.Image = null;
            this.gunaButton2.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton2.Location = new System.Drawing.Point(895, 308);
            this.gunaButton2.Name = "gunaButton2";
            this.gunaButton2.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton2.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton2.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton2.OnHoverImage = null;
            this.gunaButton2.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton2.Radius = 7;
            this.gunaButton2.Size = new System.Drawing.Size(294, 51);
            this.gunaButton2.TabIndex = 30;
            this.gunaButton2.Text = "UPLOAD FILE";
            this.gunaButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton2.Click += new System.EventHandler(this.gunaButton2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(161, 385);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(247, 232);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // UploadSkripsi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1366, 949);
            this.Controls.Add(this.gunaButton2);
            this.Controls.Add(this.gunaTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gunaComboBox2);
            this.Controls.Add(this.gunaButton1);
            this.Controls.Add(this.gunaButton5);
            this.Controls.Add(this.deskripsi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.author);
            this.Controls.Add(this.years);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UploadSkripsi";
            this.Text = "UploadSkripsi";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaTextBox title;
        private Guna.UI.WinForms.GunaTextBox years;
        private Guna.UI.WinForms.GunaTextBox author;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox deskripsi;
        private Guna.UI.WinForms.GunaButton gunaButton5;
        private Guna.UI.WinForms.GunaButton gunaButton1;
        private Guna.UI.WinForms.GunaComboBox gunaComboBox2;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaTextBox gunaTextBox1;
        private Guna.UI.WinForms.GunaButton gunaButton2;
    }
}