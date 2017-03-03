using SAP.Middleware.Connector;

namespace connDB
{
    class sapConnClass
    {
        public RfcConfigParameters setParaToConn(string sapClient)
        {
            RfcConfigParameters rfcPara = new RfcConfigParameters();
            switch (sapClient)
            {
                case "800":
                    rfcPara.Add(RfcConfigParameters.Name, "PRD");
                    rfcPara.Add(RfcConfigParameters.AppServerHost, "192.168.0.16");
                    rfcPara.Add(RfcConfigParameters.Client, "800");
                    break;
                case "620":
                    rfcPara.Add(RfcConfigParameters.Name, "DEV");
                    rfcPara.Add(RfcConfigParameters.AppServerHost, "192.168.0.15");
                    rfcPara.Add(RfcConfigParameters.Client, "620");
                    break;
                case "300":
                    rfcPara.Add(RfcConfigParameters.Name, "DEV");
                    rfcPara.Add(RfcConfigParameters.AppServerHost, "192.168.0.15");
                    rfcPara.Add(RfcConfigParameters.Client, "300");
                    break;
                default:
                    rfcPara.Add(RfcConfigParameters.Name, "DEV");
                    rfcPara.Add(RfcConfigParameters.AppServerHost, "192.168.0.15");
                    rfcPara.Add(RfcConfigParameters.Client, "620");
                    break;
            }
            rfcPara.Add(RfcConfigParameters.User, "DDIC");
            rfcPara.Add(RfcConfigParameters.Password, "Ubn3dx");
            rfcPara.Add(RfcConfigParameters.SystemNumber, "00");
            rfcPara.Add(RfcConfigParameters.Language, "ZF");
            return rfcPara;
        }

    }
    class mssqlConnClass
    {
        public string toSBSDB(string conn)
        {
            //"Data Source=SBSDB;Initial Catalog=PACKING;Uid=PACKING;Pwd=Admin12-1;";
            conn = conn.ToUpper();
            string source, catalog, auth;
            source = "Data Source=SBSDB;";
            auth = "Uid=PACKING; Pwd=Admin12-1; ";
            switch (conn)
            {
                case "PRD":
                    catalog = "Initial Catalog=PACKING;";
                    break;
                case "DEV":
                    catalog = "Initial Catalog=PACKING_DEV;";
                    break;
                default:
                    catalog = "Initial Catalog=PACKING_DEV;";
                    break;
            }
            conn = source + catalog + auth;
            return conn;
        }

    }
}
