namespace QuanLyBanCafe.PresentationTier
{
    partial class FrmChonMonTuGiaoDien
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
            this.nupSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDanhMuc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMonAn = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nupSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // nupSoLuong
            // 
            this.nupSoLuong.Location = new System.Drawing.Point(784, 32);
            this.nupSoLuong.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.nupSoLuong.Name = "nupSoLuong";
            this.nupSoLuong.Size = new System.Drawing.Size(114, 38);
            this.nupSoLuong.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(601, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "Số lượng";
            // 
            // cbxDanhMuc
            // 
            this.cbxDanhMuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDanhMuc.FormattingEnabled = true;
            this.cbxDanhMuc.Location = new System.Drawing.Point(252, 28);
            this.cbxDanhMuc.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.cbxDanhMuc.Name = "cbxDanhMuc";
            this.cbxDanhMuc.Size = new System.Drawing.Size(282, 44);
            this.cbxDanhMuc.TabIndex = 7;
            this.cbxDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cbxDanhMuc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "Danh mục";
            // 
            // pnlMonAn
            // 
            this.pnlMonAn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlMonAn.Location = new System.Drawing.Point(37, 97);
            this.pnlMonAn.Name = "pnlMonAn";
            this.pnlMonAn.Size = new System.Drawing.Size(861, 348);
            this.pnlMonAn.TabIndex = 9;
            // 
            // FrmChonMonTuGiaoDien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 481);
            this.Controls.Add(this.pnlMonAn);
            this.Controls.Add(this.nupSoLuong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDanhMuc);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmChonMonTuGiaoDien";
            this.Text = "FrmChonMonTuGiaoDien";
            ((System.ComponentModel.ISupportInitialize)(this.nupSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nupSoLuong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDanhMuc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMonAn;
    }
}