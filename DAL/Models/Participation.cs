using System.Reflection.Metadata.Ecma335;

namespace DAL.Models;

public class Participation
{
    public int Id { get; set; }
    public virtual Player Player { get; set; }
    public virtual Match Match { get; set; }
    public int StartingRank { get; set; }
    public int FinishingRank { get; set; }
    public int RankDifference { get; set; }
    public bool? HasWon { get; set; }
    
}