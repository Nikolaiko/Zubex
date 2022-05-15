using System;

public class NotEnougthObjects : Exception
{
    private static string messagePrefix = "Not enougth objects in group : ";

    public NotEnougthObjects(string resourceName)
        : base(messagePrefix + resourceName) { }

    public NotEnougthObjects(string resourceName, Exception inner)
        : base(messagePrefix + resourceName, inner) { }
}
