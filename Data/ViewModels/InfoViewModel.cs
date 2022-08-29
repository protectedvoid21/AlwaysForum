namespace Data.ViewModels; 

public class InfoViewModel {
    public string Title { get; set; }
    public string Description { get; set; }
    public InfoType InfoType { get; set; }
}

public enum InfoType {
    Success,
    Error,
    Warning,
}