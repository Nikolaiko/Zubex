using System;

public class GameState : System.Object
{
    private int levelNumber = 0;

    public int getLevelNumber() {
        return levelNumber;
    }

    public void setLevelNumber(int value) {
        levelNumber = value;
    }
}
