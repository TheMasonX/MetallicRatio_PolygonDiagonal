using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PolygonData
{
	public string name;
	public int n;
	public int diagonalCount;
	public double exteriorAngle;
	public double sideLength;
	public List<Vector2Double> verts;
	public List<double> diagonals;

	public PolygonData (int n, double vectorScaling)
	{
		name = n.ToString() + "-gon";
		this.n = n;
		diagonalCount = Mathf.CeilToInt((n - 3.0f) / 2.0f);
		diagonals = new List<double>(diagonalCount);
		exteriorAngle = PolygonDiagonal.GetExteriorAngle(n);
		
//		verts = new List<Vector2Double>(n);
		verts = new List<Vector2Double>(diagonalCount + 2);
		verts.Add(new Vector2Double(0.0, vectorScaling));
		for (int vIndex = 1; vIndex < verts.Capacity; vIndex++)
		{
			Vector2Double newVector = verts[vIndex - 1].Rotate(exteriorAngle);
			verts.Add(newVector);
			if(vIndex == 1)
			{
				sideLength = Vector2Double.Distance(verts[0], newVector);
			}
			else if(vIndex > 1)
			{
				double dist = Vector2Double.Distance(verts[0], newVector) / sideLength / vectorScaling;
//				double dist = diagonals.Count + 1.0 + sideLength / Vector2Double.Distance(verts[0], newVector);
				diagonals.Add(dist);
			}
		}
	}
}
