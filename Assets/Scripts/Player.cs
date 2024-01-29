using System;

public class Player
{
    public string playerUsername;
    public int chaptersCompleted;
    public int achievementsAcquired;
    public int booksUnlocked;
    public int gardenAreasUnlocked;

    public Player()
    {

    }

    public Player(string playerUsername, int chaptersCompleted, int achievementsAcquired, int booksUnlocked, int gardenAreasUnlocked)
    {
        this.playerUsername = playerUsername;
        this.chaptersCompleted = chaptersCompleted;
        this.achievementsAcquired = achievementsAcquired;
        this.booksUnlocked = booksUnlocked;
        this.gardenAreasUnlocked = gardenAreasUnlocked;
    }
}