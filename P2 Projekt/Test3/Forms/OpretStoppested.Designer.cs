namespace ProgramTilBusselskab
{
    partial class OpretStoppested
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_coordinate = new System.Windows.Forms.TextBox();
            this.txtbox_stopName = new System.Windows.Forms.TextBox();
            this.bnt_opretStop = new System.Windows.Forms.Button();
            this.txtbox_stoppestedID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Koordinater";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Stoppested navn";
            // 
            // txtbox_coordinate
            // 
            this.txtbox_coordinate.Location = new System.Drawing.Point(148, 38);
            this.txtbox_coordinate.Name = "txtbox_coordinate";
            this.txtbox_coordinate.Size = new System.Drawing.Size(333, 26);
            this.txtbox_coordinate.TabIndex = 20;
            // 
            // txtbox_stopName
            // 
            this.txtbox_stopName.Location = new System.Drawing.Point(148, 6);
            this.txtbox_stopName.Name = "txtbox_stopName";
            this.txtbox_stopName.Size = new System.Drawing.Size(188, 26);
            this.txtbox_stopName.TabIndex = 9;
            // 
            // bnt_opretStop
            // 
            this.bnt_opretStop.Location = new System.Drawing.Point(305, 70);
            this.bnt_opretStop.Name = "bnt_opretStop";
            this.bnt_opretStop.Size = new System.Drawing.Size(176, 50);
            this.bnt_opretStop.TabIndex = 30;
            this.bnt_opretStop.Text = "Opret";
            this.bnt_opretStop.UseVisualStyleBackColor = true;
            this.bnt_opretStop.Click += new System.EventHandler(this.bnt_opretStop_Click);
            // 
            // txtbox_stoppestedID
            // 
            this.txtbox_stoppestedID.Location = new System.Drawing.Point(374, 6);
            this.txtbox_stoppestedID.Name = "txtbox_stoppestedID";
            this.txtbox_stoppestedID.Size = new System.Drawing.Size(107, 26);
            this.txtbox_stoppestedID.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "ID";
            // 
            // OpretStoppested
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 128);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbox_stoppestedID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_coordinate);
            this.Controls.Add(this.txtbox_stopName);
            this.Controls.Add(this.bnt_opretStop);
            this.MaximumSize = new System.Drawing.Size(512, 184);
            this.MinimumSize = new System.Drawing.Size(512, 184);
            this.Name = "OpretStoppested";
            this.Text = "OpretStoppested";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbox_coordinate;
        private System.Windows.Forms.TextBox txtbox_stopName;
        private System.Windows.Forms.Button bnt_opretStop;
        private System.Windows.Forms.TextBox txtbox_stoppestedID;
        private System.Windows.Forms.Label label2;
    }
}