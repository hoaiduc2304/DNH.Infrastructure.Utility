using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Runtime.CompilerServices;
namespace DNH.Infrastructure.Utility.AutoMap
{
    public static class RegisterAutoWrapper
    {
        public static void ConfigureAutoMapperByAssemply(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var mapperConfig = AutoMapperConfigurator.Configure(assemblies);
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(x => mapper);
            services.AddScoped<IAutoMapper, AutoMapperAdapter>();
        }
    }
}
