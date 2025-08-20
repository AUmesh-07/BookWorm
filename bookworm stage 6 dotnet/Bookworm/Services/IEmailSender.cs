using Bookworm.Models;

namespace Bookworm.Services
{
    public interface IEmailSender
    {
        Task SendEmailWithAttachmentAsync(Message message, byte[] pdfAttachment, string attachmentFileName);
    }
}
