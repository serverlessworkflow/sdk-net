/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.IO;
using ServerlessWorkflow.Sdk.UnitTests.Data.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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

        [Fact(Skip = "YAML parsing issue for non-complex properties (ex: externalRefs)")]
        public async Task Read_Yaml_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "operation.yaml"));

            //act
            var parsedWorkflow = await this.Reader.ParseAsync(yaml);

            //assert
            parsedWorkflow
                .Should()
                .NotBeNull();
            parsedWorkflow.Events
                .Should()
                .NotBeEmpty();
            parsedWorkflow.Functions
                .Should()
                .NotBeEmpty();
            parsedWorkflow.States
                .Should()
                .NotBeEmpty();
            parsedWorkflow.Metadata
                .Should()
                .NotBeNull();
            parsedWorkflow.Metadata
                .Get("podSize")
                .Should()
                .Be("small");
        }

        [Fact]
        public async Task Read_Json_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "operation.json"));

            //act
            var parsedWorkflow = await this.Reader.ParseAsync(yaml);

            //assert
            parsedWorkflow
                .Should()
                .NotBeNull();
            parsedWorkflow.Events
                .Should()
                .NotBeEmpty();
            parsedWorkflow.Functions
                .Should()
                .NotBeEmpty();
            parsedWorkflow.States
                .Should()
                .NotBeEmpty();
            parsedWorkflow.Metadata
                .Should()
                .NotBeNull();
            parsedWorkflow.Metadata
                .Get("podSize")
                .Should()
                .Be("small");
        }

        [Fact]
        public async Task Read_OfficialExamples_ShouldWork()
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();
            await foreach(Example example in GetOfficialExamplesAsync())
            {
                try
                {
                    WorkflowDefinition workflow = await this.Reader.ReadAsync(example.FileStream);
                    Assert.NotNull(workflow);
                }
                catch(Exception ex)
                {
                    errors.Add(example.Name, ex.ToString());
                }
            }
        }

        [Fact(Skip = "YAML parsing issue for non-complex properties (ex: externalRefs)")]
        public async Task Read_Yaml_ExternalDefinitions_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "externalref.yaml"));

            //act
            var workflow = await this.Reader.ParseAsync(yaml);

            //assert
            workflow
                .Should()
                .NotBeNull();
            workflow.Constants
                .Should()
                .NotBeNull();
            workflow.Secrets
                .Should()
                .NotBeEmpty();
            //workflow.DataInputSchema
            //    .Should()
            //    .NotBeNull();
            workflow.Events
                .Should()
                .NotBeEmpty();
            workflow.Functions
                .Should()
                .NotBeEmpty();
            workflow.Retries
                .Should()
                .NotBeEmpty();
        }

        [Fact]
        public async Task Read_Json_ExternalDefinitions_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "externalref.json"));

            //act
            var workflow = await this.Reader.ParseAsync(yaml);

            //assert
            workflow
                .Should()
                .NotBeNull();
            workflow.Constants
                .Should()
                .NotBeNull();
            workflow.Secrets
                .Should()
                .NotBeEmpty();
            //workflow.DataInputSchema
            //    .Should()
            //    .NotBeNull();
            workflow.Events
                .Should()
                .NotBeEmpty();
            workflow.Functions
                .Should()
                .NotBeEmpty();
            workflow.Retries
                .Should()
                .NotBeEmpty();
        }

        private class Example
        {

            public string Name { get; set; }

            public MemoryStream FileStream { get; } = new MemoryStream();

        }

        private static async IAsyncEnumerable<Example> GetOfficialExamplesAsync()
        {
            using HttpClient client = new() { BaseAddress = new Uri(RepositoryUrl) };
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("NetHttp", "5.0"));
            JArray files;
            using (HttpResponseMessage response = await client.GetAsync($"{RepositoryUrl}{ListExamplesEndpoint}?branch={Branch}"))
            {
                string json = await response.Content?.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                files = JsonConvert.DeserializeObject<JArray>(json);
            }
            foreach (JObject fileInfo in files)
            {
                string fileName = fileInfo.Property("name").Value.ToString();
                Example example = new()
                {
                    Name = fileName
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
