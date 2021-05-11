using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Structure
{
    public bool moving;
    public int value;
    public string name;

    public Structure(bool _moving, int _value, string _name)
    {
        moving = _moving;
        value = _value;
        name = _name;
    }
}
