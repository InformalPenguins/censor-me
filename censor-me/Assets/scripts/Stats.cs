using System;
using System.Collections.Generic;

[Serializable]
public class Stats {
    public int EnemiesKilled { get; set; }

    public int BestScore { get; set; }

    public int GlobalScore { get; set; }

    public int GamesPlayed { get; set; }

    public List<World> WorldsOwned { get; set; }

    public World WorldSelected { get; set; }
}

public enum World {
    BASIC, ZOMBIES, BIRDS, MONSTERS
}
