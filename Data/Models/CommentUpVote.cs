namespace Data.Models; 

public class CommentUpVote {
    public int Id { get; set; }

    public Comment Comment { get; set; }

    public int CommentId { get; set; }

    public string AuthorId { get; set; }

    public ForumUser Author { get; set; }

    public bool IsUpVote { get; set; }
}