namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure an 'OAuth2' authentication scheme
/// </summary>
[DataContract]
public class OAuth2AuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public OAuth2AuthenticationProperties() { }

    /// <inheritdoc/>
    public OAuth2AuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

    /// <summary>
    /// Gets/sets the OAuth2 grant type to use
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string GrantType
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(GrantType).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(GrantType).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the uri of the OAuth2 authority to use to generate an access token
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri Authority
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Authority).ToCamelCase(), out var value)) return new((string)value);
            else return null!;
        }
        set
        {
            this.Properties[nameof(Authority).ToCamelCase()] = value.ToString();
        }
    }

    /// <summary>
    /// Gets/sets the id of the OAuth2 client to use
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string ClientId
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(ClientId).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(ClientId).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the secret of the non-public OAuth2 client to use. Required when <see cref="GrantType"/> has been set to <see cref="OAuth2GrantType.TokenExchange"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? ClientSecret
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(ClientSecret).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(ClientSecret).ToCamelCase());
            else this.Properties[nameof(ClientSecret).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the username to use when authenticating
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? Username
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Username).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(Username).ToCamelCase());
            else this.Properties[nameof(Username).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the password to use when authenticating
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? Password
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Password).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(Password).ToCamelCase());
            else this.Properties[nameof(Password).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets a space-separated list containing the authorized scopes to request
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? Scope
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Scope).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(Scope).ToCamelCase());
            else this.Properties[nameof(Scope).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets a space-separated list containing the authorized audiences of the resulting token
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? Audience
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Audience).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(Audience).ToCamelCase());
            else this.Properties[nameof(Audience).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the token that represents the identity of the party on behalf of whom the request is being made.Typically, the subject of this token will be the subject of the security token issued in response to the request.
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? SubjectToken
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(SubjectToken).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(SubjectToken).ToCamelCase());
            else this.Properties[nameof(SubjectToken).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets an identifie that indicates the type of the security token in the "subject_token" parameter.
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? SubjectTokenType
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(SubjectTokenType).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(SubjectTokenType).ToCamelCase());
            else this.Properties[nameof(SubjectTokenType).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets a token that represents the identity of the acting party.Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? ActorToken
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(ActorToken).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(ActorToken).ToCamelCase());
            else this.Properties[nameof(ActorToken).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets an identifier, as described in Section 3, that indicates the type of the security token in the "actor_token" parameter. This is REQUIRED when the "actor_token" parameter is present in the request but MUST NOT be included otherwise.
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? ActorTokenType
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(ActorTokenType).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            if (value == null) this.Properties.Remove(nameof(ActorTokenType).ToCamelCase());
            else this.Properties[nameof(ActorTokenType).ToCamelCase()] = value;
        }
    }

}
