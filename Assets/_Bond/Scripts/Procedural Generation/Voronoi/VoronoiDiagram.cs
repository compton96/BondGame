using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiDiagram : MonoBehaviour
{
	[Header("Set-up")]
	public int gameSeed;
	public Vector2Int imageDim;
	public int coarseRegionAmount;
	public int fineRegionAmount;
	public int adjacencyCount;
	public bool drawByDistance = false;
	public int increment;
	public ConnectionPoints connectionPoints;
	public Terrain terrain;
	public GameObject EncounterPosFinderObj;
	
	private EncounterPositionFinder encounterPositionFinder;

	List<Cell> coarseVisitedCells = new List<Cell>(); 
	List<Cell> fineVisitedCells = new List<Cell>();
	List<Cell> borderCells = new List<Cell>();
	
	[Header("Terrain Debugging Colors")]
	public Color32 forestColor;
	public Color32 forestBorderColor;
	public Color32 marshColor;
	public Color32 marshBorderColor;
	public Color32 meadowsColor;
	public Color32 meadowsBorderColor;
	public Color32 corruptionColor;
	public Color32 corruptionBorderColor;

	[Header("Stuff To Spawn")]
	public GameObject playerSpawnPoint;
	public GameObject levelExit;
	
	public int numberOfCombat;
	public List<Encounter> combatEncounters = new List<Encounter>();

	public int numberOfCreature;
	public GameObject FragariaEncounter;
	public GameObject AquaphimEncounter;
	//public GameObject SherifEncounter;
	//public GameObject PunchySnailEncounter;

	public BiomeObjects meadowsObjects;
	public BiomeObjects forestObjects;
	public BiomeObjects marshObjects;
	public BiomeObjects corruptionObjects;

	private float timerStart;
	private float timerEnd;

	public GameObject Parent;

	private void Start()
	{
		// GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), new Rect(0, 0, imageDim.x, imageDim.y), Vector2.one * 0.5f);
		// fineVisitedCells = new List<Cell>();
		// borderCells = new List<Cell>();
	}

	[ContextMenu("Run Code")]
	private void Run()
	{
		fineVisitedCells = new List<Cell>();
		borderCells = new List<Cell>();
		encounterPositionFinder = EncounterPosFinderObj.GetComponent<EncounterPositionFinder>();
		GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), new Rect(0, 0, imageDim.x, imageDim.y), Vector2.one * 0.5f);
		
	}
	
	//Draws between the start and end cell to get next cell on path
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

	//Gets connection points and uses the FindNextCell function to draw lines between them
	public void connectCells(Cell[] cells)
	{
		foreach(Vector2Pair v in connectionPoints.points)
		{
			Cell newCell = cells[GetClosestCellIndex((int) v.start.x, (int)v.start.y, cells)];
			newCell.biome = v.biome;
			coarseVisitedCells.Add(newCell);
			Cell endCell = cells[GetClosestCellIndex((int) v.end.x, (int)v.end.y, cells)];
			endCell.biome = v.biome;
			coarseVisitedCells.Add(newCell);
			while(newCell != endCell) 
			{
				newCell = FindNextCell(cells, newCell, endCell, increment);
				newCell.biome = v.biome;
				coarseVisitedCells.Add(newCell);
			}
		}
	}

	void updateTerrain(Texture2D _texture)
	{
		TerrainData terrainData = terrain.terrainData;

		terrainData.treeInstances = new TreeInstance[terrainData.detailWidth * terrainData.detailHeight];
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        
		terrain.Flush();
		float[, ,] alphaMapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
		var detailMapData = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 0);
		Color32[] pixels = _texture.GetPixels32();
		for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
				heights[x,y] = 0f;
				float y_01 = (float)y/(float)terrainData.alphamapHeight;
                float x_01 = (float)x/(float)terrainData.alphamapWidth;
				Color32 pixelColor = pixels[y*512 + x];

				//Place trees on borders of biomes
				if(pixelColor.Equals(forestBorderColor) ||
				   pixelColor.Equals(meadowsBorderColor) ||
				   pixelColor.Equals(marshBorderColor) ||
				   pixelColor.Equals(corruptionBorderColor)
				)
				{
					alphaMapData[x,y,2] = 1;
					heights[x,y] = 0.1f;
					// if(Random.Range(0,5) == 1){
                    //     TreeInstance treeTemp = new TreeInstance();
                    //     Vector3 position = new Vector3(x , 0, y);
                    //     float newX = linearMap(x, 0, terrainData.detailWidth, 0, 1);
                    //     float newY = linearMap(y, 0, terrainData.detailHeight, 0, 1);
                    //     treeTemp.position = new Vector3(newY,0,newX);
                    //     treeTemp.prototypeIndex = 0;
                    //     treeTemp.widthScale = 1f;
                    //     treeTemp.heightScale = 1f;
                    //     treeTemp.color = Color.white;
                    //     treeTemp.lightmapColor = Color.white;
                    //     terrain.AddTreeInstance(treeTemp);
                    //     terrain.Flush();
                    // }
				} 
				else if(pixelColor.Equals(forestColor))
				{
					alphaMapData[x,y,0] = 1;

				} else if(pixelColor.Equals(corruptionColor))
				{
					alphaMapData[x,y,4] = 1;
				} else if(pixelColor.Equals(marshColor))
				{
					alphaMapData[x,y,1] = 1;
				} 
				else if(pixelColor.Equals(meadowsColor))
				{
					alphaMapData[x,y,3] = 1;
				}
				else 
				{
					heights[x,y] = 0.1f;
				}
			}
		}
		terrainData.SetHeights(0, 0, heights);
		terrainData.SetAlphamaps(0, 0, alphaMapData);
		terrainData.SetDetailLayer(0, 0, 0, detailMapData);

		//loop through cells, using pixels, calculate distance to center, use that as probability to place grass

	}

	//Places spawnPoint, exit, creatures, and enemies
	void PlaceEncounters()
	{
		if(Parent != null)
		{
			//Destroy(Parent);
		}
		GameObject _parent = new GameObject();
		Parent = _parent;

		// //place player spawn point and level exit
		var spawnPoint = Instantiate(playerSpawnPoint, new Vector3(coarseVisitedCells[0].center.x, 0, coarseVisitedCells[0].center.y), Quaternion.identity, Parent.transform);
		var exit = Instantiate(levelExit, new Vector3(coarseVisitedCells[coarseVisitedCells.Count-1].center.x, 0, coarseVisitedCells[0].center.y), Quaternion.identity, Parent.transform);
		coarseVisitedCells.RemoveAt(0);
		coarseVisitedCells.RemoveAt(coarseVisitedCells.Count-1);


		List<Vector2> possibleEncounterPositions = encounterPositionFinder.GetPoints(new Vector3(25,37.5f,25), 512, 2);
		Debug.Log(possibleEncounterPositions.Count);
		//place random encounters on centerpoints of coarse cells
		coarseVisitedCells.Sort((x,y)=> x.size.CompareTo(y.size));
		List<Cell> encounterCells = new List<Cell>(coarseVisitedCells);

		int numOfEncounterIndicators = 10;
		List<GameObject> placedCombatEncounters = new List<GameObject>();
		bool doContinue = false;
		//Loop to place combat encounters
		for(int i = 0; i < numberOfCombat; i++)
		{
			int encounterPositionsIndex = Random.Range(0, possibleEncounterPositions.Count);
			Vector2 randomPos = possibleEncounterPositions[encounterPositionsIndex];

			//Randomly choose a combat encounter
			int encounterIndex = Random.Range(0, combatEncounters.Count);
			doContinue = false;
			//Loop through all placed encounters and make sure the new one is minimum distance away from them all
			foreach(GameObject e in placedCombatEncounters){
				//If chosen cell is too close to another encounter, remove it from the possible encounter cells
				if(Vector3.Distance(e.transform.position, new Vector3(randomPos.x, 0, randomPos.y)) < 50)
				{
					possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
					doContinue = true;
					break;
				}
			}
			if(doContinue)
			{
				continue;
			}
			//Cell is good to use, place encounter and add it to the list of encounters
			GameObject tempEncounter = Instantiate(
				combatEncounters[encounterIndex].encounter, 
				new Vector3(randomPos.x, 0, randomPos.y), 
				Quaternion.identity, Parent.transform
			);
			placedCombatEncounters.Add(tempEncounter);

			//Place indicators for the new encounter randomly in a circle
			for(int j = 0; j < numOfEncounterIndicators; j++)
			{
				Vector2 randomUnitCirclePoint = Random.insideUnitCircle;
				randomUnitCirclePoint *= Random.Range(40,70);
				Vector3 pos = new Vector3(randomUnitCirclePoint.x + randomPos.x, 0, randomUnitCirclePoint.y + randomPos.y);
				Instantiate(combatEncounters[encounterIndex].indicators[Random.Range(0,combatEncounters[encounterIndex].indicators.Count)], pos, Quaternion.identity, tempEncounter.transform);
			}
			//Remove this cell from the list of options to avoid overlap
			possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
		}
	}

	//Generate the Voronoi Diagram, then the map based on it
	Texture2D GetDiagram()
	{
		Random.InitState(gameSeed); 
		timerStart = Time.realtimeSinceStartup;
		Cell[] coarseCells = new Cell[coarseRegionAmount];
		Cell[] fineCells = new Cell[fineRegionAmount];

		for(int i = 0; i < coarseRegionAmount; i++)
		{
			coarseCells[i] = new Cell();
			coarseCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			coarseCells[i].index = i;
		}

		for(int i = 0; i < fineRegionAmount; i++)
		{
			fineCells[i] = new Cell();
			fineCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			fineCells[i].index = i;
		}

		relax(coarseCells);
		// relax(fineCells);
	
		connectCells(coarseCells);

		for(int i = 0; i < fineCells.Length; i++)
		{
			//every fine cell gets the biome of its closest coarse cell
			Biome b = coarseCells[GetClosestCellIndex((int)fineCells[i].center.x, (int)fineCells[i].center.y, coarseCells)].biome;
			fineCells[i].biome = b; 
			if(b != Biome.EMPTY)
			{
				fineVisitedCells.Add(fineCells[i]);
			}
		}

		//Generate the borders for the fine cells
		foreach(Cell c in fineVisitedCells)
		{
			List<Cell> borderCells = GetClosestKCells(c.index, adjacencyCount, fineCells);
			//Debug.Log("Border Cells" + borderCells[0]);
			foreach(Cell b in borderCells)
			{
				if(b.biome == Biome.EMPTY)
				{
					switch(c.biome)
					{
						case Biome.FOREST:
							b.biome = Biome.FOREST_BORDER;
							break;
						case Biome.MEADOWS:
							b.biome = Biome.MEADOWS_BORDER;
							break;
						case Biome.MARSH:
							b.biome = Biome.MARSH_BORDER;
							break;
						case Biome.CORRUPTION:
							b.biome = Biome.CORRUPTION_BORDER;
							break;
						default:
							b.biome = Biome.CORRUPTION_BORDER;
							break;
					}
				}
			}
		}

		//Go through the map and set cell biomes, sizes, and pixels
		Color[] pixelColors = new Color[imageDim.x * imageDim.y];
		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				int index = x * imageDim.x + y;
				int fineIndex = GetClosestCellIndex(x, y, fineCells);
				Biome b = fineCells[fineIndex].biome;
				fineCells[fineIndex].size++;
				fineCells[fineIndex].pixels.Add(new Vector2(x,y));

				coarseCells[GetClosestCellIndex(x, y, coarseCells)].size++;
				pixelColors[index] = GetBiomeColor(b);
			}
		}
		
		//Draw Coarse Center points
		// foreach(Cell c in coarseCells)
		// {
		// 	int index = (int)c.center.x * imageDim.x + (int)c.center.y;
		// 	pixelColors[index] = Color.red;
		// }

		Texture2D tex = GetImageFromColorArray(pixelColors);
		updateTerrain(tex);
		PlaceEncounters();
		Debug.Log("Finished : " + (Time.realtimeSinceStartup - timerStart));
		return tex;
		
	}

	Color GetBiomeColor(Biome b) 
	{
		switch(b)
		{
			case Biome.EMPTY :
				return Color.black;
			case Biome.FOREST :
				return forestColor;
			case Biome.MARSH :
				return marshColor;
			case Biome.MEADOWS :
				return meadowsColor;
			case Biome.CORRUPTION :
				return corruptionColor;
			case Biome.CORRUPTION_BORDER :
				return corruptionBorderColor;
			case Biome.MEADOWS_BORDER :
				return meadowsBorderColor;
			case Biome.FOREST_BORDER :
				return forestBorderColor;
			case Biome.MARSH_BORDER :
				return marshBorderColor;
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
		{
			if (Vector2.Distance(coords, cells[i].center) < smallestDst)
			{
				smallestDst = Vector2.Distance(coords, cells[i].center);
				index = i;
			}
		}
		return index;
	}

	List<Cell> GetClosestKCells(int myIndex, int k, Cell[] cells)
	{
		List<Cell> kClosestCells = new List<Cell>(k);

		Vector2 coords = new Vector2(cells[myIndex].center.x, cells[myIndex].center.y);
		for(int i = 0; i < cells.Length; i++)
		{
			if(myIndex != i)
			{
				float iDistance = Vector2.Distance(coords, cells[i].center);
				if(kClosestCells.Count < k)
				{	
					kClosestCells.Add(cells[i]);
					kClosestCells[kClosestCells.Count - 1].distance = iDistance;
					kClosestCells.Sort((x,y)=> x.distance.CompareTo(y.distance));
				}
				else 
				{
					if (iDistance < Vector2.Distance(coords, kClosestCells[kClosestCells.Count - 1].center))
					{
						
						kClosestCells[kClosestCells.Count - 1] = cells[i];
						kClosestCells[kClosestCells.Count - 1].distance = iDistance;
						kClosestCells.Sort((x,y)=> x.distance.CompareTo(y.distance));
					}
				}
			}
		}

		return kClosestCells;
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
	public int size = 0;
	public int index;
	public Vector2 center;
	public Biome biome = Biome.EMPTY;
	public float distance;
	public List<Vector2> pixels = new List<Vector2>();
}

public enum Biome 
{
	EMPTY,
	FOREST,
	FOREST_BORDER,
	MARSH,
	MARSH_BORDER,
	MEADOWS,
	MEADOWS_BORDER,
	CORRUPTION,
	CORRUPTION_BORDER,
}

