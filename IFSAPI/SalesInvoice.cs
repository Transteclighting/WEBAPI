
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace IFSAPI
{

    public class SalesInvoice
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int InvoiceStatus { get; set; }
        public int CustomerID { get; set; }
        public int WarehouseID { get; set; }
        public double InvoiceAmount { get; set; }
        public double Discount { get; set; }
        public string Remarks { get; set; }
        public int OrderID { get; set; }
        public int SalesPersonID { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int? VATChallanNo { get; set; }
        public int InvoiceTypeID { get; set; }
        public int InvoiceBy { get; set; }
        public string DeliveryAddress { get; set; }
        public int? PriceOptionID { get; set; }
        public int? DeliveredBy { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int? InvoicePrintCOunter { get; set; }
        public string RefInvoiceID { get; set; }
        public string InvoicePrintByString { get; set; }
        public double DueAmount { get; set; }
        public string RefDetails { get; set; }
        public double OtherCharge { get; set; }
        public int? UploadStatus { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime DownloadDate { get; set; }
        public int? RowStatus { get; set; }
        public int? Terminal { get; set; }
        public int? SundryCustomerID { get; set; }
        public int? SalePromotionID { get; set; }
        public string ProductCode { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public double TradePrice { get; set; }
        public double VATAmount { get; set; }
        public double ProductDiscount { get; set; }
        public double AdjDPAmount { get; set; }
        public double AdjPWAmount { get; set; }
        public double AdjTPAmount { get; set; }
        public double RC { get; set; }
        public int IsFree { get; set; }
        public int FreeQty { get; set; }
        public double SalesPersonBonus { get; set; }
        public double CustomerProfitMargin { get; set; }
        public double CustomerSellingExpense { get; set; }
        public string ProductSerialNo { get; set; }
        public double OtherPromo { get; set; }
        public string VatCode { get; set; }
        public string DelNo { get; internal set; }
        public string LineNo { get; internal set; }
        public double InvoiceItemId { get; internal set; }
        public double LineItemNo { get; internal set; }
        public string Description { get; internal set; }
        public double PerUnitVat { get; internal set; }
        public string UnitOfMeas { get; internal set; }
        public double VatRate { get; internal set; }
        public double GrossAmount { get; internal set; }
        public double systemPrice { get; internal set; }

        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public void Insert_SalesInvoice()
        {
            string[] formats = { "dd-MMM-yyyy" };
            //InvoiceID = 901981763;
            InvoiceBy = 1575;
            CustomerID = 2233;
            string sql = @"insert into t_SalesInvoice_test(InvoiceID,InvoiceDate,InvoiceNo,InvoiceStatus,CustomerID,WarehouseID,InvoiceAmount,Discount,OrderID,InvoiceTypeID,InvoiceBy) 
            values(" + InvoiceID + ",'" + InvoiceDate + "','" + InvoiceNo + "'," + InvoiceStatus + @"," + CustomerID + "," + WarehouseID + "," + InvoiceAmount + "," + Discount + "," + OrderID + @"," + InvoiceTypeID + "," + InvoiceBy + ")";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
        }
        public void Insert_SalesInvoiceDetails()
        {
            string[] formats = { "dd-MMM-yyyy" };
            //  InvoiceID = 901981763;
            InvoiceBy = 1575;
            string sql = @"insert into t_SalesInvoiceDetail_test(InvoiceID,ProductID,Quantity,UnitPrice,CostPrice,TradePrice,
            VatAmount,ProductDiscount,AdjustedDPAmount,AdjustedPWAmount,AdjustedTPAmount,IsFreeProduct,FreeQty,SalesPersonBonus,CustomerProfitMargin,CustomerSellingExpense,Tradepromotion,Advertisement,RetailCommission,ProductWarrenty,PrimaryFreightCost,OtherProvission,PromotionalDiscount) 
            values(" + InvoiceID + "," + ProductID + "," + Quantity + "," + UnitPrice + @"," + CostPrice + "," + TradePrice + "," + VATAmount + @",
                   " + Discount + "," + AdjDPAmount + "," + AdjPWAmount + "," + AdjTPAmount + "," + IsFree + "," + FreeQty + ",0,0,0,0,0,0,0,0,0,0)";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
        }
        public void Insert_ProductSerial()
        {
            string[] formats = { "dd-MMM-yyyy" };
            //  InvoiceID = 901981763;
            InvoiceBy = 1575;
            string sql = @"insert into t_SalesInvoiceProductSerial(InvoiceID,ProductID,SerialNo,ProductSerialNo,EntryDate,EntryUserID) 
            values(" + InvoiceID + "," + ProductID + "," + 1 + ",'" + ProductSerialNo + "','21-Sep-2022'," + InvoiceBy + ")";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
        }
        public void InsertVAT63(int nTranID, string sTranNo, int WarehouseID, string Remarks)
        {
            /*    Better Understnding

                  TranID=InvoiceID,  
                  TranNo=InvoiceNo

             */
            DutyAccounts oDetail = new DutyAccounts();
            oDetail.GetDutyDetailNew("Invoice", Convert.ToInt32(nTranID));

            double _TotalAmount_63_15 = 0;
            double _TotalAmount_63_5 = 0;
            double _TotalAmount_Exampled = 0;

            int countMUSHAK_63_15 = 0;
            int countMUSHAK_63_5 = 0;
            int countMUSHAK_Exampled = 0;

            DutyTran oDutyTranVAT63_15 = new DutyTran();
            DutyTran oDutyTranVAT63_5 = new DutyTran();
            DutyTran oDutyTranVATExampled = new DutyTran();

            foreach (DutyAccount oDutyTranDetail in oDetail)
            {
                if (oDutyTranDetail.VATType == (int)Dictionary.VATType.IMPORTED_15 || oDutyTranDetail.VATType == (int)Dictionary.VATType.LOCAL_15)
                {
                    if (countMUSHAK_63_15 == 0)
                    {

                        oDutyTranVAT63_15.DutyTranDate = DateTime.Now;
                        oDutyTranVAT63_15.WHID = WarehouseID;
                        oDutyTranVAT63_15.ChallanTypeID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVAT63_15.DocumentNo = sTranNo;
                        oDutyTranVAT63_15.RefID = nTranID;
                        oDutyTranVAT63_15.DutyTypeID = (int)Dictionary.DutyType.VAT;
                        oDutyTranVAT63_15.DutyTranTypeID = (int)Dictionary.DutyTranType.INVOICE;
                        oDutyTranVAT63_15.Remarks = Remarks;
                        oDutyTranVAT63_15.DutyAccountID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVAT63_15.Amount = 0;
                        oDutyTranVAT63_15.InsertVAT63ForHO();
                        countMUSHAK_63_15++;
                    }

                    DutyTranDetail oItem = new DutyTranDetail();
                    oItem.DutyTranID = oDutyTranVAT63_15.DutyTranID;
                    oItem.ProductID = oDutyTranDetail.ProductID;
                    oItem.Qty = oDutyTranDetail.Qty;
                    oItem.DutyPrice = oDutyTranDetail.TradePrice;
                    oItem.DutyRate = oDutyTranDetail.DutyRate;
                    oItem.WHID = WarehouseID;
                    oItem.UnitPrice = oDutyTranDetail.UnitPrice;
                    oItem.VAT = (oDutyTranDetail.TradePrice * oDutyTranDetail.DutyRate) * oDutyTranDetail.Qty;
                    oItem.VATType = oDutyTranDetail.VATType;
                    DutyTran oVatPaidData = new DutyTran();
                    oItem.VATPaidQty = oVatPaidData.GetVATPaidQty(oItem.ProductID, sTranNo);
                    _TotalAmount_63_15 = _TotalAmount_63_15 + oItem.VAT;
                    oItem.InsertNewVATHO63();

                }
                else if (oDutyTranDetail.VATType == (int)Dictionary.VATType.LOCAL_5 || oDutyTranDetail.VATType == (int)Dictionary.VATType.IMPORTED_5)
                {
                    if (countMUSHAK_63_5 == 0)
                    {

                        oDutyTranVAT63_5.DutyTranDate = DateTime.Now;
                        oDutyTranVAT63_5.WHID = WarehouseID;
                        oDutyTranVAT63_5.ChallanTypeID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVAT63_5.DocumentNo = sTranNo;
                        oDutyTranVAT63_5.RefID = nTranID;
                        oDutyTranVAT63_5.DutyTypeID = (int)Dictionary.DutyType.VAT;
                        oDutyTranVAT63_5.DutyTranTypeID = (int)Dictionary.DutyTranType.INVOICE;
                        oDutyTranVAT63_5.Remarks = Remarks;
                        oDutyTranVAT63_5.DutyAccountID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVAT63_5.Amount = 0;
                        oDutyTranVAT63_5.InsertVAT63ForHO();
                        countMUSHAK_63_5++;
                    }

                    DutyTranDetail oItem = new DutyTranDetail();
                    oItem.DutyTranID = oDutyTranVAT63_5.DutyTranID;
                    oItem.ProductID = oDutyTranDetail.ProductID;
                    oItem.Qty = oDutyTranDetail.Qty;
                    oItem.DutyPrice = oDutyTranDetail.TradePrice;
                    oItem.DutyRate = oDutyTranDetail.DutyRate;
                    oItem.WHID = WarehouseID;
                    oItem.UnitPrice = oDutyTranDetail.UnitPrice;
                    oItem.VAT = (oDutyTranDetail.TradePrice * oDutyTranDetail.DutyRate) * oDutyTranDetail.Qty;
                    oItem.VATType = oDutyTranDetail.VATType;
                    DutyTran oVatPaidData = new DutyTran();
                    oItem.VATPaidQty = oVatPaidData.GetVATPaidQty(oItem.ProductID, sTranNo);

                    _TotalAmount_63_5 = _TotalAmount_63_5 + oItem.VAT;
                    oItem.InsertNewVATHO63();

                }
                else
                {
                    if (countMUSHAK_Exampled == 0)
                    {

                        oDutyTranVATExampled.DutyTranDate = DateTime.Now;
                        oDutyTranVATExampled.WHID = WarehouseID;
                        oDutyTranVATExampled.ChallanTypeID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVATExampled.DocumentNo = sTranNo;
                        oDutyTranVATExampled.RefID = nTranID;
                        oDutyTranVATExampled.DutyTypeID = (int)Dictionary.DutyType.VAT;
                        oDutyTranVATExampled.DutyTranTypeID = (int)Dictionary.DutyTranType.INVOICE;
                        oDutyTranVATExampled.Remarks = Remarks;
                        oDutyTranVATExampled.DutyAccountID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
                        oDutyTranVATExampled.Amount = 0;
                        oDutyTranVATExampled.InsertVAT63ForHO();
                        countMUSHAK_Exampled++;
                    }

                    DutyTranDetail oItem = new DutyTranDetail();
                    oItem.DutyTranID = oDutyTranVATExampled.DutyTranID;
                    oItem.ProductID = oDutyTranDetail.ProductID;
                    oItem.Qty = oDutyTranDetail.Qty;
                    oItem.DutyPrice = oDutyTranDetail.TradePrice;
                    oItem.DutyRate = oDutyTranDetail.DutyRate;
                    oItem.WHID = WarehouseID;
                    oItem.UnitPrice = oDutyTranDetail.UnitPrice;
                    oItem.VAT = (oDutyTranDetail.TradePrice * oDutyTranDetail.DutyRate) * oDutyTranDetail.Qty;
                    oItem.VATType = oDutyTranDetail.VATType;
                    DutyTran oVatPaidData = new DutyTran();
                    oItem.VATPaidQty = oVatPaidData.GetVATPaidQty(oItem.ProductID, sTranNo);

                    _TotalAmount_Exampled = _TotalAmount_Exampled + oItem.VAT;
                    oItem.InsertNewVATHO63();

                }

            }

            oDutyTranVAT63_15.Amount = _TotalAmount_63_15;
            oDutyTranVAT63_15.UpdateToatlVATAmountHO();

            oDutyTranVAT63_5.Amount = _TotalAmount_63_5;
            oDutyTranVAT63_5.UpdateToatlVATAmountHO();


            oDutyTranVATExampled.Amount = _TotalAmount_Exampled;
            oDutyTranVATExampled.UpdateToatlVATAmountHO();


            DutyAccount oDutyAccount = new DutyAccount();
            oDutyAccount.DutyAccountTypeID = (int)Dictionary.DutyAccountNew.MUSHAK_6_3;
            oDutyAccount.Balance = _TotalAmount_63_15 + _TotalAmount_63_5;
            oDutyAccount.UpdateBalance(true);



        }

    }
}