using System;

public class ResourceObjectNotFound : Exception
{
    private static string messagePrefix = "Can't find resource with name : ";

    public ResourceObjectNotFound(string resourceName)
        : base(messagePrefix + resourceName) {}

    public ResourceObjectNotFound(string resourceName, Exception inner)
        : base(messagePrefix + resourceName, inner) {}
}
