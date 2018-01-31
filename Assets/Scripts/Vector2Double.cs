using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Vector2Double
{
	public double x;
	public double y;

	public static Vector2Double up { get { return new Vector2Double(0, 1); } }

	public Vector2Double (double x, double y)
	{
		this.x = x;
		this.y = y;
	}

	public static double Distance (Vector2Double a, Vector2Double b)
	{
		double x = (b.x - a.x);
		double y = (b.y - a.y);
		return Math.Sqrt(x * x + y * y);
	}
}

public static class Vector2DoubleExtensions
{
	public static Vector2Double Rotate (this Vector2Double v, double angle)
	{
		double sin = Math.Sin(angle);
		double cos = Math.Cos(angle);
		Vector2Double temp = new Vector2Double(v.x, v.y);
		temp.x = cos * v.x - sin * v.y;
		temp.y = sin * v.x + cos * v.y;
		return temp;
	}
}
