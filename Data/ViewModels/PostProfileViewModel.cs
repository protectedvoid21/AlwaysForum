namespace Data.ViewModels; 

public class PostProfileViewModel {
    public int Id { get; set; }

    public string Title { get; set; }

    public int SectionId { get; set; }

    public string SectionName { get; set; }

    public DateTime CreatedDate { get; set; }
}