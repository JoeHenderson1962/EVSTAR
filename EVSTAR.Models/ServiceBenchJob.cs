using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class ServiceBenchJob
    {
        int id;
        string crm;
        string serviceOrderID;
        string serviceJobID;
        string serviceJobJSON;
        string serviceOrderJSON;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string CRM
        {
            get { return crm; }
            set { crm = value; }
        }

        public string ServiceOrderID
        {
            get { return serviceOrderID; }
            set { serviceOrderID = value; }
        }

        public string ServiceJobID
        {
            get { return serviceJobID; }
            set { serviceJobID = value; }
        }

        public string ServiceJobJSON
        {
            get { return serviceJobJSON; }
            set { serviceJobJSON = value; }
        }

        public string ServiceOrderJSON
        {
            get { return serviceOrderJSON; }
            set { serviceOrderJSON = value; }
        }

        public ServiceBenchJob()
        {
            id = 0;
            crm = "";
            serviceOrderID = "";
            serviceJobID = "";
            serviceJobJSON = "";
            serviceOrderJSON = "";
        }

        public ServiceBenchJob(SqlDataReader row)
        {
            if (row != null)
            {
                id = DBHelper.GetInt32Value(row["ID"]);
                crm = DBHelper.GetStringValue(row["CRM"]);
                serviceOrderID = DBHelper.GetStringValue(row["ServiceOrderID"]);
                serviceJobID = DBHelper.GetStringValue(row["ServiceJobID"]);
                serviceJobJSON = DBHelper.GetStringValue(row["ServiceJobText"]);
                serviceOrderJSON = DBHelper.GetStringValue(row["ServiceOrderText"]);
            }
            else
            {
                id = 0;
                crm = "";
                serviceOrderID = "";
                serviceJobID = "";
                serviceJobJSON = "";
                serviceOrderJSON = "";
            }
        }
    }
}
