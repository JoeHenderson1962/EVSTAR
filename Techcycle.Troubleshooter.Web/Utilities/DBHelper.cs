using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcycle.Troubleshooter.Web
{
    public static class DBHelper
    {

        ///////////////////////////////////////////////////////////////////////////
        public static Byte GetByteValue(Object data)
        {
            byte value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToByte(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static Char GetCharValue(Object data)
        {
            Char value = ' ';
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToChar(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static Int16 GetInt16Value(Object data)
        {
            Int16 value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToInt16(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static Guid GetGuidValue(Object data)
        {
            Guid value = Guid.Empty;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = new Guid(Convert.ToString(data));
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static int GetInt32Value(Object data)
        {
            int value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToInt32(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static long GetInt64Value(Object data)
        {
            long value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToInt64(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static decimal GetDecimalValue(Object data)
        {
            decimal value = 0M;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToDecimal(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static double GetDoubleValue(Object data)
        {
            double value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToDouble(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static Single GetSingleValue(Object data)
        {
            Single value = 0;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToSingle(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static string GetStringValue(Object data)
        {
            string value = "";
            if (data != null && data != DBNull.Value)
                try
                {
                    value = data.ToString().Trim();
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static bool GetBooleanValue(Object data)
        {
            bool value = false;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToBoolean(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static DateTime GetDateTimeValue(Object data)
        {
            DateTime value = DateTime.MinValue;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToDateTime(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static DateTime? GetNullableDateTimeValue(Object data)
        {
            DateTime? value = null;
            if (data != null && data != DBNull.Value)
                try
                {
                    value = Convert.ToDateTime(data);
                }
                catch
                {
                }
            return value;
        }

        ///////////////////////////////////////////////////////////////////////////
        public static String GetStringValueFromMoney(Object data)
        {
            String value = "";
            if (data != null && data != DBNull.Value)
                try
                {
                    value = decimal.Round(Convert.ToDecimal(data), 2).ToString();
                }
                catch
                {
                }
            return value;
        }

        public static string UnformatPhoneNumber(string phone)
        {
            string output = "";
            for (int x = 0; x < phone.Length; x++)
                if ("01234567890".Contains(Convert.ToString(phone[x])))
                    output += phone[x];
            return output;
        }

        public static string FormatPhoneNumber(string phone)
        {
            phone = UnformatPhoneNumber(phone);
            string m_strOut = "";
            if (phone.Length == 10)
                m_strOut = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6, 4);
            else if (phone.Length == 7)
                m_strOut = phone.Substring(0, 3) + "-" + phone.Substring(3, 4);
            else
                m_strOut = phone;
            return m_strOut;
        }
    }
}
