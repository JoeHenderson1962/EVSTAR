using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;

namespace EVSTAR.RepairShopr.API
{
    internal class InvoicesManager
    {
        private string RSToken = DBHelper.GetStringValue(ConfigurationManager.AppSettings["RepairShopr"]);


    }
}
