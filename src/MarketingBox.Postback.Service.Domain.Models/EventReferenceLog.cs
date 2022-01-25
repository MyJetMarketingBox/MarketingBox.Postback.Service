﻿using System;

namespace MarketingBox.Postback.Service.Domain.Models
{
    public class EventReferenceLog
    {
        public Status EventStatus { get; set; }
        public HttpQueryType HttpQueryType { get; set; }
        public string PostbackReference { get; set; }
        public string PostbackResult { get; set; }
        public DateTime Date { get; set; }
    }
}