

namespace DAL.Models;

public class MatchQueue
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    public DateTime JoinDate { get; set; }
    
}