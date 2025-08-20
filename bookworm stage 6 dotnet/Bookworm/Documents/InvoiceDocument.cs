using Bookworm.DTO;
using Bookworm.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Bookworm.Documents
{
    public class InvoiceDocument : IDocument
    {
        private readonly InvoiceDto _invoice;
        public InvoiceDocument(InvoiceDto invoice) { _invoice = invoice; }
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Row(row =>
                {
                    row.RelativeItem().Text("INVOICE").FontSize(24).SemiBold();
                    row.ConstantItem(150).Text($"Invoice #{_invoice.InvoiceId}\nDate: {_invoice.Date:yyyy-MM-dd}").AlignRight();
                });
                page.Content().PaddingVertical(20).Column(col =>
                {
                    col.Item().Text($"Bill To: {_invoice.CustomerName} ({_invoice.CustomerEmail})").SemiBold();
                    col.Spacing(25);
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c => { c.RelativeColumn(4); c.RelativeColumn(2); c.ConstantColumn(80); });
                        table.Header(h => { h.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Description").Bold(); h.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Transaction").Bold(); h.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignRight().Text("Amount").Bold(); });
                        foreach (var item in _invoice.InvoiceDetails) { table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{item.ProductName}\nby {item.ProductAuthor}").Italic(); table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.TranType); table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text($"₹{item.SellPrice:N2}"); }
                    });
                    col.Item().AlignRight().PaddingTop(10).Text($"Grand Total: ₹{_invoice.Amount:N2}").FontSize(16).Bold();
                });
                page.Footer().AlignCenter().Text("Thank you for your business!");
            });
        }
    }
}