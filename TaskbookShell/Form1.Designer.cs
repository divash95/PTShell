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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.ptMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.langMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.modMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.internetModMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.localModMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panelStrip = new System.Windows.Forms.MenuStrip();
            this.internetModPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.localModPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.ptSetupPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.ptDemoPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.ptLoadPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.ptResPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.ptMenuStrip.SuspendLayout();
            this.panelStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "C:\\Program Files (x86)\\PT4\\PT4Help.chm";
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(0, 51);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(402, 23);
            this.downloadProgressBar.TabIndex = 13;
            this.downloadProgressBar.Visible = false;
            // 
            // ptMenuStrip
            // 
            this.ptMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings});
            this.ptMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ptMenuStrip.Name = "ptMenuStrip";
            this.ptMenuStrip.Size = new System.Drawing.Size(452, 24);
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
            // panelStrip
            // 
            this.panelStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internetModPanel,
            this.localModPanel,
            this.ptSetupPanel,
            this.ptDemoPanel,
            this.ptLoadPanel,
            this.ptResPanel});
            this.panelStrip.Location = new System.Drawing.Point(0, 24);
            this.panelStrip.Name = "panelStrip";
            this.panelStrip.Size = new System.Drawing.Size(452, 24);
            this.panelStrip.TabIndex = 14;
            this.panelStrip.Text = "MenuStrip";
            // 
            // internetModPanel
            // 
            this.internetModPanel.Name = "internetModPanel";
            this.internetModPanel.Size = new System.Drawing.Size(60, 20);
            this.internetModPanel.Text = "Internet";
            this.internetModPanel.Visible = false;
            this.internetModPanel.Click += new System.EventHandler(this.ModMenuClick);
            // 
            // localModPanel
            // 
            this.localModPanel.Name = "localModPanel";
            this.localModPanel.Size = new System.Drawing.Size(47, 20);
            this.localModPanel.Text = "Local";
            this.localModPanel.Visible = false;
            this.localModPanel.Click += new System.EventHandler(this.ModMenuClick);
            // 
            // ptSetupPanel
            // 
            this.ptSetupPanel.Name = "ptSetupPanel";
            this.ptSetupPanel.Size = new System.Drawing.Size(63, 20);
            this.ptSetupPanel.Text = "PTSetup";
            this.ptSetupPanel.Click += new System.EventHandler(this.SetupBtn_Click);
            // 
            // ptDemoPanel
            // 
            this.ptDemoPanel.Name = "ptDemoPanel";
            this.ptDemoPanel.Size = new System.Drawing.Size(65, 20);
            this.ptDemoPanel.Text = "PTDemo";
            this.ptDemoPanel.Click += new System.EventHandler(this.DemoBtn_Click);
            // 
            // ptLoadPanel
            // 
            this.ptLoadPanel.Name = "ptLoadPanel";
            this.ptLoadPanel.Size = new System.Drawing.Size(59, 20);
            this.ptLoadPanel.Text = "PTLoad";
            this.ptLoadPanel.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // ptResPanel
            // 
            this.ptResPanel.Name = "ptResPanel";
            this.ptResPanel.Size = new System.Drawing.Size(65, 20);
            this.ptResPanel.Text = "PTResult";
            this.ptResPanel.Click += new System.EventHandler(this.ResultBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 49);
            this.Controls.Add(this.panelStrip);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.ptMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ptMenuStrip;
            this.Name = "Form1";
            this.Text = "Programming Taskbook Integrator";
            this.ptMenuStrip.ResumeLayout(false);
            this.ptMenuStrip.PerformLayout();
            this.panelStrip.ResumeLayout(false);
            this.panelStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.MenuStrip ptMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem langMenu;
        private System.Windows.Forms.ToolStripMenuItem modMenu;
        private System.Windows.Forms.ToolStripMenuItem internetModMenu;
        private System.Windows.Forms.ToolStripMenuItem localModMenu;
        private System.Windows.Forms.MenuStrip panelStrip;
        private System.Windows.Forms.ToolStripMenuItem internetModPanel;
        private System.Windows.Forms.ToolStripMenuItem localModPanel;
        private System.Windows.Forms.ToolStripMenuItem ptSetupPanel;
        private System.Windows.Forms.ToolStripMenuItem ptDemoPanel;
        private System.Windows.Forms.ToolStripMenuItem ptLoadPanel;
        private System.Windows.Forms.ToolStripMenuItem ptResPanel;
    }
}

