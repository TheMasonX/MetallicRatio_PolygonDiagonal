using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PolygonDiagonal
{
	public static double GetInteriorAngle (int n)
	{
		return Math.PI - (2.0 * Math.PI) / (double)n;
	}

	public static double GetExteriorAngle (int n)
	{
		return (2.0 * Math.PI) / (double)n;
	}

	public static double GetFirstDiagonal (int n)
	{
		return 2.0 * Math.Sin(GetInteriorAngle(n) / 2.0);
	}
}
