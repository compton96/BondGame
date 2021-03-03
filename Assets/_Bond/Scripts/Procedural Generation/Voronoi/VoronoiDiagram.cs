using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiDiagram : MonoBehaviour
{
	public Vector2Int imageDim;
	public int regionAmount;
	public bool drawByDistance = false;
	public int increment;
	public ConnectionPoints connectionPoints;
	public Dictionary<Vector2Int, Cell> visitedCells;


	float currTime;
	private void Update() {
		currTime += Time.deltaTime;
	}


	private void Start()
	{
		GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), new Rect(0, 0, imageDim.x, imageDim.y), Vector2.one * 0.5f);
	}

	[ContextMenu("Run Code")]
	private void Run()
	{
		GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), new Rect(0, 0, imageDim.x, imageDim.y), Vector2.one * 0.5f);
	}
	
	Cell FindNextCell(Cell[] cells, Cell startingCell, Cell endingCell, int _increment)
	{
		Vector2 startPos = startingCell.center;
		startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
		Cell newCell = cells[GetClosestCellIndex(new Vector2Int((int)startPos.x, (int) startPos.y), cells)];
		while(newCell == startingCell)
		{
			startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
			newCell = cells[GetClosestCellIndex(new Vector2Int((int)startPos.x, (int) startPos.y), cells)];
		}
		return newCell;
	}

	void PaintLine(Cell[] cells, Cell startingCell, Cell endingCell, int _increment)
	{
		Vector2 startPos = startingCell.center;
		startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
		Cell newCell = cells[GetClosestCellIndex(new Vector2Int((int)startPos.x, (int) startPos.y), cells)];
		while(newCell != endingCell)
		{
			startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
			newCell = cells[GetClosestCellIndex(new Vector2Int((int)startPos.x, (int) startPos.y), cells)];
			newCell.biome = Biome.CORRUPTION;
		}
	}

	public void connectCells(Cell[] cells)
	{
		foreach(Vector2Pair v in connectionPoints.points)
		{
			Cell newCell = cells[GetClosestCellIndex(new Vector2Int((int) v.start.x, (int)v.start.y), cells)];
			newCell.biome = v.biome;
			//visitedCells.Add(newCell.center, newCell);
			Cell endCell = cells[GetClosestCellIndex(new Vector2Int((int) v.end.x, (int)v.end.y), cells)];
			endCell.biome = v.biome;
			//visitedCells.Add(endCell.center, endCell);
			while(newCell != endCell) 
			{
				newCell = FindNextCell(cells, newCell, endCell, increment);
				newCell.biome = v.biome;
				//visitedCells.Add(newCell.center, newCell);
			}
		}
	}

	Texture2D GetDiagram()
	{
		Cell[] cells = new Cell[regionAmount];
		Color[] regions = new Color[regionAmount];
		for(int i = 0; i < regionAmount; i++)
		{
			cells[i] = new Cell();
			cells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			regions[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		}
		connectCells(cells);

		Color[] pixelColors = new Color[imageDim.x * imageDim.y];
		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				int index = x * imageDim.x + y;
				
				Biome b = cells[GetClosestCellIndex(new Vector2Int(x, y), cells)].biome;
				pixelColors[index] = GetBiomeColor(b);
			}
		}
		return GetImageFromColorArray(pixelColors);
	}

	Color GetBiomeColor(Biome b) 
	{
		switch(b)
		{
			case Biome.EMPTY :
				return Color.black;;
			case Biome.FOREST :
				return Color.green;
			case Biome.SWAMP :
				return Color.gray;
			case Biome.WETLANDS :
				return Color.blue;
			case Biome.CORRUPTION :
				return Color.magenta;
		}
		
		return Color.black;
	}

	Texture2D GetDiagramByDistance()
	{
		Cell[] cells = new Cell[regionAmount];
		
		for (int i = 0; i < regionAmount; i++)
		{
			cells[i] = new Cell();
			cells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
		}
		Color[] pixelColors = new Color[imageDim.x * imageDim.y];
		float[] distances = new float[imageDim.x * imageDim.y];


		float maxDst = float.MinValue;
		for (int x = 0; x < imageDim.x; x++)
		{
			for (int y = 0; y < imageDim.y; y++)
			{
				int index = x * imageDim.x + y;
				distances[index] = Vector2.Distance(new Vector2Int(x,y), cells[GetClosestCellIndex(new Vector2Int(x,y), cells)].center);
				if(distances[index] > maxDst)
				{
					maxDst = distances[index];
				}
			}	
		}

		for(int i = 0; i < distances.Length; i++)
		{
			float colorValue = distances[i] / maxDst;
			pixelColors[i] = new Color(colorValue, colorValue, colorValue, 1f);
		}
		return GetImageFromColorArray(pixelColors);
	}

	int GetClosestCellIndex(Vector2Int pixelPos, Cell[] cells)
	{
		float smallestDst = float.MaxValue;
		int index = 0;
		for(int i = 0; i < cells.Length; i++)
		{
			if (Vector2.Distance(pixelPos, cells[i].center) < smallestDst)
			{
				smallestDst = Vector2.Distance(pixelPos, cells[i].center);
				index = i;
			}
		}
		return index;
	}
	Texture2D GetImageFromColorArray(Color[] pixelColors)
	{
		Texture2D tex = new Texture2D(imageDim.x, imageDim.y);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(pixelColors);
		tex.Apply();
		return tex;
	}
}



public class Cell 
{
	public Vector2Int center;
	public Biome biome = Biome.EMPTY;
}

public enum Biome 
{
	EMPTY,
	FOREST,
	SWAMP,
	WETLANDS,
	CORRUPTION
}

