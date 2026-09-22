
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Colors;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Fonts;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Billings.Reports.Pdf;
public class GenerateBillingsReportPdfUseCase : IGenerateBillingsReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    private readonly IBillingsReadOnlyRepository _repository;
    public GenerateBillingsReportPdfUseCase(IBillingsReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new BillingsReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var billings = await _repository.FilterByMonthPaidOrOpen(month);

        if (billings.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithLogoAndName(page);

        var totalExpenses = billings.Sum(billing => billing.Amount);

        CreateTotalSpentSection(page, month, totalExpenses);

        foreach (var billing in billings)
        {
            var table = CreateExpenseTable(page);

            // First Row __________
            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;
            AddServiceName(row.Cells[0], billing.ServiceName);
            AddHeaderForAmount(row.Cells[3]);


            // Second Row __________
            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            row.Cells[0].AddParagraph(billing.Date.ToString("D"));
            SetStyleBaseForBillingInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 10;

            row.Cells[1].AddParagraph("-");
            SetStyleBaseForBillingInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(billing.PaymentMethod.PaymentMethodToString());
            SetStyleBaseForBillingInformation(row.Cells[2]);

            AddAmount(row.Cells[3], billing.Amount);


            // Third Row __________
            if (!string.IsNullOrWhiteSpace(billing.Notes))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

                AddNotes(descriptionRow.Cells[0], billing.Notes);

                row.Cells[3].MergeDown = 1;
            }


            // White Space __________
            AddWhiteSpace(table);
        }


        return RenderDocument(document);
    }
    private void AddNotes(Cell cell, string notes)
    {
        cell.AddParagraph(notes);
        cell.MergeRight = 2;
        cell.Shading.Color = ColorsHelper.GREEN_LIGHTER;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 10;
        cell.Format.Font = new Font
        {
            Name = FontHelper.ROBOTO_REGULAR,
            Size = 10,
            Color = ColorsHelper.BLACK
        };
    }


    private void AddAmount(Cell cell, decimal amount)
    {
        var fomattedAmount = $"{CURRENCY_SYMBOL} {amount}";
        cell.AddParagraph(fomattedAmount);
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.Font = new Font
        {
            Name = FontHelper.ROBOTO_REGULAR,
            Size = 14,
            Color = ColorsHelper.BLACK
        };
    }

    private void SetStyleBaseForBillingInformation(Cell cell)
    {
        cell.Shading.Color = ColorsHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.Font = new Font
        {
            Name = FontHelper.ROBOTO_REGULAR,
            Size = 12,
            Color = ColorsHelper.BLACK
        };
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }


    private void AddServiceName(Cell cell, string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.MergeRight = 2;
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 10;
        cell.Format.Font = new Font
        {
            Name = FontHelper.BEBAS_NEUE_REGULAR,
            Size = 15,
            Color = ColorsHelper.WHITE
        };
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Shading.Color = ColorsHelper.GREEN_MEDIUM;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.Font = new Font
        {
            Name = FontHelper.BEBAS_NEUE_REGULAR,
            Size = 15,
            Color = ColorsHelper.WHITE
        };
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal total)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = String.Format(ResourceReportGenerationMessages.WEEKLY_REVENUE, month.ToString("Y"));

        paragraph.AddFormattedText(
            title,
            new Font
            {
                Name = FontHelper.ROBOTO_REGULAR,
                Size = 15
            });

        paragraph.AddLineBreak();

        var totalExpensesFormatted = $"{CURRENCY_SYMBOL} {total}";

        paragraph.AddFormattedText(
            totalExpensesFormatted,
            new Font
            {
                Name = FontHelper.BEBAS_NEUE_REGULAR,
                Size = 50
            });

    }

    private void CreateHeaderWithLogoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn(); // [0]
        table.AddColumn("300"); // [1]

        var barber_title = String.Format(ResourceReportGenerationMessages.BARBER_TITLE);

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "logo.png");

        var row = table.AddRow();
        row.Cells[0].AddImage(pathFile);
        row.Cells[1].AddParagraph(barber_title);
        row.Cells[1].Format.Font = new Font
        {
            Name = FontHelper.BEBAS_NEUE_REGULAR,
            Size = 25
        };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private Document CreateDocument(DateOnly month)
    {
        var doc = new Document();
        doc.Info.Title = $"{ResourceReportGenerationMessages.BARBER_TITLE}: {month:Y}";
        doc.Info.Author = "Ana Julia Lins";

        var style = doc.Styles["Normal"];
        style!.Font.Name = FontHelper.DEFAULT_FONT;

        return doc;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
