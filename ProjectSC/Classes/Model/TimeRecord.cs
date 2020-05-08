﻿using System;

namespace ProjectSC.Classes
{
    public class TimeRecord
    {
        public string Title { get; set; }

        public bool CanNotify { get; set; }
        public int NotifyType { get; set; }

        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
