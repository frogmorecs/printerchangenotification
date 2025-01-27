﻿using System;
using System.Collections.Generic;
using PrinterChangeNotification.enums;


// ReSharper disable InconsistentNaming

namespace PrinterChangeNotification
{
    public class NotifyInfo
    {
        public PRINTER_CHANGE Change { get; set; }

        public List<NotifyInfoData> Data { get; } = new List<NotifyInfoData>();

        public UInt32 Flags { get; set; }

        public const UInt32 PRINTER_NOTIFY_INFO_DISCARDED = 0x01;
    }
}