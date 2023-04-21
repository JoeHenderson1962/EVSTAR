using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace EVSTAR.Models
{
    public class Call
    {
        public Call()
        {
            id = 0;
            csrName = "";
            callerName = "";
            searchMDN = "";
            clientID = 0;
            customerID = 0;
            claimID = 0;
            actionID = 0;
            resultID = 0;
            languageID = 1;
            startTime = DateTime.Now;
            endTime = DateTime.MaxValue;
            clientCode = "";
            actionName = "";
            resultName = "";
            escalationReasonID = 0;
            escalationReason = "";
            escalationResolution = "";
            escalationResolvedDate = DateTime.MaxValue;
            
            callClaim = new Claim();
            currClient = new Client();
            CallCustomer = new Customer();
            CallUser = new User();
            CallAction = new CallAction();
            CallResult = new CallResult();
            escalate = false;
            numOpenReq = 0;
            numApprovedReq = 0;
            userID = 0;
        }

        public Call(int callid, string csrname, string caller, string searchmdn, int clientid,
            int custid, int claimid, int actionid, int resultid, int languageid, DateTime start,
            DateTime end, int escID, string escReason, string escRes, DateTime escResDate)
        {
            id = callid;
            csrName = csrname;
            callerName = caller;
            searchMDN = searchmdn;
            clientID = clientid;
            customerID = custid;
            claimID = claimid;
            actionID = actionid;
            resultID = resultid;
            languageID = languageid;
            startTime = start;
            endTime = end;
            callClaim = new Claim();
            escalationReasonID = escID;
            escalationReason = escReason;
            escalationResolution = escRes;
            escalationResolvedDate = escResDate;
            escalate = false;
            numOpenReq = 0;
            numApprovedReq = 0;
        }

        public Call(SqlDataReader row)
        {
            if (row != null)
            {
                id = DBHelper.GetInt32Value(row["ID"]);
                csrName = DBHelper.GetStringValue(row["CSRName"]).Trim();
                callerName = DBHelper.GetStringValue(row["CallerName"]).Trim();
                searchMDN = DBHelper.GetStringValue(row["SearchMDN"]).Trim();
                clientID = DBHelper.GetInt32Value(row["ClientID"]);
                customerID = DBHelper.GetInt32Value(row["CustomerID"]);
                claimID = DBHelper.GetInt32Value(row["ClaimID"]);
                actionID = DBHelper.GetInt32Value(row["ActionID"]);
                resultID = DBHelper.GetInt32Value(row["ResultID"]);
                languageID = DBHelper.GetInt32Value(row["LanguageID"]);
                startTime = DBHelper.GetDateTimeValue(row["StartTime"]);
                endTime = DBHelper.GetDateTimeValue(row["EndTime"]);
                escalationReasonID = DBHelper.GetInt32Value(row["EscalationReasonID"]);
                escalationResolution = DBHelper.GetStringValue(row["EscalationResolution"]).Trim();
                escalationResolvedDate = DBHelper.GetDateTimeValue(row["EscalationResolvedDate"]);
                UserID = DBHelper.GetInt32Value(row["UserID"]);
            }
            else
            {
                id = 0;
                csrName = "";
                callerName = "";
                searchMDN = "";
                clientID = 0;
                customerID = 0;
                claimID = 0;
                actionID = 0;
                resultID = 0;
                languageID = 1;
                startTime = DateTime.Now;
                endTime = DateTime.MaxValue;
                clientCode = "";
                actionName = "";
                resultName = "";
                escalationReasonID = 0;
                escalationReason = "";
                escalationResolution = "";
                escalationResolvedDate = DateTime.MaxValue;
            }

            callClaim = new Claim();
            CallAction = new CallAction();
            CallUser = new User();
            currClient = new Client();
            CallResult = new CallResult();
            CurrCoverage = null;

            numOpenReq = 0;
            numApprovedReq = 0;
            escalate = false;
        }

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string csrName;

        public string CsrName
        {
            get { return csrName; }
            set { csrName = value; }
        }

        private string callerName;

        public string CallerName
        {
            get { return callerName; }
            set { callerName = value; }
        }

        private string searchMDN;

        public string SearchMDN
        {
            get { return searchMDN; }
            set { searchMDN = value; }
        }

        private int clientID;

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        private string clientCode;

        public string ClientCode
        {
            get { return clientCode; }
            set { clientCode = value; }
        }

        private int customerID;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private int claimID;

        public int ClaimID
        {
            get { return claimID; }
            set { claimID = value; }
        }

        private int actionID;

        public int ActionID
        {
            get { return actionID; }
            set { actionID = value; }
        }

        private string actionName;

        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }

        private int resultID;

        public int ResultID
        {
            get { return resultID; }
            set { resultID = value; }
        }

        private string resultName;

        public string ResultName
        {
            get { return resultName; }
            set { resultName = value; }
        }

        private DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private DateTime endTime;

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private int languageID;

        public int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }

        private Claim callClaim;

        public Claim CallClaim
        {
            get { return callClaim; }
            set { callClaim = value; }
        }

        private Client currClient;

        public Client CurrClient
        {
            get { return currClient; }
            set { currClient = value; }
        }

        private Coverage currCoverage;

        public Coverage CurrCoverage
        {
            get { return currCoverage; }
            set { currCoverage = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private int escalationReasonID;

        public int EscalationReasonID
        {
            get { return escalationReasonID; }
            set { escalationReasonID = value; }
        }

        private string escalationReason;

        public string EscalationReason
        {
            get { return escalationReason; }
            set { escalationReason = value; }
        }

        private string escalationResolution;

        public string EscalationResolution
        {
            get { return escalationResolution; }
            set { escalationResolution = value; }
        }

        private DateTime escalationResolvedDate;

        public DateTime EscalationResolvedDate
        {
            get { return escalationResolvedDate; }
            set { escalationResolvedDate = value; }
        }

        private bool escalate;
        public bool Escalate
        {
            get { return escalate; }
            set { escalate = value; }
        }

        private int numOpenReq;
        public int NumOpenReq
        {
            get { return numOpenReq; }
            set { numOpenReq = value; }
        }

        private int numApprovedReq;
        public int NumApprovedReq
        {
            get { return numApprovedReq; }
            set { numApprovedReq = value; }
        }

        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public Customer CallCustomer { get; set; }
        public User CallUser { get; set; }
        public CallAction CallAction { get; set; }
        public CallResult CallResult { get; set; }

    }
}