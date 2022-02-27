using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class EmailVerificationEmailModel
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("code")]
        public int VerificationCode { get; set; }
    }
}
