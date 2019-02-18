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
            gin_rummy.Cards.TwoColourScheme twoColourScheme1 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme2 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme3 = new gin_rummy.Cards.TwoColourScheme();
            this.mnMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCPUGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomplayCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pStacks = new gin_rummy.Controls.CardStacks();
            this.pOpponentsHand = new gin_rummy.Controls.CardPanel();
            this.pYourHand = new gin_rummy.Controls.CardPanel();
            this.pActions = new gin_rummy.Controls.PlayerActions();
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
            // pStacks
            // 
            this.pStacks.AllowDrawStock = false;
            this.pStacks.AllowTakeDiscard = false;
            this.pStacks.DiscardCount = 0;
            this.pStacks.DiscardTaken = null;
            this.pStacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pStacks.Location = new System.Drawing.Point(0, 107);
            this.pStacks.Name = "pStacks";
            this.pStacks.Size = new System.Drawing.Size(629, 70);
            this.pStacks.StockCount = 0;
            this.pStacks.StockDrawn = null;
            this.pStacks.SuitColourScheme = twoColourScheme1;
            this.pStacks.TabIndex = 6;
            this.pStacks.VisibleDiscard = null;
            // 
            // pOpponentsHand
            // 
            this.pOpponentsHand.AllowReordering = false;
            this.pOpponentsHand.AllowSelection = false;
            this.pOpponentsHand.CardSelected = null;
            this.pOpponentsHand.ColourScheme = twoColourScheme2;
            this.pOpponentsHand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pOpponentsHand.Location = new System.Drawing.Point(0, 24);
            this.pOpponentsHand.Name = "pOpponentsHand";
            this.pOpponentsHand.ShowCards = false;
            this.pOpponentsHand.Size = new System.Drawing.Size(629, 83);
            this.pOpponentsHand.TabIndex = 4;
            // 
            // pYourHand
            // 
            this.pYourHand.AllowReordering = false;
            this.pYourHand.AllowSelection = false;
            this.pYourHand.CardSelected = null;
            this.pYourHand.ColourScheme = twoColourScheme3;
            this.pYourHand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pYourHand.Location = new System.Drawing.Point(0, 177);
            this.pYourHand.Name = "pYourHand";
            this.pYourHand.ShowCards = false;
            this.pYourHand.Size = new System.Drawing.Size(629, 83);
            this.pYourHand.TabIndex = 3;
            // 
            // pActions
            // 
            this.pActions.AllowDraw = false;
            this.pActions.AllowKnock = false;
            this.pActions.AllowTake = false;
            this.pActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pActions.Location = new System.Drawing.Point(629, 24);
            this.pActions.Name = "pActions";
            this.pActions.OnDraw = null;
            this.pActions.OnKnock = null;
            this.pActions.OnTake = null;
            this.pActions.Size = new System.Drawing.Size(147, 236);
            this.pActions.TabIndex = 5;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 260);
            this.Controls.Add(this.pStacks);
            this.Controls.Add(this.pOpponentsHand);
            this.Controls.Add(this.pYourHand);
            this.Controls.Add(this.pActions);
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
        private Controls.PlayerActions pActions;
        private Controls.CardStacks pStacks;
    }
}