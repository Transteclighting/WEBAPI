using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;



namespace IFSAPI
{
    public partial class Home : System.Web.UI.Page
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string result = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                /**********Get Api**************/
                // Get_DatafromIFS_PrimarySales();



                /**********Post Api*************/
                //string msg0 = Post_NormalInvoice("Data Type : Normal Invoice");
                //lbl_normalinvoice.Text = msg0;

                //string msg1 = Post_Replacement_Issued("Data Type : Replacement (Issued)");
                //lbl_replaceissue.Text = msg1;

                //string msg2 = Post_TradePromotion("Data Type : Trade Promotion");
                //lbl_tradepromotion.Text = msg2;

                //string msg3 = Post_Breakage("Data Type : Trade Breakage");
                //lbl_breakage.Text = msg3;

                //string msg4 = Post_NormalInvoice_TD("Data Type : Normal (TD)");
                //lbl_tdnormalinvoice.Text = msg4;

                string msg5 = Post_SalesOrder("Data Type : Sales Order (Primary)");
                lbl_SalesOrder.Text = msg5;

                //string msg6 = Post_Replacement_Claim("Data Type : Replacement (Claim)");
                //lbl_Replaceclaim.Text = msg6;

                //string msg7 = Post_NormalInvoice_INS("Data Type : Lighting Invoices)");
                //lbl_LightingInvoices.Text = msg7;

            }
            catch (Exception ex)
            {
                lblGeneralError.Text = "Error" + ex.Message.ToString();
            }

        }
        /*****************Get Primary Sales from IFS and Send to CJ Start***************/
        public void Get_DatafromIFS_PrimarySales()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            HttpWebResponse response = null;
            try
            {
                int warehouseid = 2020;
                int lastInvoice = Check_LastInvoice(warehouseid);
                var jsonString = new StringBuilder();
                jsonString.Append("{\"Company\":\"TEL\",\"InvoiceId\":" + lastInvoice + "}");
                string urlPath = "https://telmig.transcomifs.com.bd:36080/int/ifsapplications/projection/v1/CIndentOrderServices.svc/";
                string req = urlPath + "GetOrderInvoices(InvInput=@InvInput)?@InvInput=" + jsonString.ToString();
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(req);
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers["Authorization"] = "Basic aWZzYXBwOnRlbG1pZw==";
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                var result = (sr.ReadToEnd());
                JToken jToken = JToken.Parse(result);
                //var rresult = jToken["value"].SelectMany(x => x["InvoiceLine"].Select(z => z).ToList());
                var rresult = jToken["value"].Select(z => z).ToList();
                SalesInvoice inv = new SalesInvoice();
                foreach (var item in rresult)
                {
                    string[] formats = { "dd-MMM-yyyy" };
                    inv.InvoiceID = Convert.ToInt32(item["InvoiceId"]);
                    lastInvoice = Convert.ToInt32(item["InvoiceId"]);
                    inv.InvoiceNo = Convert.ToString(item["InvoiceNo"]);
                    inv.InvoiceDate = item["InvoiceDate"].ToString();
                    inv.InvoiceStatus = 2;
                    inv.CustomerID = getCustomerID(item["CustomerCode"].ToString());
                    inv.WarehouseID = 68;
                    inv.InvoiceAmount = Convert.ToDouble(item["InvoiceAmount"]);
                    inv.Discount = 0;
                    inv.Remarks = "";
                    inv.OrderID = Convert.ToInt32(item["InvoiceId"]);
                    inv.SalesPersonID = 00;
                    inv.SalesPersonID = 00;
                    inv.UpdateUserID = null;
                    inv.CreateDate = Convert.ToDateTime(item["InvoiceDate"]);
                    inv.VATChallanNo = null;
                    if (item["IndentType"].ToString() == "Normal")
                    {
                        inv.InvoiceTypeID = 1;
                    }
                    else if (item["IndentType"].ToString() == "Replacement")
                    {
                        inv.InvoiceTypeID = 3;
                    }
                    else
                    {
                        inv.InvoiceTypeID = 0;
                    }
                    inv.InvoiceBy = 1;
                    inv.DeliveryAddress = "";
                    inv.PriceOptionID = null;
                    inv.DeliveredBy = null;
                    inv.DeliveryDate = Convert.ToDateTime(item["InvoiceDate"]);
                    inv.Insert_SalesInvoice();
                    foreach (var line in item["InvoiceLine"])
                    {
                        inv.InvoiceItemId = Convert.ToDouble(line["InvoiceItemId"]);
                        inv.LineNo = Convert.ToString(line["LineNo"]);
                        inv.DelNo = Convert.ToString(line["DelNo"]);
                        inv.LineItemNo = Convert.ToDouble(line["InvoiceItemId"]);
                        inv.ProductCode = Convert.ToString(line["ProductCode"]);
                        inv.ProductID = getProductID(inv.ProductCode);
                        inv.Description = Convert.ToString(line["Description"]);
                        inv.Quantity = Convert.ToInt16(line["InvoicedQty"]);
                        inv.UnitPrice = Convert.ToDouble(line["UnitPrice"]);
                        inv.AdjDPAmount = Convert.ToDouble(line["DP"]);
                        inv.AdjTPAmount = Convert.ToDouble(line["TP"]);
                        inv.AdjPWAmount = Convert.ToDouble(line["SC"]);
                        inv.RC = Convert.ToDouble(line["RC"]);
                        inv.OtherPromo = Convert.ToDouble(line["OtherPromo"]);
                        inv.VATAmount = Convert.ToDouble(line["VatAmount"]);
                        inv.PerUnitVat = Convert.ToDouble(line["PerUnitVat"]);
                        inv.GrossAmount = Convert.ToDouble(line["GrossAmount"]);
                        inv.VatCode = Convert.ToString(line["VatCode"]);
                        inv.VatRate = Convert.ToDouble(line["VatRate"]);
                        inv.UnitOfMeas = Convert.ToString(line["UnitOfMeas"]);
                        inv.CostPrice = Convert.ToDouble(line["CostPrice"]);

                        if (inv.InvoiceTypeID == 1 && inv.UnitPrice == 0)
                        {
                            inv.IsFree = 1;
                            inv.FreeQty = Convert.ToInt16(line["InvoicedQty"]);
                        }
                        else
                        {
                            inv.IsFree = 0;
                            inv.FreeQty = 0;
                        }
                        // inv.systemPrice = Convert.ToDouble(line["InvoicedQty"]);
                        double TotalDiscount = inv.AdjDPAmount + inv.AdjPWAmount + inv.AdjTPAmount + inv.OtherPromo;
                        inv.TradePrice = TPCalculation(inv.VatRate, inv.UnitPrice, TotalDiscount, inv.systemPrice, inv.VatCode, inv.ProductCode);

                        //For TEL - Serial Will be Inserted to  (t_SalesInvoiceProductSerial)
                        foreach (var serial in line["Serials"])
                        {
                            inv.ProductSerialNo = serial["SerialNo"].ToString();
                            inv.Insert_ProductSerial();
                        }
                        inv.Insert_SalesInvoiceDetails();
                        inv.InsertVAT63(inv.InvoiceID, inv.InvoiceNo, inv.WarehouseID, inv.Remarks);
                    }
                }

                //Keep Last Invoice no"
                UpdateReffNo(lastInvoice, warehouseid);
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    lblGeneralError.Text = (response.StatusCode.ToString());
                }
                else
                {
                    lblGeneralError.Text = e.Status.ToString();
                }
            }
            finally
            {
                if (response != null)
                {
                    lblGeneralError.Text = result.ToString();
                }
            }
        }
        public bool IsTPVatProductExists(string sProductCode)
        {
            double _TradePrice = 0;
            int nCount = 0;
            try
            {
                string sql = "SELECT * FROM t_TPVATProduct where ProductID = (Select ProductID from t_Product where ProductCode = '" + sProductCode + "')";
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        IDataReader reader = cmd.ExecuteReader();
                        con.Close();
                        if (reader.Read())
                        {
                            _TradePrice = Convert.ToDouble(reader["TradePrice"]);
                            nCount++;
                        }
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (nCount > 0)
                return true;
            else return false;
        }
        public double TPCalculation(double _VatRate, double _UnitPrice, double _DiscountAmt, double SystemTradePrice, string _VatCode, string sProductCode)
        {
            double _TradePrice = 0;


            SystemTradePrice = Math.Round(_UnitPrice / (1 + _VatRate), 2, MidpointRounding.AwayFromZero);

            #region Calculte Per Unit Discount & Trade Price
            double _CalculativeTP = 0;
            try
            {
                _CalculativeTP =
                    Math.Round((_UnitPrice - _DiscountAmt) / (1 + _VatRate), 2,
                        MidpointRounding.AwayFromZero);
            }
            catch
            {
                _CalculativeTP = SystemTradePrice;
            }
            if (IsTPVatProductExists(sProductCode))
            {
                _TradePrice = SystemTradePrice;

            }
            else
            {
                if (_VatCode == "VAT_5")
                {
                    _TradePrice = _CalculativeTP;
                }
                else
                {
                    if (_CalculativeTP < SystemTradePrice)
                    {
                        _TradePrice = SystemTradePrice;
                    }
                    else
                    {
                        _TradePrice = _CalculativeTP;
                    }

                }
            }


            #endregion
            return _TradePrice;
        }

        /*****************Get API ENDS Here ********************************************

        /****** Data Table For Post APIs*****/
        public DataTable TradePromotion(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select a.InvoiceNo RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,TerritoryID SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,
            IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID  in (15)
            and InvoiceDate between '08-Nov-2022' and '10-Nov-2022'
            and InvoiceDate<'10-Nov-2022'   and ChannelID =2
            --and InvoiceNo=120158829
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable Breakage(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select a.InvoiceNo RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,TerritoryID SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,
            IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID  in (17)
            and InvoiceDate between '08-Nov-2022' and '10-Nov-2022'
            and InvoiceDate<'10-Nov-2022'   and ChannelID =2
            --and InvoiceNo=120159040
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable Prepare_Invoice_Data_INS(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select a.InvoiceNo RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,'0192' SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID not in (3,15,17)
            and InvoiceDate between '01-Nov-2022' and '10-Nov-2022'
            and InvoiceDate<'10-Nov-2022'   and ChannelID in (8,12) 
            --and CustomerCode in (50100405) 
            and InvoiceNo=170013971
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable Replacement(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select a.InvoiceNo RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,TerritoryID SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,
            IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID  in (3)
            and InvoiceDate between '08-Nov-2022' and '10-Nov-2022'
            and InvoiceDate<'10-Nov-2022'   and ChannelID =2
            --and InvoiceNo=120158828
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable NormalInvoice(int RefforderNo)
        {
            //  RefforderNo = 140490;
            //InvoiceNo as RefOrderNo
            string sql = @"
            Select  InvoiceNo as RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,TerritoryID SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,
            IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID not in (3,15,17)
            and InvoiceDate between '06-Nov-2022' and '08-Nov-2022' and InvoiceDate<'08-Nov-2022'
            and ChannelID =2            
            --and InvoiceNo=120158924
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable NormalInvoice_TD(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select isnull(cast(OrderNo as varchar),InvoiceNo) RefOrderNo,'MFG' as Site,
            CustomerCode CustomerNo,'*' SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,IFSIndentType IndentType
            From DistSysDB.dbo.t_SalesInvoice a
            join DistSysDB.dbo.t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join DistSysDB.dbo.t_Product c on b.ProductID=c.ProductID
            join DistSysDB.dbo.t_Customer d on a.CustomerID=d.CustomerID
            join DistSysDB.dbo.t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join DistSysDB.dbo.t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse'
            and InvoiceNo='613010151'";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable NormalInvoice_INS(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select a.InvoiceNo RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo,'0192' SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,Quantity+FreeQty OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,IFSIndentType IndentType
            From t_SalesInvoice a
            join t_SalesInvoiceDetail b on a.InvoiceID=b.InvoiceID
            join t_Product c on b.ProductID=c.ProductID
            join v_CustomerDetails d on a.CustomerID=d.CustomerID
            join t_InvoiceType e on a.InvoiceTypeID=e.InvoiceTypeID
            left outer join t_SalesOrder f on a.OrderID=f.OrderID
            where IFSIndentType<>'Reverse' and a.InvoiceStatus<>3
            and a.InvoiceTypeID not in (3,15,17) and InvoiceDate between '01-Nov-2022' and '10-Nov-2022'
            and InvoiceDate<'10-Nov-2022'   and ChannelID in (8,12) and InvoiceNo=170013971 order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }
        public DataTable SalesOrder_Data(int RefforderNo)
        {
            //  RefforderNo = 140490;
            string sql = @"Select  OrderNo as RefOrderNo,'BLL1' as Site,
            CustomerCode CustomerNo, TerritoryID SalesmanCode,0 PayTermId,a.Remarks,ProductCode PartNo,
            0 DbStock,0 BufferStock,isnull(Quantity + FreeQty, 0) OrderQty,0 LastWeekSecSales,0 MtdSecondarySales,
            'Normal' IndentType
            From t_SalesOrder a
            join t_SalesOrderDetail b on a.OrderID = b.OrderID
            join t_Product c on b.ProductID = c.ProductID
            join v_CustomerDetails d on a.CustomerID = d.CustomerID
            where
            OrderStatus = 1 and OrderDate between '01-Dec-2022' and '20-Dec-2022' and OrderDate< '20-Dec-2022'
            and ChannelID = 2 and(Quantity + FreeQty) > 0
            order by RefOrderNo";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    return dt;
                    //return DataTableToJsonWithStringBuilder(dt);                  
                }
            }
        }

        /******** Call Process for Temporary Api's (Initial Process to Upload Backlocked Data)***********/
        public string Post_TradePromotion(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = TradePromotion(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_Breakage(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = Breakage(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_NormalInvoice(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = NormalInvoice(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_Replacement_Issued(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = Replacement(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            else if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");


                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }


            //Update Last "OrderReffNo" in  Table after finishing insert
            UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_NormalInvoice_TD(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = NormalInvoice_TD(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            //UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_NormalInvoice_INS(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int lastReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = NormalInvoice_INS(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                lastReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), lastReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            //UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }

        /********Call Process for Permanent APIs***********/
        public string Post_SalesOrder(string Module)
        {
            string exe = "";
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 1010;
            int NextReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable table = new DataTable();
            table = SalesOrder_Data(ReffOrder);
            // Make the Format
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (j == 0)
                            {
                                NextReffNo = Convert.ToInt32(table.Rows[i][j]);
                            }
                            if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
                            }
                            else
                            {
                                jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + table.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
                        }
                    }
                    jsonString.Append("}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), NextReffNo, Module) + "\n";
                }
            //Update Last "OrderReffNo" in  Table after finishing insert
            //UpdateReffNo(lastReffNo, warehouseid);
            return exe;
        }
        public string Post_Replacement_Claim(string Module)
        {
            string exe = "";
            ReplacementOrder RepOrd = new ReplacementOrder();
            //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
            int warehouseid = 202;
            int NextReffNo = 0;
            int ReffOrder = Check_LastInvoice(warehouseid);
            // Get Data From Query
            DataTable Rep_Claim = new DataTable();
            DataTable Rep_CLaim_Lines = new DataTable();


            Rep_Claim = RepOrd.Get_ReplacementOrder(ReffOrder);

            // Make the Format
            //var jsonString = new StringBuilder();
            var jsonString = new StringBuilder();
            if (Rep_Claim.Rows.Count > 0)
            {

                for (int i = 0; i < Rep_Claim.Rows.Count; i++)
                {
                    NextReffNo = Convert.ToInt32(Rep_Claim.Rows[i][0]);
                    Rep_CLaim_Lines = RepOrd.Get_ReplacementOrder_Details(NextReffNo);
                    jsonString.Clear();
                    jsonString.Append("{");
                    for (int j = 0; j < Rep_Claim.Columns.Count; j++)
                    {
                        if (j < Rep_Claim.Columns.Count - 1)
                        {

                            if (j == 3 || j == 4)
                            {
                                jsonString.Append("\"" + Rep_Claim.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(Rep_Claim.Rows[i][j]) + ",");
                            }
                            else
                            {
                                if (j == 0)
                                {
                                    NextReffNo = Convert.ToInt32(Rep_Claim.Rows[i][j]);
                                }

                                jsonString.Append("\"" + Rep_Claim.Columns[j].ColumnName.ToString()
                                                  + "\":" + "\""
                                                  + Rep_Claim.Rows[i][j].ToString() + "\",");
                            }
                        }
                        else if (j == Rep_Claim.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + Rep_Claim.Columns[j].ColumnName.ToString() + "\":" + "\"" + Rep_Claim.Rows[i][j].ToString() + "\" ,");
                        }
                    }
                    //------------Lines Start
                    if (Rep_CLaim_Lines.Rows.Count > 0)
                    {
                        jsonString.Append("\"ReplaceLines\": [");
                        for (int k = 0; k < Rep_CLaim_Lines.Rows.Count; k++)
                        {
                            jsonString.Append("{");
                            for (int j = 0; j < Rep_CLaim_Lines.Columns.Count; j++)
                            {
                                if (j < Rep_CLaim_Lines.Columns.Count - 1)
                                {

                                    if (j == 0 || j == 2 || j == 4)
                                    {
                                        jsonString.Append("\"" + Rep_CLaim_Lines.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(Rep_CLaim_Lines.Rows[k][j]) + ",");
                                    }
                                    else
                                    {
                                        jsonString.Append("\"" + Rep_CLaim_Lines.Columns[j].ColumnName.ToString()
                                                          + "\":" + "\"" + Rep_CLaim_Lines.Rows[k][j].ToString() + "\",");
                                    }
                                }
                                else if (j == Rep_CLaim_Lines.Columns.Count - 1)
                                {
                                    jsonString.Append("\"" + Rep_CLaim_Lines.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(Rep_CLaim_Lines.Rows[k][j]) + " ");
                                    if (k == Rep_CLaim_Lines.Rows.Count - 1)
                                    {
                                        jsonString.Append("}");
                                    }
                                    else
                                    {
                                        jsonString.Append("},");
                                    }
                                }
                            }
                        }
                    }

                    //----------------Lines End
                    jsonString.Append("]}}");
                    //Send To API
                    exe += Format_Json(jsonString.ToString(), NextReffNo, Module) + "\n";
                }
                //Update Last "OrderReffNo" in  Table after finishing insert
                UpdateReffNo(NextReffNo, warehouseid);
            }
            return exe;
        }

        /*************Global Functions (Used in ALL Api's)*******************/
        public string Format_Json(string jsonString, int reffno, string Module)
        {
            string RT = "";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            try
            {
                string url = "https://telmig.transcomifs.com.bd:36080/int/ifsapplications/projection/v1/CIndentOrderServices.svc/NewIndentLine";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers["Authorization"] = "Basic aWZzYXBwOnRlbG1pZw==";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"IndentInput\":" + jsonString.ToString();
                    streamWriter.Write(json);
                }
                var response = (HttpWebResponse)request.GetResponse();
                //  var obj = JsonConvert.DeserializeObject<Response>(response.ToString());
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result1 = streamReader.ReadToEnd();
                    result = result1;
                    var obj = JsonConvert.DeserializeObject<Response>(result);


                    string error = "";
                    if (obj.ErrorMessage == null)
                    {
                        error = "Order No:" + obj.OrderNo + "/" + "Line No :" + obj.LineNo;
                    }
                    else
                    {
                        error = obj.ErrorMessage;
                        Insert_ErrorinLog(reffno.ToString(), Module, obj.ErrorMessage.ToString());
                    }
                    RT = "[" + "Invoice No:" + reffno + "," + "Message : " + error + "]";


                    //if we want to update Reff No after inserting Every Line then Open This
                    //UpdateReffNo(reffno);
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
                lblGeneralError.Text = result.ToString();
            }
            return RT;
        }
        public void Insert_ErrorinLog(string reff, string Module, string error)
        {
            string sql = @"Insert into ifserror_log values('" + error + "','" + reff + "','" + Module + "')";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        private int Check_LastInvoice(int WarehouseID)
        {
            // using (SqlConnection con = new SqlConnection("Data Source=10.168.14.36;Initial Catalog=BLLSysDB;User ID=bllsyscon; Password=bllconnection;Integrated Security=True"))
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sql = "Select NextInvoiceNo from t_NextDocumentNo where WarehouseID = " + WarehouseID + " ";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int LastDocumentNo = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return LastDocumentNo;
                }
            }
        }
        public int getCustomerID(string code)
        {
            code = "22300048";
            using (SqlConnection con1 = new SqlConnection(ConnectionString))
            {
                string sql = "Select CustomerID from v_CustomerDetails where CUstomercode='" + code + "'";
                using (SqlCommand cmd1 = new SqlCommand(sql, con1))
                {
                    con1.Open();
                    int id = Convert.ToInt16(cmd1.ExecuteScalar());
                    con1.Close();
                    return id;
                }
            }
        }
        public int getProductID(string code)
        {
            code = "10011";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sql = "Select ProductID from v_ProductDetails where ProductCode='" + code + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int id = Convert.ToInt16(cmd.ExecuteScalar());
                    con.Close();
                    return id;
                }
            }
        }
        private void UpdateReffNo(int documentno, int warehouseid)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sql = "Update t_NextDocumentNo set   NextInvoiceNo=" + documentno + " where WarehouseID = " + warehouseid + "";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        /************** Global Function End ******************/


        //*******Replacement Claim  Data To IFS

        //public string ReplacementOrder_JsonData(string jsonString, int reffno)
        //{
        //    string RT = "";
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
        //    try
        //    {
        //        // var request = (HttpWebRequest)WebRequest.Create("https://teltest.transcomifs.com.bd:56080/int/ifsapplications/projection/v1/CReplacementOrderServices.svc/NewReplacementOrder");
        //        var request = (HttpWebRequest)WebRequest.Create("https://telmig.transcomifs.com.bd:36080/int/ifsapplications/projection/v1/CReplacementOrderServices.svc/NewReplacementOrder");
        //        request.ContentType = "application/json";
        //        request.Method = "POST";
        //        //Test Key : aWZzYXBwOmlmc3Rlc3Q =
        //        request.Headers["Authorization"] = "Basic aWZzYXBwOnRlbG1pZw==";
        //        //request.Headers["Authorization"] = "Basic aWZzYXBwOmlmc3Rlc3Q=";
        //        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //        {
        //            string json = "{\"ReplaceInput\":" + jsonString.ToString();
        //            streamWriter.Write(json);
        //        }
        //        var response = (HttpWebResponse)request.GetResponse();
        //        using (var streamReader = new StreamReader(response.GetResponseStream()))
        //        {
        //            var result1 = streamReader.ReadToEnd();
        //            result = result1;
        //            //if we want to update Reff No after inserting Every Line then Open This
        //            //UpdateReffNo(reffno);

        //            var obj = JsonConvert.DeserializeObject<Response>(result);

        //            string error = "";
        //            if (obj.ErrorMessage == null)
        //            {
        //                error = "Order No:" + obj.OrderNo + "/" + "Line No :" + obj.LineNo;
        //            }
        //            else
        //            {
        //                error = obj.ErrorMessage;
        //                Insert_ErrorinLog(reffno.ToString(), "Replace Claim", obj.ErrorMessage.ToString());
        //            }
        //            RT = "[" + "Invoice No:" + reffno + "," + "Message : " + error + "]";

        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // lblGeneralError.Text = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
        //        lblGeneralError.Text = result.ToString();
        //    }
        //    return RT;
        //}

        //*****************Post Sales Order to IFS Start***************
        //public void Post_SalesOrder_Data_To_Json()
        //{
        //    //Warehouseid =2020 for InvoiceID and 1010 for ReffOrderID
        //    int warehouseid = 1010;
        //    int lastReffNo = 0;
        //    int ReffOrder = Check_LastInvoice(warehouseid);
        //    // Get Data From Query
        //    DataTable table = new DataTable();
        //    table = Prepare_SalesOrder_Data(ReffOrder);
        //    // Make the Format
        //    var jsonString = new StringBuilder();
        //    if (table.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < table.Rows.Count; i++)
        //        {
        //            jsonString.Clear();
        //            jsonString.Append("{");
        //            for (int j = 0; j < table.Columns.Count; j++)
        //            {
        //                if (j < table.Columns.Count - 1)
        //                {
        //                    if (j == 0)
        //                    {
        //                        lastReffNo = Convert.ToInt32(table.Rows[i][j]);
        //                    }
        //                    if (j == 7 || j == 8 || j == 9 || j == 10 || j == 11)
        //                    {
        //                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + Convert.ToInt32(table.Rows[i][j]) + ",");
        //                    }
        //                    else
        //                    {
        //                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
        //                                          + "\":" + "\""
        //                                          + table.Rows[i][j].ToString() + "\",");
        //                    }
        //                }
        //                else if (j == table.Columns.Count - 1)
        //                {
        //                    jsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\" ");
        //                }
        //            }
        //            jsonString.Append("}}");
        //            //Send To API
        //            SalesOrder_JsonData(jsonString.ToString(), lastReffNo);
        //        }
        //        //Update Last "OrderReffNo" in  Table after finishing insert
        //        UpdateReffNo(lastReffNo, warehouseid);
        //    }
        //}
        //public void SalesOrder_JsonData(string jsonString, int reffno)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
        //    try
        //    {
        //        var request = (HttpWebRequest)WebRequest.Create("https://telmig.transcomifs.com.bd:36080/int/ifsapplications/projection/v1/CIndentOrderServices.svc/NewIndentLine");
        //        request.ContentType = "application/json";
        //        request.Method = "POST";
        //        request.Headers["Authorization"] = "Basic aWZzYXBwOmlmc21pZw==";
        //        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //        {
        //            //string json = "{\"IndentInput\":{\"RefOrderNo\":\"10002\",\"Site\":\"BLL1\",\"CustomerNo\":\"D002\",\"SalesmanCode\":\"\",\"PayTermId\":\"CASH\",\"Remarks\":\"Test By MIS  BLL\",\"PartNo\":\"1003\",\"DbStock\":100,\"BufferStock\":15,\"OrderQty\":0,\"LastWeekSecSales\":0,\"MtdSecondarySales\":0}}";
        //            //string json = "{\"IndentInput\":" + ConvertDataTabletoString();
        //            string json = "{\"IndentInput\":" + jsonString.ToString();
        //            streamWriter.Write(json);
        //        }

        //        var response = (HttpWebResponse)request.GetResponse();
        //        using (var streamReader = new StreamReader(response.GetResponseStream()))
        //        {
        //            var result1 = streamReader.ReadToEnd();
        //            result = result1;
        //            //if we want to update Reff No after inserting Every Line then Open This
        //            //UpdateReffNo(reffno);

        //            var obj = JsonConvert.DeserializeObject<Response>(result);
        //            if (obj.ErrorMessage != null)
        //            {
        //                Insert_ErrorinLog(reffno.ToString(), "Indent Order", obj.ErrorMessage.ToString());
        //            }
        //        }


        //    }
        //    catch (WebException ex)
        //    {
        //        result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
        //    }
        //}

    }

}
