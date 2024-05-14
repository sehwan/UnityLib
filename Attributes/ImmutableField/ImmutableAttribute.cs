using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class ImmutableAttribute : PropertyAttribute
{
    public bool isEnabled = true;

    public ImmutableAttribute()
    {
        this.isEnabled = true;
    }

    public ImmutableAttribute(bool isLocked)
    {
        this.isEnabled = isLocked;
    }
}