using System;
using System.Drawing;

namespace BrowserWrapper
{
    partial class BrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.exitButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // 
            // 
            this.toolStripContainer.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.BottomToolStripPanel.Name = "";
            this.toolStripContainer.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripContainer.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            // 
            // 
            // 
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1216, 943);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.toolStripContainer.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.LeftToolStripPanel.Name = "";
            this.toolStripContainer.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripContainer.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.toolStripContainer.LeftToolStripPanel.Visible = false;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // 
            // 
            this.toolStripContainer.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.RightToolStripPanel.Name = "";
            this.toolStripContainer.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripContainer.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.toolStripContainer.RightToolStripPanel.Visible = false;
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(1216, 943);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // 
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.TopToolStripPanel.Name = "";
            this.toolStripContainer.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripContainer.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.toolStripContainer.TopToolStripPanel.Size = new System.Drawing.Size(1216, 35);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1216, 34);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(299, 29);
            this.exitButton.Text = "Exit and return to calling application";
            this.exitButton.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 943);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "BrowserForm";
            this.Text = "BrowserForm";
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ToolStripButton exitButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;

        #endregion
    }
}