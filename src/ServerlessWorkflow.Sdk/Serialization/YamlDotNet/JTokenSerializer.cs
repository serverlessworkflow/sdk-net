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
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk;
using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents the <see cref="IYamlTypeConverter"/> used to serialize <see cref="JToken"/>s
    /// </summary>
    public class JTokenSerializer
        : IYamlTypeConverter
    {

        /// <inheritdoc/>
        public virtual bool Accepts(Type type)
        {
            return typeof(JToken).IsAssignableFrom(type);
        }

        /// <inheritdoc/>
        public virtual object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public virtual void WriteYaml(IEmitter emitter, object value, Type type)
        {
            this.WriteJToken(emitter, value as JToken);
        }

        /// <summary>
        /// Serializes the specified <see cref="JToken"/>
        /// </summary>
        /// <param name="emitter">The current <see cref="IEmitter"/></param>
        /// <param name="token">The <see cref="JToken"/> to serialize</param>
        protected void WriteJToken(IEmitter emitter, JToken token)
        {
            switch (token)
            {
                case JObject jobject:
                    this.WriteJObject(emitter, jobject);
                    break;
                case JArray jarray:
                    this.WriteJArray(emitter, jarray);
                    break;
                default:
                    string scalar = null;
                    switch (token.Type)
                    {
                        case JTokenType.Boolean:
                            scalar = token.ToString().ToLower();
                            break;
                        case JTokenType.TimeSpan:
                            scalar = Iso8601TimeSpan.Format(token.ToObject<TimeSpan>());
                            break;
                        default:
                            scalar = token.ToString();
                            break;
                    }
                    emitter.Emit(new Scalar(scalar));
                    break;
            }
        }

        /// <summary>
        /// Serializes the specified <see cref="JArray"/>
        /// </summary>
        /// <param name="emitter">The current <see cref="IEmitter"/></param>
        /// <param name="array">The <see cref="JArray"/> to serialize</param>
        protected void WriteJArray(IEmitter emitter, JArray array)
        {
            emitter.Emit(new SequenceStart(null, null, false, SequenceStyle.Block));
            foreach (JToken token in array)
            {
                this.WriteJToken(emitter, token);
            }
            emitter.Emit(new SequenceEnd());
        }

        /// <summary>
        /// Serializes the specified <see cref="JObject"/>
        /// </summary>
        /// <param name="emitter">The current <see cref="IEmitter"/></param>
        /// <param name="jobject">The <see cref="JObject"/> to serialize</param>
        protected void WriteJObject(IEmitter emitter, JObject jobject)
        {
            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));
            foreach (JProperty property in jobject.Properties())
            {
                this.WriteJProperty(emitter, property);
            }
            emitter.Emit(new MappingEnd());
        }

        /// <summary>
        /// Serializes the specified <see cref="JProperty"/>
        /// </summary>
        /// <param name="emitter">The current <see cref="IEmitter"/></param>
        /// <param name="jproperty">The <see cref="JProperty"/> to serialize</param>
        protected void WriteJProperty(IEmitter emitter, JProperty jproperty)
        {
            if (jproperty.Value == null
                || jproperty.Value.Type == JTokenType.Null)
                return;
            emitter.Emit(new Scalar(jproperty.Name.ToCamelCase()));
            this.WriteJToken(emitter, jproperty.Value);
        }

    }

}
