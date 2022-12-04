// <summary>
// Compamy: Transcom Electronics Limited
// Author: Shyam Sundar Biswas
// Date: July 01, 2012
// Time :  02:20 PM
// Description: Class for Duty transaction.
// Modify Person And Date:
// </summary>


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace IFSAPI
{

    public class DutyTranDetail
    {
        private int _nServiceDutyHeadID;
        public int ServiceDutyHeadID
        {
            get { return _nServiceDutyHeadID; }
            set { _nServiceDutyHeadID = value; }
        }
        private string _sServiceDutyHeadName;
        public string ServiceDutyHeadName
        {
            get { return _sServiceDutyHeadName; }
            set { _sServiceDutyHeadName = value; }
        }
        private string _sShortCode;
        public string ShortCode
        {
            get { return _sShortCode; }
            set { _sShortCode = value; }
        }

        private string _sRevReason;
        public string RevReason
        {
            get { return _sRevReason; }
            set { _sRevReason = value; }
        }


        private string _sMainDutytranDetail;
        public string MainDutytranDetail
        {
            get { return _sMainDutytranDetail; }
            set { _sMainDutytranDetail = value; }
        }
        private double _MainTotalPrice;
        public double MainTotalPrice
        {
            get { return _MainTotalPrice; }
            set { _MainTotalPrice = value; }
        }
        private int _MainTotalQty;
        public int MainTotalQty
        {
            get { return _MainTotalQty; }
            set { _MainTotalQty = value; }
        }
        private double _MainTotalVat;
        public double MainTotalVat
        {
            get { return _MainTotalVat; }
            set { _MainTotalVat = value; }
        }

        private double _RevTotalPrice;
        public double RevTotalPrice
        {
            get { return _RevTotalPrice; }
            set { _RevTotalPrice = value; }
        }
        private int _RevTotalQty;
        public int RevTotalQty
        {
            get { return _RevTotalQty; }
            set { _RevTotalQty = value; }
        }
        private double _RevTotalVat;

        public double RevTotalVat
        {
            get { return _RevTotalVat; }
            set { _RevTotalVat = value; }
        }


        private int _DutyTranID;
        private int _ProductID;
        private int _Qty;
        private double _DutyPrice;
        private double _DutyRate;
        private string _ProductName;
        private int _Counter;
        private int _WHID;
        private double _TotalPrice;
        private int _nVATPaidQty;
        public int VATPaidQty
        {
            get { return _nVATPaidQty; }
            set { _nVATPaidQty = value; }
        }

        private int _nSupplyType;
        public int SupplyType
        {
            get { return _nSupplyType; }
            set { _nSupplyType = value; }
        }
        private int _nVATType;
        public int VATType
        {
            get { return _nVATType; }
            set { _nVATType = value; }
        }



        private double _NSP;
        public double NSP
        {
            get { return _NSP; }
            set { _NSP = value; }
        }
        private double _RSP;
        public double RSP
        {
            get { return _RSP; }
            set { _RSP = value; }
        }
        private double _TradePrice;
        public double TradePrice
        {
            get { return _TradePrice; }
            set { _TradePrice = value; }
        }
        private double _DutyPriceForRetail;
        public double DutyPriceForRetail
        {
            get { return _DutyPriceForRetail; }
            set { _DutyPriceForRetail = value; }
        }

        private double _DutyPriceForDealer;
        public double DutyPriceForDealer
        {
            get { return _DutyPriceForDealer; }
            set { _DutyPriceForDealer = value; }
        }

        private double _TotalPriceTP;
        public double TotalPriceTP
        {
            get { return _TotalPriceTP; }
            set { _TotalPriceTP = value; }
        }



        public double TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        private double _TotalVAT;
        public double TotalVAT
        {
            get { return _TotalVAT; }
            set { _TotalVAT = value; }
        }
        private double _VAT;
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        private double _UnitPrice;
        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }


        /// <summary>
        /// Get set property for DutyTranID
        /// </summary>
        public int DutyTranID
        {
            get { return _DutyTranID; }
            set { _DutyTranID = value; }
        }

        /// <summary>
        /// Get set property for ProductID
        /// </summary>
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        /// <summary>
        /// Get set property for Qty
        /// </summary>
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        /// <summary>
        /// Get set property for DutyPrice
        /// </summary>
        public double DutyPrice
        {
            get { return _DutyPrice; }
            set { _DutyPrice = value; }
        }
        private double _ClosingStkVal;
        public double ClosingStkVal
        {
            get { return _ClosingStkVal; }
            set { _ClosingStkVal = value; }
        }
        private double _StockOutDutyRate;
        public double StockOutDutyRate
        {
            get { return _StockOutDutyRate; }
            set { _StockOutDutyRate = value; }
        }

        private double _StockInDutyRate;
        public double StockInDutyRate
        {
            get { return _StockInDutyRate; }
            set { _StockInDutyRate = value; }
        }
        private DateTime _dDutyTranDate;
        public DateTime DutyTranDate
        {
            get { return _dDutyTranDate; }
            set { _dDutyTranDate = value; }
        }

        private double _StockOutDutyPrice;
        public double StockOutDutyPrice
        {
            get { return _StockOutDutyPrice; }
            set { _StockOutDutyPrice = value; }
        }
        private double _StockInDutyPrice;
        public double StockInDutyPrice
        {
            get { return _StockInDutyPrice; }
            set { _StockInDutyPrice = value; }
        }
        private double _StockInVAT;
        public double StockInVAT
        {
            get { return _StockInVAT; }
            set { _StockInVAT = value; }
        }
        private double _StockOutVAT;
        public double StockOutVAT
        {
            get { return _StockOutVAT; }
            set { _StockOutVAT = value; }
        }


        /// <summary>
        /// Get set property for DutyRate
        /// </summary>
        public double DutyRate
        {
            get { return _DutyRate; }
            set { _DutyRate = value; }
        }

        private string _sProductModel;
        private string _sSupplierName;
        public string SupplierName
        {
            get { return _sSupplierName; }
            set { _sSupplierName = value; }
        }
        public string ProductModel
        {
            get { return _sProductModel; }
            set { _sProductModel = value; }
        }
        private string _sProductDesc;
        public string ProductDesc
        {
            get { return _sProductDesc; }
            set { _sProductDesc = value; }
        }
        private string _sProductCode;
        public string ProductCode
        {
            get { return _sProductCode; }
            set { _sProductCode = value; }
        }

        private string _GRDDate;
        public string GRDDate
        {
            get { return _GRDDate; }
            set { _GRDDate = value; }
        }
        private string _GRDConsumerName;
        public string GRDConsumerName
        {
            get { return _GRDConsumerName; }
            set { _GRDConsumerName = value; }
        }

        private string _GRDConsumerAddress;
        public string GRDConsumerAddress
        {
            get { return _GRDConsumerAddress; }
            set { _GRDConsumerAddress = value; }
        }
        private string _GRDConsumerNID;
        public string GRDConsumerNID
        {
            get { return _GRDConsumerNID; }
            set { _GRDConsumerNID = value; }
        }





        /// <summary>
        private string _InvoiceDate;
        public string InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        private string _InvoiceConsumerName;
        public string InvoiceConsumerName
        {
            get { return _InvoiceConsumerName; }
            set { _InvoiceConsumerName = value; }
        }

        private string _InvoiceConsumerAddress;
        public string InvoiceConsumerAddress
        {
            get { return _InvoiceConsumerAddress; }
            set { _InvoiceConsumerAddress = value; }
        }
        private string _InvoiceConsumerNID;
        public string InvoiceConsumerNID
        {
            get { return _InvoiceConsumerNID; }
            set { _InvoiceConsumerNID = value; }
        }

        private string _sGRDNo;
        public string GRDNo
        {
            get { return _sGRDNo; }
            set { _sGRDNo = value; }
        }
        private string _sInvoiceNo;
        public string InvoiceNo
        {
            get { return _sInvoiceNo; }
            set { _sInvoiceNo = value; }
        }
        private string _sSupplierDetail;
        public string SupplierDetail
        {
            get { return _sSupplierDetail; }
            set { _sSupplierDetail = value; }
        }

        private string _sTranNo;
        public string TranNo
        {
            get { return _sTranNo; }
            set { _sTranNo = value; }
        }
        private string _sType;
        public string Type
        {
            get { return _sType; }
            set { _sType = value; }
        }
        private DateTime _dtTranDate;
        public DateTime TranDate
        {
            get { return _dtTranDate; }
            set { _dtTranDate = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        private string _sConsumerName;
        public string ConsumerName
        {
            get { return _sConsumerName; }
            set { _sConsumerName = value; }
        }
        private string _sNationalID;
        public string NationalID
        {
            get { return _sNationalID; }
            set { _sNationalID = value; }
        }
        private string _sAddress;
        public string Address
        {
            get { return _sAddress; }
            set { _sAddress = value; }
        }





        public int Counter
        {
            get { return _Counter; }
            set { _Counter = value; }
        }
        public int WHID
        {
            get { return _WHID; }
            set { _WHID = value; }
        }


        private int _OpeningStock;
        public int OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }


        private int _StockInQty;
        public int StockInQty
        {
            get { return _StockInQty; }
            set { _StockInQty = value; }
        }
        private int _StockOutQty;
        public int StockOutQty
        {
            get { return _StockOutQty; }
            set { _StockOutQty = value; }
        }
        private int _ClosingStock;
        public int ClosingStock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }
        private int _CurrentStock;
        public int CurrentStock
        {
            get { return _CurrentStock; }
            set { _CurrentStock = value; }
        }
        private string _DutyTranNo;
        public string DutyTranNo
        {
            get { return _DutyTranNo; }
            set { _DutyTranNo = value.Trim(); }
        }
        private int _StockRequisitionID;
        public int StockRequisitionID
        {
            get { return _StockRequisitionID; }
            set { _StockRequisitionID = value; }
        }
        private int _TranID;
        public int TranID
        {
            get { return _TranID; }
            set { _TranID = value; }
        }
        public void InsertNewVATHO63()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();


            try
            {
                con.Open();
                cmd.CommandText = " INSERT INTO t_DutyTranDetail (DutyTranID,ProductID,Qty,DutyPrice,DutyRate,VATType,VAT) VALUES (?,?,?,?,?,?,?)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("DutyTranID", _DutyTranID);
                cmd.Parameters.AddWithValue("ProductID", _ProductID);
                cmd.Parameters.AddWithValue("Qty", _Qty);
                cmd.Parameters.AddWithValue("DutyPrice", _DutyPrice);
                cmd.Parameters.AddWithValue("DutyRate", _DutyRate);
                cmd.Parameters.AddWithValue("VATType", _nVATType);
                cmd.Parameters.AddWithValue("VAT", _VAT);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

            }

            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }
    public class DutyTran : CollectionBase
    {
        public DutyTranDetail this[int i]
        {
            get { return (DutyTranDetail)InnerList[i]; }
            set { InnerList[i] = value; }
        }
        public void Add(DutyTranDetail oDutyTranDetail)
        {
            InnerList.Add(oDutyTranDetail);
        }

        private int _DutyTranID;
        private DateTime _DutyTranDate;
        private string _DutyTranNo;
        private int _WHID;
        private int _ChallanTypeID;
        private string _DocumentNo;
        private int _RefID;
        private int _DutyTypeID;
        private int _DutyTranTypeID;
        private string _Remarks;
        private double _Amount;
        private int _DutyAccountID;
        private string _VehicleDetail;
        private string _sVehicleNumber;

        public string VehicleNumber
        {
            get { return _sVehicleNumber; }
            set { _sVehicleNumber = value; }
        }


        /// <summary>
        /// Get set property for DutyTranID
        /// </summary>
        public int DutyTranID
        {
            get { return _DutyTranID; }
            set { _DutyTranID = value; }
        }
        private DateTime _InvoiceDate;
        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }

        /// <summary>
        /// Get set property for DutyTranDate
        /// </summary>
        public DateTime DutyTranDate
        {
            get { return _DutyTranDate; }
            set { _DutyTranDate = value; }
        }

        /// <summary>
        /// Get set property for DutyTranNo
        /// </summary>
        public string DutyTranNo
        {
            get { return _DutyTranNo; }
            set { _DutyTranNo = value.Trim(); }
        }
        private string _NationalID;
        public string NationalID
        {
            get { return _NationalID; }
            set { _NationalID = value; }
        }
        private int _RefInvoiceID;
        public int RefInvoiceID
        {
            get { return _RefInvoiceID; }
            set { _RefInvoiceID = value; }
        }

        /// <summary>
        /// Get set property for WHID
        /// </summary>
        public int WHID
        {
            get { return _WHID; }
            set { _WHID = value; }
        }
        private int _nInvoiceStatus;
        public int InvoiceStatus
        {
            get { return _nInvoiceStatus; }
            set { _nInvoiceStatus = value; }
        }
        /// <summary>
        /// Get set property for ChallanTypeID
        /// </summary>
        public int ChallanTypeID
        {
            get { return _ChallanTypeID; }
            set { _ChallanTypeID = value; }
        }

        /// <summary>
        /// Get set property for DocumentNo
        /// </summary>
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value.Trim(); }
        }

        /// <summary>
        /// Get set property for RefID
        /// </summary>
        public int RefID
        {
            get { return _RefID; }
            set { _RefID = value; }
        }

        /// <summary>
        /// Get set property for DutyTypeID
        /// </summary>
        public int DutyTypeID
        {
            get { return _DutyTypeID; }
            set { _DutyTypeID = value; }
        }

        /// <summary>
        /// Get set property for DutyTranTypeID
        /// </summary>
        public int DutyTranTypeID
        {
            get { return _DutyTranTypeID; }
            set { _DutyTranTypeID = value; }
        }

        private string _sConsumerCode;
        public string ConsumerCode
        {
            get { return _sConsumerCode; }
            set { _sConsumerCode = value; }
        }
        private string _sShowroomCode;
        private string _sWarehouseAddress;
        private string _sConsumerAddress;
        public string ConsumerAddress
        {
            get { return _sConsumerAddress; }
            set { _sConsumerAddress = value; }
        }
        public string WarehouseAddress
        {
            get { return _sWarehouseAddress; }
            set { _sWarehouseAddress = value; }
        }
        private string _sTaxNo;
        public string TaxNo
        {
            get { return _sTaxNo; }
            set { _sTaxNo = value; }
        }
        public string ShowroomCode
        {
            get { return _sShowroomCode; }
            set { _sShowroomCode = value; }
        }
        private string _sConsumerName;
        public string ConsumerName
        {
            get { return _sConsumerName; }
            set { _sConsumerName = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private double _InvoiceAmount;
        public double InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public int DutyAccountID
        {
            get { return _DutyAccountID; }
            set { _DutyAccountID = value; }
        }

        public string _sDeliveryAddress;
        public string DeliveryAddress
        {
            get { return _sDeliveryAddress; }
            set { _sDeliveryAddress = value; }
        }

        public string VehicleDetail
        {
            get { return _VehicleDetail; }
            set { _VehicleDetail = value; }
        }

        public void InsertVAT63ForHO()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);


            int nMaxID = 0;
            int nNextTranNo = 0;
            string sSql = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                sSql = "SELECT MAX([DutyTranID]) FROM t_DutyTran";
                cmd.CommandText = sSql;
                using (new SqlCommand(cmd.CommandText, con))
                {
                    object maxID = cmd.ExecuteScalar();
                    if (maxID == DBNull.Value)
                    {
                        nMaxID = 1;
                    }
                    else
                    {
                        nMaxID = int.Parse(maxID.ToString()) + 1;

                    }
                    _DutyTranID = nMaxID;

                }

                /* This Variable is Manual Binding for Both TEL and BLL. Make sure the Company is correct while Publish the API */
                string sCompany = "BLL";



                if (_ChallanTypeID == (int)Dictionary.ChallanType.VAT_11_KA)
                {
                    cmd.Dispose();
                    using (cmd = new SqlCommand(cmd.CommandText, con))
                    {
                        if (sCompany == "BLL")
                        {
                            cmd.CommandText = "select NextVatChallanNo from t_NextDocumentNo where WarehouseID=68";
                        }
                        else
                        {
                            cmd.CommandText = "select NextVatChallanNo from t_NextDocumentNo where WarehouseID='" + _WHID + "'";
                        }

                        object NextVatChallanNo = cmd.ExecuteScalar();
                        if (NextVatChallanNo == DBNull.Value)
                        {
                            _DutyTranNo = "1";
                        }
                        else
                        {
                            _DutyTranNo = NextVatChallanNo.ToString();
                        }
                    }
                }
                else
                {
                    cmd.Dispose();
                    using (cmd = new SqlCommand(cmd.CommandText, con))
                    {
                        if (sCompany == "BLL")
                        {
                            cmd.CommandText = "select NextIVChallanNo from t_NextDocumentNo where WarehouseID=68";
                        }
                        else
                        {
                            cmd.CommandText = "select NextIVChallanNo from t_NextDocumentNo where WarehouseID='" + _WHID + "'";
                        }
                        object NextVatChallanNo = cmd.ExecuteScalar();
                        if (NextVatChallanNo == DBNull.Value)
                        {
                            _DutyTranNo = "1";
                        }
                        else
                        {
                            _DutyTranNo = NextVatChallanNo.ToString();
                        }
                    }
                }

                cmd.Dispose();
                using (cmd = new SqlCommand(cmd.CommandText, con))
                {
                    cmd.CommandText = "INSERT INTO t_DutyTran (DutyTranID,DutyTranDate,DutyTranNo,WHID,ChallanTypeID,DocumentNo,RefID,DutyTypeID,DutyTranTypeID,Remarks,Amount,DutyAccountID,VehicleNumber) Values (?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("DutyTranID", _DutyTranID);
                    cmd.Parameters.AddWithValue("DutyTranDate", _DutyTranDate);
                    cmd.Parameters.AddWithValue("DutyTranNo", _DutyTranNo);
                    cmd.Parameters.AddWithValue("WHID", _WHID);
                    cmd.Parameters.AddWithValue("ChallanTypeID", _ChallanTypeID);
                    cmd.Parameters.AddWithValue("DocumentNo", _DocumentNo);
                    cmd.Parameters.AddWithValue("RefID", _RefID);
                    cmd.Parameters.AddWithValue("DutyTypeID", _DutyTypeID);
                    cmd.Parameters.AddWithValue("DutyTranTypeID", _DutyTranTypeID);
                    cmd.Parameters.AddWithValue("Remarks", _Remarks);
                    cmd.Parameters.AddWithValue("Amount", _Amount);
                    cmd.Parameters.AddWithValue("DutyAccountID", _DutyAccountID);

                    if (_sVehicleNumber == null)
                    {
                        cmd.Parameters.AddWithValue("VehicleNumber", "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("VehicleNumber", _sVehicleNumber);
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    nNextTranNo = Convert.ToInt32(_DutyTranNo);
                    nNextTranNo = nNextTranNo + 1;
                }


                if (sCompany == "BLL")
                {
                    if (_ChallanTypeID == (int)Dictionary.ChallanType.VAT_11_KA)
                    {
                        using (cmd = new SqlCommand(cmd.CommandText, con))
                        {
                            cmd.CommandText = "update t_NextDocumentNo set NextVatChallanNo='" + nNextTranNo + "' where  WarehouseID=68";

                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }

                    }
                    else
                    {
                        using (cmd = new SqlCommand(cmd.CommandText, con))
                        {
                            cmd.CommandText = "update t_NextDocumentNo set NextIVChallanNo='" + nNextTranNo + "' where  WarehouseID=68";
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }

                    }
                }
                else
                {

                    if (_ChallanTypeID == (int)Dictionary.ChallanType.VAT_11_KA)
                    {
                        using (cmd = new SqlCommand())
                        {
                            cmd.CommandText = "update t_NextDocumentNo set NextVatChallanNo='" + nNextTranNo + "' where  WarehouseID=?";
                            cmd.Parameters.AddWithValue("WarehouseID", _WHID);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }

                    }
                    else
                    {
                        using (cmd = new SqlCommand(cmd.CommandText, con))
                        {
                            cmd.CommandText = "update t_NextDocumentNo set NextIVChallanNo='" + nNextTranNo + "' where  WarehouseID=?";
                            cmd.Parameters.AddWithValue("WarehouseID", _WHID);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void UpdateToatlVATAmountHO()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmd.CommandText, con))
                {
                    try
                    {
                        cmd.CommandText = "Update t_DutyTran set Amount = ? Where DutyTranID=? ";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("Amount", _Amount);
                        cmd.Parameters.AddWithValue("DutyTranID", _DutyTranID);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }

        }
        public int GetVATPaidQty(int nProductID, string sInvoiceNo)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                int nVATPaidQty = 0;

                string sSql = "Select count(a.ProductSerialNo) VATPaidQty  " +
                            "From (Select distinct ProductID,ProductSerialNo From t_ProductStockSerialVatPaid) a,t_SalesInvoiceProductSerial b  " +
                            "where a.ProductID = b.ProductID and a.ProductSerialNo = b.ProductSerialNo  " +
                            "and InvoiceID = (Select InvoiceID from t_SalesInvoice where InvoiceNo = '" + sInvoiceNo + "')  " +
                            "and a.ProductID = " + nProductID + "";
                using (SqlCommand cmd = new SqlCommand(sSql, con))
                {
                    try
                    {
                        cmd.CommandText = sSql;
                        cmd.CommandType = CommandType.Text;
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            nVATPaidQty = (int)reader["VATPaidQty"];
                        }

                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }

                return nVATPaidQty;
            }

        }


    }
    public class DutyTranList : CollectionBase
    {
        public DutyTran this[int i]
        {
            get { return (DutyTran)InnerList[i]; }
            set { InnerList[i] = value; }
        }

    }

    public class DutyAccount
    {
        private int _DutyAccountID;
        private int _DutyAccountTypeID;
        private string _DutyAccountNo;
        private double _Balance;
        private int _DutyTranID;
        private int _ProductID;
        private int _Qty;
        private double _DutyPrice;
        private double _DutyRate;
        private string _ProductName;
        private int _Counter;
        private int _WHID;
        private double _TotalPrice;
        private int _nSupplyType;
        public int SupplyType
        {
            get { return _nSupplyType; }
            set { _nSupplyType = value; }
        }
        private int _nVATType;
        public int VATType
        {
            get { return _nVATType; }
            set { _nVATType = value; }
        }
        private double _NSP;
        public double NSP
        {
            get { return _NSP; }
            set { _NSP = value; }
        }
        private double _VATCP;
        public double VATCP
        {
            get { return _VATCP; }
            set { _VATCP = value; }
        }
        private double _RSP;
        public double RSP
        {
            get { return _RSP; }
            set { _RSP = value; }
        }
        private double _TradePrice;
        public double TradePrice
        {
            get { return _TradePrice; }
            set { _TradePrice = value; }
        }
        private double _DutyPriceForRetail;
        public double DutyPriceForRetail
        {
            get { return _DutyPriceForRetail; }
            set { _DutyPriceForRetail = value; }
        }

        private double _DutyPriceForDealer;
        public double DutyPriceForDealer
        {
            get { return _DutyPriceForDealer; }
            set { _DutyPriceForDealer = value; }
        }
        private double _TPFromPrice;
        public double TPFromPrice
        {
            get { return _TPFromPrice; }
            set { _TPFromPrice = value; }
        }
        public double TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        private double _TotalVAT;
        public double TotalVAT
        {
            get { return _TotalVAT; }
            set { _TotalVAT = value; }
        }
        private double _VAT;
        public double VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        private double _UnitPrice;
        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }


        /// <summary>
        /// Get set property for DutyTranID
        /// </summary>
        public int DutyTranID
        {
            get { return _DutyTranID; }
            set { _DutyTranID = value; }
        }

        /// <summary>
        /// Get set property for ProductID
        /// </summary>
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        /// <summary>
        /// Get set property for Qty
        /// </summary>
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        /// <summary>
        /// Get set property for DutyPrice
        /// </summary>
        public double DutyPrice
        {
            get { return _DutyPrice; }
            set { _DutyPrice = value; }
        }

        /// <summary>
        /// Get set property for DutyRate
        /// </summary>
        public double DutyRate
        {
            get { return _DutyRate; }
            set { _DutyRate = value; }
        }
        /// <summary>
        /// Get set property for DutyTranID
        /// </summary>
        public int DutyAccountID
        {
            get { return _DutyAccountID; }
            set { _DutyAccountID = value; }
        }
        public int DutyAccountTypeID
        {
            get { return _DutyAccountTypeID; }
            set { _DutyAccountTypeID = value; }
        }
        public string DutyAccountNo
        {
            get { return _DutyAccountNo; }
            set { _DutyAccountNo = value; }
        }
        /// <summary>
        /// Get set property for DutyPrice
        /// </summary>
        public double Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        public void UpdateBalance(bool IsTrue)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sSql = "";
                using (SqlCommand cmd = new SqlCommand(cmd.CommandText, con))
                {
                    try
                    {
                        if (IsTrue)
                            sSql = "UPDATE t_DutyAccount SET Balance = Balance+? WHERE DutyAccountTypeID=? ";
                        else sSql = "UPDATE t_DutyAccount SET Balance = Balance-? WHERE DutyAccountTypeID=? ";
                        cmd.CommandText = sSql;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("Balance", _Balance);
                        cmd.Parameters.AddWithValue("DutyAccountTypeID", _DutyAccountTypeID);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }


    }
    public class DutyAccounts : CollectionBase
    {
        public DutyAccount this[int i]
        {
            get { return (DutyAccount)InnerList[i]; }
            set { InnerList[i] = value; }
        }

        public void GetDutyDetailNew(string sType, int nTranID)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                string sSql = "";
                if (sType == "Invoice")
                {
                    sSql = "Select b.ProductID,SupplyType,VATType,NSP,RSP,b.TradePrice, " +
                            "VAT as DutyRate,DutyPriceForRetail,DutyPriceForDealer, " +
                            "UnitPrice,Quantity " +
                            "From " +
                            "( " +
                            "Select a.ProductID, SupplyType, VATType, " +
                            "isnull(NSP, 0) NSP, isnull(RSP, 0) RSP, isnull(TradePrice, 0) TradePrice, isnull(VAT, 0) VAT, " +
                            "case when IsTPVatProduct = 1 then TradePrice else round(RSP / (1 + VAT), 2) end as DutyPriceForRetail, " +
                            "case when IsTPVatProduct = 1 then TradePrice else round(NSP / (1 + VAT), 2)  end as DutyPriceForDealer " +
                            "From t_Product a, ( " +
                            "Select 0 as IsTPVatProduct, ProductID, NSP, RSP, TradePrice, VAT " +
                            "From v_ProductDetails where ProductID not in (Select ProductID from t_TPVatProduct) " +
                            "Union All " +
                            "Select 1 as IsTPVatProduct,a.ProductID,NSP,RSP,TradePrice,VAT " +
                            "From t_FinishedGoodsPrice a, t_TPVatProduct b " +
                            "where a.ProductID = b.ProductID and IsCurrent = 1 " +
                            "and PriceID in ( " +
                            "Select Max(PriceID) PriceID " +
                            "From t_FinishedGoodsPrice where IsCurrent = 1 " +
                            "group by ProductID) " +
                            ") b " +
                            "where a.ProductID = b.ProductID and VATApplicable=1 " +
                            ") a,t_SalesInvoiceDetail b " +
                            "where a.ProductID = b.ProductID  " +
                            "and InvoiceID = " + nTranID + "";
                }
                else
                {
                    sSql = "Select b.ProductID,SupplyType,VATType,NSP,RSP,a.TradePrice, " +
                             "VAT as DutyRate,DutyPriceForRetail,DutyPriceForDealer, " +
                             "b.StockPrice as UnitPrice,b.Qty as Quantity  " +
                             "From " +
                             "( " +
                             "Select a.ProductID, SupplyType, VATType, " +
                             "isnull(NSP, 0) NSP, isnull(RSP, 0) RSP, isnull(TradePrice, 0) TradePrice, isnull(VAT, 0) VAT, " +
                             "case when IsTPVatProduct = 1 then TradePrice else round(RSP / (1 + VAT), 2) end as DutyPriceForRetail, " +
                             "case when IsTPVatProduct = 1 then TradePrice else round(NSP / (1 + VAT), 2)  end as DutyPriceForDealer " +
                             "From t_Product a, ( " +
                             "Select 0 as IsTPVatProduct, ProductID, NSP, RSP, TradePrice, VAT " +
                             "From v_ProductDetails where ProductID not in (Select ProductID from t_TPVatProduct) " +
                             "Union All " +
                             "Select 1 as IsTPVatProduct,a.ProductID,NSP,RSP,TradePrice,VAT " +
                             "From t_FinishedGoodsPrice a, t_TPVatProduct b " +
                             "where a.ProductID = b.ProductID and IsCurrent = 1 " +
                             "and PriceID in ( " +
                             "Select Max(PriceID) PriceID " +
                             "From t_FinishedGoodsPrice where IsCurrent = 1 " +
                             "group by ProductID) " +
                             ") b " +
                             "where a.ProductID = b.ProductID and VATApplicable=1 " +
                             ") a,t_ProductStockTranItem b " +
                             "where a.ProductID = b.ProductID  " +
                             "and TranID = " + nTranID + "";

                }


                using (SqlCommand cmd = new SqlCommand(cmd.CommandText, con))
                {


                    try
                    {

                        cmd.CommandText = sSql;
                        cmd.CommandType = CommandType.Text;
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DutyTranDetail oDutyTranDetail = new DutyTranDetail();

                            oDutyTranDetail.ProductID = (int)reader["ProductID"];
                            oDutyTranDetail.SupplyType = (int)reader["SupplyType"];
                            oDutyTranDetail.VATType = (int)reader["VATType"];
                            oDutyTranDetail.NSP = (double)reader["NSP"];
                            oDutyTranDetail.RSP = (double)reader["RSP"];
                            oDutyTranDetail.TradePrice = (double)reader["TradePrice"];
                            oDutyTranDetail.DutyRate = (double)reader["DutyRate"];
                            oDutyTranDetail.DutyPriceForRetail = (double)reader["DutyPriceForRetail"];
                            oDutyTranDetail.DutyPriceForDealer = (double)reader["DutyPriceForDealer"];
                            oDutyTranDetail.UnitPrice = (double)reader["UnitPrice"];
                            oDutyTranDetail.Qty = Convert.ToInt32(reader["Quantity"].ToString());

                            InnerList.Add(oDutyTranDetail);
                        }
                        reader.Close();
                        InnerList.TrimToSize();

                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }



    }

}
