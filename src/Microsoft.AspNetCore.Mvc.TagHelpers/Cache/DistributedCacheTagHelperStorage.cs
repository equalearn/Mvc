// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Internal;
using Microsoft.Extensions.Caching.Distributed;

namespace Microsoft.AspNetCore.Mvc.TagHelpers.Cache
{
    /// <summary>
    /// Implements <see cref="IDistributedCacheTagHelperStorage"/> by storing the content
    /// in using <see cref="IDistributedCache"/> as the store.
    /// </summary>
    public class DistributedCacheTagHelperStorage : IDistributedCacheTagHelperStorage
    {
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Creates a new <see cref="DistributedCacheTagHelperStorage"/>.
        /// </summary>
        /// <param name="cacheSelector">The <see cref="IDistributedCache"/> to use.</param>
        public DistributedCacheTagHelperStorage(DistributedCacheSelector cacheSelector)
        {
            _distributedCache = cacheSelector.GetCache(typeof(DistributedCacheTagHelper).FullName);
        }

        /// <inheritdoc />
        public Task<byte[]> GetAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _distributedCache.GetAsync(key);
        }

        /// <inheritdoc />
        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return _distributedCache.SetAsync(key, value, options);
        }
    }
}
