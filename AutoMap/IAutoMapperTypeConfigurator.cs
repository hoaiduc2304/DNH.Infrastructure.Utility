using AutoMapper;

namespace DNH.Infrastructure.Utility.AutoMap
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}
