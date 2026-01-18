using System;
using System.Text.Json;
using FormatterLibrary.Interfaces;

namespace FormatterLibrary.Models;

public class JsonResponseFormatter : IResponseFormatter
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions()
    {
        WriteIndented = true
    };

    public string MediaType => "application/json";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");

        try
        {
            using var memory = new MemoryStream();

            await JsonSerializer.SerializeAsync(memory, data, options, cancellationToken);

            memory.Position = 0;

            using var reader = new StreamReader(memory);

            return await reader.ReadToEndAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpeted error occurred", ex);
        }
    }
}
