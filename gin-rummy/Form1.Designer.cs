namespace gin_rummy
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
            this.eYourHand = new System.Windows.Forms.TextBox();
            this.lYourHand = new System.Windows.Forms.Label();
            this.bNewHand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eYourHand
            // 
            this.eYourHand.Location = new System.Drawing.Point(128, 172);
            this.eYourHand.Name = "eYourHand";
            this.eYourHand.Size = new System.Drawing.Size(337, 20);
            this.eYourHand.TabIndex = 0;
            // 
            // lYourHand
            // 
            this.lYourHand.AutoSize = true;
            this.lYourHand.Location = new System.Drawing.Point(63, 175);
            this.lYourHand.Name = "lYourHand";
            this.lYourHand.Size = new System.Drawing.Size(59, 13);
            this.lYourHand.TabIndex = 1;
            this.lYourHand.Text = "Your hand:";
            // 
            // bNewHand
            // 
            this.bNewHand.Location = new System.Drawing.Point(179, 63);
            this.bNewHand.Name = "bNewHand";
            this.bNewHand.Size = new System.Drawing.Size(75, 23);
            this.bNewHand.TabIndex = 2;
            this.bNewHand.Text = "New hand";
            this.bNewHand.UseVisualStyleBackColor = true;
            this.bNewHand.Click += new System.EventHandler(this.bNewHand_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 241);
            this.Controls.Add(this.bNewHand);
            this.Controls.Add(this.lYourHand);
            this.Controls.Add(this.eYourHand);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eYourHand;
        private System.Windows.Forms.Label lYourHand;
        private System.Windows.Forms.Button bNewHand;
    }
}

