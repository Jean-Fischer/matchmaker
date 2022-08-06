using System.ComponentModel.DataAnnotations.Schema;
using DAL.Migrations;

namespace DAL.Models;

public class Match
{
    public int Id { get; set; }
    
    public virtual List<Participation> Participations { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? PlayDate { get; set; }


    
}