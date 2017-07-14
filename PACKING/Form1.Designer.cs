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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClear = new System.Windows.Forms.Button();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.dgvPacking = new System.Windows.Forms.DataGridView();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtVbeln = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEdatu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbSalesText = new System.Windows.Forms.ListBox();
            this.lblEstDeliveryDate = new System.Windows.Forms.Label();
            this.lblCusName = new System.Windows.Forms.Label();
            this.lblCusNum = new System.Windows.Forms.Label();
            this.gvOrderInput = new System.Windows.Forms.DataGridView();
            this.lblOrderNum = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInput)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(183, 134);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 23);
            this.btnClear.TabIndex = 82;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Location = new System.Drawing.Point(14, 135);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(40, 23);
            this.BtnSubmit.TabIndex = 80;
            this.BtnSubmit.Text = "確定";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // dgvPacking
            // 
            this.dgvPacking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPacking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvPacking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPacking.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvPacking.Location = new System.Drawing.Point(12, 169);
            this.dgvPacking.Name = "dgvPacking";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPacking.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvPacking.RowTemplate.Height = 24;
            this.dgvPacking.Size = new System.Drawing.Size(990, 408);
            this.dgvPacking.TabIndex = 66;
            this.dgvPacking.TabStop = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(60, 134);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(88, 23);
            this.btnConvert.TabIndex = 81;
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
            this.lbSalesText.UseTabStops = false;
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
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.gvOrderInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOrderInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtVbeln,
            this.txtEdatu});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvOrderInput.DefaultCellStyle = dataGridViewCellStyle23;
            this.gvOrderInput.Location = new System.Drawing.Point(12, 9);
            this.gvOrderInput.Name = "gvOrderInput";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.gvOrderInput.RowTemplate.Height = 24;
            this.gvOrderInput.Size = new System.Drawing.Size(211, 119);
            this.gvOrderInput.TabIndex = 70;
            this.gvOrderInput.TabStop = false;
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
            this.Controls.Add(this.dgvPacking);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lbSalesText);
            this.Controls.Add(this.lblEstDeliveryDate);
            this.Controls.Add(this.lblCusName);
            this.Controls.Add(this.lblCusNum);
            this.Controls.Add(this.gvOrderInput);
            this.Controls.Add(this.lblOrderNum);
            this.Name = "Form1";
            this.Text = "PACKING ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInput)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.DataGridView dgvPacking;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtVbeln;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtEdatu;
        private System.Windows.Forms.ListBox lbSalesText;
        private System.Windows.Forms.Label lblEstDeliveryDate;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.Label lblCusNum;
        private System.Windows.Forms.DataGridView gvOrderInput;
        private System.Windows.Forms.Label lblOrderNum;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsLabel;
    }
}

