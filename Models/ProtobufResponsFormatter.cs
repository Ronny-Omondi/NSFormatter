using System;
using FormatterLibrary.Interfaces;
using ProtoBuf;

namespace FormatterLibrary.Models;

public class ProtobufResponseFormatter : IResponseFormatter
{
    public string MediaType => "application/x-protobuf";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");
        try
        {
            using var memory = new MemoryStream();

            Serializer.Serialize(memory, data);

            var result = memory.ToArray();

            return Convert.ToBase64String(memory.ToArray());
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Invalid operation", ex);
        }
        catch (ProtoException ex)
        {
            throw new ProtoException("Protobuf error occured during serialization", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred during protobuf serialization", ex);
        }
    }
}
