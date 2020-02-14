using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity
{
    public float Vx;
    public float Vy;
    public float Vz;

    public Velocity(float Vx, float Vy)
    {
        this.Vx = Vx;
        this.Vy = Vy;
    }
    public Velocity(float Vx, float Vy, float Vz)
    {
        this.Vx = Vx;
        this.Vy = Vy;
        this.Vz = Vz;
    }
}
