namespace Business.Services.RandomUserGeneration.Model;

public class GeneratedPlayerResults
{
    public List<GeneratedPlayer> Results { get; set; }
}

public class GeneratedPlayer
{
    public GeneratedPlayerName Name { get; set; }
}

public class GeneratedPlayerName
{
    public string First { get; set; }
    public string Last { get; set; }
}
