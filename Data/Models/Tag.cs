﻿namespace Data.Models; 

public class Tag {
    public int Id { get; set; }

    public string Name { get; set; }

    public List<PostTag> Posts { get; set; }
}