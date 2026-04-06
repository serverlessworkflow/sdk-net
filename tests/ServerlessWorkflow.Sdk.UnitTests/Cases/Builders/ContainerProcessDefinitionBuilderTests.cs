namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ContainerProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Container_With_All_Properties()
    {
        //arrange
        var image = "alpine:latest";
        var name = "my-container";
        var command = "echo hello";
        ushort hostPort = 8080;
        ushort containerPort = 80;
        var volumeHost = "/host";
        var volumeContainer = "/container";
        var envKey = "KEY";
        var envValue = "VALUE";

        //act
        var container = new ContainerProcessDefinitionBuilder()
            .WithImage(image)
            .WithName(name)
            .WithCommand(command)
            .WithPort(hostPort, containerPort)
            .WithVolume(volumeHost, volumeContainer)
            .WithEnvironment(envKey, envValue)
            .Build();

        //assert
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
        //arrange
        var image = "nginx";
        ushort httpPort = 80;
        ushort httpsPort = 443;
        var dataVolume = "/data";
        var envName = "ENV";
        var envValue = "prod";
        var expectedPortCount = 2;

        //act
        var container = new ContainerProcessDefinitionBuilder()
            .WithImage(image)
            .WithPorts(new Dictionary<ushort, ushort> { [httpPort] = httpPort, [httpsPort] = httpsPort })
            .WithVolumes(new Dictionary<string, string> { [dataVolume] = dataVolume })
            .WithEnvironment(new Dictionary<string, string> { [envName] = envValue })
            .Build();

        //assert
        container.Ports.Should().HaveCount(expectedPortCount);
        container.Ports![httpPort].Should().Be(httpPort);
        container.Volumes![dataVolume].Should().Be(dataVolume);
        container.Environment![envName].Should().Be(envValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Image_Missing()
    {
        //arrange
        var builder = new ContainerProcessDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
