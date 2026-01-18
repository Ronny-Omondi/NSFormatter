using System;
using FormatterLibrary.Interfaces;
using MessagePack;

namespace FormatterLibrary.Models;

public class MessagePackResponseFormatter : IResponseFormatter
{
    public string MediaType => "application/x-msgpack";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");

        try
        {
            await using var memoryStream = new MemoryStream();

            await MessagePackSerializer.SerializeAsync(memoryStream, data, MessagePack.Resolvers.ContractlessStandardResolver.Options, cancellationToken);

            return Convert.ToBase64String(memoryStream.ToArray());
        }
        catch (MessagePackSerializationException ex)
        {
            throw new MessagePackSerializationException("An error have occured", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpeted error occurred messagepack serialization", ex);
        }
    }
}
