using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class CallResult
    {
        int id;
        int clientID;
        string result;
        int sortOrder;
        string shortDescription;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public string ShortDescription
        {
            get { return shortDescription; }
            set { shortDescription = value; }
        }

        public CallResult()
        {
            id = 0;
            clientID = 0;
            result = "";
            sortOrder = 0;
            shortDescription = "";
        }

        public CallResult(SqlDataReader row)
        {
            if (row != null)
            {
                id = DBHelper.GetInt32Value(row["ID"]);
                clientID = DBHelper.GetInt32Value(row["ClientID"]);
                result = DBHelper.GetStringValue(row["CallResult"]);
                sortOrder = DBHelper.GetInt32Value(row["SortOrder"]);
                shortDescription = DBHelper.GetStringValue(row["ShortDescription"]);
            }
            else
            {
                id = 0;
                clientID = 0;
                result = "";
                sortOrder = 0;
                shortDescription = "";
            }
        }
    }
}
