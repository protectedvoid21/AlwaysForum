namespace Data.Models; 

public class Comment {
    public int Id { get; set; }

    public string Description { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }

    public string AuthorId { get; set; }

    public ForumUser Author { get; set; }
    
    public DateTime CreatedTime { get; set; }

    public List<CommentVote> CommentVotes { get; set; } = new();

    public List<CommentReport> CommentReports { get; set; } = new();
}