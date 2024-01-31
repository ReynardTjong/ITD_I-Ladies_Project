using System;

public class Player
{
    public string playerUsername;
    public int chaptersCompleted;
    public int achievementsAchieved;
    public int booksUnlocked;
    public int gardenAreasUnlocked;

    public Player()
    {

    }

    public Player(string playerUsername, int chaptersCompleted, int achievementsAchieved, int booksUnlocked, int gardenAreasUnlocked)
    {
        this.playerUsername = playerUsername;
        this.chaptersCompleted = chaptersCompleted;
        this.achievementsAchieved = achievementsAchieved;
        this.booksUnlocked = booksUnlocked;
        this.gardenAreasUnlocked = gardenAreasUnlocked;
    }
}