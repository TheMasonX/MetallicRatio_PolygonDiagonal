
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MetallicRatio
{
	public static double GetRatio (int n)
	{
		return (n + Math.Sqrt(n * n + 4.0)) / 2.0;
	}
}
