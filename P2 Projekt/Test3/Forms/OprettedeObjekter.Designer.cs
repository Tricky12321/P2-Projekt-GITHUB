﻿namespace ProgramTilBusselskab
{
    partial class OprettedeObjekter
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
            this.treview_oprettedeObjekter = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treview_oprettedeObjekter
            // 
            this.treview_oprettedeObjekter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treview_oprettedeObjekter.Location = new System.Drawing.Point(12, 12);
            this.treview_oprettedeObjekter.Name = "treview_oprettedeObjekter";
            this.treview_oprettedeObjekter.Size = new System.Drawing.Size(538, 486);
            this.treview_oprettedeObjekter.TabIndex = 11;
            this.treview_oprettedeObjekter.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treview_oprettedeObjekter_AfterSelect);
            // 
            // OprettedeObjekter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 510);
            this.Controls.Add(this.treview_oprettedeObjekter);
            this.Name = "OprettedeObjekter";
            this.Text = "OprettedeObjekter";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OprettedeObjekter_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treview_oprettedeObjekter;
    }
}