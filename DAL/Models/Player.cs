namespace DAL.Models;

public class Player
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public virtual List<Participation> Participations { get; set; }
    public int Rank { get; set; }

    public virtual List<MatchQueue> MatchQueues { get; set; }
}