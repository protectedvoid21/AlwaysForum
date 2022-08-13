namespace Data.ViewModels; 

public class MessageViewModel {
    public string Title { get; set; }
    public string Description { get; set; }
    public MessageType MessageType { get; set; }
}

public enum MessageType {
    Success,
    Error,
    Warning,
}