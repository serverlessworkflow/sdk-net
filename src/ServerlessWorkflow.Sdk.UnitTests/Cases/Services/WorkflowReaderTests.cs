using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Services
{

    public class WorkflowReaderTests
    {
        private const string RepositoryUrl = "https://api.github.com/repos/serverlessworkflow/sdk-java";
        private const string ListExamplesEndpoint = "/contents/api/src/test/resources/examples";
        private const string Branch = "main";

        protected IWorkflowReader Reader { get; } = WorkflowReader.Create();

        [Fact]
        public async Task Read()
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();
            await foreach(Example example in this.GetOfficialExamplesAsync())
            {
                WorkflowDefinition workflow = null;
                try
                {
                    workflow = this.Reader.Read(example.FileStream, example.Format);
                    Assert.NotNull(workflow);
                }
                catch(Exception ex)
                {
                    errors.Add(example.Name, ex.ToString());
                }
            }
        }

        private class Example
        {

            public string Name { get; set; }

            public WorkflowDefinitionFormat Format { get; set; }

            public MemoryStream FileStream { get; } = new MemoryStream();

        }

        private async IAsyncEnumerable<Example> GetOfficialExamplesAsync()
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(RepositoryUrl) })
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("NetHttp", "5.0"));
                JArray files;
                using (HttpResponseMessage response = await client.GetAsync($"{RepositoryUrl}{ListExamplesEndpoint}?branch={Branch}"))
                {
                    string json = await response.Content?.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    files = JsonConvert.DeserializeObject<JArray>(json);
                }
                foreach(JObject fileInfo in files)
                {
                    string fileName = fileInfo.Property("name").Value.ToString();
                    Example example = new Example() 
                    { 
                        Name = fileName,
                        Format = fileName.ToLower().EndsWith(".yaml") | fileName.ToLower().EndsWith(".yml") ? WorkflowDefinitionFormat.Yaml : WorkflowDefinitionFormat.Json
                    };
                    using (HttpResponseMessage response = await client.GetAsync(fileInfo.Property("url").Value.ToString()))
                    {
                        string json = await response.Content?.ReadAsStringAsync();
                        response.EnsureSuccessStatusCode();
                        JObject file = JObject.Parse(json);
                        await example.FileStream.WriteAsync(Convert.FromBase64String(file.Property("content").Value.ToString()));
                        await example.FileStream.FlushAsync();
                        example.FileStream.Position = 0;
                    }
                    yield return example;
                }
            }
        }

    }

}
