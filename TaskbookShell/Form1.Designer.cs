namespace TaskbookShell
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.ptMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.langMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.modMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.internetModMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.localModMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.radioButtonOnline = new System.Windows.Forms.RadioButton();
            this.radioButtonLocal = new System.Windows.Forms.RadioButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ptMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "C:\\Program Files (x86)\\PT4\\PT4Help.chm";
            // 
            // ptMenuStrip
            // 
            this.ptMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings});
            this.ptMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ptMenuStrip.Name = "ptMenuStrip";
            this.ptMenuStrip.Size = new System.Drawing.Size(530, 24);
            this.ptMenuStrip.TabIndex = 11;
            this.ptMenuStrip.Text = "MenuStrip";
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langMenu,
            this.modMenu});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            // 
            // langMenu
            // 
            this.langMenu.Name = "langMenu";
            this.langMenu.Size = new System.Drawing.Size(126, 22);
            this.langMenu.Text = "Language";
            // 
            // modMenu
            // 
            this.modMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internetModMenu,
            this.localModMenu});
            this.modMenu.Name = "modMenu";
            this.modMenu.Size = new System.Drawing.Size(126, 22);
            this.modMenu.Text = "Mod";
            // 
            // internetModMenu
            // 
            this.internetModMenu.Name = "internetModMenu";
            this.internetModMenu.Size = new System.Drawing.Size(115, 22);
            this.internetModMenu.Text = "Internet";
            this.internetModMenu.Click += new System.EventHandler(this.ModMenuClick);
            // 
            // localModMenu
            // 
            this.localModMenu.Name = "localModMenu";
            this.localModMenu.Size = new System.Drawing.Size(115, 22);
            this.localModMenu.Text = "Local";
            this.localModMenu.Click += new System.EventHandler(this.ModMenuClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TaskbookShell.Properties.Resources.PT3Setup;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(142, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 22);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.SetupBtn_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.PictureBox3_MouseHover);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::TaskbookShell.Properties.Resources.PT3Demo;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(179, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 22);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.DemoBtn_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.PictureBox3_MouseHover);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox3.BackgroundImage = global::TaskbookShell.Properties.Resources.PT3Load;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(216, 30);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 22);
            this.pictureBox3.TabIndex = 17;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.LoadBtn_Click);
            this.pictureBox3.MouseEnter += new System.EventHandler(this.PictureBox3_MouseHover);
            this.pictureBox3.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::TaskbookShell.Properties.Resources.PT3Run;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(253, 30);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(29, 22);
            this.pictureBox4.TabIndex = 18;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.ResultBtn_Click);
            this.pictureBox4.MouseEnter += new System.EventHandler(this.PictureBox3_MouseHover);
            this.pictureBox4.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(291, 32);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(227, 20);
            this.downloadProgressBar.TabIndex = 19;
            this.downloadProgressBar.Visible = false;
            // 
            // radioButtonOnline
            // 
            this.radioButtonOnline.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonOnline.AutoSize = true;
            this.radioButtonOnline.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.radioButtonOnline.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButtonOnline.Location = new System.Drawing.Point(10, 29);
            this.radioButtonOnline.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonOnline.Name = "radioButtonOnline";
            this.radioButtonOnline.Size = new System.Drawing.Size(47, 23);
            this.radioButtonOnline.TabIndex = 20;
            this.radioButtonOnline.Text = "Online";
            this.radioButtonOnline.UseVisualStyleBackColor = false;
            this.radioButtonOnline.Click += new System.EventHandler(this.RadioButtonOnline_Click);
            // 
            // radioButtonLocal
            // 
            this.radioButtonLocal.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonLocal.AutoSize = true;
            this.radioButtonLocal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.radioButtonLocal.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLocal.Checked = true;
            this.radioButtonLocal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButtonLocal.Location = new System.Drawing.Point(60, 29);
            this.radioButtonLocal.Margin = new System.Windows.Forms.Padding(0);
            this.radioButtonLocal.Name = "radioButtonLocal";
            this.radioButtonLocal.Size = new System.Drawing.Size(43, 23);
            this.radioButtonLocal.TabIndex = 21;
            this.radioButtonLocal.TabStop = true;
            this.radioButtonLocal.Text = "Local";
            this.radioButtonLocal.UseVisualStyleBackColor = false;
            this.radioButtonLocal.Click += new System.EventHandler(this.RadioButtonLocal_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PT";
            this.notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 57);
            this.Controls.Add(this.radioButtonLocal);
            this.Controls.Add(this.radioButtonOnline);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ptMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ptMenuStrip;
            this.Name = "Form1";
            this.Text = "Programming Taskbook Integrator";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ptMenuStrip.ResumeLayout(false);
            this.ptMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.MenuStrip ptMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem langMenu;
        private System.Windows.Forms.ToolStripMenuItem modMenu;
        private System.Windows.Forms.ToolStripMenuItem internetModMenu;
        private System.Windows.Forms.ToolStripMenuItem localModMenu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.RadioButton radioButtonOnline;
        private System.Windows.Forms.RadioButton radioButtonLocal;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

