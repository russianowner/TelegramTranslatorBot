namespace TelegramTranslatorBot.Models;

public class UserState
{
    public string? TargetLang { get; set; } = "en"; 
    public string? SourceLang { get; set; } = "auto"; 
    public string? Mode { get; set; } = null; 
}
