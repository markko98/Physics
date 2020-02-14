using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position 
{
    public float X;
    public float Y;
    public float Z;

    public Position(float X, float Y)
    {
        this.X = X;
        this.Y = Y;
    }
    public Position(float X, float Y, float Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }
}
