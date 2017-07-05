// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Mvc.TagHelpers.Internal
{
    public class DistributedCacheSelector
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOptionsMonitor<MemoryCacheOptions> _defaultCacheOptions;

        public DistributedCacheSelector(
            IDistributedCache distributedCache,
            IOptionsMonitor<MemoryCacheOptions> defaultCacheOptions)
        {
            _distributedCache = distributedCache;
            _defaultCacheOptions = defaultCacheOptions;
        }

        public DistributedCacheSelector(IOptionsMonitor<MemoryCacheOptions> defaultCacheOptions)
        {
            _defaultCacheOptions = defaultCacheOptions;
        }

        public IDistributedCache GetCache(string name) =>
            _distributedCache ?? new MemoryDistributedCache(new MemoryCache(_defaultCacheOptions.Get(name)));
    }
}
