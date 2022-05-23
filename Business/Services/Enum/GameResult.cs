namespace Business.Services.Enum;

public enum GameResultCoefficient
{
    Win = 1,
    Draw = 1/2,
    Lose = 0
}

public static class GameResultCoefficientHelper{

    public static GameResultCoefficient GetCoefficientFromResult(bool? hasWon)
    {
        switch (hasWon)
        {
            case false: return GameResultCoefficient.Lose;
            case true : return GameResultCoefficient.Win;
            default : return GameResultCoefficient.Draw;
        }
        
    }

}
