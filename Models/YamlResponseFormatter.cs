using System;
using FormatterLibrary.Interfaces;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace FormatterLibrary.Models;

public class YamlResponseFormatter : IResponseFormatter
{
    public string MediaType => "application/yaml";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");

        try
        {
            var serializer = new SerializerBuilder().Build();

            return await Task.FromResult(serializer.Serialize(data));
        }
        catch (YamlException ex)
        {
            throw new YamlException("Yaml exception error occured", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpeted error occurred during yaml serialization", ex);
        }
    }
}
