using System;
using UnityEngine;

/// <summary>
/// Attribute to specify the type of the field serialized by the <see cref="SerializeReference"/> attribute in the inspector.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class SubclassSelectorAttribute : PropertyAttribute
{
}