﻿namespace gin_rummy.Forms
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
            gin_rummy.Cards.TwoColourScheme twoColourScheme1 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme2 = new gin_rummy.Cards.TwoColourScheme();
            this.mnMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCPUGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomplayCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pYourHand = new gin_rummy.Controls.CardPanel();
            this.pOpponentsHand = new gin_rummy.Controls.CardPanel();
            this.playerActions1 = new gin_rummy.Controls.PlayerActions();
            this.mnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnMain
            // 
            this.mnMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.mnMain.Location = new System.Drawing.Point(0, 0);
            this.mnMain.Name = "mnMain";
            this.mnMain.Size = new System.Drawing.Size(776, 24);
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
            // pYourHand
            // 
            this.pYourHand.AllowReordering = false;
            this.pYourHand.AllowSelection = false;
            this.pYourHand.CardSelected = null;
            this.pYourHand.ColourMap = null;
            this.pYourHand.ColourScheme = twoColourScheme1;
            this.pYourHand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pYourHand.Location = new System.Drawing.Point(0, 249);
            this.pYourHand.Name = "pYourHand";
            this.pYourHand.ShowCards = false;
            this.pYourHand.Size = new System.Drawing.Size(623, 83);
            this.pYourHand.TabIndex = 3;
            // 
            // pOpponentsHand
            // 
            this.pOpponentsHand.AllowReordering = false;
            this.pOpponentsHand.AllowSelection = false;
            this.pOpponentsHand.CardSelected = null;
            this.pOpponentsHand.ColourMap = null;
            this.pOpponentsHand.ColourScheme = twoColourScheme2;
            this.pOpponentsHand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pOpponentsHand.Location = new System.Drawing.Point(0, 24);
            this.pOpponentsHand.Name = "pOpponentsHand";
            this.pOpponentsHand.ShowCards = false;
            this.pOpponentsHand.Size = new System.Drawing.Size(623, 83);
            this.pOpponentsHand.TabIndex = 4;
            // 
            // playerActions1
            // 
            this.playerActions1.AllowDraw = false;
            this.playerActions1.AllowKnock = false;
            this.playerActions1.AllowTake = false;
            this.playerActions1.Dock = System.Windows.Forms.DockStyle.Right;
            this.playerActions1.Location = new System.Drawing.Point(623, 24);
            this.playerActions1.Name = "playerActions1";
            this.playerActions1.OnDraw = null;
            this.playerActions1.OnKnock = null;
            this.playerActions1.OnTake = null;
            this.playerActions1.Size = new System.Drawing.Size(153, 308);
            this.playerActions1.TabIndex = 5;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 332);
            this.Controls.Add(this.pOpponentsHand);
            this.Controls.Add(this.pYourHand);
            this.Controls.Add(this.playerActions1);
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
        private System.Windows.Forms.MenuStrip mnMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCPUGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomplayCPUToolStripMenuItem;
        private Controls.CardPanel pYourHand;
        private Controls.CardPanel pOpponentsHand;
        private Controls.PlayerActions playerActions1;
    }
}