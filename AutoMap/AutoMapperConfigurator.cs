using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DNH.Infrastructure.Utility.AutoMap
{
    public static class AutoMapperConfigurator
    {
        private static readonly object Lock = new object();
        private static MapperConfiguration _configuration;
        public static MapperConfiguration Configure(IEnumerable<Assembly> assemblies)
        {
            lock (Lock)
            {
                if (_configuration != null) return _configuration;

                var configInterfaceType = typeof(IAutoMapperTypeConfigurator);
                var thisType = from a in assemblies
                               from t in a.GetTypes()
                               select t;

                var configurators = thisType
                    //.Where(x => !string.IsNullOrWhiteSpace(x.Namespace))
                    //// ReSharper disable once AssignNullToNotNullAttribute
                    //.Where(x => x.Namespace.Contains(Namespace))
                    .Where(x => x.GetTypeInfo().GetInterface(configInterfaceType.Name) != null)
                    .Select(x => (IAutoMapperTypeConfigurator)Activator.CreateInstance(x))
                    .ToArray();

                void AggregatedConfigurator(IMapperConfigurationExpression config)
                {
                    foreach (var configurator in configurators)
                    {
                        configurator.Configure(config);
                    }
                }

                _configuration = new MapperConfiguration(AggregatedConfigurator);
                return _configuration;
            }
        }
    }
}
