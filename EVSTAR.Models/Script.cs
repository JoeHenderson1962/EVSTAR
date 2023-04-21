using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Script
    {
        private int id;
        private string scriptName;
        private int languageCode;
        private bool readAloud;
        private int clientID;
        private string cssClass;
        private string text;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string ScriptName
        {
            get { return scriptName; }
            set { scriptName = value; }
        }

        public int LanguageCode
        {
            get { return languageCode; }
            set { languageCode = value; }
        }

        public bool ReadAloud
        {
            get { return readAloud; }
            set { readAloud = value; }
        }

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Script()
        {
            id = 0;
            scriptName = "";
            languageCode = 0;
            readAloud = true;
            clientID = 0;
            cssClass = "";
            text = "";
        }

        public Script(SqlDataReader r)
        {
            if (r != null)
            {
                id = DBHelper.GetInt32Value(r["ID"]);
                scriptName = DBHelper.GetStringValue(r["ScriptName"]);
                languageCode = DBHelper.GetInt32Value(r["LanguageCode"]);
                readAloud = DBHelper.GetBooleanValue(r["ReadAloud"]);
                cssClass = DBHelper.GetStringValue(r["CssClass"]);
                text = DBHelper.GetStringValue(r["Text"]);
                clientID = DBHelper.GetInt32Value(r["ClientID"]);
            }
            else
            {
                id = 0;
                scriptName = "";
                languageCode = 0;
                readAloud = true;
                clientID = 0;
                cssClass = "";
                text = "";
            }
        }
    }
}
