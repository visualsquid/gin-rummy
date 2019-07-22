namespace gin_rummy.Controls
{
    partial class MeldCreator
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gin_rummy.Cards.TwoColourScheme twoColourScheme1 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme2 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme3 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme4 = new gin_rummy.Cards.TwoColourScheme();
            gin_rummy.Cards.TwoColourScheme twoColourScheme5 = new gin_rummy.Cards.TwoColourScheme();
            this.pTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.bClearMeldFour = new System.Windows.Forms.Button();
            this.bClearMeldThree = new System.Windows.Forms.Button();
            this.pButtons = new System.Windows.Forms.Panel();
            this.bAcceptMelds = new System.Windows.Forms.Button();
            this.bClearMeldOne = new System.Windows.Forms.Button();
            this.bClearMeldTwo = new System.Windows.Forms.Button();
            this.cbMeldOneValid = new System.Windows.Forms.CheckBox();
            this.cbMeldTwoValid = new System.Windows.Forms.CheckBox();
            this.cbMeldThreeValid = new System.Windows.Forms.CheckBox();
            this.cbMeldFourValid = new System.Windows.Forms.CheckBox();
            this.pMeldFour = new gin_rummy.Controls.CardPanel();
            this.pMeldThree = new gin_rummy.Controls.CardPanel();
            this.pMeldTwo = new gin_rummy.Controls.CardPanel();
            this.pMeldOne = new gin_rummy.Controls.CardPanel();
            this.pHand = new gin_rummy.Controls.CardPanel();
            this.pTableLayout.SuspendLayout();
            this.pButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTableLayout
            // 
            this.pTableLayout.ColumnCount = 3;
            this.pTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.pTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.pTableLayout.Controls.Add(this.bClearMeldFour, 1, 4);
            this.pTableLayout.Controls.Add(this.bClearMeldThree, 1, 3);
            this.pTableLayout.Controls.Add(this.pMeldFour, 0, 4);
            this.pTableLayout.Controls.Add(this.pMeldThree, 0, 3);
            this.pTableLayout.Controls.Add(this.pMeldTwo, 0, 2);
            this.pTableLayout.Controls.Add(this.pMeldOne, 0, 1);
            this.pTableLayout.Controls.Add(this.pHand, 0, 0);
            this.pTableLayout.Controls.Add(this.pButtons, 0, 5);
            this.pTableLayout.Controls.Add(this.bClearMeldOne, 1, 1);
            this.pTableLayout.Controls.Add(this.bClearMeldTwo, 1, 2);
            this.pTableLayout.Controls.Add(this.cbMeldOneValid, 2, 1);
            this.pTableLayout.Controls.Add(this.cbMeldTwoValid, 2, 2);
            this.pTableLayout.Controls.Add(this.cbMeldThreeValid, 2, 3);
            this.pTableLayout.Controls.Add(this.cbMeldFourValid, 2, 4);
            this.pTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTableLayout.Location = new System.Drawing.Point(0, 0);
            this.pTableLayout.Name = "pTableLayout";
            this.pTableLayout.RowCount = 6;
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.pTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pTableLayout.Size = new System.Drawing.Size(851, 427);
            this.pTableLayout.TabIndex = 6;
            // 
            // bClearMeldFour
            // 
            this.bClearMeldFour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClearMeldFour.Location = new System.Drawing.Point(662, 307);
            this.bClearMeldFour.Name = "bClearMeldFour";
            this.bClearMeldFour.Size = new System.Drawing.Size(125, 70);
            this.bClearMeldFour.TabIndex = 14;
            this.bClearMeldFour.Text = "Clear";
            this.bClearMeldFour.UseVisualStyleBackColor = true;
            this.bClearMeldFour.Click += new System.EventHandler(this.bClearMeldAny_Click);
            // 
            // bClearMeldThree
            // 
            this.bClearMeldThree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClearMeldThree.Location = new System.Drawing.Point(662, 231);
            this.bClearMeldThree.Name = "bClearMeldThree";
            this.bClearMeldThree.Size = new System.Drawing.Size(125, 70);
            this.bClearMeldThree.TabIndex = 13;
            this.bClearMeldThree.Text = "Clear";
            this.bClearMeldThree.UseVisualStyleBackColor = true;
            this.bClearMeldThree.Click += new System.EventHandler(this.bClearMeldAny_Click);
            // 
            // pButtons
            // 
            this.pTableLayout.SetColumnSpan(this.pButtons, 2);
            this.pButtons.Controls.Add(this.bAcceptMelds);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pButtons.Location = new System.Drawing.Point(3, 383);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(784, 41);
            this.pButtons.TabIndex = 2;
            // 
            // bAcceptMelds
            // 
            this.bAcceptMelds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAcceptMelds.Enabled = false;
            this.bAcceptMelds.Location = new System.Drawing.Point(706, 14);
            this.bAcceptMelds.Name = "bAcceptMelds";
            this.bAcceptMelds.Size = new System.Drawing.Size(75, 23);
            this.bAcceptMelds.TabIndex = 0;
            this.bAcceptMelds.Text = "Accept";
            this.bAcceptMelds.UseVisualStyleBackColor = true;
            this.bAcceptMelds.Click += new System.EventHandler(this.bAcceptMelds_Click);
            // 
            // bClearMeldOne
            // 
            this.bClearMeldOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClearMeldOne.Location = new System.Drawing.Point(662, 79);
            this.bClearMeldOne.Name = "bClearMeldOne";
            this.bClearMeldOne.Size = new System.Drawing.Size(125, 70);
            this.bClearMeldOne.TabIndex = 11;
            this.bClearMeldOne.Text = "Clear";
            this.bClearMeldOne.UseVisualStyleBackColor = true;
            this.bClearMeldOne.Click += new System.EventHandler(this.bClearMeldAny_Click);
            // 
            // bClearMeldTwo
            // 
            this.bClearMeldTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClearMeldTwo.Location = new System.Drawing.Point(662, 155);
            this.bClearMeldTwo.Name = "bClearMeldTwo";
            this.bClearMeldTwo.Size = new System.Drawing.Size(125, 70);
            this.bClearMeldTwo.TabIndex = 12;
            this.bClearMeldTwo.Text = "Clear";
            this.bClearMeldTwo.UseVisualStyleBackColor = true;
            this.bClearMeldTwo.Click += new System.EventHandler(this.bClearMeldAny_Click);
            // 
            // cbMeldOneValid
            // 
            this.cbMeldOneValid.AutoSize = true;
            this.cbMeldOneValid.BackColor = System.Drawing.Color.Red;
            this.cbMeldOneValid.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbMeldOneValid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMeldOneValid.Location = new System.Drawing.Point(793, 79);
            this.cbMeldOneValid.Name = "cbMeldOneValid";
            this.cbMeldOneValid.Size = new System.Drawing.Size(55, 70);
            this.cbMeldOneValid.TabIndex = 15;
            this.cbMeldOneValid.UseVisualStyleBackColor = false;
            // 
            // cbMeldTwoValid
            // 
            this.cbMeldTwoValid.AutoSize = true;
            this.cbMeldTwoValid.BackColor = System.Drawing.Color.Red;
            this.cbMeldTwoValid.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbMeldTwoValid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMeldTwoValid.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cbMeldTwoValid.Location = new System.Drawing.Point(793, 155);
            this.cbMeldTwoValid.Name = "cbMeldTwoValid";
            this.cbMeldTwoValid.Size = new System.Drawing.Size(55, 70);
            this.cbMeldTwoValid.TabIndex = 16;
            this.cbMeldTwoValid.UseVisualStyleBackColor = false;
            // 
            // cbMeldThreeValid
            // 
            this.cbMeldThreeValid.AutoSize = true;
            this.cbMeldThreeValid.BackColor = System.Drawing.Color.Red;
            this.cbMeldThreeValid.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbMeldThreeValid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMeldThreeValid.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cbMeldThreeValid.Location = new System.Drawing.Point(793, 231);
            this.cbMeldThreeValid.Name = "cbMeldThreeValid";
            this.cbMeldThreeValid.Size = new System.Drawing.Size(55, 70);
            this.cbMeldThreeValid.TabIndex = 17;
            this.cbMeldThreeValid.UseVisualStyleBackColor = false;
            // 
            // cbMeldFourValid
            // 
            this.cbMeldFourValid.AutoSize = true;
            this.cbMeldFourValid.BackColor = System.Drawing.Color.Red;
            this.cbMeldFourValid.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbMeldFourValid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMeldFourValid.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cbMeldFourValid.Location = new System.Drawing.Point(793, 307);
            this.cbMeldFourValid.Name = "cbMeldFourValid";
            this.cbMeldFourValid.Size = new System.Drawing.Size(55, 70);
            this.cbMeldFourValid.TabIndex = 18;
            this.cbMeldFourValid.UseVisualStyleBackColor = false;
            // 
            // pMeldFour
            // 
            this.pMeldFour.AllowDragFrom = false;
            this.pMeldFour.AllowDragTo = false;
            this.pMeldFour.AllowReordering = false;
            this.pMeldFour.AllowSelection = false;
            this.pMeldFour.CardAdded = null;
            this.pMeldFour.CardRemoved = null;
            this.pMeldFour.CardSelected = null;
            this.pMeldFour.ColourScheme = twoColourScheme1;
            this.pMeldFour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMeldFour.LastHighlightedCard = null;
            this.pMeldFour.Location = new System.Drawing.Point(3, 307);
            this.pMeldFour.Name = "pMeldFour";
            this.pMeldFour.ShowCards = false;
            this.pMeldFour.Size = new System.Drawing.Size(653, 70);
            this.pMeldFour.TabIndex = 10;
            // 
            // pMeldThree
            // 
            this.pMeldThree.AllowDragFrom = false;
            this.pMeldThree.AllowDragTo = false;
            this.pMeldThree.AllowReordering = false;
            this.pMeldThree.AllowSelection = false;
            this.pMeldThree.CardAdded = null;
            this.pMeldThree.CardRemoved = null;
            this.pMeldThree.CardSelected = null;
            this.pMeldThree.ColourScheme = twoColourScheme2;
            this.pMeldThree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMeldThree.LastHighlightedCard = null;
            this.pMeldThree.Location = new System.Drawing.Point(3, 231);
            this.pMeldThree.Name = "pMeldThree";
            this.pMeldThree.ShowCards = false;
            this.pMeldThree.Size = new System.Drawing.Size(653, 70);
            this.pMeldThree.TabIndex = 9;
            // 
            // pMeldTwo
            // 
            this.pMeldTwo.AllowDragFrom = false;
            this.pMeldTwo.AllowDragTo = false;
            this.pMeldTwo.AllowReordering = false;
            this.pMeldTwo.AllowSelection = false;
            this.pMeldTwo.CardAdded = null;
            this.pMeldTwo.CardRemoved = null;
            this.pMeldTwo.CardSelected = null;
            this.pMeldTwo.ColourScheme = twoColourScheme3;
            this.pMeldTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMeldTwo.LastHighlightedCard = null;
            this.pMeldTwo.Location = new System.Drawing.Point(3, 155);
            this.pMeldTwo.Name = "pMeldTwo";
            this.pMeldTwo.ShowCards = false;
            this.pMeldTwo.Size = new System.Drawing.Size(653, 70);
            this.pMeldTwo.TabIndex = 8;
            // 
            // pMeldOne
            // 
            this.pMeldOne.AllowDragFrom = false;
            this.pMeldOne.AllowDragTo = false;
            this.pMeldOne.AllowReordering = false;
            this.pMeldOne.AllowSelection = false;
            this.pMeldOne.CardAdded = null;
            this.pMeldOne.CardRemoved = null;
            this.pMeldOne.CardSelected = null;
            this.pMeldOne.ColourScheme = twoColourScheme4;
            this.pMeldOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMeldOne.LastHighlightedCard = null;
            this.pMeldOne.Location = new System.Drawing.Point(3, 79);
            this.pMeldOne.Name = "pMeldOne";
            this.pMeldOne.ShowCards = false;
            this.pMeldOne.Size = new System.Drawing.Size(653, 70);
            this.pMeldOne.TabIndex = 7;
            // 
            // pHand
            // 
            this.pHand.AllowDragFrom = false;
            this.pHand.AllowDragTo = false;
            this.pHand.AllowReordering = false;
            this.pHand.AllowSelection = false;
            this.pHand.AutoScroll = true;
            this.pHand.CardAdded = null;
            this.pHand.CardRemoved = null;
            this.pHand.CardSelected = null;
            this.pHand.ColourScheme = twoColourScheme5;
            this.pTableLayout.SetColumnSpan(this.pHand, 2);
            this.pHand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHand.LastHighlightedCard = null;
            this.pHand.Location = new System.Drawing.Point(3, 3);
            this.pHand.Name = "pHand";
            this.pHand.ShowCards = false;
            this.pHand.Size = new System.Drawing.Size(784, 70);
            this.pHand.TabIndex = 3;
            // 
            // MeldCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pTableLayout);
            this.Name = "MeldCreator";
            this.Size = new System.Drawing.Size(851, 427);
            this.pTableLayout.ResumeLayout(false);
            this.pTableLayout.PerformLayout();
            this.pButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel pTableLayout;
        private System.Windows.Forms.Panel pButtons;
        private System.Windows.Forms.Button bAcceptMelds;
        private CardPanel pMeldFour;
        private CardPanel pMeldThree;
        private CardPanel pMeldTwo;
        private CardPanel pMeldOne;
        private CardPanel pHand;
        private System.Windows.Forms.Button bClearMeldFour;
        private System.Windows.Forms.Button bClearMeldThree;
        private System.Windows.Forms.Button bClearMeldOne;
        private System.Windows.Forms.Button bClearMeldTwo;
        private System.Windows.Forms.CheckBox cbMeldOneValid;
        private System.Windows.Forms.CheckBox cbMeldTwoValid;
        private System.Windows.Forms.CheckBox cbMeldThreeValid;
        private System.Windows.Forms.CheckBox cbMeldFourValid;
    }
}
