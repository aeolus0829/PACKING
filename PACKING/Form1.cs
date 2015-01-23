﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SAP.Middleware.Connector;

namespace PACKINGLIST
{
    public partial class Form1 : Form
    {
        string D_connIP, D_connUser, D_connPwd, D_rptNm, D_status, D_connClient, D_connLanguage;
        public Form1()
        {
            sapReportPrms sapReportPrms = new sapReportPrms();
            string[] ALL = sapReportPrms.SQL();

            D_connIP = ALL[0];
            D_connUser = ALL[1];
            D_connPwd = ALL[2];
            D_rptNm = ALL[3];
            D_status = ALL[4];
            D_connClient = ALL[5];
            D_connLanguage = ALL[6];

            if (D_status == "False")
            {
                //目前程式停用中，請連絡資訊組
                MessageBox.Show("目前程式停用中，請連絡資訊組");
            }
            else
            {
                InitializeComponent();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //清除資料
            dt.Clear();
            listBox1.Items.Clear();
        }
        
        DataTable dt = new DataTable();

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            //先清除資料
            dt.Clear();
            listBox1.Items.Clear();
            //使用者
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //KEY
            string key = DateTime.Now.ToString("yyyyMMdd").Trim() + DateTime.Now.ToString("HHmmss").Trim();
     
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            RfcConfigParameters rfcPar = new RfcConfigParameters();
            rfcPar.Add(RfcConfigParameters.Name, "PRD");
            rfcPar.Add(RfcConfigParameters.AppServerHost, D_connIP);
            rfcPar.Add(RfcConfigParameters.Client, D_connClient);
            rfcPar.Add(RfcConfigParameters.User, D_connUser);
            rfcPar.Add(RfcConfigParameters.Password, D_connPwd);
            rfcPar.Add(RfcConfigParameters.SystemNumber, "00");
            rfcPar.Add(RfcConfigParameters.Language, D_connLanguage);
            RfcDestination dest = RfcDestinationManager.GetDestination(rfcPar);
            RfcRepository rfcrep = dest.Repository;
            IRfcFunction myfun = null;

            //結束箱號暫存
            int count = 0;

            //gvSelectOption筆數
            int n = gvSelectOption.RowCount - 1;
            if (n == 0)
            {
                MessageBox.Show("請輸入查詢資料！", "錯誤");
            }
            for (int k = 1; k <= n; k++)
            {
                //輸入參數
                String sVbeln = gvSelectOption.Rows[k - 1].Cells[0].Value.ToString().Trim();
                String sEdatu = null;
                if (gvSelectOption.Rows[k - 1].Cells[1].Value != null)
                {
                    sEdatu = gvSelectOption.Rows[k - 1].Cells[1].Value.ToString().Trim();
                }
                //函數名稱
                myfun = rfcrep.CreateFunction("ZSDRFC002");

                //設置輸入參數
                myfun.SetValue("P_VBELN", sVbeln);
                if (sEdatu != null) myfun.SetValue("P_EDATU", sEdatu);

                //調用RFC方法
                myfun.Invoke(dest);

                //輸出參數
                string type = myfun.GetValue("STYPE").ToString();
                string status = myfun.GetValue("STATUS").ToString();

                // Declare message title. 
                string title = "";
                switch (type)
                {
                    //訊息類型︰S 成功，E 錯誤， W 警告﹐I 資訊﹐A 取消
                    case "S": title = "成功"; break;
                    case "E": title = "錯誤"; break;
                    case "W": title = "警告"; break;
                    case "I": title = "資訊"; break;
                    case "A": title = "取消"; break;
                }

                //MessageBox.Show(status, title);
                if (status != "")
                {
                    MessageBox.Show(status, title);
                    //btnClear.PerformClick();
                }
                else
                {
                    IRfcTable HEADER = myfun.GetTable("HEADER");
                    IRfcTable ITEM = myfun.GetTable("ITEM");
                    IRfcTable TLINE1 = myfun.GetTable("TLINE1");
                    IRfcTable TLINE2 = myfun.GetTable("TLINE2");
                    IRfcTable TLINE3 = myfun.GetTable("TLINE3");


                    //当前内表的索引行
                    HEADER.CurrentIndex = 0;
                    //訂單號碼
                    string VBELN = HEADER.GetString("VBELN").TrimStart('0');
                    //買方代號
                    string KUNNR_S = HEADER.GetString("KUNNR_S");
                    //收貨方代號
                    string KUNNR_H = HEADER.GetString("KUNNR_H");
                    //買方名稱
                    string NAME1_S = HEADER.GetString("NAME1_S");
                    //收貨方名稱
                    string NAME1_H = HEADER.GetString("NAME1_H");
                    //預計出貨日
                    string VDATU = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyy/MM/dd");

                    //累加訂單
                    if (k == 1 && VBELN != "")
                    {
                        lblVbeln.Text = "訂單號碼 : " + VBELN;
                    }
                    else if (VBELN != "")
                    {
                        lblVbeln.Text = lblVbeln.Text + " ; " + VBELN;
                    }
                    //顯示最後一筆的資料
                    if (k == n)
                    {
                        lblVbeln.Visible = true;
                        lblKunnr.Text = "買方/出貨方 : " + KUNNR_S + " / " + KUNNR_H;
                        lblKunnr.Visible = true;
                        lblName1.Text = "買方/出貨方 : " + NAME1_S + " / " + NAME1_H;
                        lblName1.Visible = true;
                        lblVdatu.Text = "預計出貨日 : " + VDATU;
                        lblVdatu.Visible = true;
                        //內文
                        for (int t = 0; t < TLINE1.RowCount; t++)
                        {
                            TLINE1.CurrentIndex = t;
                            listBox1.Items.Add(TLINE1.GetString("TDLINE"));
                        }
                        for (int t = 0; t < TLINE2.RowCount; t++)
                        {
                            TLINE2.CurrentIndex = t;
                            listBox1.Items.Add(TLINE2.GetString("TDLINE"));
                        }
                        for (int t = 0; t < TLINE3.RowCount; t++)
                        {
                            TLINE3.CurrentIndex = t;
                            listBox1.Items.Add(TLINE3.GetString("TDLINE"));
                        }

                    }

                    //項目筆數
                    int j = ITEM.RowCount;
                    for (int i = 0; i <= ITEM.RowCount - 1; i++)
                    {
                        DataRow dr = dt.NewRow();

                        bool yesOrNo = dt.Columns.Contains("起訖箱號");
                        if (i == 0 && yesOrNo == false)
                        {
                            dt.Columns.Add("起訖箱號");
                            dt.Columns.Add("箱數");
                            dt.Columns.Add("客戶物料");
                            dt.Columns.Add("品號");
                            dt.Columns.Add("品名");
                            dt.Columns.Add("單箱數量");
                            dt.Columns.Add("單位");
                            dt.Columns.Add("淨重");
                            dt.Columns.Add("毛重");
                            dt.Columns.Add("才數");
                            dt.Columns.Add("內盒");
                            dt.Columns.Add("外箱");
                            dt.Columns.Add("訂單號碼");

                            dt.Columns.Add("    ");
                            dt.Columns.Add(" ");
                            dt.Columns.Add("  ");
                            dt.Columns.Add("舊料號");
                            dt.Columns.Add("客戶訂單");
                            dt.Columns.Add("總數量");
                            dt.Columns.Add("   ");
                            dt.Columns.Add("總淨重");
                            dt.Columns.Add("總毛重");
                            dt.Columns.Add("總才數");
                            dt.Columns.Add("內盒舊品號");
                            dt.Columns.Add("外箱舊品號");
                            dt.Columns.Add("項次");

                            dt.Columns.Add("箱號");
                            dt.Columns.Add("包裝指示碼");     
                            dt.Columns.Add("滿箱數");
                            dt.Columns.Add("買方代號");
                            dt.Columns.Add("買方名稱");
                            dt.Columns.Add("出貨人代號");
                            dt.Columns.Add("出貨人名稱");
                            dt.Columns.Add("預計出貨日");
                            dt.Columns.Add("結帳月份");
                            dt.Columns.Add("起始箱號");
                            dt.Columns.Add("結束箱號");
                            dt.Columns.Add("單價");
                            dt.Columns.Add("KEY");
                            dt.Columns.Add("USERID");
                            dt.Columns.Add("原始單價");
                            dt.Columns.Add("客戶折價");

                        }
                        //index
                        HEADER.CurrentIndex = 0;
                        ITEM.CurrentIndex = i;
                        //箱數
                        int ctnqty = Convert.ToInt32(ITEM.GetString("CTNQTY").ToString().TrimEnd('0').TrimEnd('.'));
                        //結束箱號，結束箱號
                        int stabix = new int();
                        int etabix = new int();
                        //箱數計算
                        if (i == 0 && k == 1)
                        {
                            stabix = 1;
                            etabix = ctnqty;
                            count = etabix;
                        }
                        else
                        {
                            stabix = count + 1;
                            etabix = stabix + ctnqty - 1;
                            count = etabix;
                        }
                        //參考號碼(客戶物料)
                        string kdmat = ITEM.GetString("KDMAT").ToString();
                        //客戶採購單
                        string bstkd = ITEM.GetString("BSTKD").ToString();
                        //品號
                        string matnr = ITEM.GetString("MATNR").ToString().TrimStart('0');
                        //品名
                        string arktx = ITEM.GetString("ARKTX").ToString();
                        //牌參考號碼20140901移除
                        //string bstkd_e = ITEM.GetString("BSTKD_E").ToString();
                        //單位
                        string vrkme = ITEM.GetString("VRKME").ToString();
                        //數量
                        string kwmeng = ITEM.GetString("KWMENG").ToString().TrimEnd('0').TrimEnd('.');
                        //內盒
                        string box = ITEM.GetString("BOX").ToString().TrimStart('0');
                        //外箱
                        string ctn = ITEM.GetString("CTN").ToString().TrimStart('0');
                        //滿箱數
                        string packqty = ITEM.GetString("PACKQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //單箱數量
                        string boxqty = ITEM.GetString("BOXQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //淨重
                        string ntgew1 = ITEM.GetString("NTGEW1").ToString().TrimEnd('0').TrimEnd('.');
                        //總淨重
                        string ntgew2 = ITEM.GetString("NTGEW2").ToString().TrimEnd('0').TrimEnd('.');
                        //毛重
                        string ntgew3 = ITEM.GetString("NTGEW3").ToString().TrimEnd('0').TrimEnd('.');
                        //總毛重
                        string ntgew4 = ITEM.GetString("NTGEW4").ToString().TrimEnd('0').TrimEnd('.');
                        //才數
                        string volum1 = ITEM.GetString("VOLUM1").ToString().TrimEnd('0').TrimEnd('.');
                        //總才數
                        string volum2 = ITEM.GetString("VOLUM2").ToString().TrimEnd('0').TrimEnd('.');
                        //項次
                        string posnr = ITEM.GetString("POSNR").ToString().TrimStart('0');
                        //舊料號
                        string ihrez_e = ITEM.GetString("IHREZ_E").ToString();
                        //舊外箱
                        string ctn_o = ITEM.GetString("CTN_O").ToString();
                        //舊內盒
                        string box_o = ITEM.GetString("BOX_O").ToString();
                        //包裝指示碼
                        string pobjid = ITEM.GetString("POBJID").ToString();
                        //單價
                        string price = ITEM.GetString("U_PRICE").ToString();

                        dr["起訖箱號"] = stabix + "~" + etabix;
                        dr["箱數"] = ctnqty;
                        dr["客戶物料"] = kdmat;
                        dr["品號"] = matnr;
                        dr["品名"] = arktx;
                        dr["單箱數量"] = boxqty;
                        dr["單位"] = vrkme;
                        dr["淨重"] = ntgew1;
                        dr["毛重"] = ntgew3;
                        dr["才數"] = volum1;
                        dr["內盒"] = box;
                        dr["外箱"] = ctn;
                        dr["訂單號碼"] = VBELN;
                        //2014/09/01移除廠牌參考號碼
                        //dr["廠牌參考號碼"] = bstkd_e;
                        //2014/07/28 add REF
                        //dr["REF"] = ITEM.GetString("REF").ToString();
                        dr["舊料號"] = ihrez_e;
                        dr["客戶訂單"] = bstkd;
                        dr["總數量"] = kwmeng;
                        dr["總淨重"] = ntgew2;
                        dr["總毛重"] = ntgew4;
                        dr["總才數"] = volum2;
                        dr["內盒舊品號"] = box_o;
                        dr["外箱舊品號"] = ctn_o;
                        dr["項次"] = posnr;

                        dr["箱號"] = etabix;
                        dr["包裝指示碼"] = pobjid;
                        dr["滿箱數"] = packqty;
                        dr["買方代號"] = KUNNR_S;
                        dr["買方名稱"] = NAME1_S;
                        dr["出貨人代號"] = KUNNR_H;
                        dr["出貨人名稱"] = NAME1_H;
                        dr["預計出貨日"] = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyyMMdd");
                        dr["結帳月份"] = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyyMM");
                        dr["起始箱號"] = stabix;
                        dr["結束箱號"] = etabix;
                        dr["單價"] = price;
                        dr["KEY"] = key;
                        dr["USERID"] = username;
                        dr["原始單價"] = ITEM.GetString("KBTER1").ToString();
                        dr["客戶折價"] = ITEM.GetString("KBTER2").ToString();

                        dt.Rows.Add(dr);
                    }

                }

                //GridData資料來源
                dataGridView1.DataSource = dt.DefaultView;
                dataGridView1.ReadOnly = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            //datatable加總
            int totkwmeng = 0;
            int totctnqty = 0;
            double totntgew1 = 0;
            double totntgew2 = 0;
            double totvolum1 = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                    totkwmeng += Convert.ToInt32(dr["總數量"]);
                totctnqty += Convert.ToInt32(dr["箱數"]);
                totntgew1 += Convert.ToDouble(dr["總淨重"]);
                totntgew2 += Convert.ToDouble(dr["總毛重"]);
                totvolum1 += Convert.ToDouble(dr["總才數"]);
            }
            listBox1.Items.Add("總數量：" + totkwmeng);
            listBox1.Items.Add("總箱數：" + totctnqty);
            listBox1.Items.Add("總淨重：" + totntgew1);
            listBox1.Items.Add("總毛重：" + totntgew2);
            listBox1.Items.Add("總才數：" + totvolum1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Form1_Load(null, null);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            //桌面路徑
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            
            worksheet = workbook.Sheets["工作表1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "訂單包裝明細";
            worksheet.Cells[1, 1] = lblVbeln.Text;
            worksheet.Cells[2, 1] = lblKunnr.Text;
            worksheet.Cells[3, 1] = lblName1.Text;
            worksheet.Cells[4, 1] = lblVdatu.Text;
            worksheet.Cells[5, 1] = "包裝單號：" + dataGridView1.Rows[0].Cells[38].Value.ToString();
            //表頭
            for (int i = 1; i < 26 + 1; i++) // dataGridView1.Columns.Count + 1
            {
                if (i <= 13)
                    worksheet.Cells[7, i] = dataGridView1.Columns[i - 1].HeaderText;
                else if (i >= 14)
                    worksheet.Cells[8, i - 13] = dataGridView1.Columns[i - 1].HeaderText;
            }
            //項目
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)  //DATAGRID行 
            {
                for (int j = 0; j < 26; j++) //DATAGRID列 dataGridView1.Columns.Count
                {
                    if (j <= 12)
                        worksheet.Cells[i * 2 + 9, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    else if (j >= 13)
                        worksheet.Cells[i * 2 + 10, j - 12] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            //內文
            for (int s = 0; s < listBox1.Items.Count ; s++)  
            {
                worksheet.Cells[s + 9 + dataGridView1.Rows.Count * 2, 1] = listBox1.Items[s].ToString();
            }
            if (File.Exists(path + "\\包裝明細清單.xlsx"))
            {
                File.Delete(path + "\\包裝明細清單.xlsx");
            }
            worksheet.Cells.EntireColumn.AutoFit(); //自動調整欄寬 
            workbook.SaveAs(path + "\\包裝明細清單.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            MessageBox.Show("轉檔完畢!", "資訊");
        }

       
        private void btnTodb_Click(object sender, EventArgs e)
        {
            //檢查GRIDVIEW
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("資料為空", "錯誤");
                btnClear.PerformClick();
            }
            else
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //PACKING
                BluckPacking(dt);
                //CUSTOMS
                BluckCustoms(dt);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                DialogResult dr = MessageBox.Show("資料已寫入資料庫" + Environment.NewLine + "鍵值為：" +dataGridView1.Rows[0].Cells[38].Value.ToString(), "資訊");
                if (dr == DialogResult.OK)
                {
                    btnClear.PerformClick();
                }
            }
        }
        private void BluckPacking(DataTable dataTable)
        {
            string connectionString = "Data Source=SBSDB;Initial Catalog=PACKING;Uid=PACKING;Pwd=Admin12-1;";
            SqlConnection Bulkcn = new SqlConnection(connectionString);
            SqlBulkCopy SBC = new SqlBulkCopy(Bulkcn);
            SBC.DestinationTableName = "dbo.PACKING";
            //對應資料行
            SBC.ColumnMappings.Add("客戶物料", "CUS_ITEM");
            SBC.ColumnMappings.Add("訂單號碼", "NBR");
            SBC.ColumnMappings.Add("出貨人代號", "CUS_NBR");
            SBC.ColumnMappings.Add("出貨人名稱", "CUS_ALIAS");
            SBC.ColumnMappings.Add("預計出貨日", "DATE");
            SBC.ColumnMappings.Add("結帳月份", "ACR_MON");
            SBC.ColumnMappings.Add("客戶訂單", "DESC_NO");
            SBC.ColumnMappings.Add("品號", "ITEM_NBR");
            SBC.ColumnMappings.Add("品名", "ITEM_DESC");
            //SBC.ColumnMappings.Add("例外參考號碼", "PANY_ITEM");
            SBC.ColumnMappings.Add("單位", "UN_DESC");
            SBC.ColumnMappings.Add("總數量", "QTY");
            SBC.ColumnMappings.Add("內盒", "IN_NBR");
            SBC.ColumnMappings.Add("滿箱數", "IN_BOX");
            SBC.ColumnMappings.Add("外箱", "PBOX_NBR");
            SBC.ColumnMappings.Add("單箱數量", "QTY_PBOX");
            SBC.ColumnMappings.Add("淨重", "N_WIGHT");
            SBC.ColumnMappings.Add("總淨重", "TOTN_WIGHT");
            SBC.ColumnMappings.Add("毛重", "G_WIGHT");
            SBC.ColumnMappings.Add("總毛重", "TOTG_WIGHT");
            SBC.ColumnMappings.Add("才數", "CUFT");
            SBC.ColumnMappings.Add("總才數", "TOTCUFT");
            SBC.ColumnMappings.Add("箱數", "PACK_QTY");
            SBC.ColumnMappings.Add("起始箱號", "NO1");
            SBC.ColumnMappings.Add("結束箱號", "NO2");
            SBC.ColumnMappings.Add("箱號", "_NullFlags");
            SBC.ColumnMappings.Add("項次", "POSNR");
            SBC.ColumnMappings.Add("舊料號", "IHREZ_E");
            SBC.ColumnMappings.Add("內盒舊品號", "BOX_O");
            SBC.ColumnMappings.Add("外箱舊品號", "CTN_O");
            SBC.ColumnMappings.Add("包裝指示碼", "POBJID");
            SBC.ColumnMappings.Add("買方代號", "KUNNR_S");
            SBC.ColumnMappings.Add("買方名稱", "NAME1_S");
            SBC.ColumnMappings.Add("KEY", "KEY");
            SBC.ColumnMappings.Add("USERID", "USERID");
            Bulkcn.Open();
            SBC.WriteToServer(dataTable);
            SBC.Close();
            Bulkcn.Close();
        }
        private void BluckCustoms(DataTable dataTable)
        {
            string connectionString = "Data Source=SBSDB;Initial Catalog=PACKING;Uid=PACKING;Pwd=Admin12-1;";
            SqlConnection Bulkcn = new SqlConnection(connectionString);
            SqlBulkCopy SBC = new SqlBulkCopy(Bulkcn);
            SBC.DestinationTableName = "dbo.CUSTOMS";
            SBC.ColumnMappings.Add("KEY", "KEY");
            SBC.ColumnMappings.Add("訂單號碼", "NBR");
            SBC.ColumnMappings.Add("項次", "POSNR");
            SBC.ColumnMappings.Add("單價", "PRICE");
            SBC.ColumnMappings.Add("原始單價", "ORGNL");
            SBC.ColumnMappings.Add("客戶折價", "DSCNT");
            Bulkcn.Open();
            SBC.WriteToServer(dt);
            SBC.Close();
            Bulkcn.Close();
        }

    }
}