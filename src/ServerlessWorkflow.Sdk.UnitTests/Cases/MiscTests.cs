using Microsoft.Extensions.DependencyInjection;
using ServerlessWorkflow.Sdk.Services.IO;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases
{

    public class MiscTests
    {

        [Fact]
        public void DependencyInjection_Extensions_ShouldWork()
        {
            //arrange
            var services = new ServiceCollection();
            services.AddServerlessWorkflow();

            //act
            var provider = services.BuildServiceProvider();

            //assert
            provider.GetRequiredService<IWorkflowReader>();

        }

    }

}
