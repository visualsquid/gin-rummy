namespace gin_rummy.Controls
{
    partial class CardStacks
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
            this.pStacks = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pStacks
            // 
            this.pStacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pStacks.Location = new System.Drawing.Point(0, 0);
            this.pStacks.Name = "pStacks";
            this.pStacks.Size = new System.Drawing.Size(356, 167);
            this.pStacks.TabIndex = 1;
            // 
            // CardStacks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pStacks);
            this.Name = "CardStacks";
            this.Size = new System.Drawing.Size(356, 167);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pStacks;
    }
}
