using System;
using System.Xml;
using System.Xml.Serialization;
using FormatterLibrary.Interfaces;

namespace FormatterLibrary.Models;

public class XmlResponseFormatter : IResponseFormatter
{
    public string MediaType => "application/xml";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            var writer = new StringWriter();

            serializer.Serialize(writer, data);

            return await Task.FromResult(writer.ToString());
        }
        catch (XmlException ex)
        {
            throw new XmlException("Xmlserializer error", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("Unsupported type error", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpeted error occurred during xml serialization", ex);
        }
    }
}
