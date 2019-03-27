﻿using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteraction : MonoBehaviour
{
    public static Hex SelectHexagon(Vector3 mousePosition, GridInfo grid)
    {
        float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        float r = (                                        2f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        Axial a = Hex_round(new Axial(q, r));
        Debug.Log(a.q + " " + a.r);

        return null;
    }

    static Axial Hex_round(Axial axial)
    {
        return cube_to_axial(cube_round(axial_to_cube(axial)));
    }

    static Axial cube_to_axial(Cube cube)
    {
        var q = cube.x;
        var r = cube.z;
        return new Axial(q, r);
    }

    static Cube axial_to_cube(Axial axial)
    {
        var x = axial.q;
        var y = axial.r;
        var z = -x - y;
        return new Cube(x, y, z);
    }

    static Cube cube_round(Cube cube)
    {
        var rx = Mathf.Round(cube.x);
        var ry = Mathf.Round(cube.y);
        var rz = Mathf.Round(cube.z);

        var x_diff = Mathf.Abs(rx - cube.x);
        var y_diff = Mathf.Abs(ry - cube.y);
        var z_diff = Mathf.Abs(rz - cube.z);

        if (x_diff > y_diff && x_diff > z_diff)
            rx = -ry - rz;
        else if (y_diff > z_diff)
            ry = -rx - rz;
        else
            rz = -rx - ry;

        return new Cube(rx, ry, rz);
    }
}

class Cube
{
    public float x;
    public float y;
    public float z;

    public Cube(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

class Axial
{
    public float q;
    public float r;

    public Axial(float q, float r)
    {
        this.q = q;
        this.r = r;
    }
}