namespace zoocool
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.priceoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.featureValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.skusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.featureBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.featureValueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skusBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::zoocool.Properties.Resources.Vextractor_ico_vextractor;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "����������� �����";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priceoldToolStripMenuItem,
            this.productAllToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // priceoldToolStripMenuItem
            // 
            this.priceoldToolStripMenuItem.Name = "priceoldToolStripMenuItem";
            this.priceoldToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.priceoldToolStripMenuItem.Text = "price_old";
            this.priceoldToolStripMenuItem.Click += new System.EventHandler(this.priceoldToolStripMenuItem_Click);
            // 
            // productAllToolStripMenuItem
            // 
            this.productAllToolStripMenuItem.Name = "productAllToolStripMenuItem";
            this.productAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.productAllToolStripMenuItem.Text = "productAll";
            this.productAllToolStripMenuItem.Click += new System.EventHandler(this.productAllToolStripMenuItem_Click);
            // 
            // featureBindingSource
            // 
            this.featureBindingSource.DataSource = typeof(zoocool.Feature);
            // 
            // featureValueBindingSource
            // 
            this.featureValueBindingSource.DataSource = typeof(zoocool.FeatureValue);
            // 
            // productBindingSource
            // 
            this.productBindingSource.DataSource = typeof(zoocool.Product);
            // 
            // skusBindingSource
            // 
            this.skusBindingSource.DataSource = typeof(zoocool.Skus);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Form1";
            this.Text = "����� ��� �������";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.featureBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.featureValueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skusBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.BindingSource productBindingSource;
        private System.Windows.Forms.BindingSource skusBindingSource;
        private System.Windows.Forms.BindingSource featureBindingSource;
        private System.Windows.Forms.BindingSource featureValueBindingSource;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem priceoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productAllToolStripMenuItem;
    }
}

