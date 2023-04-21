using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Techcycle.Web.Models
{
   // PortalUsers myDeserializedClass = JsonConvert.DeserializeObject<PortalUsers>(myJsonResponse); 
    public class PortalUser
    {
        public int account_id { get; set; }
        public int portal_group_id { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public bool disabled { get; set; }
        public int customer_id { get; set; }
        public int? contact_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int second_factor_attempts_count { get; set; }
        public object encrypted_otp_secret_key { get; set; }
        public object encrypted_otp_secret_key_iv { get; set; }
        public object encrypted_otp_secret_key_salt { get; set; }
        public object direct_otp { get; set; }
        public object direct_otp_sent_at { get; set; }
        public object totp_timestamp { get; set; }
        public object mobile { get; set; }
        public object confirmed_mobile { get; set; }
        public object otp_recovery_secret_key { get; set; }
        public int second_factor_recovery_attempts_count { get; set; }
        public bool require_mfa { get; set; }
    }

    public class PortalUsers
    {
        public List<PortalUser> portal_users { get; set; }
    }

}
