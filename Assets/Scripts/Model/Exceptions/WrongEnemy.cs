using System;

public class WrongEnemy : Exception
{
    public WrongEnemy(string message)
        : base(message) { }

    public WrongEnemy(string message, Exception inner)
        : base(message, inner) { }
}   
