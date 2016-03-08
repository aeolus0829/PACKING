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
        string dbConnStr = "Data Source=SBSDB;Initial Catalog=PACKING;Uid=PACKING;Pwd=Admin12-1;";
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

            D_connIP = "192.168.0.16";
            D_connClient = "800";
            D_connSID = "PRD";
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
            lbSalesText.Items.Clear();
        }
        
        DataTable dt = new DataTable();

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            lbSalesText.Items.Clear();
            //使用者
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //KEY
            packingKey = DateTime.Now.ToString("yyyyMMdd").Trim() + DateTime.Now.ToString("HHmmss").Trim();

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            RfcConfigParameters rfcPara = new RfcConfigParameters();

            rfcPara.Add(RfcConfigParameters.Name, D_connSID);
            rfcPara.Add(RfcConfigParameters.AppServerHost, D_connIP.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.Client, D_connClient.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.User, D_connUser.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.Password, D_connPwd.ToString().Trim());
            rfcPara.Add(RfcConfigParameters.SystemNumber, "00");
            rfcPara.Add(RfcConfigParameters.Language, D_connLanguage.ToString().Trim());
            RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfcPara);
            RfcRepository rfcRepo = rfcDest.Repository;
            IRfcFunction rfcFMname;

            //結束箱號暫存
            int tmpCoNumEnd = 0;
            int orderNumberCount = gvOrderInput.RowCount - 1;

            if (orderNumberCount == 0)
            {
                MessageBox.Show("請輸入訂單資料！", "錯誤");
            }
            for (int k = 1; k <= orderNumberCount; k++)
            {
                //輸入參數
                String paraOrderNumber = gvOrderInput.Rows[k - 1].Cells[0].Value.ToString().Trim();
                String paraDeliveryDate = null;
                if (gvOrderInput.Rows[k - 1].Cells[1].Value != null)
                {
                    paraDeliveryDate = gvOrderInput.Rows[k - 1].Cells[1].Value.ToString().Trim();
                }               
                //函數名稱
                rfcFMname = rfcRepo.CreateFunction(D_connRFC);

                //設置輸入參數
                rfcFMname.SetValue("P_VBELN", paraOrderNumber);
                if (! string.IsNullOrEmpty(paraDeliveryDate)) rfcFMname.SetValue("P_EDATU", paraDeliveryDate);

                //調用RFC方法
                rfcFMname.Invoke(rfcDest);

                //輸出參數
                string messageType = rfcFMname.GetValue("STYPE").ToString();
                string messageStatus = rfcFMname.GetValue("STATUS").ToString();

                string messageBoxTitle = "";
                switch (messageType)
                {
                    case "S": messageBoxTitle = "成功"; break;
                    case "E": messageBoxTitle = "錯誤"; break;
                    case "W": messageBoxTitle = "警告"; break;
                    case "I": messageBoxTitle = "資訊"; break;
                    case "A": messageBoxTitle = "取消"; break;
                }

                if (messageStatus != "")
                {
                    MessageBox.Show(messageStatus, messageBoxTitle);
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
                    string orderNumber = HEADER.GetString("VBELN").TrimStart('0');
                    //買方代號
                    string buyerNum = HEADER.GetString("KUNNR_S");
                    //收貨方代號
                    string shiperNum = HEADER.GetString("KUNNR_H");
                    //買方名稱
                    string buyerName = HEADER.GetString("NAME1_S");
                    //收貨方名稱
                    string shiperName = HEADER.GetString("NAME1_H");
                    //預計出貨日
                    string VDATU = Convert.ToDateTime(HEADER.GetString("VDATU")).ToString("yyyy/MM/dd");

                    //累加訂單
                    if (k == 1 && orderNumber != "")
                    {
                        lblOrderNum.Text = "訂單號碼 : " + orderNumber;
                    }
                    else if (orderNumber != "")
                    {
                        lblOrderNum.Text = lblOrderNum.Text + " ; " + orderNumber;
                    }
                    //顯示最後一筆的資料
                    if (k == orderNumberCount)
                    {
                        lblOrderNum.Visible = true;
                        lblCusNum.Text = "買方/出貨方 : " + buyerNum + " / " + shiperNum;
                        lblCusNum.Visible = true;
                        lblCusName.Text = "買方/出貨方 : " + buyerName + " / " + shiperName;
                        lblCusName.Visible = true;
                        lblEstDeliveryDate.Visible = true;
                        if (! string.IsNullOrEmpty(paraDeliveryDate))
                        {                            
                            lblEstDeliveryDate.Text = "第一交貨日 : " + paraDeliveryDate;
                        } else
                        {
                            lblEstDeliveryDate.Text = "第一交貨日未指定";
                        }

                        //銷售內文
                        addTLINEtoListbox(TLINE1);
                        addTLINEtoListbox(TLINE2);
                        addTLINEtoListbox(TLINE3);
                    }

                    for (int i = 0; i <= ITEM.RowCount - 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        bool isHeaderExist = dt.Columns.Contains("箱數");

                        if (i == 0 && !isHeaderExist)
                        {
                            dt.Columns.Add("箱數");
                            dt.Columns.Add("箱號");
                            dt.Columns.Add("客戶物料");
                            dt.Columns.Add("品號");
                            dt.Columns.Add("舊料號");
                            dt.Columns.Add("品名");
                            dt.Columns.Add("總數量");
                            dt.Columns.Add("單位");
                            dt.Columns.Add("總淨重");
                            dt.Columns.Add("總毛重");
                            dt.Columns.Add("總才數");                            
                            dt.Columns.Add("內盒");
                            dt.Columns.Add("內盒舊品號");
                            dt.Columns.Add("外箱");
                            dt.Columns.Add("外箱舊品號");
                            dt.Columns.Add("客戶訂單");
                            dt.Columns.Add("訂單號碼");
                            dt.Columns.Add("項次");

                            // 以下資料不會轉出成 excel
                            dt.Columns.Add("預計出貨日");
                            dt.Columns.Add("起訖箱號");
                            dt.Columns.Add("單箱數量");
                            dt.Columns.Add("淨重");
                            dt.Columns.Add("毛重");
                            dt.Columns.Add("才數");
                            dt.Columns.Add("包裝指示碼");
                            dt.Columns.Add("滿箱數");
                            dt.Columns.Add("買方代號");
                            dt.Columns.Add("買方名稱");
                            dt.Columns.Add("出貨人代號");
                            dt.Columns.Add("出貨人名稱");                            
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
                        //起始箱號，結束箱號
                        int ctnNumStart, ctnNumEnd;

                        //箱數計算
                        if (i == 0 && k == 1)
                        {
                            ctnNumStart = 1;
                            ctnNumEnd = ctnQty;
                            tmpCoNumEnd = ctnNumEnd;
                        }
                        else
                        {
                            ctnNumStart = tmpCoNumEnd + 1;
                            ctnNumEnd = ctnNumStart + ctnQty - 1;
                            tmpCoNumEnd = ctnNumEnd;
                        }
                        //參考號碼(客戶物料)
                        string cusMat = ITEM.GetString("KDMAT").ToString();
                        //客戶採購單
                        string cusPoNum = ITEM.GetString("BSTKD").ToString();
                        //品號
                        string matNum = ITEM.GetString("MATNR").ToString().TrimStart('0');
                        //品名
                        string matName = ITEM.GetString("ARKTX").ToString();
                        //單位
                        string unitOfMeasure = ITEM.GetString("VRKME").ToString();
                        //數量
                        string totQty = ITEM.GetString("KWMENG").ToString().TrimEnd('0').TrimEnd('.');
                        //內盒
                        string boxMatNum = ITEM.GetString("BOX").ToString().TrimStart('0');
                        //外箱
                        string ctnMatNum = ITEM.GetString("CTN").ToString().TrimStart('0');
                        //滿箱數
                        string fullPackQty = ITEM.GetString("PACKQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //單箱數量
                        string boxQty = ITEM.GetString("BOXQTY").ToString().TrimEnd('0').TrimEnd('.');
                        //淨重
                        string netWeight = ITEM.GetString("NTGEW1").ToString();
                        //總淨重
                        string totNetWeight = ITEM.GetString("NTGEW2").ToString();
                        //毛重
                        string grossWeight = ITEM.GetString("NTGEW3").ToString();
                        //總毛重
                        string totGrossWeight = ITEM.GetString("NTGEW4").ToString();
                        //才數
                        string volume = ITEM.GetString("VOLUM1").ToString();
                        //總才數
                        string totVolume = ITEM.GetString("VOLUM2").ToString();
                        //項次
                        string itemNum = ITEM.GetString("POSNR").ToString().TrimStart('0');
                        //舊料號
                        string oldMatNum = ITEM.GetString("IHREZ_E").ToString();
                        //舊外箱
                        string ctnOldMatNum = ITEM.GetString("CTN_O").ToString();
                        //舊內盒
                        string boxOldMatNum = ITEM.GetString("BOX_O").ToString();
                        //包裝指示碼
                        string packingInstruc = ITEM.GetString("POBJID").ToString();
                        //單價
                        string unitPrice = ITEM.GetString("U_PRICE").ToString();

                        dr["箱數"] = ctnQty;
                        dr["客戶物料"] = cusMat;
                        dr["品號"] = matNum;
                        dr["品名"] = matName;
                        dr["單箱數量"] = boxQty.Trim();
                        dr["單位"] = unitOfMeasure;
                        dr["淨重"] = netWeight;
                        dr["毛重"] = grossWeight;
                        dr["才數"] = volume;
                        dr["內盒"] = boxMatNum;
                        dr["外箱"] = ctnMatNum;
                        dr["訂單號碼"] = orderNumber;
                        dr["舊料號"] = oldMatNum;
                        dr["客戶訂單"] = cusPoNum;
                        dr["總數量"] = totQty;
                        dr["總淨重"] = totNetWeight;
                        dr["總毛重"] = totGrossWeight;
                        dr["總才數"] = totVolume;
                        dr["內盒舊品號"] = boxOldMatNum;
                        dr["外箱舊品號"] = ctnOldMatNum;
                        dr["項次"] = itemNum;
                        dr["起訖箱號"] = ctnNumStart + "~" + ctnNumEnd;

                        dr["箱號"] = ctnNumEnd;
                        dr["包裝指示碼"] = packingInstruc;
                        dr["滿箱數"] = fullPackQty;
                        dr["買方代號"] = buyerNum;
                        dr["買方名稱"] = buyerName;
                        dr["預計出貨日"] = Convert.ToDateTime(ITEM.GetString("EDATU")).ToString("yyyyMMdd");
                        dr["出貨人代號"] = shiperNum;
                        dr["出貨人名稱"] = shiperName;                        
                        dr["結帳月份"] = Convert.ToDateTime(ITEM.GetString("EDATU")).ToString("yyyyMM");
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
                Cursor.Current = Cursors.Default;
            }
            //datatable加總
            int sumTotQty = 0;
            int sumTotCtnQty = 0;
            double sumTotNetWeight = 0.000;
            double sumTotGrossWeight = 0.000;
            double sumTotVolume = 0.000;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                    sumTotQty += Convert.ToInt32(dr["總數量"]);
                    sumTotCtnQty += Convert.ToInt32(dr["箱數"]);
                    sumTotNetWeight += Convert.ToDouble(dr["總淨重"]);
                    sumTotGrossWeight += Convert.ToDouble(dr["總毛重"]);
                    sumTotVolume += Convert.ToDouble(dr["總才數"]);
                }
            
            lbSalesText.Items.Add("加總數量：" + sumTotQty);
            lbSalesText.Items.Add("加總箱數：" + sumTotCtnQty);
            lbSalesText.Items.Add("加總淨重：" + sumTotNetWeight.ToString("0.000")); // 小數會自動補0，格式為 1.000
            lbSalesText.Items.Add("加總毛重：" + sumTotGrossWeight.ToString("0.000"));
            lbSalesText.Items.Add("加總才數：" + sumTotVolume.ToString("0.000"));
        }

        private void addTLINEtoListbox(IRfcTable TLINE)
        {
            for (int t = 0; t < TLINE.RowCount; t++)
            {
                TLINE.CurrentIndex = t;
                lbSalesText.Items.Add(TLINE.GetString("TDLINE"));
                if (t == TLINE.RowCount - 1) lbSalesText.Items.Add("  "); //最後一筆資料之後加上分隔符號
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Form1_Load(null, null);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            
            worksheet = workbook.Sheets["工作表1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "訂單包裝明細";

            int orderNumRow = 1;
            int cusNumRow= 2;
            int cusNameRow = 3;
            int estDeliveryDateRow = 4;
            int packingKeyRow = 5;
            int firstColumnNum = 1;
            int lastVisbleColumnCount = 17;
            int itemHeaderRowStart = 7;
            int columnNum;

            // 將買方基本資料，放置於工作表左上角，[第一列，第一行]
            worksheet.Cells[orderNumRow, firstColumnNum] = lblOrderNum.Text;
            worksheet.Cells[cusNumRow, firstColumnNum] = lblCusNum.Text;
            worksheet.Cells[cusNameRow, firstColumnNum] = lblCusName.Text;
            worksheet.Cells[estDeliveryDateRow, firstColumnNum] = lblEstDeliveryDate.Text;
            worksheet.Cells[packingKeyRow, firstColumnNum] = "包裝單號：" + packingKey;

            // 計算項目資料筆數，lastVisbleColumnCount 是允許出貨組看到的最大欄位數
            itemCount = itemCount + lastVisbleColumnCount + dataGridView1.Rows.Count;

            //  excel 資料全部轉為文字 
            worksheet.Columns.EntireColumn.NumberFormat = "@";
            
            //表頭
            for (int i = 0; i <= lastVisbleColumnCount ; i++) 
            {
                columnNum = i + 1;
                worksheet.Cells[itemHeaderRowStart, columnNum] = dataGridView1.Columns[i].HeaderText;
            }

            int itemDetailRowStart = itemHeaderRowStart + 1;

            //項目
            for (int x = 0; x < dataGridView1.Rows.Count - 1; x++) 
            {                
                for (int j = 0; j <= lastVisbleColumnCount; j++) 
                {
                    worksheet.Cells[x + itemDetailRowStart, j+1] = dataGridView1.Rows[x].Cells[j].Value.ToString();
                }
            }

            int textRowStart = itemDetailRowStart + dataGridView1.Rows.Count + 1;

            //內文
            for (int s = 0; s < lbSalesText.Items.Count ; s++)  
            {
                worksheet.Cells[s + textRowStart, 1] = lbSalesText.Items[s].ToString();
            }
            if (File.Exists(desktopFolderPath + "\\包裝明細清單.xlsx"))
            {
                File.Delete(desktopFolderPath + "\\包裝明細清單.xlsx");
            }
            
            worksheet.Cells.EntireColumn.AutoFit(); //自動調整欄寬 
            workbook.SaveAs(desktopFolderPath + "\\包裝明細清單.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            Cursor.Current = Cursors.Default;
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
                DialogResult dr = MessageBox.Show("資料已寫入資料庫" + Environment.NewLine + "鍵值為：" + packingKey, "資訊");
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

            try
            {
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
                SBC.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SBC.Close();
                bulkConn.Close();
            }
            
        }
        private void bulkWriteToCustoms(DataTable dataTable)
        {

            SqlConnection bulkConn = new SqlConnection(dbConnStr);
            SqlBulkCopy SBC = new SqlBulkCopy(bulkConn);
            SBC.DestinationTableName = cusTblNm;

            try
            {
                //對應資料行
                SBC.ColumnMappings.Add("KEY", "KEY");
                SBC.ColumnMappings.Add("訂單號碼", "NBR");
                SBC.ColumnMappings.Add("項次", "POSNR");
                SBC.ColumnMappings.Add("單價", "PRICE");
                SBC.ColumnMappings.Add("原始單價", "ORGNL");
                SBC.ColumnMappings.Add("客戶折價", "DSCNT");

                bulkConn.Open();
                SBC.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            SBC.Close();
            bulkConn.Close();
        }
    }
}
