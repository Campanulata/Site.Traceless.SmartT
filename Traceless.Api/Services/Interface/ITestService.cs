﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traceless.Api.Services.Interface
{
    public interface ITestService
    {
        Dictionary<string,DateTime> GetTopSignByDay(DateTime dt,int count);
    }
}
