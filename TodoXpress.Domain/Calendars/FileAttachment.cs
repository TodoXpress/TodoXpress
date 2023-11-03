namespace TodoXpress.Domain.Calendars;

public class FileAttachment : IAttachment
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the file.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// The content of the file as a byte array.
    /// </summary>
    public byte[] Content { get; set; } = [];
}
