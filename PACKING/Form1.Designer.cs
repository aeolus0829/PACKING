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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnClear.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.btnClear.Location = new System.Drawing.Point(271, 135);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 28);
            this.btnClear.TabIndex = 82;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.BtnSubmit.Location = new System.Drawing.Point(12, 135);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(72, 28);
            this.BtnSubmit.TabIndex = 80;
            this.BtnSubmit.Text = "確定";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // dgvPacking
            // 
            dataGridViewCellStyle21.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dgvPacking.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvPacking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("PMingLiU", 12F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPacking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvPacking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("PMingLiU", 12F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPacking.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvPacking.Location = new System.Drawing.Point(12, 169);
            this.dgvPacking.Name = "dgvPacking";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("PMingLiU", 12F);
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPacking.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dgvPacking.RowsDefaultCellStyle = dataGridViewCellStyle25;
            this.dgvPacking.RowTemplate.Height = 24;
            this.dgvPacking.Size = new System.Drawing.Size(990, 408);
            this.dgvPacking.TabIndex = 66;
            this.dgvPacking.TabStop = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.btnConvert.Location = new System.Drawing.Point(90, 134);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(120, 29);
            this.btnConvert.TabIndex = 81;
            this.btnConvert.Text = "產生包裝明細";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtVbeln
            // 
            this.txtVbeln.HeaderText = "訂單號碼";
            this.txtVbeln.Name = "txtVbeln";
            this.txtVbeln.Width = 97;
            // 
            // txtEdatu
            // 
            this.txtEdatu.HeaderText = "第一交貨日";
            this.txtEdatu.Name = "txtEdatu";
            this.txtEdatu.Width = 113;
            // 
            // lbSalesText
            // 
            this.lbSalesText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSalesText.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.lbSalesText.FormattingEnabled = true;
            this.lbSalesText.ItemHeight = 16;
            this.lbSalesText.Location = new System.Drawing.Point(12, 583);
            this.lbSalesText.Name = "lbSalesText";
            this.lbSalesText.Size = new System.Drawing.Size(990, 116);
            this.lbSalesText.TabIndex = 74;
            this.lbSalesText.UseTabStops = false;
            // 
            // lblEstDeliveryDate
            // 
            this.lblEstDeliveryDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEstDeliveryDate.AutoSize = true;
            this.lblEstDeliveryDate.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.lblEstDeliveryDate.Location = new System.Drawing.Point(301, 86);
            this.lblEstDeliveryDate.Name = "lblEstDeliveryDate";
            this.lblEstDeliveryDate.Size = new System.Drawing.Size(126, 16);
            this.lblEstDeliveryDate.TabIndex = 77;
            this.lblEstDeliveryDate.Text = "lblEstDeliveryDate";
            // 
            // lblCusName
            // 
            this.lblCusName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.lblCusName.Location = new System.Drawing.Point(301, 61);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(85, 16);
            this.lblCusName.TabIndex = 78;
            this.lblCusName.Text = "lblCusName";
            // 
            // lblCusNum
            // 
            this.lblCusNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCusNum.AutoSize = true;
            this.lblCusNum.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.lblCusNum.Location = new System.Drawing.Point(301, 36);
            this.lblCusNum.Name = "lblCusNum";
            this.lblCusNum.Size = new System.Drawing.Size(79, 16);
            this.lblCusNum.TabIndex = 75;
            this.lblCusNum.Text = "lblCusNum";
            // 
            // gvOrderInput
            // 
            dataGridViewCellStyle26.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gvOrderInput.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle26;
            this.gvOrderInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.gvOrderInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOrderInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtVbeln,
            this.txtEdatu});
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvOrderInput.DefaultCellStyle = dataGridViewCellStyle28;
            this.gvOrderInput.Location = new System.Drawing.Point(12, 9);
            this.gvOrderInput.Name = "gvOrderInput";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOrderInput.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gvOrderInput.RowsDefaultCellStyle = dataGridViewCellStyle30;
            this.gvOrderInput.RowTemplate.Height = 24;
            this.gvOrderInput.Size = new System.Drawing.Size(255, 119);
            this.gvOrderInput.TabIndex = 70;
            this.gvOrderInput.TabStop = false;
            // 
            // lblOrderNum
            // 
            this.lblOrderNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOrderNum.AutoSize = true;
            this.lblOrderNum.Font = new System.Drawing.Font("PMingLiU", 12F);
            this.lblOrderNum.Location = new System.Drawing.Point(301, 9);
            this.lblOrderNum.Name = "lblOrderNum";
            this.lblOrderNum.Size = new System.Drawing.Size(91, 16);
            this.lblOrderNum.TabIndex = 76;
            this.lblOrderNum.Text = "lblOrderNum";
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

