using System;
using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FormatterLibrary.Interfaces;

namespace FormatterLibrary.Models;

public class CsvResponseFormatter : IResponseFormatter
{
    private readonly CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true
    };

    public string MediaType => "application/csv";

    public async Task<string> StringFormatAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data), "Data cannot be null");

        try
        {
            using var writer = new StringWriter();

            using var csvWriter = new CsvWriter(writer, configuration);

            if (data is IEnumerable ienumerable && data is not string)
            {
                await csvWriter.WriteRecordsAsync(ienumerable, cancellationToken);
            }
            else
            {
                csvWriter.WriteHeader<T>();

                await csvWriter.NextRecordAsync();

                csvWriter.WriteRecord(data);

                await csvWriter.NextRecordAsync();
            }

            return writer.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpeted error occurred", ex);
        }
    }
}
