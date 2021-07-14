namespace BitBox
{
    partial class BitView
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
            this.bitsArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // bitsArea
            // 
            this.bitsArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bitsArea.Location = new System.Drawing.Point(0, 3);
            this.bitsArea.Name = "bitsArea";
            this.bitsArea.Size = new System.Drawing.Size(824, 465);
            this.bitsArea.TabIndex = 0;
            this.bitsArea.Paint += new System.Windows.Forms.PaintEventHandler(this.bitsArea_Paint);
            this.bitsArea.Resize += new System.EventHandler(this.bitsArea_Resize);
            // 
            // BitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bitsArea);
            this.Name = "BitView";
            this.Size = new System.Drawing.Size(827, 471);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bitsArea;
    }
}