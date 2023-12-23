using Yarp.ReverseProxy.ServiceDiscovery;

namespace LoadBalancer.Extensions;

public static class Extensions
{
    public static IReverseProxyBuilder AddServiceDiscoveryDestinationResolver(this IReverseProxyBuilder builder)
    {
        builder.Services.AddServiceDiscoveryCore();
        builder.Services.AddSingleton<IDestinationResolver, ServiceDiscoveryDestinationResolver>();
        return builder;
    }
}
