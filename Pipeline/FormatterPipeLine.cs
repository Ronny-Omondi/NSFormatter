using System;
using FormatterLibrary.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FormatterLibrary.Pipeline;
/// <summary>
/// Provides a method for registering formatters and format response in a specified type
/// </summary>
public class FormatterPipeLine
{
    /// <summary>
    /// Dictionary that stores formatters type and their respective implementation
    /// </summary>
    private readonly Dictionary<string, IResponseFormatter> _formatters = new();
    
    /// <summary>
    /// Initializes the dictionary with values
    /// </summary>
    /// <param name="formatters"></param>
    public FormatterPipeLine(IEnumerable<IResponseFormatter> formatters)
    {
        foreach (var formatter in formatters)
        {
            _formatters[formatter.MediaType] = formatter;
        }
    }

    /// <summary>
    /// Enables registration of formatters
    /// </summary>
    /// <param name="mediaType">Specifies the media type</param>
    /// <param name="formatter">TRepresents formatter</param>
    public void RegisterFormatter(string mediaType, IResponseFormatter formatter)
    {
        _formatters[mediaType] = formatter;
    }

    /// <summary>
    /// Displays all registered formatters
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetMediaTypes() => _formatters.Keys;

    /// <summary>
    /// Format response in a specified format
    /// </summary>
    /// <typeparam name="T">Represents an object</typeparam>
    /// <param name="data">Data to be formatted</param>
    /// <param name="mediaType">Specifies the media type</param>
    /// <param name="context"></param>
    /// <returns>Returns string respresenting serialized data</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<string> FormatResponseAsync<T>(T data, string mediaType, HttpContext context)
    {
        if (!_formatters.TryGetValue(mediaType, out IResponseFormatter? response))
            throw new KeyNotFoundException("Unsupported media type");

        return await response.StringFormatAsync(data);
    }
}
