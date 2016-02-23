using System;
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
        string dbConnStr = "Data Source=SBSDB;Initial Catalog=PACKING_DEV;Uid=PACKING;Pwd=Admin12-1;";
        string pakTblNm = "dbo.PACKING";
        string cusTblNm = "dbo.CUSTOMS";

        string D_connIP, D_connUser, D_connPwd, D_connClient, D_connLanguage, D_connSID, D_connRFC, D_status;
        string userName, packingKey;
        int itemCount = 0;
        sapReportPrms sapReportPrms = new sapReportPrms();

        public Form1()
        {
            string[] ALL = sapReportPrms.SQL();
            D_status = ALL[4];

            D_connIP = "192.168.0.15";
            D_connClient = "300";
            D_connSID = "DEV";
            D_connUser = "DDIC";
            D_connPwd = "Ubn3dx";
            D_connRFC = "ZSDRFC002";
            D_connLanguage = "ZF";

            if (D_status == "false")
            {
                MessageBox.Show("目前程式停用中，請連絡資訊組");
            }
            else
            {
                InitializeComponent();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Clear();
            listBox1.Items.Clear();
        }
        
        DataTable dt = new DataTable();

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            listBox1.Items.Clear();
            //使用者
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //KEY
            packingKey = DateTime.Now.ToString("yyyyMMdd").Trim() + DateTime.Now.ToString("HHmmss").Trim();

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            RfcConfigParameters rfcPara = new RfcConfigParameters();
            
            rfcPara.Add(RfcConfigParameters.Name, D_connSID );
            rfcPara.Add(RfcConfigParameters.AppServerHost, D_connIP.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.Client, D_connClient.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.User, D_connUser.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.Password, D_connPwd.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.SystemNumber, "00");
            rfcPara.Add(RfcConfigParameters.Language, D_connLanguage.ToString().Trim());
            RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfcPara);
            RfcRepository rfcRepo = rfcDest.Repository;
            IRfcFunction rfcFMname = null;

            //結束箱號暫存
            int coNumEnd = 0;
            int orderNumberCount = gvOrderInput.RowCount - 1;

            if (orderNumberCount == 0)
            {
                MessageBox.Show("請輸入訂單資料！", "錯誤");
            }
            for (int k = 1; k <= orderNumberCount; k++)
            {
                //輸入參數
                String orderNumber = gvOrderInput.Rows[k - 1].Cells[0].Value.ToString().Trim();
                String deliveryDate = null;
                if (gvOrderInput.Rows[k - 1].Cells[1].Value != null)
                {
                    deliveryDate = gvOrderInput.Rows[k - 1].Cells[1].Value.ToString().Trim();
                }
                //函數名稱
                rfcFMname = rfcRepo.CreateFunction(D_connRFC);

                //設置輸入參數
                rfcFMname.SetValue("P_VBELN", orderNumber);
                if (deliveryDate != null) rfcFMname.SetValue("P_EDATU", deliveryDate);

                //調用RFC方法
                rfcFMname.Invoke(rfcDest);

                //輸出參數
                string messageType = rfcFMname.GetValue("STYPE").ToString();
                string messageStatus = rfcFMname.GetValue("STATUS").ToString();

                // Declare message title. 
                string dialogTitle = "";
                switch (messageType)
                {
                    case "S": dialogTitle = "成功"; break;
                    case "E": dialogTitle = "錯誤"; break;
                    case "W": dialogTitle = "警告"; break;
                    case "I": dialogTitle = "資訊"; break;
                    case "A": dialogTitle = "取消"; break;
                }

                //MessageBox.Show(status, title);
                if (messageStatus != "")
                {
                    MessageBox.Show(messageStatus, dialogTitle);
                    //btnClear.PerformClick();
                }
                else
                {
                    IRfcTable HEADER = rfcFMname.GetTable("HEADER");
                    IRfcTable ITEM = rfcFMname.GetTable("ITEM");
                    IRfcTable TLINE1 = rfcFMname.GetTable("TLINE1");
                    IRfcTable TLINE2 = rfcFMname.GetTable("TLINE2");
                    IRfcTable TLINE3 = rfcFMname.GetTable("TLINE3");


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
                    if (k == orderNumberCount)
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

                    for (int i = 0; i <= ITEM.RowCount - 1; i++)
                    {
                        DataRow dr = dt.NewRow();

                        if (i == 0 && ! dt.Columns.Contains("起訖箱號"))
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
                            dt.Columns.Add("預計出貨日");
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

                            // 以下資料不會轉出成 excel
                            dt.Columns.Add("箱號");
                            dt.Columns.Add("包裝指示碼");     
                            dt.Columns.Add("滿箱數");
                            dt.Columns.Add("買方代號");
                            dt.Columns.Add("買方名稱");
                            dt.Columns.Add("出貨人代號");
                            dt.Columns.Add("出貨人名稱");
                            // dt.Columns.Add("預計出貨日");
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
                        int ctnQty = Convert.ToInt32(ITEM.GetString("CTNQTY").ToString().TrimEnd('0').TrimEnd('.'));
                        //結束箱號，結束箱號
                        int ctnNumStart, ctnNumEnd;

                        //箱數計算
                        if (i == 0 && k == 1)
                        {
                            ctnNumStart = 1;
                            ctnNumEnd = ctnQty;
                            coNumEnd = ctnNumEnd;
                        }
                        else
                        {
                            ctnNumStart = coNumEnd + 1;
                            ctnNumEnd = ctnNumStart + ctnQty - 1;
                            coNumEnd = ctnNumEnd;
                        }
                        //參考號碼(客戶物料)
                        string kdmat = ITEM.GetString("KDMAT").ToString();
                        //客戶採購單
                        string bstkd = ITEM.GetString("BSTKD").ToString();
                        //品號
                        string matnr = ITEM.GetString("MATNR").ToString().TrimStart('0');
                        //品名
                        string arktx = ITEM.GetString("ARKTX").ToString();
                        //廠牌參考號碼20140901移除
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
                        string fullPackQty = ITEM.GetString("PACKQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //單箱數量
                        string boxQty = ITEM.GetString("BOXQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //淨重
                        //string ntgew1 = ITEM.GetString("NTGEW1").ToString().TrimEnd('0').TrimEnd('.');
                        string ntgew1 = ITEM.GetString("NTGEW1").ToString();
                        //總淨重
                        //string ntgew2 = ITEM.GetString("NTGEW2").ToString().TrimEnd('0').TrimEnd('.');
                        string ntgew2 = ITEM.GetString("NTGEW2").ToString();
                        //毛重
                        //string ntgew3 = ITEM.GetString("NTGEW3").ToString().TrimEnd('0').TrimEnd('.');
                        string ntgew3 = ITEM.GetString("NTGEW3").ToString();
                        //總毛重
                        //string ntgew4 = ITEM.GetString("NTGEW4").ToString().TrimEnd('0').TrimEnd('.');
                        string ntgew4 = ITEM.GetString("NTGEW4").ToString();
                        //才數
                        //string volum1 = ITEM.GetString("VOLUM1").ToString().TrimEnd('0').TrimEnd('.');
                        string volum1 = ITEM.GetString("VOLUM1").ToString();
                        //總才數
                        //string volum2 = ITEM.GetString("VOLUM2").ToString().TrimEnd('0').TrimEnd('.');
                        string volum2 = ITEM.GetString("VOLUM2").ToString();
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
                        string unitPrice = ITEM.GetString("U_PRICE").ToString();

                        dr["起訖箱號"] = ctnNumStart + "~" + ctnNumEnd;
                        dr["箱數"] = ctnQty;
                        dr["客戶物料"] = kdmat;
                        dr["品號"] = matnr;
                        dr["品名"] = arktx;
                        dr["單箱數量"] = boxQty.Trim();
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

                        dr["箱號"] = ctnNumEnd;
                        dr["包裝指示碼"] = pobjid;
                        dr["滿箱數"] = fullPackQty;
                        dr["買方代號"] = KUNNR_S;
                        dr["買方名稱"] = NAME1_S;
                        dr["出貨人代號"] = KUNNR_H;
                        dr["出貨人名稱"] = NAME1_H;
                        // dr["預計出貨日"] = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyy/MM/dd");
                        dr["預計出貨日"] = Convert.ToDateTime(ITEM.GetString("EDATU")).ToString("yyyy/MM/dd");
                        dr["結帳月份"] = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyyMM");
                        dr["起始箱號"] = ctnNumStart;
                        dr["結束箱號"] = ctnNumEnd;
                        dr["單價"] = unitPrice;
                        dr["KEY"] = packingKey;
                        dr["USERID"] = userName;
                        dr["原始單價"] = ITEM.GetString("KBTER1").ToString();
                        dr["客戶折價"] = ITEM.GetString("KBTER2").ToString();

                        dt.Rows.Add(dr);
                    }
                }

                dataGridView1.DataSource = dt.DefaultView;
                dataGridView1.ReadOnly = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            //datatable加總
            int totKwmeng = 0;
            int totCtnQty = 0;
            double totNtgew1 = 0.000;
            double totNtgew2 = 0.000;
            double totVolum1 = 0.000;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                    totKwmeng += Convert.ToInt32(dr["總數量"]);
                    totCtnQty += Convert.ToInt32(dr["箱數"]);
                    totNtgew1 += Convert.ToDouble(dr["總淨重"]);
                    totNtgew2 += Convert.ToDouble(dr["總毛重"]);
                    totVolum1 += Convert.ToDouble(dr["總才數"]);
                }
            
            listBox1.Items.Add("總數量：" + totKwmeng);
            listBox1.Items.Add("總箱數：" + totCtnQty);
            listBox1.Items.Add("總淨重：" + totNtgew1.ToString("0.000")); // 小數會自動補0，格式為 1.000
            listBox1.Items.Add("總毛重：" + totNtgew2.ToString("0.000"));
            listBox1.Items.Add("總才數：" + totVolum1.ToString("0.000"));
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
            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            
            worksheet = workbook.Sheets["工作表1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "訂單包裝明細";
            
            // 將買方基本資料，放置於工作表左上角，[第一列，第一行]
            worksheet.Cells[1, 1] = lblVbeln.Text;
            worksheet.Cells[2, 1] = lblKunnr.Text;
            worksheet.Cells[3, 1] = lblName1.Text;
            worksheet.Cells[4, 1] = lblVdatu.Text;
            worksheet.Cells[5, 1] = "包裝單號：" + packingKey;

            // 計算項目資料筆數，26是最大欄位數
            itemCount = itemCount + 26 + dataGridView1.Rows.Count;

            //設定 excel 資料格式為 文字
            //客戶物料(3)	品號(4)	品名(5) 單位(7) 內盒(11)	外箱(12)	訂單號碼(13)
            /*
            worksheet.Range["C7", "C" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["D7", "D" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["E7", "E" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["G7", "G" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["K7", "K" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["L7", "L" + itemCount.ToString()].NumberFormat = "@";
            worksheet.Range["M7", "M" + itemCount.ToString()].NumberFormat = "@";
             */

            //  excel 資料全部轉為文字 
            worksheet.Columns.EntireColumn.NumberFormat = "@";
            
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
            if (File.Exists(desktopFolderPath + "\\包裝明細清單.xlsx"))
            {
                File.Delete(desktopFolderPath + "\\包裝明細清單.xlsx");
            }
            
            worksheet.Cells.EntireColumn.AutoFit(); //自動調整欄寬 
            workbook.SaveAs(desktopFolderPath + "\\包裝明細清單.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            MessageBox.Show("轉檔完畢!", "資訊");
        }

       
        private void btnTodb_Click(object sender, EventArgs e)
        {
            //檢查GRIDVIEW
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("沒有資料", "錯誤");
                btnClear.PerformClick();
            }
            else
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //寫入 PACKING table
                bulkWriteToPacking(dt);
                //寫入 CUSTOMS table
                bulkWriteToCustoms(dt);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                DialogResult dr = MessageBox.Show("資料已寫入資料庫" + Environment.NewLine + "鍵值為：" +dataGridView1.Rows[0].Cells[38].Value.ToString(), "資訊");
                if (dr == DialogResult.OK)
                {
                    btnClear.PerformClick();
                }
            }
        }
        private void bulkWriteToPacking(DataTable dataTable)
        {                                    
            SqlConnection bulkConn = new SqlConnection(dbConnStr);
            SqlBulkCopy SBC = new SqlBulkCopy(bulkConn);
            SBC.DestinationTableName = pakTblNm;
            
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
            bulkConn.Open();

            //寫入 PACKING table
            SBC.WriteToServer(dataTable);
            SBC.Close();
            bulkConn.Close();
        }
        private void bulkWriteToCustoms(DataTable dataTable)
        {

            SqlConnection bulkConn = new SqlConnection(dbConnStr);
            SqlBulkCopy SBC = new SqlBulkCopy(bulkConn);
            SBC.DestinationTableName = cusTblNm;

            //對應資料行
            SBC.ColumnMappings.Add("KEY", "KEY");
            SBC.ColumnMappings.Add("訂單號碼", "NBR");
            SBC.ColumnMappings.Add("項次", "POSNR");
            SBC.ColumnMappings.Add("單價", "PRICE");
            SBC.ColumnMappings.Add("原始單價", "ORGNL");
            SBC.ColumnMappings.Add("客戶折價", "DSCNT");
            bulkConn.Open();

            //寫入 CUSTOMS table
            SBC.WriteToServer(dt);
            SBC.Close();
            bulkConn.Close();
        }

    }
}
