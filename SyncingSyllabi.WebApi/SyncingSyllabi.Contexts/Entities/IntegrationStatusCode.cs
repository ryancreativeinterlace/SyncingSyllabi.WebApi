﻿using System;
using System.Collections.Generic;
using System.Text;
using SyncingSyllabi.Contexts.Entities.Base;

namespace SyncingSyllabi.Contexts.Entities
{
    public class IntegrationStatusCode : BaseEntity
    {
        public int IntegrationId { get; set; }
        public string StatusCode { get; set; }
    }
}
