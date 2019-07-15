namespace gin_rummy.Controls
{
    partial class PlayerActions
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
            this.bKnock = new System.Windows.Forms.Button();
            this.bTake = new System.Windows.Forms.Button();
            this.bDraw = new System.Windows.Forms.Button();
            this.bDiscard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bKnock
            // 
            this.bKnock.Location = new System.Drawing.Point(38, 119);
            this.bKnock.Name = "bKnock";
            this.bKnock.Size = new System.Drawing.Size(75, 23);
            this.bKnock.TabIndex = 0;
            this.bKnock.Text = "Knock";
            this.bKnock.UseVisualStyleBackColor = true;
            this.bKnock.Click += new System.EventHandler(this.bKnock_Click);
            // 
            // bTake
            // 
            this.bTake.Location = new System.Drawing.Point(38, 18);
            this.bTake.Name = "bTake";
            this.bTake.Size = new System.Drawing.Size(75, 23);
            this.bTake.TabIndex = 1;
            this.bTake.Text = "Take discard";
            this.bTake.UseVisualStyleBackColor = true;
            this.bTake.Click += new System.EventHandler(this.bTake_Click);
            // 
            // bDraw
            // 
            this.bDraw.Location = new System.Drawing.Point(38, 47);
            this.bDraw.Name = "bDraw";
            this.bDraw.Size = new System.Drawing.Size(75, 23);
            this.bDraw.TabIndex = 2;
            this.bDraw.Text = "Draw";
            this.bDraw.UseVisualStyleBackColor = true;
            this.bDraw.Click += new System.EventHandler(this.bDraw_Click);
            // 
            // bDiscard
            // 
            this.bDiscard.Location = new System.Drawing.Point(38, 76);
            this.bDiscard.Name = "bDiscard";
            this.bDiscard.Size = new System.Drawing.Size(75, 23);
            this.bDiscard.TabIndex = 3;
            this.bDiscard.Text = "Discard";
            this.bDiscard.UseVisualStyleBackColor = true;
            this.bDiscard.Click += new System.EventHandler(this.bDiscard_Click);
            // 
            // PlayerActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bDiscard);
            this.Controls.Add(this.bDraw);
            this.Controls.Add(this.bTake);
            this.Controls.Add(this.bKnock);
            this.Name = "PlayerActions";
            this.Size = new System.Drawing.Size(153, 157);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bKnock;
        private System.Windows.Forms.Button bTake;
        private System.Windows.Forms.Button bDraw;
        private System.Windows.Forms.Button bDiscard;
    }
}
