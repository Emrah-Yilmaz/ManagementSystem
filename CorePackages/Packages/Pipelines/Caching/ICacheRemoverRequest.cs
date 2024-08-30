﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages.Pipelines.Caching;

public interface ICacheRemoverRequest
{
    string? CacheKey { get; }
    bool BypassCache { get; }
    string? CacheGroupKey { get; }
}
