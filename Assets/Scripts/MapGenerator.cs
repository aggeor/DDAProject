using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour {

	public int width;
	public int height;
	private int wallThresholdSize;
	private int roomThresholdSize;
	public int lineDuration;
	public bool useRandomSeed;
	public string seed;

	[Range(0,100)]
	public int randomFillPercent;

	public int[,] map;

	public List<Room> survivingRooms;
	public List<Coord> survivingPassageCoords;
	public Canvas canv;
	public string mapLayout;

	public int minSpikes,maxSpikes;
	public float randomBoolValueS;

	public int minWalker,maxWalker;
	public float randomBoolValueW;

	public int minFlyerX,maxFlyerX,minFlyerY,maxFlyerY;
	public float randomBoolValueF;

	public int minJumper,maxJumper;
	public float randomBoolValueJ;

	public int minDropper,maxDropper;
	public float randomBoolValueD;

	public int minCoin,maxCoin;
	public float randomBoolValueC;

	void Awake () {
		//Debug.LogError("MapGen Awake started");
		AssignValues();
		Debug.LogError ("Layout:" + mapLayout + "-width:" + width + "-height:" + height);

		GenerateMap ();
		SmoothCells ();
		//Debug.LogError("MapGen Awake finished");

	}

	void SmoothCells(){
		//Smooth cells
		for (int i = 1; i < width-1; i++) {
			for (int j = 1; j < height-1; j++) {
				if (map [i, j] == 1) {

					if (map [i-1, j] == 1 && map [i+1, j] != 1  && map [i, j-1] != 1 && map [i, j+1] != 1) {
						map [i, j] = 0;
					}
					if (map [i-1, j] != 1 && map [i+1, j] == 1  && map [i, j-1] != 1 && map [i, j+1] != 1) {
						map [i, j] = 0;
					}
					if (map [i-1, j] != 1 && map [i+1, j] != 1  && map [i, j-1] != 1 && map [i, j+1] == 1) {
						map [i, j] = 0;

					}
				}
			}
		}
	}
	
	void AssignValues(){
		GameManager gm = FindObjectOfType<GameManager> ();
		gm.DynamicDifficultyAdjustment ();
		width=gm.width;
		height=gm.height;
		randomFillPercent = gm.randomFillPercent;
		mapLayout = gm.mapLayout;
		minSpikes = gm.minSpikes;
		maxSpikes = gm.maxSpikes;
		minWalker = gm.minWalker;
		maxWalker = gm.maxWalker;
		minFlyerX = gm.minFlyerX;
		maxFlyerX = gm.maxFlyerX;
		minFlyerY = gm.minFlyerY;
		maxFlyerY = gm.maxFlyerY;
		minJumper = gm.minJumper;
		maxJumper = gm.maxJumper;
		minDropper = gm.minDropper;
		maxDropper = gm.maxDropper;
		minCoin = gm.minCoin;
		maxCoin = gm.maxCoin;

		randomBoolValueS=gm.randomBoolValueS;

		randomBoolValueW = gm.randomBoolValueW;

		randomBoolValueF=gm.randomBoolValueF;

		randomBoolValueJ=gm.randomBoolValueJ;

		randomBoolValueD=gm.randomBoolValueD;

		randomBoolValueC=gm.randomBoolValueC;
	
	}
	

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene(1);
		}
	}


	public void GenerateMap(){
		map = new int[width, height];
		RandomFillMap ();
		for (int i = 0; i < 5; i++) {
			SmoothMap ();
		}
		survivingRooms=ProcessMap();

	}

	void RandomFillMap(){
		if (useRandomSeed) {
			seed = System.DateTime.Now.Ticks.ToString ();
		}

		System.Random pseudoRandom = new System.Random (seed.GetHashCode ());

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if(x==0||x==width-1||y==0||y==height-1){
					map[x,y]=1;
				}
				else{
					map[x,y]=(pseudoRandom.Next(0,100)<randomFillPercent)?1:0;
				}
			}
		}
	}
	void SmoothMap(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int neighbourWallTiles = GetSurroundingWallCount (x, y);

				if (neighbourWallTiles > 4)
					map [x, y] = 1;
				else if (neighbourWallTiles < 4)
					map [x, y] = 0;
			}
		}

	}
	public List<Room> ProcessMap(){
		List<Room> survivingRooms = new List<Room> ();
		survivingPassageCoords = new List<Coord> ();
		List<List<Coord>> wallRegions = GetRegions (1);
		wallThresholdSize = 12;
		foreach (List<Coord> wallRegion in wallRegions) {
			if (wallRegion.Count < wallThresholdSize) {
				foreach (Coord tile in wallRegion) {
					map[tile.tileX,tile.tileY] = 0;
				}
			}
		}
		roomThresholdSize = 20;
		List<List<Coord>> roomRegions = GetRegions (0);
		foreach (List<Coord> roomRegion in roomRegions) {
			if (roomRegion.Count < roomThresholdSize) {
				foreach (Coord tile in roomRegion) {
					map [tile.tileX, tile.tileY] = 1;
				}
			} else {
				survivingRooms.Add (new Room (roomRegion, map));
			}
		}
		survivingRooms.Sort ();
		survivingRooms [0].isMainroom = true;
		survivingRooms [0].isAccessibleFromMainRoom = true;
		ConnectClosestRooms (survivingRooms);

		return survivingRooms;

	}


	void ConnectClosestRooms(List<Room> allRooms,bool forceAccessibilityFromMainRoom=false){

		List<Room> roomListA = new List<Room> ();
		List<Room> roomListB = new List<Room> ();
		if (forceAccessibilityFromMainRoom) {
			foreach (Room room in allRooms) {
				if (room.isAccessibleFromMainRoom) {
					roomListB.Add (room);

				} else {
					roomListA.Add (room);
				}
			}
		} else {
			roomListA = allRooms;
			roomListB = allRooms;
		}
		int bestDistance = 0;
		Coord bestTileA = new Coord ();
		Coord bestTileB = new Coord ();
		Room bestRoomA = new Room ();
		Room bestRoomB = new Room ();
		bool possibleConnectionFound = false;

		foreach (Room roomA in roomListA) {
			if (!forceAccessibilityFromMainRoom) {
				possibleConnectionFound = false;
				if (roomA.connectedRooms.Count > 0) {
					continue;
				}
			}

			foreach (Room roomB in roomListB) {
				if (roomA == roomB||roomA.IsConnected(roomB)) {
					continue;
				}

				for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++) {
					for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++) {
						Coord tileA = roomA.edgeTiles [tileIndexA];
						Coord tileB = roomB.edgeTiles [tileIndexB];
						int distanceBetweenRooms =(int)( Mathf.Pow (tileA.tileX - tileB.tileX, 2) + Mathf.Pow (tileA.tileY - tileB.tileY, 2));
						if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
							bestDistance = distanceBetweenRooms;
							possibleConnectionFound = true;
							bestTileA = tileA;
							bestTileB = tileB;
							bestRoomA = roomA;
							bestRoomB = roomB;
						}
					}
				}
			}
			if (possibleConnectionFound&&!forceAccessibilityFromMainRoom) {
				CreatePassage (bestRoomA, bestRoomB, bestTileA, bestTileB);
			}
		}
		if (possibleConnectionFound&&forceAccessibilityFromMainRoom) {
			CreatePassage (bestRoomA, bestRoomB, bestTileA, bestTileB);
			ConnectClosestRooms (allRooms, true);
		}
		if (!forceAccessibilityFromMainRoom) {
			ConnectClosestRooms (allRooms, true);
		}
	}
	void CreatePassage(Room roomA,Room roomB,Coord tileA,Coord tileB){
		Room.ConnectRooms (roomA, roomB);


		List<Coord> line = GetLine (tileA, tileB);


		foreach(Coord c in line){
			DrawCircle(c,2);
		}
	}

	void DrawCircle(Coord c,int r){
		for (int x = -r; x <= r; x++) {
			for (int y = -r; y <= r; y++) {
				if (x * x + y * y <= r * r) {
					int drawX = c.tileX + x;
					int drawY = c.tileY + y;
					if (IsInMapRange (drawX, drawY)) {
						map [drawX, drawY] = 0;
						survivingPassageCoords.Add (new Coord(drawX,drawY));
					}
				}
			}
		}
	}

	List<Coord> GetLine(Coord from,Coord to){
		List<Coord> line = new List<Coord> ();

		int x = from.tileX;
		int y = from.tileY;

		int dx = to.tileX - from.tileX;
		int dy = to.tileY - from.tileY;

		bool inverted = false;
		int step = Math.Sign (dx);
		int gradientStep = Math.Sign (dy);

		int longest = Mathf.Abs (dx);
		int shortest = Mathf.Abs (dy);

		if (longest < shortest) {
			inverted = true;
			longest = Mathf.Abs (dy);
			shortest = Mathf.Abs (dx);

			step = Math.Sign (dy);
			gradientStep = Math.Sign (dx);
		}

		int gradientAccumulation = longest / 2;
		for (int i = 0; i < longest; i++) {
			line.Add (new Coord (x, y));

			if (inverted) {
				y += step;
			} else {
				x += step;
			}
			gradientAccumulation += shortest;
			if (gradientAccumulation >= longest) {
				if (inverted) {
					x += gradientStep;
				} else {
					y += gradientStep;
				}
				gradientAccumulation -= longest;
			}
		}
		return line;
	}
	Vector3 CoordToWorldPoint(Coord tile){
		
		return new Vector3 (tile.tileX,tile.tileY ,2 );
	}
	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[width,height];

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);

					foreach (Coord tile in newRegion) {
						mapFlags[tile.tileX, tile.tileY] = 1;
					}
				}
			}
		}

		return regions;
	}
	public List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[width,height];
		int tileType = map [startX, startY];

		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);

			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
						}
					}
				}
			}
		}

		return tiles;
	}
	public bool IsInMapRange(int x, int y) {
		return x >= 0 && x < width && y >= 0 && y < height;
	}
	int GetSurroundingWallCount(int gridX,int gridY){
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
				if (IsInMapRange(neighbourX,neighbourY)) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}
		return wallCount;
	}
	public struct Coord {
		public int tileX;
		public int tileY;

		public Coord(int x, int y) {
			tileX = x;
			tileY = y;
		}
	}
	public class Room : IComparable<Room>{
		public List<Coord> tiles;
		public List<Coord> edgeTiles;
		public List<Room> connectedRooms;
		public int roomSize;
		public bool isAccessibleFromMainRoom;
		public bool isMainroom;

		public Room(){
			
		}

		public Room(List<Coord> roomTiles,int[,] map){
			tiles=roomTiles;
			roomSize=tiles.Count;
			connectedRooms=new List<Room>();

			edgeTiles=new List<Coord>();
			foreach(Coord tile in tiles){
				for(int x=tile.tileX-1;x<=tile.tileX+1;x++){
					for(int y=tile.tileY-1;y<=tile.tileY+1;y++){
						if(x==tile.tileX||y==tile.tileY){
							if(map[x,y]==1){
								edgeTiles.Add(tile);
							}
						}
					}
				}
			}
		}
		public void SetAccessibleFromMainRoom(){
			if (!isAccessibleFromMainRoom) {
				isAccessibleFromMainRoom = true;
				foreach (Room connectedRoom in connectedRooms) {
					connectedRoom.SetAccessibleFromMainRoom ();
				}
			}
		}

		public static void ConnectRooms(Room roomA,Room roomB){
			if (roomA.isAccessibleFromMainRoom) {
				roomB.SetAccessibleFromMainRoom ();
			} else if (roomB.isAccessibleFromMainRoom) {
				roomA.SetAccessibleFromMainRoom ();
			}

			roomA.connectedRooms.Add (roomB);
			roomB.connectedRooms.Add (roomA);
		}
		public bool IsConnected(Room otherRoom){
			return connectedRooms.Contains (otherRoom);
		}
		public int CompareTo(Room otherRoom){
			return otherRoom.roomSize.CompareTo (roomSize);
		}
		public List<Coord> ReturnTiles(){
			return tiles;
		}

	}
	 

}
