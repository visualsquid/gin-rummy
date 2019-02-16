namespace gin_rummy.Controls
{
    partial class CardPanel
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
            this.pCards = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pCards
            // 
            this.pCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCards.Location = new System.Drawing.Point(0, 0);
            this.pCards.Name = "pCards";
            this.pCards.Size = new System.Drawing.Size(582, 97);
            this.pCards.TabIndex = 0;
            // 
            // CardPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pCards);
            this.Name = "CardPanel";
            this.Size = new System.Drawing.Size(582, 97);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pCards;
    }
}
