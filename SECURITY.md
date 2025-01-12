# Security Policy

## Reporting a Vulnerability

The Serverless Workflow .NET SDK team and community take security vulnerabilities very seriously. Responsible disclosure of security issues is greatly appreciated, and every effort will be made to acknowledge and address your findings.

To report a security issue:

- **Use the GitHub Security Advisory**: Please use the ["Report a Vulnerability"](https://github.com/serverlessworkflow/sdk-net/security/advisories/new) tab on GitHub to submit your report.

The team will acknowledge your report and provide details on the next steps. After the initial response, the security team will keep you informed of the progress towards a fix and any subsequent announcements. Additional information or guidance may be requested as necessary.

## Security Best Practices

To ensure the security and stability of the Serverless Workflow .NET SDK, consider the following best practices:

- **Runtime Environment Hardening**: Secure the underlying infrastructure where the SDK is used. This includes using up-to-date operating systems, applying security patches regularly, and configuring firewalls and security groups to limit access to only necessary ports and services.

- **Secure Configuration Management**: Ensure that configuration files, especially those containing sensitive information like API keys, connection strings, or certificates, are stored securely. Use environment variables, secret management tools, or configuration providers to avoid hardcoding sensitive data in your application.

- **Dependency Management**: Regularly audit and update dependencies used in your project. Use tools like [Dependabot](https://github.com/dependabot) or similar dependency management solutions to identify vulnerabilities in third-party NuGet packages and address them promptly.

By adhering to these best practices, the security of workflows and applications built using the Serverless Workflow .NET SDK can be significantly enhanced, reducing the risk of vulnerabilities and ensuring the integrity and reliability of the workflows executed.

---

Thank you for contributing to the security and integrity of the Serverless Workflow .NET SDK!
