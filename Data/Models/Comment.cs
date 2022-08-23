namespace Data.Models; 

public class Comment {
    public int Id { get; set; }

    public string Description { get; set; }

    public int PostId { get; set; }

    public string AuthorId { get; set; }

    public ForumUser Author { get; set; }
    
    public DateTime CreatedTime { get; set; }

    public List<CommentUpVote> Votes { get; set; } = new();
}