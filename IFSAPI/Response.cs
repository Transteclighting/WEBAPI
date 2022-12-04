using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IFSAPI
{

    public class Response
    {

        public string OrderNo { get; set; }
        public string LineNo { get; set; }
        public string ErrorMessage { get; set; }
    }
}