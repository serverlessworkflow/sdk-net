// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

global using Json.Pointer;
global using Json.Schema;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using ServerlessWorkflow.Sdk;
global using ServerlessWorkflow.Sdk.Models;
global using ServerlessWorkflow.Sdk.Models.Authentication;
global using ServerlessWorkflow.Sdk.Models.Tasks;
global using ServerlessWorkflow.Sdk.Runtime.Configuration;
global using ServerlessWorkflow.Sdk.Runtime.Models;
global using ServerlessWorkflow.Sdk.Runtime.Services;
global using ServerlessWorkflow.Sdk.Runtime.Services.Executors;
global using System.Collections.Concurrent;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Mime;
global using System.Reactive.Linq;
global using System.Reactive.Subjects;
global using System.Runtime.InteropServices;
global using System.Runtime.Serialization;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Nodes;
global using System.Text.Json.Serialization;
