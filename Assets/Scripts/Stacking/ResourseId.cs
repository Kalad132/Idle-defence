using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourseId 
{
    public int Value { get; private set; }

    public ResourseId(int value)
    {
        Value = value;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(ResourseId other)
    {
        if (Value == 0)
            return true;
        if (other.Value == 0)
            return true;
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
