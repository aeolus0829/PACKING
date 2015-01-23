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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblVdatu = new System.Windows.Forms.Label();
            this.lblName1 = new System.Windows.Forms.Label();
            this.lblKunnr = new System.Windows.Forms.Label();
            this.gvSelectOption = new System.Windows.Forms.DataGridView();
            this.btnTodb = new System.Windows.Forms.Button();
            this.lblVbeln = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectOption)).BeginInit();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
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
            this.btnConvert.Size = new System.Drawing.Size(65, 23);
            this.btnConvert.TabIndex = 69;
            this.btnConvert.Text = "匯出清單";
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
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 583);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(990, 148);
            this.listBox1.TabIndex = 74;
            // 
            // lblVdatu
            // 
            this.lblVdatu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblVdatu.AutoSize = true;
            this.lblVdatu.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVdatu.Location = new System.Drawing.Point(712, 145);
            this.lblVdatu.Name = "lblVdatu";
            this.lblVdatu.Size = new System.Drawing.Size(0, 13);
            this.lblVdatu.TabIndex = 77;
            this.lblVdatu.Visible = false;
            // 
            // lblName1
            // 
            this.lblName1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblName1.AutoSize = true;
            this.lblName1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblName1.Location = new System.Drawing.Point(712, 117);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(0, 13);
            this.lblName1.TabIndex = 78;
            this.lblName1.Visible = false;
            // 
            // lblKunnr
            // 
            this.lblKunnr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKunnr.AutoSize = true;
            this.lblKunnr.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblKunnr.Location = new System.Drawing.Point(712, 90);
            this.lblKunnr.Name = "lblKunnr";
            this.lblKunnr.Size = new System.Drawing.Size(0, 13);
            this.lblKunnr.TabIndex = 75;
            this.lblKunnr.Visible = false;
            // 
            // gvSelectOption
            // 
            this.gvSelectOption.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSelectOption.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvSelectOption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSelectOption.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtVbeln,
            this.txtEdatu});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSelectOption.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvSelectOption.Location = new System.Drawing.Point(12, 9);
            this.gvSelectOption.Name = "gvSelectOption";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSelectOption.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvSelectOption.RowTemplate.Height = 24;
            this.gvSelectOption.Size = new System.Drawing.Size(204, 119);
            this.gvSelectOption.TabIndex = 70;
            // 
            // btnTodb
            // 
            this.btnTodb.Location = new System.Drawing.Point(177, 135);
            this.btnTodb.Name = "btnTodb";
            this.btnTodb.Size = new System.Drawing.Size(62, 23);
            this.btnTodb.TabIndex = 72;
            this.btnTodb.Text = "開立包裝";
            this.btnTodb.UseVisualStyleBackColor = true;
            this.btnTodb.Click += new System.EventHandler(this.btnTodb_Click);
            // 
            // lblVbeln
            // 
            this.lblVbeln.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblVbeln.AutoSize = true;
            this.lblVbeln.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVbeln.Location = new System.Drawing.Point(712, 65);
            this.lblVbeln.Name = "lblVbeln";
            this.lblVbeln.Size = new System.Drawing.Size(0, 13);
            this.lblVbeln.TabIndex = 76;
            this.lblVbeln.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 739);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblVdatu);
            this.Controls.Add(this.lblName1);
            this.Controls.Add(this.lblKunnr);
            this.Controls.Add(this.gvSelectOption);
            this.Controls.Add(this.btnTodb);
            this.Controls.Add(this.lblVbeln);
            this.Name = "Form1";
            this.Text = "PACKING 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectOption)).EndInit();
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblVdatu;
        private System.Windows.Forms.Label lblName1;
        private System.Windows.Forms.Label lblKunnr;
        private System.Windows.Forms.DataGridView gvSelectOption;
        private System.Windows.Forms.Button btnTodb;
        private System.Windows.Forms.Label lblVbeln;
    }
}

