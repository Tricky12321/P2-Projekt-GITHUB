namespace ProgramTilBusselskab
{
    partial class OpretBus
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
            this.components = new System.ComponentModel.Container();
            this.bnt_opretBus = new System.Windows.Forms.Button();
            this.txtbox_busName = new System.Windows.Forms.TextBox();
            this.txtbox_capacitySit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stoppestedDataAfPåstigningBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.combox_vælgRute = new System.Windows.Forms.ComboBox();
            this.btn_refreshStoppesteder = new System.Windows.Forms.Button();
            this.treeview_stopMTidspunkter = new System.Windows.Forms.TreeView();
            this.txtbox_capacityStå = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.combox_vælgStop = new System.Windows.Forms.ComboBox();
            this.txtbox_tidspunkt = new System.Windows.Forms.TextBox();
            this.lstbox_tidspunkter = new System.Windows.Forms.ListBox();
            this.btn_tilføjTidspunkt = new System.Windows.Forms.Button();
            this.btn_tilføjStoppested = new System.Windows.Forms.Button();
            this.ruteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbl_ruteSucces = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_busID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.stoppestedDataAfPåstigningBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bnt_opretBus
            // 
            this.bnt_opretBus.Location = new System.Drawing.Point(593, 240);
            this.bnt_opretBus.Name = "bnt_opretBus";
            this.bnt_opretBus.Size = new System.Drawing.Size(329, 59);
            this.bnt_opretBus.TabIndex = 30;
            this.bnt_opretBus.Text = "Opret bus";
            this.bnt_opretBus.UseVisualStyleBackColor = true;
            this.bnt_opretBus.Click += new System.EventHandler(this.bnt_opretBus_Click);
            // 
            // txtbox_busName
            // 
            this.txtbox_busName.Location = new System.Drawing.Point(159, 6);
            this.txtbox_busName.Name = "txtbox_busName";
            this.txtbox_busName.Size = new System.Drawing.Size(157, 26);
            this.txtbox_busName.TabIndex = 1;
            // 
            // txtbox_capacitySit
            // 
            this.txtbox_capacitySit.Location = new System.Drawing.Point(269, 40);
            this.txtbox_capacitySit.Name = "txtbox_capacitySit";
            this.txtbox_capacitySit.Size = new System.Drawing.Size(47, 26);
            this.txtbox_capacitySit.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bus navn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kapacitet";
            // 
            // combox_vælgRute
            // 
            this.combox_vælgRute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combox_vælgRute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_vælgRute.FormattingEnabled = true;
            this.combox_vælgRute.Location = new System.Drawing.Point(159, 72);
            this.combox_vælgRute.Name = "combox_vælgRute";
            this.combox_vælgRute.Size = new System.Drawing.Size(304, 28);
            this.combox_vælgRute.TabIndex = 11;
            // 
            // btn_refreshStoppesteder
            // 
            this.btn_refreshStoppesteder.Location = new System.Drawing.Point(469, 68);
            this.btn_refreshStoppesteder.Name = "btn_refreshStoppesteder";
            this.btn_refreshStoppesteder.Size = new System.Drawing.Size(118, 35);
            this.btn_refreshStoppesteder.TabIndex = 13;
            this.btn_refreshStoppesteder.Text = "Vælg";
            this.btn_refreshStoppesteder.UseVisualStyleBackColor = true;
            this.btn_refreshStoppesteder.Click += new System.EventHandler(this.btn_refreshStoppesteder_Click);
            // 
            // treeview_stopMTidspunkter
            // 
            this.treeview_stopMTidspunkter.Location = new System.Drawing.Point(593, 6);
            this.treeview_stopMTidspunkter.Name = "treeview_stopMTidspunkter";
            this.treeview_stopMTidspunkter.Size = new System.Drawing.Size(329, 228);
            this.treeview_stopMTidspunkter.TabIndex = 10;
            this.treeview_stopMTidspunkter.DoubleClick += new System.EventHandler(this.treeview_stopMTidspunkter_DoubleClick);
            // 
            // txtbox_capacityStå
            // 
            this.txtbox_capacityStå.Location = new System.Drawing.Point(416, 40);
            this.txtbox_capacityStå.Name = "txtbox_capacityStå";
            this.txtbox_capacityStå.Size = new System.Drawing.Size(47, 26);
            this.txtbox_capacityStå.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Siddepladser";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ståpladser";
            // 
            // combox_vælgStop
            // 
            this.combox_vælgStop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combox_vælgStop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_vælgStop.FormattingEnabled = true;
            this.combox_vælgStop.Location = new System.Drawing.Point(159, 169);
            this.combox_vælgStop.Name = "combox_vælgStop";
            this.combox_vælgStop.Size = new System.Drawing.Size(304, 28);
            this.combox_vælgStop.TabIndex = 32;
            // 
            // txtbox_tidspunkt
            // 
            this.txtbox_tidspunkt.Location = new System.Drawing.Point(159, 203);
            this.txtbox_tidspunkt.Name = "txtbox_tidspunkt";
            this.txtbox_tidspunkt.Size = new System.Drawing.Size(304, 26);
            this.txtbox_tidspunkt.TabIndex = 33;
            // 
            // lstbox_tidspunkter
            // 
            this.lstbox_tidspunkter.FormattingEnabled = true;
            this.lstbox_tidspunkter.ItemHeight = 20;
            this.lstbox_tidspunkter.Location = new System.Drawing.Point(16, 235);
            this.lstbox_tidspunkter.Name = "lstbox_tidspunkter";
            this.lstbox_tidspunkter.Size = new System.Drawing.Size(447, 64);
            this.lstbox_tidspunkter.TabIndex = 34;
            this.lstbox_tidspunkter.DoubleClick += new System.EventHandler(this.lstbox_tidspunkter_DoubleClick_1);
            // 
            // btn_tilføjTidspunkt
            // 
            this.btn_tilføjTidspunkt.Location = new System.Drawing.Point(469, 175);
            this.btn_tilføjTidspunkt.Name = "btn_tilføjTidspunkt";
            this.btn_tilføjTidspunkt.Size = new System.Drawing.Size(118, 59);
            this.btn_tilføjTidspunkt.TabIndex = 34;
            this.btn_tilføjTidspunkt.Text = "Tilføj tidspunkt";
            this.btn_tilføjTidspunkt.UseVisualStyleBackColor = true;
            this.btn_tilføjTidspunkt.Click += new System.EventHandler(this.btn_tilføjTidspunkt_Click_1);
            // 
            // btn_tilføjStoppested
            // 
            this.btn_tilføjStoppested.Location = new System.Drawing.Point(469, 240);
            this.btn_tilføjStoppested.Name = "btn_tilføjStoppested";
            this.btn_tilføjStoppested.Size = new System.Drawing.Size(118, 59);
            this.btn_tilføjStoppested.TabIndex = 35;
            this.btn_tilføjStoppested.Text = "Tilføj Stoppested";
            this.btn_tilføjStoppested.UseVisualStyleBackColor = true;
            this.btn_tilføjStoppested.Click += new System.EventHandler(this.btn_tilføjStoppested_Click_1);
            // 
            // ruteBindingSource
            // 
            this.ruteBindingSource.DataSource = typeof(ProgramTilBusselskab.Rute);
            // 
            // lbl_ruteSucces
            // 
            this.lbl_ruteSucces.AutoSize = true;
            this.lbl_ruteSucces.Location = new System.Drawing.Point(468, 106);
            this.lbl_ruteSucces.Name = "lbl_ruteSucces";
            this.lbl_ruteSucces.Size = new System.Drawing.Size(81, 20);
            this.lbl_ruteSucces.TabIndex = 36;
            this.lbl_ruteSucces.Text = "Rute valgt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 38;
            this.label5.Text = "Rute";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 20);
            this.label6.TabIndex = 39;
            this.label6.Text = "Stoppesteder";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 20);
            this.label7.TabIndex = 40;
            this.label7.Text = "Tidspunkt (TT:MM)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(322, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 20);
            this.label8.TabIndex = 41;
            this.label8.Text = "ID";
            // 
            // txt_busID
            // 
            this.txt_busID.Location = new System.Drawing.Point(354, 8);
            this.txt_busID.Name = "txt_busID";
            this.txt_busID.Size = new System.Drawing.Size(109, 26);
            this.txt_busID.TabIndex = 2;
            // 
            // OpretBus
            // 
            this.AcceptButton = this.btn_tilføjTidspunkt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 306);
            this.Controls.Add(this.txt_busID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_ruteSucces);
            this.Controls.Add(this.combox_vælgStop);
            this.Controls.Add(this.txtbox_tidspunkt);
            this.Controls.Add(this.lstbox_tidspunkter);
            this.Controls.Add(this.btn_tilføjTidspunkt);
            this.Controls.Add(this.btn_tilføjStoppested);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbox_capacityStå);
            this.Controls.Add(this.btn_refreshStoppesteder);
            this.Controls.Add(this.combox_vælgRute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_capacitySit);
            this.Controls.Add(this.txtbox_busName);
            this.Controls.Add(this.bnt_opretBus);
            this.Controls.Add(this.treeview_stopMTidspunkter);
            this.MaximumSize = new System.Drawing.Size(952, 362);
            this.MinimumSize = new System.Drawing.Size(952, 362);
            this.Name = "OpretBus";
            this.Text = "OpretBus";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OpretBus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stoppestedDataAfPåstigningBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_opretBus;
        private System.Windows.Forms.TextBox txtbox_busName;
        private System.Windows.Forms.TextBox txtbox_capacitySit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource ruteBindingSource;
        private System.Windows.Forms.BindingSource stoppestedDataAfPåstigningBindingSource;
        private System.Windows.Forms.ComboBox combox_vælgRute;
        private System.Windows.Forms.Button btn_refreshStoppesteder;
        private System.Windows.Forms.TreeView treeview_stopMTidspunkter;
        private System.Windows.Forms.TextBox txtbox_capacityStå;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combox_vælgStop;
        private System.Windows.Forms.TextBox txtbox_tidspunkt;
        private System.Windows.Forms.ListBox lstbox_tidspunkter;
        private System.Windows.Forms.Button btn_tilføjTidspunkt;
        private System.Windows.Forms.Button btn_tilføjStoppested;
        private System.Windows.Forms.Label lbl_ruteSucces;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_busID;
    }
}