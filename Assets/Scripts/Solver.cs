using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Solver : MonoBehaviour
{
	public List<double> ratios;
	public List<PolygonData> polygons;

	public int ratioCount = 100;
	public int polygonCount = 20;
	public int startingPolygon = 4;

	public double closeThreshold = .05;

	[ContextMenu("Run")]
	public void DebugRun ()
	{
		startingPolygon = Mathf.Max(startingPolygon, 4);
		ratios = new List<double>(ratioCount);
		for (int rIndex = 0; rIndex < ratioCount; rIndex++)
		{
			ratios.Add(MetallicRatio.GetRatio(rIndex));
		}

		int pCount = Mathf.Max(1, polygonCount + 1 - startingPolygon);
		polygons = new List<PolygonData>(pCount);
		for (int pIndex = startingPolygon; pIndex <= polygonCount; pIndex++)
		{
			polygons.Add(new PolygonData(pIndex));
		}

		for (int pIndex = 0; pIndex < polygons.Count; pIndex++)
		{
			for (int dIndex = 0; dIndex < polygons[pIndex].diagonals.Count; dIndex++)
			{
				double val = polygons[pIndex].diagonals[dIndex];
				for (int rIndex = 0; rIndex < ratios.Count; rIndex++)
				{
					double error = ratios[rIndex] - val;
					if(Math.Abs(error) < closeThreshold)
					{
						UnityEngine.Debug.LogFormat ("{0} Diagonal {1} Matched With Sigma {2} \n{3} Difference ({4}% Error)", polygons[pIndex].name, dIndex + 1, rIndex + 1, error.ToString("E5"), (error / val));
					}

					if(ratios[rIndex] > val * 1.1)
						break;


				}
			}
		}
	}
}
