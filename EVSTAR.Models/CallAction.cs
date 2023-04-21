using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class CallAction
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int actionID;

        public int ActionID
        {
            get { return actionID; }
            set { actionID = value; }
        }

        private int clientID;

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        private string actionName;

        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool visibleExisting;

        public bool VisibleExisting
        {
            get { return visibleExisting; }
            set { visibleExisting = value; }
        }

        private bool visibleNonExisting;

        public bool VisibleNonExisting
        {
            get { return visibleNonExisting; }
            set { visibleNonExisting = value; }
        }

        public CallAction()
        {
            id = 0;
            actionID = 0;
            clientID = 0;
            actionName = "";
            description = "";
            visibleExisting = false;
            visibleNonExisting = false;
        }

        public CallAction(int actid, int actionid, int clientid, string name, string desc, bool visibleEx, bool visibleNonEx)
        {
            id = actid;
            actionID = actionid;
            clientID = clientid;
            actionName = name;
            description = desc;
            visibleExisting = visibleEx;
            visibleNonExisting = visibleNonEx;
        }

        public CallAction(SqlDataReader row)
        {
            id = DBHelper.GetInt32Value(row["ID"]);
            actionID = DBHelper.GetInt32Value(row["ID"]);
            clientID = DBHelper.GetInt32Value(row["ClientID"]);
            actionName = DBHelper.GetStringValue(row["ActionName"]);
            description = DBHelper.GetStringValue(row["Description"]);
            visibleExisting = DBHelper.GetBooleanValue(row["VisibleExisting"]);
            visibleNonExisting = DBHelper.GetBooleanValue(row["VisibleNonExisting"]);
        }
    }
}
