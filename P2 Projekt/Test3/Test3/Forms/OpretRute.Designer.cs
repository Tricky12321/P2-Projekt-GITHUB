namespace ProgramTilBusselskab
{
    partial class OpretRute
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
            this.combox_chooseStops = new System.Windows.Forms.ComboBox();
            this.btn_addStop = new System.Windows.Forms.Button();
            this.lstbox_Ruter = new System.Windows.Forms.ListBox();
            this.btn_opretRute = new System.Windows.Forms.Button();
            this.txtbox_ruteName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_ruteID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // combox_chooseStops
            // 
            this.combox_chooseStops.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combox_chooseStops.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_chooseStops.FormattingEnabled = true;
            this.combox_chooseStops.Location = new System.Drawing.Point(110, 41);
            this.combox_chooseStops.Name = "combox_chooseStops";
            this.combox_chooseStops.Size = new System.Drawing.Size(323, 28);
            this.combox_chooseStops.TabIndex = 0;
            // 
            // btn_addStop
            // 
            this.btn_addStop.Location = new System.Drawing.Point(275, 75);
            this.btn_addStop.Name = "btn_addStop";
            this.btn_addStop.Size = new System.Drawing.Size(158, 35);
            this.btn_addStop.TabIndex = 1;
            this.btn_addStop.Text = "Tilføj stoppested";
            this.btn_addStop.UseVisualStyleBackColor = true;
            this.btn_addStop.Click += new System.EventHandler(this.btn_addStop_Click);
            // 
            // lstbox_Ruter
            // 
            this.lstbox_Ruter.FormattingEnabled = true;
            this.lstbox_Ruter.ItemHeight = 20;
            this.lstbox_Ruter.Location = new System.Drawing.Point(439, 9);
            this.lstbox_Ruter.Name = "lstbox_Ruter";
            this.lstbox_Ruter.Size = new System.Drawing.Size(166, 104);
            this.lstbox_Ruter.TabIndex = 2;
            this.lstbox_Ruter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstbox_Ruter_MouseDoubleClick);
            // 
            // btn_opretRute
            // 
            this.btn_opretRute.Location = new System.Drawing.Point(439, 119);
            this.btn_opretRute.Name = "btn_opretRute";
            this.btn_opretRute.Size = new System.Drawing.Size(166, 38);
            this.btn_opretRute.TabIndex = 3;
            this.btn_opretRute.Text = "Opret rute";
            this.btn_opretRute.UseVisualStyleBackColor = true;
            this.btn_opretRute.Click += new System.EventHandler(this.btn_opretRute_Click);
            // 
            // txtbox_ruteName
            // 
            this.txtbox_ruteName.Location = new System.Drawing.Point(110, 9);
            this.txtbox_ruteName.Name = "txtbox_ruteName";
            this.txtbox_ruteName.Size = new System.Drawing.Size(194, 26);
            this.txtbox_ruteName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Stoppested";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Rute navn";
            // 
            // txtbox_ruteID
            // 
            this.txtbox_ruteID.Location = new System.Drawing.Point(342, 9);
            this.txtbox_ruteID.Name = "txtbox_ruteID";
            this.txtbox_ruteID.Size = new System.Drawing.Size(91, 26);
            this.txtbox_ruteID.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "ID";
            // 
            // OpretRute
            // 
            this.AcceptButton = this.btn_addStop;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 161);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbox_ruteID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_ruteName);
            this.Controls.Add(this.btn_opretRute);
            this.Controls.Add(this.lstbox_Ruter);
            this.Controls.Add(this.btn_addStop);
            this.Controls.Add(this.combox_chooseStops);
            this.MaximumSize = new System.Drawing.Size(634, 217);
            this.MinimumSize = new System.Drawing.Size(634, 217);
            this.Name = "OpretRute";
            this.Text = "OpretRute";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OpretRute_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combox_chooseStops;
        private System.Windows.Forms.Button btn_addStop;
        private System.Windows.Forms.ListBox lstbox_Ruter;
        private System.Windows.Forms.Button btn_opretRute;
        private System.Windows.Forms.TextBox txtbox_ruteName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbox_ruteID;
        private System.Windows.Forms.Label label2;
    }
}