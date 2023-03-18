
namespace DADApp.best
{
    partial class Best
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
            System.Windows.Forms.Label label1;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MobsTreeView = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.werwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EXPLabel = new System.Windows.Forms.Label();
            this.WarnLabel = new System.Windows.Forms.Label();
            this.secondPartName = new System.Windows.Forms.Label();
            this.labelRace = new System.Windows.Forms.Label();
            this.firstPartName = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.KDImage = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.KDLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KDImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MobsTreeView);
            this.groupBox1.Location = new System.Drawing.Point(13, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 805);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // MobsTreeView
            // 
            this.MobsTreeView.Location = new System.Drawing.Point(7, 19);
            this.MobsTreeView.Name = "MobsTreeView";
            this.MobsTreeView.Size = new System.Drawing.Size(259, 780);
            this.MobsTreeView.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.werwToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1317, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // werwToolStripMenuItem
            // 
            this.werwToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eToolStripMenuItem});
            this.werwToolStripMenuItem.Name = "werwToolStripMenuItem";
            this.werwToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.werwToolStripMenuItem.Text = "Меню существ";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.eToolStripMenuItem.Text = "Добавить существо";
            this.eToolStripMenuItem.Click += new System.EventHandler(this.eToolStripMenuItem_Click);
            // 
            // EXPLabel
            // 
            this.EXPLabel.AutoSize = true;
            this.EXPLabel.Font = new System.Drawing.Font("Nodesto Cyrillic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EXPLabel.Location = new System.Drawing.Point(649, 86);
            this.EXPLabel.Name = "EXPLabel";
            this.EXPLabel.Size = new System.Drawing.Size(150, 53);
            this.EXPLabel.TabIndex = 20;
            this.EXPLabel.Text = "(99999)";
            // 
            // WarnLabel
            // 
            this.WarnLabel.AutoSize = true;
            this.WarnLabel.Font = new System.Drawing.Font("Nodesto Cyrillic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WarnLabel.Location = new System.Drawing.Point(589, 70);
            this.WarnLabel.Name = "WarnLabel";
            this.WarnLabel.Size = new System.Drawing.Size(80, 70);
            this.WarnLabel.TabIndex = 19;
            this.WarnLabel.Text = "99";
            // 
            // secondPartName
            // 
            this.secondPartName.AutoSize = true;
            this.secondPartName.BackColor = System.Drawing.Color.Transparent;
            this.secondPartName.Font = new System.Drawing.Font("Nodesto Cyrillic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondPartName.Location = new System.Drawing.Point(381, 70);
            this.secondPartName.Name = "secondPartName";
            this.secondPartName.Size = new System.Drawing.Size(106, 70);
            this.secondPartName.TabIndex = 17;
            this.secondPartName.Text = "ame";
            // 
            // labelRace
            // 
            this.labelRace.AutoSize = true;
            this.labelRace.Font = new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRace.Location = new System.Drawing.Point(299, 138);
            this.labelRace.Name = "labelRace";
            this.labelRace.Size = new System.Drawing.Size(346, 40);
            this.labelRace.TabIndex = 13;
            this.labelRace.Text = "Расса, мировоззрение";
            // 
            // firstPartName
            // 
            this.firstPartName.AutoSize = true;
            this.firstPartName.Font = new System.Drawing.Font("Volbera", 71.99999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstPartName.Location = new System.Drawing.Point(288, 33);
            this.firstPartName.Name = "firstPartName";
            this.firstPartName.Size = new System.Drawing.Size(121, 107);
            this.firstPartName.TabIndex = 12;
            this.firstPartName.Text = "N";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DADApp.Properties.Resources.heart;
            this.pictureBox1.Location = new System.Drawing.Point(393, 181);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(108, 90);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // KDImage
            // 
            this.KDImage.BackColor = System.Drawing.Color.Transparent;
            this.KDImage.Image = global::DADApp.Properties.Resources.Без_имени_1;
            this.KDImage.Location = new System.Drawing.Point(306, 181);
            this.KDImage.Name = "KDImage";
            this.KDImage.Size = new System.Drawing.Size(74, 90);
            this.KDImage.TabIndex = 21;
            this.KDImage.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DADApp.Properties.Resources.icons8_ошибка_64_2;
            this.pictureBox2.Location = new System.Drawing.Point(550, 97);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 43);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label1.ForeColor = System.Drawing.Color.Black;
            label1.Location = new System.Drawing.Point(429, 191);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 17);
            label1.TabIndex = 24;
            label1.Text = "label1";
            label1.UseCompatibleTextRendering = true;
            label1.UseMnemonic = false;
            label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // KDLabel
            // 
            this.KDLabel.AutoSize = true;
            this.KDLabel.BackColor = System.Drawing.Color.DimGray;
            this.KDLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KDLabel.Font = new System.Drawing.Font("Nodesto Cyrillic", 30F);
            this.KDLabel.Location = new System.Drawing.Point(316, 191);
            this.KDLabel.Name = "KDLabel";
            this.KDLabel.Size = new System.Drawing.Size(51, 44);
            this.KDLabel.TabIndex = 25;
            this.KDLabel.Text = "12";
            // 
            // Best
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 844);
            this.Controls.Add(this.KDLabel);
            this.Controls.Add(label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.EXPLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.WarnLabel);
            this.Controls.Add(this.secondPartName);
            this.Controls.Add(this.labelRace);
            this.Controls.Add(this.firstPartName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.KDImage);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Best";
            this.Text = "Best";
            this.Load += new System.EventHandler(this.Best_Load);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KDImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView MobsTreeView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem werwToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.Label EXPLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label WarnLabel;
        private System.Windows.Forms.Label secondPartName;
        private System.Windows.Forms.Label labelRace;
        private System.Windows.Forms.Label firstPartName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox KDImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label KDLabel;
    }
}