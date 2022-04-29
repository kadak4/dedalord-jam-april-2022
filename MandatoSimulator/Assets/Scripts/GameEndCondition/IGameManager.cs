using System;

public interface IGameManager
{
    event Action<string> OnGameLost;
    event Action<string> OnGameWon;
}
