using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Techcycle.Web.Models
{
    // Assets myDeserializedClass = JsonConvert.DeserializeObject<Assets>(myJsonResponse); 
 
    public class SnmpConfig
    {
        public int port { get; set; }
        public bool enabled { get; set; }
        public int version { get; set; }
        public string community { get; set; }
    }

    public class DeviceInfo
    {
        public SnmpConfig snmp_config { get; set; }
    }

    public class Triggers
    {
        public string bsod_triggered { get; set; }
        public string time_triggered { get; set; }
        public string no_av_triggered { get; set; }
        public string defrag_triggered { get; set; }
        public string firewall_triggered { get; set; }
        public string app_crash_triggered { get; set; }
        public string low_hd_space_triggered { get; set; }
        public string smart_failure_triggered { get; set; }
        public string device_manager_triggered { get; set; }
        public string agent_offline_triggered { get; set; }
    }

    public class WindowsUpdates
    {
    }

    public class Emsisoft
    {
    }

    public class General
    {
    }

    public class RmmStore
    {
        public int id { get; set; }
        public int asset_id { get; set; }
        public int account_id { get; set; }
        public Triggers triggers { get; set; }
        public WindowsUpdates windows_updates { get; set; }
        public Emsisoft emsisoft { get; set; }
        public General general { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object override_alert_agent_offline_mins { get; set; }
        public object override_alert_agent_rearm_after_mins { get; set; }
        public object override_low_hd_threshold { get; set; }
        public object override_autoresolve_offline_alert { get; set; }
        public object override_low_hd_thresholds { get; set; }
    }

    public class Asset
    {
        public int id { get; set; }
        public string name { get; set; }
        public int customer_id { get; set; }
        public object contact_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Properties properties { get; set; }
        public string asset_type { get; set; }
        public string asset_serial { get; set; }
        public object external_rmm_link { get; set; }
        public Customer customer { get; set; }
        public List<object> rmm_links { get; set; }
        public bool has_live_chat { get; set; }
        public object snmp_enabled { get; set; }
        public DeviceInfo device_info { get; set; }
        public RmmStore rmm_store { get; set; }
        public object address { get; set; }
    }

    public class Assets
    {
        public List<Asset> assets { get; set; }
        public Meta meta { get; set; }
    }
}
