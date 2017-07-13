using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using connDB;
using System.Windows.Forms;

using SAP.Middleware.Connector;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;

namespace PACKINGLIST
{
    public partial class Form1 : Form
    {
        string D_status;
        string userName, packingKey, dbConnStr;        
        int itemCount = 0;
        sapReportPrms sapReportPrms = new sapReportPrms();

        //連線資訊
        string getPackingData = "ZSDRFC002";
        string getLabelData = "ZSDRFC009";

        //開發資訊
        bool isTesting = true;
        string formName = "PACKING";
        string winFormVersion = "1.12";

        public Form1()
        {
            setConnEev(isTesting);

            var mssqlConn = new mssqlConnClass();            
            
            

            dbConnStr  = mssqlConn.toSBSDB(packingDB);

            string[] ALL = sapReportPrms.SQL();
            D_status = ALL[4];

            if (D_status == "false")
            {
                MessageBox.Show("目前程式停用中，請連絡資訊組");
            }
            else
            {
                InitializeComponent();
            }
        }

        private void setConnEev(bool isTesting)
        {
            switch(isTesting)
            {
                case true:
                    packingDB = "DEV";
                    sapDB = "620";
                    break;
                case false:
                    packingDB = "PRD";
                    sapDB = "800";
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Clear();
            lbSalesText.Items.Clear();
            if (isTesting) this.Text += winFormVersion + " 測試版 " + "/ MSSQL: " + packingDB + " / SAP資料環境: " + sapDB;
            else this.Text += winFormVersion;

            sapConn = new sapConnClass();
            rfcPara = sapConn.setParaToConn(sapDB);
            rfcDest = RfcDestinationManager.GetDestination(rfcPara);
            rfcRepo = rfcDest.Repository;

        }

        System.Data.DataTable dt = new System.Data.DataTable();

        public string packingDB { get; private set; }
        public string sapDB { get; private set; }
        internal sapConnClass sapConn { get; private set; }
        public RfcConfigParameters rfcPara { get; private set; }
        public RfcDestination rfcDest { get; private set; }
        public RfcRepository rfcRepo { get; private set; }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            lbSalesText.Items.Clear();
            //使用者
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //KEY
            packingKey = DateTime.Now.ToString("yyyyMMdd").Trim() + DateTime.Now.ToString("HHmmss").Trim();

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

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

                var rfcGetPackingFM = rfcRepo.CreateFunction(getPackingData);

                //設置輸入參數
                rfcGetPackingFM.SetValue("P_VBELN", paraOrderNumber);
                if (! string.IsNullOrEmpty(paraDeliveryDate)) rfcGetPackingFM.SetValue("P_EDATU", paraDeliveryDate);

                //調用RFC方法
                rfcGetPackingFM.Invoke(rfcDest);

                //輸出參數
                string messageType = rfcGetPackingFM.GetValue("STYPE").ToString();
                string messageStatus = rfcGetPackingFM.GetValue("STATUS").ToString();

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
                    IRfcTable HEADER = rfcGetPackingFM.GetTable("HEADER");
                    IRfcTable ITEM = rfcGetPackingFM.GetTable("ITEM");
                    IRfcTable TLINE1 = rfcGetPackingFM.GetTable("TLINE1");
                    IRfcTable TLINE2 = rfcGetPackingFM.GetTable("TLINE2");
                    IRfcTable TLINE3 = rfcGetPackingFM.GetTable("TLINE3");

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
                            dt.Columns.Add("箱號");
                            dt.Columns.Add("箱數");
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

        private void addTLINEtoListbox(IRfcTable TLINE) //添加銷售內文到 excel 底下
        {
            for (int tCount = 0; tCount < TLINE.RowCount; tCount++)
            {
                TLINE.CurrentIndex = tCount;
                string fixedTline = removeFormatString(TLINE.GetString("TDLINE"));


                lbSalesText.Items.Add(fixedTline);
                if (tCount == TLINE.RowCount - 1) lbSalesText.Items.Add("  "); //最後一筆資料之後加上分隔符號
            }
        }

        private string removeFormatString(string inputText)
        {
            string regPattern, fixedText, replaceWith;

            regPattern = "<\\(>.*<\\)>";
            replaceWith = " ";
            Regex rx = new Regex(regPattern);

            fixedText = rx.Replace(inputText, replaceWith);

            return fixedText;
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
            string excelFileName = "訂單包裝明細.xlsx";
            string fullPathToExcel = desktopFolderPath + "\\" + excelFileName;

            //設定工作表數量
            app.SheetsInNewWorkbook = 4;

            app.Visible = false;  // excel 程式是否顯示

            generatePackingSheet(workbook);
            generateLabelSheet(workbook);

            if (File.Exists(fullPathToExcel))
            {
                try
                {
                    File.Delete(fullPathToExcel);
                }
                catch (Exception)
                {
                    MessageBox.Show("檔案已存在，無法覆蓋，原檔案有開著嗎？", "錯誤");
                }

            }

            workbook.SaveAs(fullPathToExcel, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            Cursor.Current = Cursors.Default;

            tsLabel.Text = "轉檔已完成！放在 " + fullPathToExcel;

            //MessageBox.Show("轉檔完畢!", "資訊");
        }

        private void generateLabelSheet(_Workbook workbook)
        {
            string strSQL = "Select * from LABEL where [KEY] = @key order by _NullFlags"; //LABEL VIEW
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            SqlParameter key = new SqlParameter("@key", SqlDbType.Char, 14);
            key.Value = txtKey.Text;
            key.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(key);

            SqlDataReader sqlReader = cmd.ExecuteReader();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(sqlReader, LoadOption.OverwriteChanges);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            dt.Columns.Add("OTH_REF");
            dt.Columns.Add("OLD_ITEM");
            dt.Columns.Add("BOXCODE");
            dt.Columns.Add("MOGNBR1");
            dt.Columns.Add("ENGDES");
            dt.Columns.Add("SPNDES");
            dt.Columns.Add("GERDES");
            dt.Columns.Add("LBLCODE");
            dt.Columns.Add("LBLTYPE");
            dt.Columns.Add("LBLSTYE");
            dt.Columns.Add("OENBR1");
            dt.Columns.Add("OENBR2");
            dt.Columns.Add("OENBR3");
            dt.Columns.Add("OENBR4");
            dt.Columns.Add("OENBR5");
            dt.Columns.Add("OENBR6");
            dt.Columns.Add("OENBR7");
            dt.Columns.Add("OENBR8");
            dt.Columns.Add("OENBR9");
            dt.Columns.Add("OENBR10");
            dt.Columns.Add("MOGNBR2");
            dt.Columns.Add("TPFNBR1");
            dt.Columns.Add("TPFNBR2");
            dt.Columns.Add("TRWNBR1");
            dt.Columns.Add("TRWNBR2");
            dt.Columns.Add("DANNBR1");
            dt.Columns.Add("DANNBR2");
            dt.Columns.Add("MCQNBR1");
            dt.Columns.Add("MCQNBR2");
            dt.Columns.Add("DESC1");
            dt.Columns.Add("DESC2");
            dt.Columns.Add("DESC3");
            dt.Columns.Add("DESC4");
            dt.Columns.Add("DESC5");
            dt.Columns.Add("DESC6");
            dt.Columns.Add("DESC7");
            dt.Columns.Add("DESC8");
            dt.Columns.Add("DESC9");
            dt.Columns.Add("DESC10");
            dt.Columns.Add("QRCODE");
            dt.Columns.Add("HBSQRCODE");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                RfcConfigParameters rfcPar = new RfcConfigParameters();
                rfcPar.Add(RfcConfigParameters.Name, D_connSID);
                rfcPar.Add(RfcConfigParameters.AppServerHost, D_connIP);
                rfcPar.Add(RfcConfigParameters.Client, D_connClient);
                rfcPar.Add(RfcConfigParameters.User, D_connUser);
                rfcPar.Add(RfcConfigParameters.Password, D_connPwd);
                rfcPar.Add(RfcConfigParameters.SystemNumber, "00");
                rfcPar.Add(RfcConfigParameters.Language, D_connLanguage);
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcPar);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;

                myfun = rfcrep.CreateFunction(getLabelData);

                myfun.SetValue("P_VBELN", dt.Rows[i]["NBR"]);   //訂單號碼
                myfun.SetValue("P_POSNR", dt.Rows[i]["POSNR"].ToString());  //訂單項次

                myfun.Invoke(dest);

                IRfcTable ITAB = myfun.GetTable("ITAB");
                if (ITAB.CurrentIndex == 0)
                {
                    //只取第一列
                    ITAB.CurrentIndex = 0;
                    dt.Rows[i]["OTH_REF"] = ITAB.GetString("KDMAT");
                    dt.Rows[i]["OLD_ITEM"] = ITAB.GetString("BISMT");
                    dt.Rows[i]["OENBR1"] = ITAB.GetString("OENBR1");
                    dt.Rows[i]["OENBR2"] = ITAB.GetString("OENBR2");
                    dt.Rows[i]["OENBR3"] = ITAB.GetString("OENBR3");
                    dt.Rows[i]["OENBR4"] = ITAB.GetString("OENBR4");
                    dt.Rows[i]["OENBR5"] = ITAB.GetString("OENBR5");
                    dt.Rows[i]["OENBR6"] = ITAB.GetString("OENBR6");
                    dt.Rows[i]["OENBR7"] = ITAB.GetString("OENBR7");
                    dt.Rows[i]["OENBR8"] = ITAB.GetString("OENBR8");
                    dt.Rows[i]["OENBR9"] = ITAB.GetString("OENBR9");
                    dt.Rows[i]["OENBR10"] = ITAB.GetString("OENBR10");
                    dt.Rows[i]["MOGNBR1"] = ITAB.GetString("MOGNBR1");
                    dt.Rows[i]["MOGNBR2"] = ITAB.GetString("MOGNBR2");
                    dt.Rows[i]["TPFNBR1"] = ITAB.GetString("TPFNBR1");
                    dt.Rows[i]["TPFNBR2"] = ITAB.GetString("TPFNBR2");
                    dt.Rows[i]["TRWNBR1"] = ITAB.GetString("TRWNBR1");
                    dt.Rows[i]["TRWNBR2"] = ITAB.GetString("TRWNBR2");
                    dt.Rows[i]["DANNBR1"] = ITAB.GetString("DANNBR1");
                    dt.Rows[i]["DANNBR2"] = ITAB.GetString("DANNBR2");
                    dt.Rows[i]["MCQNBR1"] = ITAB.GetString("MCQNBR1");
                    dt.Rows[i]["MCQNBR2"] = ITAB.GetString("MCQNBR2");
                    dt.Rows[i]["ENGDES"] = ITAB.GetString("ENGDES");
                    dt.Rows[i]["SPNDES"] = ITAB.GetString("SPNDES");
                    dt.Rows[i]["GERDES"] = ITAB.GetString("GERDES");
                    dt.Rows[i]["DESC1"] = ITAB.GetString("DESC1");
                    dt.Rows[i]["DESC2"] = ITAB.GetString("DESC2");
                    dt.Rows[i]["DESC3"] = ITAB.GetString("DESC3");
                    dt.Rows[i]["DESC4"] = ITAB.GetString("DESC4");
                    dt.Rows[i]["DESC5"] = ITAB.GetString("DESC5");
                    dt.Rows[i]["DESC6"] = ITAB.GetString("DESC6");
                    dt.Rows[i]["DESC7"] = ITAB.GetString("DESC7");
                    dt.Rows[i]["DESC8"] = ITAB.GetString("DESC8");
                    dt.Rows[i]["DESC9"] = ITAB.GetString("DESC9");
                    dt.Rows[i]["DESC10"] = ITAB.GetString("DESC10");
                    dt.Rows[i]["BOXCODE"] = ITAB.GetString("BOXCODE");
                    dt.Rows[i]["LBLCODE"] = ITAB.GetString("LBLCODE");
                    dt.Rows[i]["LBLTYPE"] = ITAB.GetString("LBLTYPE");
                    dt.Rows[i]["LBLSTYE"] = ITAB.GetString("LBLSTYE");

                    if ((ITAB.GetString("QRCODE") == null) || (ITAB.GetString("QRCODE") == "") || (ITAB.GetString("QRCODE") == " "))
                    {
                        dt.Rows[i]["QRCODE"] = "";
                        dt.Rows[i]["HBSQRCODE"] = "";
                    }
                    else
                    {
                        dt.Rows[i]["QRCODE"] = ITAB.GetString("QRCODE");
                        dt.Rows[i]["HBSQRCODE"] = dt.Rows[i]["QRCODE"].ToString() + dt.Rows[i]["CUS_ITEM"].ToString();
                    }

                }


            }
            BindingSource bs = new BindingSource();
            dtCount = dt.Rows.Count + 1;
            bs.DataSource = dt.DefaultView;
            dataGridView1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            dataGridView1.DataSource = bs;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                //表頭選擇
                column.HeaderCell = new
                DataGridViewAutoFilterColumnHeaderCell(column.HeaderCell);

                //禁止排序
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //欄位數值格式, 設定到資料最後一筆，避免大容量的excel檔
            app.Application.get_Range("A1", "BZ" + dtCount.ToString()).NumberFormat = "@";

            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            Microsoft.Office.Interop.Excel._Worksheet calcSheet = null;

            app.Visible = true;  // excel 程式是否顯示

            worksheet = workbook.Sheets["工作表2"];
            worksheet.Name = "標籤明細";

            //計算 dataGrid 的欄列數
            string maxCols = GetStandardExcelColumnName(dataGridView1.Columns.Count);
            string maxRows = dataGridView1.Rows.Count.ToString();

            //將 datagrid 填入 excel 中
            //填上標題列
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }

