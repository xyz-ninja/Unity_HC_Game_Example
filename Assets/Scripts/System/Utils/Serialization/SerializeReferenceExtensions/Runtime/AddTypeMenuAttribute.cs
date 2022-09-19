using System;

/// <summary>
/// An attribute that overrides the type name and category displayed in the <see cref="SubclassSelectorAttribute"/> popup.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,
    AllowMultiple = false, Inherited = false)]
public sealed class AddTypeMenuAttribute : Attribute
{
    public string MenuName { get; }

    public int Order { get; }

    public AddTypeMenuAttribute(string menuName, int order = 0)
    {
        MenuName = menuName;
        Order = order;
    }

    private static readonly char[] Separeters = new char[] { '/' };

    /// <summary>
    /// Returns the menu name split by the '/' separator.
    /// </summary>
    public string[] GetSplitMenuName()
    {
        return string.IsNullOrWhiteSpace(MenuName) == false
            ? MenuName.Split(Separeters, StringSplitOptions.RemoveEmptyEntries)
            : Array.Empty<string>();
    }

    /// <summary>
    /// Returns the display name without the path.
    /// </summary>
    public string GetTypeNameWithoutPath()
    {
        var splitMenuName = GetSplitMenuName();
        return (splitMenuName.Length != 0) ? splitMenuName[splitMenuName.Length - 1] : null;
    }
}