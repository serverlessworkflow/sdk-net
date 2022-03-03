using FluentAssertions;
using ProtoBuf;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.UnitTests.Data.Factories;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases
{

    public class SerializationTests
    {

        [Fact]
        public async Task SerializeAndDeserializer_WorkflowDefinition_To_ProtoBuf_ShouldWork()
        {
            //arrange
            var sourceDefinition = WorkflowDefinitionFactory.Create();

            //act
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, sourceDefinition);
            await stream.FlushAsync();
            stream.Position = 0;
            var deserializedDefinition = Serializer.Deserialize<WorkflowDefinition>(stream);

            //assert
            deserializedDefinition.Should().NotBeNull();
            deserializedDefinition.Should().BeEquivalentTo(sourceDefinition);
        }

    }

}
