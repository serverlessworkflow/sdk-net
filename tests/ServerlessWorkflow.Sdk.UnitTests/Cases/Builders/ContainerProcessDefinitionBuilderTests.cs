namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ContainerProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Container_With_All_Properties()
    {
        var image = "alpine:latest";
        var name = "my-container";
        var command = "echo hello";
        ushort hostPort = 8080;
        ushort containerPort = 80;
        var volumeHost = "/host";
        var volumeContainer = "/container";
        var envKey = "KEY";
        var envValue = "VALUE";
        var container = new ContainerProcessDefinitionBuilder()
            .WithImage(image)
            .WithName(name)
            .WithCommand(command)
            .WithPort(hostPort, containerPort)
            .WithVolume(volumeHost, volumeContainer)
            .WithEnvironment(envKey, envValue)
            .Build();
        container.Image.Should().Be(image);
        container.Name.Should().Be(name);
        container.Command.Should().Be(command);
        container.Ports![hostPort].Should().Be(containerPort);
        container.Volumes![volumeHost].Should().Be(volumeContainer);
        container.Environment![envKey].Should().Be(envValue);
    }

    [Fact]
    public void Build_Should_Accept_Bulk_Ports_And_Volumes()
    {
        var image = "nginx";
        ushort httpPort = 80;
        ushort httpsPort = 443;
        var dataVolume = "/data";
        var envName = "ENV";
        var envValue = "prod";
        var container = new ContainerProcessDefinitionBuilder()
            .WithImage(image)
            .WithPorts(new Dictionary<ushort, ushort> { [httpPort] = httpPort, [httpsPort] = httpsPort })
            .WithVolumes(new Dictionary<string, string> { [dataVolume] = dataVolume })
            .WithEnvironment(new Dictionary<string, string> { [envName] = envValue })
            .Build();
        container.Ports.Should().HaveCount(2);
        container.Ports![httpPort].Should().Be(httpPort);
        container.Volumes![dataVolume].Should().Be(dataVolume);
        container.Environment![envName].Should().Be(envValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Image_Missing()
    {
        var act = () => new ContainerProcessDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
