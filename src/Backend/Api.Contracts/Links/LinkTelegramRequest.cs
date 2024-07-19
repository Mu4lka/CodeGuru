namespace Api.Contracts.Links;

public class LinkTelegramRequest
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
    public long TelegramUserId { get; set; }
}
