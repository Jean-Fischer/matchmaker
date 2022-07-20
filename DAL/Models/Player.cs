﻿namespace DAL.Models;

public class Player
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public List<Participation> Participations { get; set; }
    public int Rank { get; set; }

    public List<MatchQueue> MatchQueues { get; set; }
}