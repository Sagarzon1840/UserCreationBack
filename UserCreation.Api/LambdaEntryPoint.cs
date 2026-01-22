using Amazon.Lambda.AspNetCoreServer;

namespace UserCreation.Api;
public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }
}