// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.AspNetCore.Mvc.TagHelpers.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring Razor cache tag helpers.
    /// </summary>
    public static class TagHelperServicesExtensions
    {
        private const int DefaultEntryCountLimit = 1000;
        private static readonly string MemoryDistributedCacheMemoryOptionsName = typeof(DistributedCacheTagHelper).FullName;
        private static readonly string CacheTagHelperMemoryOptionsName = typeof(CacheTagHelper).FullName;

        /// <summary>
        ///  Adds MVC cache tag helper services to the application.
        ///  By default the <see cref="DistributedCacheTagHelper"/> uses a private <see cref="MemoryDistributedCache"/>
        ///  instance as its <see cref="IDistributedCache"/>. This <see cref="IDistributedCache"/> instance can be
        ///  configured through <see cref="ConfigureDistributedCacheTagHelperDefaultLimits(IMvcCoreBuilder, Action{MemoryCacheOptions})"/>.
        ///  The <see cref="CacheTagHelper"/> uses a private <see cref="MemoryCache"/> instance as its <see cref="IMemoryCache"/>.
        ///  This <see cref="IMemoryCache"/> instance can be configured through <see cref="ConfigureCacheTagHelperLimits(IMvcCoreBuilder, Action{MemoryCacheOptions})"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder"/>.</param>
        /// <returns>The <see cref="IMvcCoreBuilder"/>.</returns>
        public static IMvcCoreBuilder AddCacheTagHelper(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddSingleton<IDistributedCacheTagHelperStorage, DistributedCacheTagHelperStorage>();
            builder.Services.TryAddSingleton<IDistributedCacheTagHelperFormatter, DistributedCacheTagHelperFormatter>();
            builder.Services.TryAddSingleton<IDistributedCacheTagHelperService, DistributedCacheTagHelperService>();

            // Required default services for cache tag helpers
            builder.Services.TryAddSingleton<DistributedCacheSelector>();
            builder.Services.TryAddSingleton<CacheTagHelperMemoryCacheFactory>();
            builder.ConfigureCacheTagHelperLimits(options => { /*TODO: Set default limits */ });
            builder.ConfigureDistributedCacheTagHelperDefaultLimits(options => { /*TODO: Set default limits */ });

            return builder;
        }

        /// <summary>
        ///  Configures the <see cref="MemoryCacheOptions"/> of the <see cref="MemoryDistributedCache"/> used as the
        ///  default <see cref="IDistributedCache"/> implementation on <see cref="DistributedCacheTagHelper"/>.
        ///  These options will be ignored if an <see cref="IDistributedCache"/> instance is explicitly registered in
        ///  the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder"/>.</param>
        /// <param name="configure">The <see cref="Action{MemoryCacheOptions}"/>to configure the default cache options.</param>
        /// <returns>The <see cref="IMvcCoreBuilder"/>.</returns>
        public static IMvcCoreBuilder ConfigureDistributedCacheTagHelperDefaultLimits(this IMvcCoreBuilder builder, Action<MemoryCacheOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.Services.Configure(MemoryDistributedCacheMemoryOptionsName, configure);

            return builder;
        }

        /// <summary>
        ///  Configures the <see cref="MemoryCacheOptions"/> of the <see cref="MemoryDistributedCache"/> used as the
        ///  default <see cref="IDistributedCache"/> implementation on <see cref="DistributedCacheTagHelper"/>.
        ///  These options will be ignored if an <see cref="IDistributedCache"/> instance is explicitly registered in
        ///  the <see cref="IServiceCollection"/>.
        ///  </summary>
        /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
        /// <param name="configure">The <see cref="Action{MemoryCacheOptions}"/>to configure the default cache options.</param>
        /// <returns>The <see cref="IMvcBuilder"/>.</returns>
        public static IMvcBuilder ConfigureDistributedCacheTagHelperDefaultLimits(this IMvcBuilder builder, Action<MemoryCacheOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.Services.Configure(MemoryDistributedCacheMemoryOptionsName, configure);

            return builder;
        }

        /// <summary>
        ///  Configures the limits on the <see cref="MemoryCache"/> used by the cache tag helper.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
        /// <param name="configure">The <see cref="Action{MemoryCacheOptions}"/>to configure the cache options.</param>
        /// <returns>The <see cref="IMvcBuilder"/>.</returns>
        public static IMvcBuilder ConfigureCacheTagHelperLimits(this IMvcBuilder builder, Action<MemoryCacheOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.Services.Configure(CacheTagHelperMemoryOptionsName, configure);

            return builder;
        }

        /// <summary>
        ///  Configures the limits on the <see cref="MemoryCache"/> used by the cache tag helper.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder"/>.</param>
        /// <param name="configure">The <see cref="Action{MemoryCacheOptions}"/>to configure the cache options.</param>
        /// <returns>The <see cref="IMvcCoreBuilder"/>.</returns>
        public static IMvcCoreBuilder ConfigureCacheTagHelperLimits(this IMvcCoreBuilder builder, Action<MemoryCacheOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.Services.Configure(CacheTagHelperMemoryOptionsName, configure);

            return builder;
        }
    }
}