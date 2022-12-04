using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IFSAPI
{

    public class ReplacementOrder
    {
        public string RefClaimNo { get; set; }
        public string CustomerNo { get; set; }
        public DateTime EntryDate { get; set; }
        public int ClaimYear { get; set; }
        public int ClaimMonth { get; set; }
        public string Site { get; set; }
        public string PriceListNo { get; set; }
        public string Remarks { get; set; }
        public int LineNo { get; set; }
        public string PartNo { get; set; }
        public int ClaimQty { get; set; }
        public string ReturnLocation { get; set; }
        public int CountedQty { get; set; }

        public string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public DataTable Get_ReplacementOrder(int RefforderNo)
        {

            string sql = @"Select ReplaceClaimID as RefClaimNo,CustomerCode as CustomerNo,
            --cast(a.EntryDate as Date) as EntryDate,
            '2022-11-07' as EntryDate,
            ClaimYear, 11 as ClaimMonth,
            --Convert(int,(Case  
            -- ClaimedMonth='January' then 1 when ClaimedMonth='February' then 2 
            --when ClaimedMonth='March' then 3when ClaimedMonth='April' then 4 
            --when ClaimedMonth='May'then 5 when ClaimedMonth='June' then 6 
            --when ClaimedMonth='July' then 7 when ClaimedMonth='August' then 8 when ClaimedMonth='September' then 9 
            --when ClaimedMonth='October' then 10  
            --when ClaimedMonth='November' then 11 when ClaimedMonth='December' then 12 else 0 End))as ClaimMonth,
            'BLL1' as Site,'BLL-NSP' as PriceListNo,'Test' as Remarks
            from t_ReplaceClaim a, v_CustomerDetails b where a.CustomerID=b.CustomerID and ClaimedMonth !='' and ReplaceClaimID>" + RefforderNo + "";
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
        public DataTable Get_ReplacementOrder_Details(int RefforderNo)
        {

            string sql = @"Select 
            ROW_NUMBER() OVER(ORDER BY a. ProductID DESC)  as 'LineNo',
            ProductCode as PartNo,
            --'10011' as PartNo,
            ClaimedQty as ClaimQty,
            'Defective' as ReturnLocation,
            ClaimedQty as CountedQty
            from t_ReplaceClaimItem a, v_ProductDetails b
            where a.ProductID=b.ProductID and ReplaceClaimID=" + RefforderNo + "";
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


    }
}