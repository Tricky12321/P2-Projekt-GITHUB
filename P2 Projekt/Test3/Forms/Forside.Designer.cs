namespace ProgramTilBusselskab
{
    partial class Simulation
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
            this.gMapsMap = new GMap.NET.WindowsForms.GMapControl();
            this.btn_opretBus = new System.Windows.Forms.Button();
            this.btn_opretStoppested = new System.Windows.Forms.Button();
            this.btn_opretRute = new System.Windows.Forms.Button();
            this.combox_ruterTilVisning = new System.Windows.Forms.ComboBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_visPåKort = new System.Windows.Forms.Button();
            this.btn_clearMap = new System.Windows.Forms.Button();
            this.chkbox_toggleStoppesteder = new System.Windows.Forms.CheckBox();
            this.btn_visOprettede = new System.Windows.Forms.Button();
            this.btn_visBusPåRute = new System.Windows.Forms.Button();
            this.combox_vælgBus = new System.Windows.Forms.ComboBox();
            this.chkbox_medRute = new System.Windows.Forms.CheckBox();
            this.timer_UpdateMap = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gMapsMap
            // 
            this.gMapsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapsMap.AutoSize = true;
            this.gMapsMap.Bearing = 0F;
            this.gMapsMap.CanDragMap = true;
            this.gMapsMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapsMap.GrayScaleMode = false;
            this.gMapsMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapsMap.LevelsKeepInMemmory = 5;
            this.gMapsMap.Location = new System.Drawing.Point(12, 59);
            this.gMapsMap.MarkersEnabled = true;
            this.gMapsMap.MaxZoom = 22;
            this.gMapsMap.MinZoom = 2;
            this.gMapsMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapsMap.Name = "gMapsMap";
            this.gMapsMap.NegativeMode = false;
            this.gMapsMap.PolygonsEnabled = true;
            this.gMapsMap.RetryLoadTile = 0;
            this.gMapsMap.RoutesEnabled = true;
            this.gMapsMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapsMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapsMap.ShowTileGridLines = false;
            this.gMapsMap.Size = new System.Drawing.Size(1021, 523);
            this.gMapsMap.TabIndex = 0;
            this.gMapsMap.Zoom = 13D;
            this.gMapsMap.Load += new System.EventHandler(this.gMapsMap_Load);
            // 
            // btn_opretBus
            // 
            this.btn_opretBus.Location = new System.Drawing.Point(12, 12);
            this.btn_opretBus.Name = "btn_opretBus";
            this.btn_opretBus.Size = new System.Drawing.Size(149, 41);
            this.btn_opretBus.TabIndex = 1;
            this.btn_opretBus.Text = "Opret bus";
            this.btn_opretBus.UseVisualStyleBackColor = true;
            this.btn_opretBus.Click += new System.EventHandler(this.btn_opretBus_Click);
            // 
            // btn_opretStoppested
            // 
            this.btn_opretStoppested.Location = new System.Drawing.Point(167, 12);
            this.btn_opretStoppested.Name = "btn_opretStoppested";
            this.btn_opretStoppested.Size = new System.Drawing.Size(149, 41);
            this.btn_opretStoppested.TabIndex = 2;
            this.btn_opretStoppested.Text = "Opret stoppested";
            this.btn_opretStoppested.UseVisualStyleBackColor = true;
            this.btn_opretStoppested.Click += new System.EventHandler(this.btn_opretStoppested_Click);
            // 
            // btn_opretRute
            // 
            this.btn_opretRute.Location = new System.Drawing.Point(322, 12);
            this.btn_opretRute.Name = "btn_opretRute";
            this.btn_opretRute.Size = new System.Drawing.Size(149, 41);
            this.btn_opretRute.TabIndex = 3;
            this.btn_opretRute.Text = "Opret rute";
            this.btn_opretRute.UseVisualStyleBackColor = true;
            this.btn_opretRute.Click += new System.EventHandler(this.btn_opretRute_Click);
            // 
            // combox_ruterTilVisning
            // 
            this.combox_ruterTilVisning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_ruterTilVisning.FormattingEnabled = true;
            this.combox_ruterTilVisning.Location = new System.Drawing.Point(624, 648);
            this.combox_ruterTilVisning.Name = "combox_ruterTilVisning";
            this.combox_ruterTilVisning.Size = new System.Drawing.Size(409, 28);
            this.combox_ruterTilVisning.TabIndex = 5;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.Location = new System.Drawing.Point(897, 12);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(136, 41);
            this.btn_refresh.TabIndex = 6;
            this.btn_refresh.Text = "Opdater vindue";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_visPåKort
            // 
            this.btn_visPåKort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_visPåKort.Location = new System.Drawing.Point(785, 601);
            this.btn_visPåKort.Name = "btn_visPåKort";
            this.btn_visPåKort.Size = new System.Drawing.Size(121, 41);
            this.btn_visPåKort.TabIndex = 7;
            this.btn_visPåKort.Text = "Vis på kort";
            this.btn_visPåKort.UseVisualStyleBackColor = true;
            this.btn_visPåKort.Click += new System.EventHandler(this.btn_visPåKort_Click);
            // 
            // btn_clearMap
            // 
            this.btn_clearMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clearMap.Location = new System.Drawing.Point(912, 601);
            this.btn_clearMap.Name = "btn_clearMap";
            this.btn_clearMap.Size = new System.Drawing.Size(121, 41);
            this.btn_clearMap.TabIndex = 8;
            this.btn_clearMap.Text = "Nulstil kort";
            this.btn_clearMap.UseVisualStyleBackColor = true;
            this.btn_clearMap.Click += new System.EventHandler(this.btn_clearMap_Click);
            // 
            // chkbox_toggleStoppesteder
            // 
            this.chkbox_toggleStoppesteder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbox_toggleStoppesteder.AutoSize = true;
            this.chkbox_toggleStoppesteder.Location = new System.Drawing.Point(624, 610);
            this.chkbox_toggleStoppesteder.Name = "chkbox_toggleStoppesteder";
            this.chkbox_toggleStoppesteder.Size = new System.Drawing.Size(155, 24);
            this.chkbox_toggleStoppesteder.TabIndex = 9;
            this.chkbox_toggleStoppesteder.Text = "Vis stoppesteder";
            this.chkbox_toggleStoppesteder.UseVisualStyleBackColor = true;
            // 
            // btn_visOprettede
            // 
            this.btn_visOprettede.Location = new System.Drawing.Point(477, 12);
            this.btn_visOprettede.Name = "btn_visOprettede";
            this.btn_visOprettede.Size = new System.Drawing.Size(274, 41);
            this.btn_visOprettede.TabIndex = 11;
            this.btn_visOprettede.Text = "Vis allerede oprettede objekter";
            this.btn_visOprettede.UseVisualStyleBackColor = true;
            this.btn_visOprettede.Click += new System.EventHandler(this.btn_visOprettede_Click);
            // 
            // btn_visBusPåRute
            // 
            this.btn_visBusPåRute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_visBusPåRute.Location = new System.Drawing.Point(281, 601);
            this.btn_visBusPåRute.Name = "btn_visBusPåRute";
            this.btn_visBusPåRute.Size = new System.Drawing.Size(140, 41);
            this.btn_visBusPåRute.TabIndex = 12;
            this.btn_visBusPåRute.Text = "Vis bus";
            this.btn_visBusPåRute.UseVisualStyleBackColor = true;
            this.btn_visBusPåRute.Click += new System.EventHandler(this.btn_visBusPåRute_Click);
            // 
            // combox_vælgBus
            // 
            this.combox_vælgBus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.combox_vælgBus.FormattingEnabled = true;
            this.combox_vælgBus.Location = new System.Drawing.Point(12, 648);
            this.combox_vælgBus.Name = "combox_vælgBus";
            this.combox_vælgBus.Size = new System.Drawing.Size(409, 28);
            this.combox_vælgBus.TabIndex = 13;
            // 
            // chkbox_medRute
            // 
            this.chkbox_medRute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkbox_medRute.AutoSize = true;
            this.chkbox_medRute.Location = new System.Drawing.Point(12, 610);
            this.chkbox_medRute.Name = "chkbox_medRute";
            this.chkbox_medRute.Size = new System.Drawing.Size(219, 24);
            this.chkbox_medRute.TabIndex = 14;
            this.chkbox_medRute.Text = "Vis rute sammen med bus";
            this.chkbox_medRute.UseVisualStyleBackColor = true;
            // 
            // timer_UpdateMap
            // 
            this.timer_UpdateMap.Interval = 1000;
            this.timer_UpdateMap.Tick += new System.EventHandler(this.timer_UpdateMap_Tick);
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1045, 688);
            this.Controls.Add(this.chkbox_medRute);
            this.Controls.Add(this.combox_vælgBus);
            this.Controls.Add(this.btn_visBusPåRute);
            this.Controls.Add(this.btn_visOprettede);
            this.Controls.Add(this.chkbox_toggleStoppesteder);
            this.Controls.Add(this.btn_clearMap);
            this.Controls.Add(this.btn_visPåKort);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.combox_ruterTilVisning);
            this.Controls.Add(this.btn_opretRute);
            this.Controls.Add(this.btn_opretStoppested);
            this.Controls.Add(this.btn_opretBus);
            this.Controls.Add(this.gMapsMap);
            this.MinimumSize = new System.Drawing.Size(1067, 744);
            this.Name = "Simulation";
            this.Text = "Bus simulering";
            this.Load += new System.EventHandler(this.Simulation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapsMap;
        private System.Windows.Forms.Button btn_opretBus;
        private System.Windows.Forms.Button btn_opretStoppested;
        private System.Windows.Forms.Button btn_opretRute;
        private System.Windows.Forms.ComboBox combox_ruterTilVisning;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_visPåKort;
        private System.Windows.Forms.Button btn_clearMap;
        private System.Windows.Forms.CheckBox chkbox_toggleStoppesteder;
        private System.Windows.Forms.Button btn_visOprettede;
        private System.Windows.Forms.Button btn_visBusPåRute;
        private System.Windows.Forms.ComboBox combox_vælgBus;
        private System.Windows.Forms.CheckBox chkbox_medRute;
        private System.Windows.Forms.Timer timer_UpdateMap;
    }
}

