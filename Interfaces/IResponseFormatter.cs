using System;

namespace FormatterLibrary.Interfaces;

public interface IResponseFormatter
{
    /// <summary>
    /// Serializes data to string
    /// </summary>
    /// <typeparam name="T">Specifies the type to be serialized</typeparam>
    /// <param name="data">Represents data to be serialized</param>
    /// <returns>Returns string</returns>
    Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Specifies the media type
    /// </summary>
    string MediaType{get;}
}
