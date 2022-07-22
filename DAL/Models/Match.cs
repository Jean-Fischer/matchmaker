using System.ComponentModel.DataAnnotations.Schema;
using DAL.Migrations;

namespace DAL.Models;

public class Match
{
    public int Id { get; set; }
    
    public virtual List<Participation> Participations { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? PlayDate { get; set; }


    public (Participation A, Participation B) GetParticipations()
    {
        if (Participations.Count != 2)
            throw new NotImplementedException("SimulateMatchResult implemented only for 2 players");
        return (Participations[0], Participations[1]);
    }
}