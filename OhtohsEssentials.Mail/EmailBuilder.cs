using MimeKit;
using MimeKit.Text;
using static OhtohsEssentials.Mail.EmailBuilder;

namespace OhtohsEssentials.Mail;

public class EmailBuilder
    : ISenderStep, IRecipientStep, IMessageTypeStep, IContentStep, IBuildStep
{
    private MailboxAddress _sender = default!;
    private MailboxAddress _recipient = default!;
    private TextFormat _messageType;
    private string _content = string.Empty;
    private readonly List<MailboxAddress> _ccList = new();
    private readonly List<MailboxAddress> _bccList = new();
    private MessagePriority _priority = MessagePriority.Normal;
    private string _subject = string.Empty;

    private EmailBuilder() { }

    public MimeMessage Build()
    {
        var message = new MimeMessage
        {
            Sender = _sender,
            Subject = _subject,
            Priority = _priority,
            Body = new TextPart(_messageType)
            {
                Text = _content
            }
        };

        message.From.Add(_sender);
        message.To.Add(_recipient);

        foreach (var cc in _ccList)
            message.Cc.Add(cc);

        foreach (var bcc in _bccList)
            message.Bcc.Add(bcc);

        return message;
    }

    public static ISenderStep Start()
    {
        return new EmailBuilder();
    }

    public IRecipientStep SetSender(string name, string email)
    {
        _sender = new MailboxAddress(name, email);
        return this;
    }

    public IRecipientStep SetSender(string email)
    {
        _sender = new MailboxAddress(string.Empty, email);
        return this;
    }

    public IMessageTypeStep SetRecipient(string name, string email)
    {
        _recipient = new MailboxAddress(name, email);
        return this;
    }

    public IMessageTypeStep SetRecipient(string email)
    {
        _recipient = new MailboxAddress(string.Empty, email);
        return this;
    }

    public IContentStep AsPlainText()
    {
        _messageType = TextFormat.Plain;
        return this;
    }

    public IContentStep AsHtml()
    {
        _messageType = TextFormat.Html;
        return this;
    }

    public IBuildStep SetContent(string content)
    {
        _content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }

    public IBuildStep SetSubject(string subject)
    {
        _subject = subject ?? throw new ArgumentNullException(nameof(subject));
        return this;
    }

    public IBuildStep AddCc(string name, string email)
    {
        _ccList.Add(new MailboxAddress(name, email));
        return this;
    }

    public IBuildStep AddCc(string email)
    {
        _ccList.Add(new MailboxAddress(string.Empty, email));
        return this;
    }

    public IBuildStep AddBcc(string name, string email)
    {
        _bccList.Add(new MailboxAddress(name, email));
        return this;
    }

    public IBuildStep AddBcc(string email)
    {
        _bccList.Add(new MailboxAddress(string.Empty, email));
        return this;
    }

    public IBuildStep SetPriority(MessagePriority priority)
    {
        _priority = priority;
        return this;
    }

    #region [ Stages ]

    public interface ISenderStep
    {
        IRecipientStep SetSender(string name, string email);
        IRecipientStep SetSender(string email);
    }

    public interface IRecipientStep
    {
        IMessageTypeStep SetRecipient(string name, string email);
        IMessageTypeStep SetRecipient(string email);
    }

    public interface IMessageTypeStep
    {
        IContentStep AsPlainText();
        IContentStep AsHtml();
    }

    public interface IContentStep
    {
        IBuildStep SetContent(string content);
        IBuildStep SetSubject(string subject);
        IBuildStep AddCc(string name, string email);
        IBuildStep AddCc(string email);
        IBuildStep AddBcc(string name, string email);
        IBuildStep AddBcc(string email);
        IBuildStep SetPriority(MessagePriority priority);
        MimeMessage Build();
    }

    public interface IBuildStep
    {
        IBuildStep SetSubject(string subject);
        IBuildStep AddCc(string name, string email);
        IBuildStep AddCc(string email);
        IBuildStep AddBcc(string name, string email);
        IBuildStep AddBcc(string email);
        IBuildStep SetPriority(MessagePriority priority);
        MimeMessage Build();
    }

    #endregion
}

//var email = EmailBuilder.Start()
//    .SetSender("app@myapp.com")
//    .SetRecipient("user@example.com")
//    .AsPlainText()
//    .SetSubject("Test Email")
//    .SetContent("This is a test email from the step builder pattern!")
//    .Build();

//// For testing, you can inspect the MIME message
//Console.WriteLine("Email built successfully:");
//Console.WriteLine($"From: {email.From}");
//Console.WriteLine($"To: {email.To}");
//Console.WriteLine($"Subject: {email.Subject}");
