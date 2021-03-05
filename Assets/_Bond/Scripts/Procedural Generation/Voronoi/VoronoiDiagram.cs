using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiDiagram : MonoBehaviour
{
	public Vector2Int imageDim;
	public int coarseRegionAmount;
	public int fineRegionAmount;

	public bool drawByDistance = false;

	public int increment;
	public ConnectionPoints connectionPoints;

	public Dictionary<Vector2Int, Cell> visitedCells;

	public Terrain terrain;





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
		
		Cell newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
		while(newCell == startingCell)
		{
			startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
			newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
		}
		return newCell;
	}

	// void PaintLine(Cell[] cells, Cell startingCell, Cell endingCell, int _increment)
	// {
	// 	Vector2 startPos = startingCell.center;
	// 	startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
	// 	Cell newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
	// 	while(newCell != endingCell)
	// 	{
	// 		startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
	// 		newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
	// 		newCell.biome = Biome.CORRUPTION;
	// 	}
	// }

	public void connectCells(Cell[] cells)
	{
		foreach(Vector2Pair v in connectionPoints.points)
		{
			Cell newCell = cells[GetClosestCellIndex((int) v.start.x, (int)v.start.y, cells)];
			newCell.biome = v.biome;
			//visitedCells.Add(newCell.center, newCell);
			Cell endCell = cells[GetClosestCellIndex((int) v.end.x, (int)v.end.y, cells)];
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

	void updateTerrain(Texture2D _texture)
	{
		TerrainData terrainData = terrain.terrainData;

		terrainData.treeInstances = new TreeInstance[terrainData.detailWidth * terrainData.detailHeight];
		float[,] heights = terrainData.GetHeights(0, 0, 0, 0);
        terrainData.SetHeights(0, 0, heights);
		terrain.Flush();

		float[, ,] alphaMapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
		var detailMapData = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 0);
		for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
				float y_01 = (float)y/(float)terrainData.alphamapHeight;
                float x_01 = (float)x/(float)terrainData.alphamapWidth;
				if(_texture.GetPixel(x,y) == Color.black)
				{
					alphaMapData[x,y,2] = 1;
					if(Random.Range(0,5) == 1){
                        TreeInstance treeTemp = new TreeInstance();
                        Vector3 position = new Vector3(x , 0, y);
                        float newX = linearMap(x, 0, terrainData.detailWidth, 0, 1);
                        float newY = linearMap(y, 0, terrainData.detailHeight, 0, 1);
                        treeTemp.position = new Vector3(newY,0,newX);
                        treeTemp.prototypeIndex = 0;
                        treeTemp.widthScale = 1f;
                        treeTemp.heightScale = 1f;
                        treeTemp.color = Color.white;
                        treeTemp.lightmapColor = Color.white;
                        terrain.AddTreeInstance(treeTemp);
                        terrain.Flush();
                    }

				} 
				else if(_texture.GetPixel(x,y) == Color.green)
				{
					alphaMapData[x,y,0] = 1;
				}
				else if(_texture.GetPixel(x,y) == Color.blue)
				{
					alphaMapData[x,y,1] = 1;
				}
			}
		}
		terrainData.SetAlphamaps(0, 0, alphaMapData);
		terrainData.SetDetailLayer(0, 0, 0, detailMapData);

	}

	Texture2D GetDiagram()
	{
		Cell[] coarseCells = new Cell[coarseRegionAmount];
		Cell[] fineCells = new Cell[fineRegionAmount];


		for(int i = 0; i < coarseRegionAmount; i++)
		{
			coarseCells[i] = new Cell();
			coarseCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			
		}

		for(int i = 0; i < fineRegionAmount; i++)
		{
			fineCells[i] = new Cell();
			fineCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
		}
		
		
		// relax(coarseCells);
		// relax(fineCells);

	
		connectCells(coarseCells);

		for(int i = 0; i < fineCells.Length; i++)
		{
			//every fine cell gets the biome of its closest coarse cell
			fineCells[i].biome = coarseCells[GetClosestCellIndex((int)fineCells[i].center.x, (int)fineCells[i].center.y, coarseCells)].biome; 
		}

		Color[] pixelColors = new Color[imageDim.x * imageDim.y];
		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				int index = x * imageDim.x + y;
				
				Biome b = fineCells[GetClosestCellIndex(x, y, fineCells)].biome;
				pixelColors[index] = GetBiomeColor(b);
			}
		}

		Texture2D tex = GetImageFromColorArray(pixelColors);
		//updateTerrain(tex);
		return tex;
	}

	Color GetBiomeColor(Biome b) 
	{
		switch(b)
		{
			case Biome.EMPTY :
				return Color.black;
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
		Cell[] cells = new Cell[coarseRegionAmount];
		
		for (int i = 0; i < coarseRegionAmount; i++)
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
				distances[index] = Vector2.Distance(new Vector2Int(x,y), cells[GetClosestCellIndex(x, y, cells)].center);
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

	int GetClosestCellIndex(int x, int y, Cell[] cells)
	{
		float smallestDst = float.MaxValue;
		int index = 0;
		Vector2 coords = new Vector2(x, y);
		for(int i = 0; i < cells.Length; i++)
		{// Mathf.Sqrt(Mathf.Pow(coords.x - cells[i].center.x,2) + Mathf.Pow(coords.y - cells[i].center.y,2));
			if (Vector2.Distance(coords, cells[i].center) < smallestDst)
			{
				smallestDst = Vector2.Distance(coords, cells[i].center);
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


	public float linearMap(float value, float inputLow, float inputHigh, float outputLow, float outputHigh) 
    {
        return outputLow + (outputHigh - outputLow) * (value - inputLow) / (inputHigh - inputLow);
    }

	public void relax(Cell[] cells)
	{
		Vector2Int[] contributions = new Vector2Int[cells.Length];
		int[] hits = new int[cells.Length];
		Vector2Int V2here = new Vector2Int();
		// int hereX;
		// int hereY;
		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				V2here.x = x;
				V2here.y = y;

				int index = GetClosestCellIndex(V2here.x, V2here.y, cells);
				contributions[index] += V2here;
				hits[index]++;
				
			}
		}

		for(int i = 0; i < cells.Length; i++)
		{
			if(hits[i] > 0)
				cells[i].center = contributions[i] / hits[i];
		}
	}

}



public class Cell 
{
	public Vector2 center;
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

