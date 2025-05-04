namespace WindowsFormsApp1.Presentation
{
    partial class FormPostViewingAdmin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPostTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvCmt = new System.Windows.Forms.DataGridView();
            this.btnCmt = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnAcp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.rTxtContent = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên bài báo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tác giả";
            // 
            // txtPostTitle
            // 
            this.txtPostTitle.Location = new System.Drawing.Point(82, 6);
            this.txtPostTitle.Name = "txtPostTitle";
            this.txtPostTitle.ReadOnly = true;
            this.txtPostTitle.Size = new System.Drawing.Size(158, 20);
            this.txtPostTitle.TabIndex = 2;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(82, 34);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(158, 20);
            this.txtAuthor.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nội dung";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Thể loại";
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(339, 6);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(168, 20);
            this.txtCategory.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Bình luận";
            // 
            // dgvCmt
            // 
            this.dgvCmt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCmt.Location = new System.Drawing.Point(15, 273);
            this.dgvCmt.Name = "dgvCmt";
            this.dgvCmt.Size = new System.Drawing.Size(773, 119);
            this.dgvCmt.TabIndex = 10;
            this.dgvCmt.SelectionChanged += new System.EventHandler(this.dgvCmt_SelectionChanged);
            // 
            // btnCmt
            // 
            this.btnCmt.Location = new System.Drawing.Point(12, 437);
            this.btnCmt.Name = "btnCmt";
            this.btnCmt.Size = new System.Drawing.Size(130, 23);
            this.btnCmt.TabIndex = 12;
            this.btnCmt.Text = "Thêm bình luận";
            this.btnCmt.UseVisualStyleBackColor = true;
            this.btnCmt.Click += new System.EventHandler(this.btnCmt_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(713, 437);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 13;
            this.btnReturn.Text = "Quay lại";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Trạng thái";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(349, 34);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(158, 20);
            this.txtStatus.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Nhập nội dung phản hồi";
            // 
            // txtResponse
            // 
            this.txtResponse.Enabled = false;
            this.txtResponse.Location = new System.Drawing.Point(12, 411);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(776, 20);
            this.txtResponse.TabIndex = 18;
            // 
            // btnAcp
            // 
            this.btnAcp.Enabled = false;
            this.btnAcp.Location = new System.Drawing.Point(713, 6);
            this.btnAcp.Name = "btnAcp";
            this.btnAcp.Size = new System.Drawing.Size(75, 23);
            this.btnAcp.TabIndex = 19;
            this.btnAcp.Text = "Duyệt";
            this.btnAcp.UseVisualStyleBackColor = true;
            this.btnAcp.Click += new System.EventHandler(this.btnAcp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(713, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Không duyệt";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(165, 437);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(135, 23);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "Chỉnh sửa bình luận";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(320, 437);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Xóa bình luận";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // rTxtContent
            // 
            this.rTxtContent.Location = new System.Drawing.Point(15, 76);
            this.rTxtContent.Name = "rTxtContent";
            this.rTxtContent.ReadOnly = true;
            this.rTxtContent.Size = new System.Drawing.Size(773, 178);
            this.rTxtContent.TabIndex = 11;
            this.rTxtContent.Text = "";
            // 
            // FormPostViewingAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 479);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAcp);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCmt);
            this.Controls.Add(this.rTxtContent);
            this.Controls.Add(this.dgvCmt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtPostTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPostViewingAdmin";
            this.Text = "FormPostViewing";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPostTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvCmt;
        private System.Windows.Forms.Button btnCmt;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnAcp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RichTextBox rTxtContent;
    }
}