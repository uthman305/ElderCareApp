using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElderCareApp.Implementations.Services
{
    public class PdfService : IPdfService
    
    {
      
      public byte[] GenerateHealthReport(Elder elder, IEnumerable<ElderHealthRecord> records)
    {
        // QuestPDF requires a License setup (Community is free for personal/small use)
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Text($"Health Report: {elder.FullName}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Header Row
                    table.Header(header =>
                    {
                        header.Cell().Text("Date");
                        header.Cell().Text("Heart Rate");
                        header.Cell().Text("Blood Pressure");
                    });

                    // Data Rows
                    foreach (var record in records)
                    {
                        table.Cell().Text(record.DateRecorded.ToString("g"));
                        table.Cell().Text($"{record.HeartRate} bpm");
                        table.Cell().Text(record.BloodPressure);
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        }).GeneratePdf();
    }
    }
}
