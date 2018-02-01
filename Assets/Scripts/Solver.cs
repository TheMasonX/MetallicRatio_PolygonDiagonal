using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class Solver : MonoBehaviour
{
	public List<double> ratios;
	public List<PolygonData> polygons;

	public int ratioCount = 100;
	public int polygonCount = 20;
	public int startingPolygon = 4;
	public double vectorScaling = 100.0;

	public double closeThreshold = .05;

	[HideInInspector]
	public string debugLog; 
	
	[ContextMenu("Run")]
	public void DebugRun ()
	{
		StartCoroutine(Run ());
	}

	public IEnumerator Run ()
	{
		debugLog = "";
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("Searching through sigma {0} and {1}-gon. Reporting threshold is {2}%.\n\n", ratioCount, polygonCount, closeThreshold);
		Debug.Log("Starting Search...");
		var dt = System.DateTime.Now;
		int total = 0;
		int hits = 0;

		startingPolygon = Mathf.Max(startingPolygon, 4);
		ratios = new List<double>(ratioCount);
		for (int rIndex = 0; rIndex < ratioCount; rIndex++)
		{
			ratios.Add(MetallicRatio.GetRatio(rIndex));
		}

		yield return null;

		int pCount = Mathf.Max(1, polygonCount + 1 - startingPolygon);
		polygons = new List<PolygonData>(pCount);
		for (int pIndex = startingPolygon; pIndex <= polygonCount; pIndex++)
		{
			polygons.Add(new PolygonData(pIndex, vectorScaling));
		}

		for (int pIndex = 0; pIndex < polygons.Count; pIndex++)
		{
			for (int dIndex = 0; dIndex < polygons[pIndex].diagonals.Count; dIndex++)
			{
				double val = polygons[pIndex].diagonals[dIndex];
				for (int rIndex = 0; rIndex < ratios.Count; rIndex++)
				{
					double error = ratios[rIndex] - val;
					double errorPercent = error / val * 100.0;
					if(Math.Abs(errorPercent) < closeThreshold)
					{
						yield return null;
						sb.AppendFormat("{0} Diagonal {1} Matched Sigma {2}", polygons[pIndex].name, dIndex + 1, rIndex + 1);
						sb.AppendFormat("\nDiagonal: {0} || Sigma: {1}\nPrecision Info -- Difference: {2} ({3}% Error)\n\n", val, ratios[rIndex], error.ToString("E5"), (errorPercent));
						hits++;
						if(hits % 50 == 0)
						{
							debugLog = sb.ToString();
							UnityEngine.Debug.Log(debugLog);
							sb.Remove(0, sb.Length);
							yield return null;
						}
					}

					total++;
					if(ratios[rIndex] > val * 1.1)
					{
						break;
					}


				}
			}
			yield return null;
		}

		sb.AppendFormat("Search took {0} seconds to search through {1} items. Discovered {2} possible relations", (System.DateTime.Now.Subtract(dt).TotalSeconds).ToString("E5"), total, hits);
		debugLog = sb.ToString();
		UnityEngine.Debug.Log(debugLog);
	}
}
