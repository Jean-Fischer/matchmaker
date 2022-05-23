namespace Business.Dto;

public class ParticipationDto
{
    public int Id { get; set; }
    public PlayerDto Player { get; set; }
    public int StartingRank { get; set; }
    public int FinishingRank { get; set; }
    public int RankDifference { get; set; }
    public bool? HasWon { get; set; }
}