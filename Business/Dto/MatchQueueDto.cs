namespace Business.Dto;

public class MatchQueueDto
{
    public int Id { get; set; }
    public PlayerDto Player { get; set; } 
    public DateTime JoinDate { get; set; }
    
}