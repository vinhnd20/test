﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuangCaoDieuHoaAPI.Common.Enum
{
    public enum RoomTypeEnum : byte
    {
        [Description("SingleRoom")] SingleRoom = 0,
        [Description("DoubleRoom")] DoubleRoom = 1,
        [Description("VIPRoom")] VIPRoom = 2,
    }
}
