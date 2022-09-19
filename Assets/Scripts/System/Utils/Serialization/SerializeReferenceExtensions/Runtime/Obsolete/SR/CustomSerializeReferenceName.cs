using System;

public class CustomSerializeReferenceName : Attribute
{
    public string Name { get; }
    
    public CustomSerializeReferenceName(string name)
    {
        Name = name;
    }
}