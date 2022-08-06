namespace Business.Dto;

public class MatchDto
{
    public int Id { get; set; }
    public ParticipationDto ParticipationA { get; set; }
    public ParticipationDto ParticipationB { get; set; }

    public IEnumerable<ParticipationDto> Participations { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? PlayDate { get; set; }
}