            //宣告 datagrid 沒有標題列
            dataGridView1.RowHeadersVisible = false;

            //將資料從 datagrid 貼到 object 
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null) Clipboard.SetDataObject(dataObj);

            //將 object 傳給 workbook
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

            //將資料貼入第二列 (第一列是標題列)
            Microsoft.Office.Interop.Excel.Range workRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 1];

            workRange.Select();

            //從剪貼薄貼上
            worksheet.PasteSpecial(workRange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            //將 worksheet 複製一份到 calcsheet
            worksheet.Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);

            // 複製的 excel 更名
            workbook.Sheets[workbook.Sheets.Count].Name = "計算張數";

            //選取 calcSheet
            calcSheet = workbook.Sheets[workbook.Sheets.Count];

            //刪掉不要的部份
            calcSheet.Range["A1:I" + maxRows].Delete();
            calcSheet.Range["B1:" + maxCols + maxRows].Delete();

            //去除重覆料號
            calcSheet.Range["A1:A" + maxRows].RemoveDuplicates(1);
            calcSheet.UsedRange.NumberFormat = "General";
            calcSheet.Range["A1"].Value = "料號";
            calcSheet.Range["B1"].Value = "張數";

            //求有值的最大列數
            int calcsheetMaxRows = calcSheet.Range["A1"].Offset[calcSheet.Rows.Count - 1, 0].End[Microsoft.Office.Interop.Excel.XlDirection.xlUp].Row;

            calcSheet.Range["B2"].Value = "=COUNTIF(標籤明細!J:J,A2)";

            //將公式往下複製
            calcSheet.Range["B2"].Copy(calcSheet.Range["B3:B" + calcsheetMaxRows]);

            //刪掉多的工作表
            workbook.Sheets["工作表2"].Delete();

        }

        private void generatePackingSheet(_Workbook workbook)
        {
            
            Microsoft.Office.Interop.Excel._Worksheet packingSheet = null;
            packingSheet = workbook.Sheets["工作表1"];
            packingSheet = workbook.ActiveSheet;
            packingSheet.Name = "訂單包裝明細";

            int packingKeyRow = 1;
            int orderNumRow = 2;
            int cusNumRow = 3;
            int cusNameRow = 4;
            int estDeliveryDateRow = 5;

            int firstColumnNum = 1;
            int lastVisbleColumnCount = 17;
            int itemHeaderRowStart = 7;
            int itemBodyRowStart = 8;
            int columnNum;

            // 將包裝單號，放置於工作表左上角，[第一列，第一行]
            packingSheet.Cells[packingKeyRow, firstColumnNum] = "包裝單號： " + packingKey;
            packingSheet.Cells[orderNumRow, firstColumnNum] = lblOrderNum.Text;
            packingSheet.Cells[cusNumRow, firstColumnNum] = lblCusNum.Text;
            packingSheet.Cells[cusNameRow, firstColumnNum] = lblCusName.Text;
            packingSheet.Cells[estDeliveryDateRow, firstColumnNum] = lblEstDeliveryDate.Text;

            // 計算項目資料筆數，lastVisbleColumnCount 是允許出貨組看到的最大欄位數
            itemCount = itemCount + lastVisbleColumnCount + dataGridView1.Rows.Count;

            //  excel 資料全部轉為文字 
            packingSheet.Columns.EntireColumn.NumberFormat = "@";

            int itemDetailRowStart = itemHeaderRowStart + 1;
            int textRowStart = itemDetailRowStart + dataGridView1.Rows.Count + 1;


            //表頭
            for (int i = 0; i <= lastVisbleColumnCount; i++)
            {
                columnNum = i + 1;
                packingSheet.Cells[itemHeaderRowStart, columnNum] = dataGridView1.Columns[i].HeaderText;
            }

            //項目
            //宣告 datagrid 沒有標題列
            dataGridView1.RowHeadersVisible = false;

            //將資料從 datagrid 貼到 object 
            dataGridView1.SelectAll();

            DataObject dobj = dataGridView1.GetClipboardContent();
            if (dobj != null) Clipboard.SetDataObject(dobj);

            //將 object 傳給 workbook
            packingSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

            //將資料貼入第七列 (前面是表頭資訊)
            Microsoft.Office.Interop.Excel.Range itemRangeStart = (Microsoft.Office.Interop.Excel.Range)packingSheet.Cells[itemBodyRowStart, firstColumnNum];
            itemRangeStart.Select();

            //從剪貼薄貼上
            packingSheet.PasteSpecial(itemRangeStart, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            //清除不需要資料的欄位
            packingSheet.Range["S" + itemBodyRowStart + ":AL" + dataGridView1.Rows.Count + itemBodyRowStart].Clear();

            int borderRange = dataGridView1.Rows.Count + itemHeaderRowStart;

            for (int i = itemHeaderRowStart; i < borderRange; i++)
            {
                //標註列，加上底線
                packingSheet.Range["A" + i + ":R" + i].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }

            //內文
            for (int s = 0; s < lbSalesText.Items.Count; s++)
            {
                packingSheet.Cells[s + textRowStart, 1] = lbSalesText.Items[s].ToString();
            }
            packingSheet.Cells.EntireColumn.AutoFit(); //自動調整欄寬
            ((Microsoft.Office.Interop.Excel.Range)packingSheet.Columns["A:B", System.Type.Missing]).ColumnWidth = 8;

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
                Cursor.Current = Cursors.WaitCursor;
                //寫入 PACKING table
                bulkWriteToPacking(dt);
                //寫入 CUSTOMS table
                bulkWriteToCustoms(dt);
                Cursor.Current = Cursors.Default;

                tsLabel.Text = "資料已寫入資料庫，鍵值為：" + packingKey + "，已自動複製到剪貼簿！";

                Clipboard.SetText(packingKey);

                /*
                DialogResult dr = MessageBox.Show("資料已寫入資料庫" + Environment.NewLine + "鍵值為：" + packingKey, "資訊");
                if (dr == DialogResult.OK)
                {
                    btnClear.PerformClick();
                }
                */
            }
        }
        private void bulkWriteToPacking(System.Data.DataTable dataTable)
        {
            SqlConnection bulkConn = new SqlConnection(dbConnStr);
            SqlBulkCopy SBC = new SqlBulkCopy(bulkConn);
            SBC.DestinationTableName = "dbo.PACKING";

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
        private void bulkWriteToCustoms(System.Data.DataTable dataTable)
        {
            SqlConnection bulkConn = new SqlConnection(dbConnStr);
            SqlBulkCopy SBC = new SqlBulkCopy(bulkConn);
            SBC.DestinationTableName = "dbo.CUSTOMS";

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
