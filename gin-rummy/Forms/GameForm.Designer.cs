namespace gin_rummy.Forms
{
    partial class GameForm
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
            this.pYourHand = new System.Windows.Forms.Panel();
            this.mnMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCPUGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomplayCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pYourHand
            // 
            this.pYourHand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pYourHand.Location = new System.Drawing.Point(0, 356);
            this.pYourHand.Name = "pYourHand";
            this.pYourHand.Size = new System.Drawing.Size(800, 94);
            this.pYourHand.TabIndex = 0;
            // 
            // mnMain
            // 
            this.mnMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.mnMain.Location = new System.Drawing.Point(0, 0);
            this.mnMain.Name = "mnMain";
            this.mnMain.Size = new System.Drawing.Size(800, 24);
            this.mnMain.TabIndex = 1;
            this.mnMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCPUGameToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newCPUGameToolStripMenuItem
            // 
            this.newCPUGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomplayCPUToolStripMenuItem});
            this.newCPUGameToolStripMenuItem.Name = "newCPUGameToolStripMenuItem";
            this.newCPUGameToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.newCPUGameToolStripMenuItem.Text = "New CPU game";
            // 
            // randomplayCPUToolStripMenuItem
            // 
            this.randomplayCPUToolStripMenuItem.Name = "randomplayCPUToolStripMenuItem";
            this.randomplayCPUToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.randomplayCPUToolStripMenuItem.Text = "Random-play CPU";
            this.randomplayCPUToolStripMenuItem.Click += new System.EventHandler(this.randomplayCPUToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pYourHand);
            this.Controls.Add(this.mnMain);
            this.MainMenuStrip = this.mnMain;
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Shown += new System.EventHandler(this.GameForm_Shown);
            this.mnMain.ResumeLayout(false);
            this.mnMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pYourHand;
        private System.Windows.Forms.MenuStrip mnMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCPUGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomplayCPUToolStripMenuItem;
    }
}