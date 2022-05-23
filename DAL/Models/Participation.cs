using System.Reflection.Metadata.Ecma335;

namespace DAL.Models;

public class Participation
{
    public int Id { get; set; }
    public Player Player { get; set; }
    public Match Match { get; set; }
    public int StartingRank { get; set; }
    public int FinishingRank { get; set; }
    public int RankDifference { get; set; }
    public bool? HasWon { get; set; }
    
}