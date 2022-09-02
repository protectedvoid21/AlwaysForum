namespace Data.Models; 

public class Reaction {
    public int Id { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }

    public ReactionType ReactionType { get; set; }

    public string UserId { get; set; }

    public ForumUser User { get; set; }
}