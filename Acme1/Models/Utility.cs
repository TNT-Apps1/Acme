using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

namespace Acme1.Models
{
    public class Utility
    {

        public static int GetIdNumber(SqlConnection dbcon, string ctlkey)
        {
            string strquery = "SELECT idnumber FROM controltable" +
            " WHERE ctlkey = @ctlkey";
            SqlCommand myCmd = new SqlCommand(strquery, dbcon);
            myCmd.Parameters.AddWithValue("@ctlkey", SqlDbType.VarChar).Value = ctlkey;
            int count = Convert.ToInt32(myCmd.ExecuteScalar().ToString()) + 1;
            strquery = "UPDATE controltable SET idnumber = " + count +
            " where ctlkey = @ctlkey";
            myCmd = new SqlCommand(strquery, dbcon);
            myCmd.Parameters.AddWithValue("@ctlkey", SqlDbType.VarChar).Value = ctlkey;
            myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            return count;
        }

        //public static int GetIdNumberByName(SqlConnection dbcon, String userName)
        //{
        //    string strquery = "SELECT idnumber FROM controltable" +
        //    " WHERE ctlkey = @ctlkey";
        //    SqlCommand myCmd = new SqlCommand(strquery, dbcon);
        //    myCmd.Parameters.AddWithValue("@ctlkey", SqlDbType.VarChar).Value = ctlkey;
        //    int count = Convert.ToInt32(myCmd.ExecuteScalar().ToString()) + 1;
        //    strquery = "UPDATE controltable SET idnumber = " + count +
        //    " where ctlkey = @ctlkey";
        //    myCmd = new SqlCommand(strquery, dbcon);
        //    myCmd.Parameters.AddWithValue("@ctlkey", SqlDbType.VarChar).Value = ctlkey;
        //    myCmd.ExecuteNonQuery();
        //    myCmd.Dispose();
        //    return count;
        //}

    }
}