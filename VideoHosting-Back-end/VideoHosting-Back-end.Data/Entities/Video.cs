﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoHosting_Back_end.Data.Entities;
public class Video
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
        
    public DateTime DayOfCreation { get; set; }

    public int Views { get; set; }

    public virtual User User { get; set; }

    public virtual List<Commentary> Commentaries { get; set; }

    public virtual List<VideoUser> Likes { get; set; }

    public virtual List<VideoUser> Dislikes { get; set; }

    public string PhotoPath { get; set; }

    public string VideoPath { get; set; }

    public Video()
    {
        Commentaries = new List<Commentary>();
        Likes = new List<VideoUser>();
        Dislikes = new List<VideoUser>();
    }

    public void AddCommentary(Commentary commentary)
    {
        Commentaries.Add(commentary);
    }
    public void DeleteCommentary(Commentary commentary)
    {
        Commentaries.Remove(commentary);
    }
}
