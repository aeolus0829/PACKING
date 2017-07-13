namespace PACKINGLIST
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClear = new System.Windows.Forms.Button();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtVbeln = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEdatu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbSalesText = new System.Windows.Forms.ListBox();
            this.lblEstDeliveryDate = new System.Windows.Forms.Label();
            this.lblCusName = new System.Windows.Forms.Label();
            this.lblCusNum = new System.Windows.Forms.Label();
            this.gvOrderInput = new System.Windows.Forms.DataGridView();
            this.btnTodb = new System.Windows.Forms.Button();
            this.lblOrderNum = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInput)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(60, 135);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 23);
            this.btnClear.TabIndex = 68;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(14, 135);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(40, 23);
            this.BtnSubmit.TabIndex = 67;
            this.BtnSubmit.Text = "確定";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(990, 408);
            this.dataGridView1.TabIndex = 66;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(106, 135);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(88, 23);
            this.btnConvert.TabIndex = 69;
            this.btnConvert.Text = "產生包裝明細";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtVbeln
            // 
            this.txtVbeln.HeaderText = "訂單號碼";
            this.txtVbeln.Name = "txtVbeln";
            this.txtVbeln.Width = 78;
            // 
            // txtEdatu
            // 
            this.txtEdatu.HeaderText = "第一交貨日";
            this.txtEdatu.Name = "txtEdatu";
            this.txtEdatu.Width = 90;
            // 
            // lbSalesText
            // 
            this.lbSalesText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSalesText.FormattingEnabled = true;
            this.lbSalesText.ItemHeight = 12;
            this.lbSalesText.Location = new System.Drawing.Point(12, 583);
            this.lbSalesText.Name = "lbSalesText";
            this.lbSalesText.Size = new System.Drawing.Size(990, 124);
            this.lbSalesText.TabIndex = 74;
            // 
            // lblEstDeliveryDate
            // 
            this.lblEstDeliveryDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEstDeliveryDate.AutoSize = true;
            this.lblEstDeliveryDate.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEstDeliveryDate.Location = new System.Drawing.Point(712, 145);
            this.lblEstDeliveryDate.Name = "lblEstDeliveryDate";
            this.lblEstDeliveryDate.Size = new System.Drawing.Size(0, 13);
            this.lblEstDeliveryDate.TabIndex = 77;
            // 
            // lblCusName
            // 
            this.lblCusName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCusName.Location = new System.Drawing.Point(712, 117);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(0, 13);
            this.lblCusName.TabIndex = 78;
            // 
            // lblCusNum
            // 
            this.lblCusNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCusNum.AutoSize = true;
            this.lblCusNum.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCusNum.Location = new System.Drawing.Point(712, 90);
            this.lblCusNum.Name = "lblCusNum";
            this.lblCusNum.Size = new System.Drawing.Size(0, 13);
            this.lblCusNum.TabIndex = 75;
            // 
            // gvOrderInput
            // 
            this.gvOrderInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvOrderInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOrderInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtVbeln,
            this.txtEdatu});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvOrderInput.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvOrderInput.Location = new System.Drawing.Point(12, 9);
            this.gvOrderInput.Name = "gvOrderInput";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvOrderInput.RowTemplate.Height = 24;
            this.gvOrderInput.Size = new System.Drawing.Size(211, 119);
            this.gvOrderInput.TabIndex = 70;
            // 
            // btnTodb
            // 
            this.btnTodb.Location = new System.Drawing.Point(200, 135);
            this.btnTodb.Name = "btnTodb";
            this.btnTodb.Size = new System.Drawing.Size(93, 23);
            this.btnTodb.TabIndex = 72;
            this.btnTodb.Text = "產生標籤明細";
            this.btnTodb.UseVisualStyleBackColor = true;
            this.btnTodb.Click += new System.EventHandler(this.btnTodb_Click);
            // 
            // lblOrderNum
            // 
            this.lblOrderNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOrderNum.AutoSize = true;
            this.lblOrderNum.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOrderNum.Location = new System.Drawing.Point(712, 65);
            this.lblOrderNum.Name = "lblOrderNum";
            this.lblOrderNum.Size = new System.Drawing.Size(0, 13);
            this.lblOrderNum.TabIndex = 76;
            this.lblOrderNum.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 717);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1014, 22);
            this.statusStrip.TabIndex = 79;
            // 
            // tsLabel
            // 
            this.tsLabel.Name = "tsLabel";
            this.tsLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 739);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lbSalesText);
            this.Controls.Add(this.lblEstDeliveryDate);
            this.Controls.Add(this.lblCusName);
            this.Controls.Add(this.lblCusNum);
            this.Controls.Add(this.gvOrderInput);
            this.Controls.Add(this.btnTodb);
            this.Controls.Add(this.lblOrderNum);
            this.Name = "Form1";
            this.Text = "PACKING ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInput)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtVbeln;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtEdatu;
        private System.Windows.Forms.ListBox lbSalesText;
        private System.Windows.Forms.Label lblEstDeliveryDate;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.Label lblCusNum;
        private System.Windows.Forms.DataGridView gvOrderInput;
        private System.Windows.Forms.Button btnTodb;
        private System.Windows.Forms.Label lblOrderNum;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsLabel;
    }
}

