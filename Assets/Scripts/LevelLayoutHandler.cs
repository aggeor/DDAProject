using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLayoutHandler : MonoBehaviour {

	public MapGenerator mapGen;
	public int checkpointRadius;
	public int previousCheckX,previousCheckY;
	public int startX,startY,goalX,goalY,checkX,checkY;
	public MapGenerator.Room startRoom;
	public MapGenerator.Room checkRoom;
	public MapGenerator.Room goalRoom;

	public float a,b,c;
	public List<MapGenerator.Coord> spikesPos;
	public List<MapGenerator.Coord> walkersPos;
	public List<MapGenerator.Coord> jumpersPos;
	public List<MapGenerator.Coord> flyersPos;
	public List<MapGenerator.Coord> droppersPos;
	public List<MapGenerator.Coord> coinsPos;
	public List<MapGenerator.Coord> checkpointsPos;

	void Awake () {
		//Debug.LogError("lvl layout handler awake started");
		spikesPos=new List<MapGenerator.Coord> ();
		walkersPos=new List<MapGenerator.Coord> ();
		jumpersPos=new List<MapGenerator.Coord> ();
		flyersPos=new List<MapGenerator.Coord> ();
		droppersPos=new List<MapGenerator.Coord> ();
		coinsPos = new List<MapGenerator.Coord> ();
		checkpointsPos=new List<MapGenerator.Coord> ();
		mapGen = gameObject.GetComponent<MapGenerator> ();
		List<MapGenerator.Room> survivingRooms=mapGen.survivingRooms;
		if(mapGen.mapLayout=="VeryEasy"){
			CreateCheckPoints_Linear (survivingRooms);
			CreateObjectPositions_Linear (survivingRooms);
		}
		if(mapGen.mapLayout=="Easy"){
			CreateCheckPoints_Linear (survivingRooms);
			CreateObjectPositions_Linear (survivingRooms);
		}
		if(mapGen.mapLayout=="Medium"){
			if(survivingRooms.Count!=1){
				CreateCheckPoints_Roombased (survivingRooms);
			}else{
				CreateCheckPoints_LinearVertical(survivingRooms);
			}
			CreateObjectPositions_Roombased (survivingRooms);
		}
		if(mapGen.mapLayout=="Hard"){
			CreateCheckPoints_Roombased (survivingRooms);
			CreateObjectPositions_Roombased (survivingRooms);
		}
		if(mapGen.mapLayout=="VeryHard"){
			CreateCheckPoints_Roombased (survivingRooms);
			CreateObjectPositions_Roombased (survivingRooms);
		}
		//Debug.LogError ( "Layout:"+mapGen.mapLayout+"-width:"+mapGen.width + "-height:"+mapGen.height);
		//Debug.LogError("lvl layout handler awake finished");
	}

	
	public void CreateCheckPoints_Linear(List<MapGenerator.Room> survivingRooms){
		for (int i=0;i<mapGen.width-1;i++) {
			for (int j = 0; j < mapGen.height-1; j++) {
				//Create start point
				if (mapGen.map [i, j] == 0 && mapGen.IsInMapRange(i,j)&&startX==0&&startY==0) {
					startX = i;
					startY = j;
					mapGen.map [i, j] = 2;
					CreateArea (i, j);
					i = mapGen.width / 2;
					break;
				}
				//Create check point
				if (mapGen.map [i, j] == 0 && mapGen.IsInMapRange(i,j)&&checkX==0&&checkY==0) {
					checkX = i;
					checkY = j;
					mapGen.map [i, j] = 9;
					CreateArea (i, j);
					break;
				}
				//Create goal
				if (mapGen.map [i, j] == 0 && mapGen.IsInMapRange(i,j)&& i>10*mapGen.width/11 &&goalX==0&&goalY==0) {
					goalX = i;
					goalY = j;
					mapGen.map [i, j] = 3;
					CreateArea (i, j);
					break;
				}

			}
			if (startX != 0&& checkX != 0 && goalX != 0 ) {
				break;
			}
		}
	}
	public void CreateCheckPoints_LinearVertical(List<MapGenerator.Room> survivingRooms){
		for (int i=0;i<mapGen.width-1;i++) {
			for (int j = mapGen.height-1; j >= 0; j--) {
				//Create start point
				if (mapGen.map [i, j] == 0 && mapGen.map[i,j-1]==1 && mapGen.IsInMapRange(i,j)&&j>3*mapGen.height/4&&startX==0&&startY==0) {
					startX = i;
					startY = j;
					mapGen.map [i, j] = 2;
					CreateArea (i, j);
					i = mapGen.width / 2;
					j = mapGen.height / 2;
					break;
				}
				//Create check point
				if (mapGen.map [i, j] == 0 && mapGen.map[i,j-1]==1 && mapGen.IsInMapRange(i,j)&&checkX==0&&checkY==0) {
					checkX = i;
					checkY = j;
					mapGen.map [i, j] = 9;
					CreateArea (i, j);
					break;
				}
				//Create goal
				if (mapGen.map [i, j] == 0 && mapGen.map[i,j-1]==1 && mapGen.IsInMapRange(i,j)&& j<mapGen.height/4 &&goalX==0&&goalY==0) {
					goalX = i;
					goalY = j;
					mapGen.map [i, j] = 3;
					CreateArea (i, j);
					break;
				}

			}
			if (startX != 0&& checkX != 0 && goalX != 0 ) {
				break;
			}
		}
	}

	public void CreateCheckPoints_Roombased(List<MapGenerator.Room> survivingRooms){

		startRoom=new MapGenerator.Room();
		goalRoom=new MapGenerator.Room();
		checkRoom = new MapGenerator.Room ();
		bool startC = false;
		bool goalC = false;
		int connRoomsCount;
		foreach (MapGenerator.Room room in survivingRooms) {
			connRoomsCount=room.connectedRooms.Count;
			//Create start point
			if (connRoomsCount == 1&&!startC) {
				startRoom = room;
				foreach (MapGenerator.Coord tile in startRoom.ReturnTiles()) {
					if (mapGen.map [tile.tileX, tile.tileY - 1] == 1 && mapGen.IsInMapRange (tile.tileX, tile.tileY)) {
						startX = tile.tileX;
						startY = tile.tileY;
						mapGen.map [tile.tileX, tile.tileY] = 2;
						CreateArea (tile.tileX, tile.tileY);
						startC = true;
						break;
					}
				}
			}
			//Create goal
			if (connRoomsCount == 1 && room != startRoom&&!goalC) {
				goalRoom = room;
				foreach (MapGenerator.Coord tile in goalRoom.ReturnTiles()) {
					if (mapGen.map [tile.tileX, tile.tileY - 1] == 1 && mapGen.IsInMapRange (tile.tileX, tile.tileY)) {
						goalX = tile.tileX;
						goalY = tile.tileY;
						mapGen.map [tile.tileX, tile.tileY] = 3;
						CreateArea (tile.tileX, tile.tileY);
						goalC = true;
						break;
					}
				}
			}
		}

		//Create checkpoints
		foreach (MapGenerator.Coord tile in mapGen.survivingPassageCoords) {
			if (previousCheckX==0&&previousCheckY==0) {
				previousCheckX = tile.tileX;
				previousCheckY = tile.tileY;
			}
			if(Mathf.Abs(tile.tileX-previousCheckX)>20&&Mathf.Abs(tile.tileY-previousCheckY)>20){
				if (mapGen.map [tile.tileX, tile.tileY - 1] == 1&&mapGen.map [tile.tileX, tile.tileY] == 0 
					&& mapGen.IsInMapRange (tile.tileX, tile.tileY)) {
					checkpointsPos.Add (tile);
					previousCheckX = tile.tileX;
					previousCheckY = tile.tileY;
					mapGen.map [tile.tileX, tile.tileY] = 9;
					CreateArea (tile.tileX, tile.tileY);
				}
			}
		}
	}

	public void CreateObjectPositions_Linear(List<MapGenerator.Room> survivingRooms){

		//SPIKES
		int rand = UnityEngine.Random.Range (mapGen.minSpikes, mapGen.maxSpikes);
		for(int i=mapGen.minSpikes;i<mapGen.width-mapGen.maxSpikes;i+=rand){	
			for (int j=1;j<mapGen.height-1;j++) {
				if (mapGen.map [i, j - 1] == 1 && mapGen.map [i, j] == 0 && mapGen.IsInMapRange (i, j)) {
					mapGen.map [i, j] = 4;
					spikesPos.Add (new MapGenerator.Coord(i,j));
					rand = UnityEngine.Random.Range (mapGen.minSpikes, mapGen.maxSpikes);


				}

			
				if (i > mapGen.width - mapGen.maxSpikes) {
					break;
				}
			}
			if (i > mapGen.width - mapGen.maxSpikes) {
				break;
			}
		}
		//WALKERS
		rand = UnityEngine.Random.Range (mapGen.minWalker, mapGen.maxWalker);
		for(int i=mapGen.minWalker;i<mapGen.width-mapGen.maxWalker;i+=rand){	
			for (int j=1;j<mapGen.height-1;j++) {
				if (mapGen.map [i, j - 1] == 1 && mapGen.map [i, j] == 0&&mapGen.map[i+1,j-1]==1&&
					mapGen.map[i+1,j]==0&&mapGen.map[i+2,j-1]==1&&mapGen.map[i+2,j]==0 && mapGen.IsInMapRange (i, j)) {
					mapGen.map [i, j] = 7;
					mapGen.map [i+1, j] = 7;
					mapGen.map [i+2, j] = 7;
					walkersPos.Add (new MapGenerator.Coord(i,j));
					rand = UnityEngine.Random.Range (mapGen.minWalker, mapGen.maxWalker);


				}


				if (i > mapGen.width - mapGen.maxWalker) {
					break;
				}
			}
			if (i > mapGen.width - mapGen.maxWalker) {
				break;
			}
		}
		//FLYERS
		rand = UnityEngine.Random.Range (mapGen.minFlyerX, mapGen.maxFlyerX);
		for (int i=mapGen.minFlyerX;i<mapGen.width-mapGen.maxFlyerX;i+=rand){
			if(i>=mapGen.width-mapGen.maxFlyerX){
				break;
			}
			for (int j = 1; j <= mapGen.height-1 ; j++) {
				if (mapGen.map [i, j] == 0 &&mapGen.map[i,j-1]==1&& mapGen.IsInMapRange(i,j)) {
					rand = UnityEngine.Random.Range (mapGen.minFlyerY, mapGen.maxFlyerY);
					if(mapGen.IsInMapRange(i,j+rand)&&mapGen.map[i,j+rand]==0){
						mapGen.map [i, j+rand] = 5;
						flyersPos.Add (new MapGenerator.Coord (i, j+rand));
						break;
					}
				}

			}
			rand = UnityEngine.Random.Range (mapGen.minFlyerX,mapGen.maxFlyerX);
		}
		//JUMPERS
		rand = UnityEngine.Random.Range (mapGen.minJumper, mapGen.maxJumper);
		for (int i = mapGen.minJumper; i < mapGen.width-mapGen.maxJumper; i+=rand) {
			for (int j = 1; j < mapGen.height-1; j++) {
				if(mapGen.map[i,j-1]==1&&mapGen.map[i,j]==0&&mapGen.IsInMapRange(i,j)){

					mapGen.map [i, j] = 6;
					jumpersPos.Add (new MapGenerator.Coord (i, j));
					rand = UnityEngine.Random.Range (mapGen.minJumper, mapGen.maxJumper);
				}
				if (i > mapGen.width-mapGen.maxJumper) {
					break;
				}
			}
			if (i > mapGen.width-mapGen.maxJumper) {
				break;
			}
		}
		//DROPPERS
		rand = UnityEngine.Random.Range (mapGen.minDropper,mapGen.maxDropper);
		for (int i =mapGen.minDropper; i < mapGen.width-mapGen.maxDropper; i++) {
			for (int j=1;j<=mapGen.height-1;j++){
				if(mapGen.map[i,j]==0&&mapGen.map[i,j+1]==1&&mapGen.IsInMapRange(i,j)){
					mapGen.map [i, j] = 8;
					droppersPos.Add (new MapGenerator.Coord (i, j));
					rand = UnityEngine.Random.Range (mapGen.minDropper, mapGen.maxDropper);
					i = i + rand;
				}
				if (i > mapGen.width-mapGen.maxDropper) {
					break;
				}
			}


			if (i >mapGen.width-mapGen.maxDropper) {
				break;
			}

		}
		//COINS
		rand = UnityEngine.Random.Range (mapGen.minCoin,mapGen.maxCoin);
		for (int i =mapGen.minCoin; i < mapGen.width-mapGen.minCoin; i++) {
			for (int j=1;j<=mapGen.height-1;j++){
				if(mapGen.map[i,j]==0&&mapGen.map[i,j-1]==0&&mapGen.map[i,j-2]==1&&mapGen.IsInMapRange(i,j)){
					mapGen.map [i, j] = 10;
					coinsPos.Add (new MapGenerator.Coord (i, j));
					rand = UnityEngine.Random.Range (mapGen.minCoin, mapGen.maxCoin);
					i = i + rand;
				}
				if (i > mapGen.width-mapGen.maxCoin) {
					break;
				}
			}


			if (i >mapGen.width-mapGen.maxCoin) {
				break;
			}

		}
	}

	public void CreateObjectPositions_Roombased(List<MapGenerator.Room> survivingRooms){

		foreach (MapGenerator.Room room in survivingRooms) {

			//SPIKES
			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}

				if (mapGen.map [tile.tileX, tile.tileY-1] == 1 && mapGen.map [tile.tileX, tile.tileY] == 0) {
					if (randomBoolean (mapGen.randomBoolValueS)) {
						mapGen.map [tile.tileX, tile.tileY] = 4;
						spikesPos.Add (tile);
					}

				}
			}
			//WALKERS
			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}

				if (mapGen.map [tile.tileX, tile.tileY] == 0 && mapGen.map [tile.tileX+1, tile.tileY] == 0&&
					mapGen.map [tile.tileX+2, tile.tileY] == 0 &&mapGen.map [tile.tileX, tile.tileY-1] == 1&&
					mapGen.map [tile.tileX+1, tile.tileY-1] == 1&&mapGen.map [tile.tileX+2, tile.tileY-1] == 1) {
					if (randomBoolean (mapGen.randomBoolValueW)) {
						mapGen.map [tile.tileX, tile.tileY] = 7;
						mapGen.map [tile.tileX + 1, tile.tileY] = 7;
						mapGen.map [tile.tileX + 2, tile.tileY] = 7;
						walkersPos.Add (tile);
					}

				}
			}

			//FLYERS
			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}

				if (mapGen.map [tile.tileX, tile.tileY]==0&&randomBoolean (mapGen.randomBoolValueF)) {
					mapGen.map [tile.tileX, tile.tileY] = 5;
					flyersPos.Add (tile);
				}


			}

			//JUMPERS

			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}
				if (mapGen.map [tile.tileX, tile.tileY-1] == 1 && mapGen.map [tile.tileX, tile.tileY] == 0) {
					if (randomBoolean (mapGen.randomBoolValueJ)) {
						mapGen.map [tile.tileX, tile.tileY] = 6;
						jumpersPos.Add (tile);
					}

				}
			}
			//DROPPERS

			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}
				if (mapGen.map [tile.tileX, tile.tileY+1] == 1 && mapGen.map [tile.tileX, tile.tileY] == 0) {
					if (randomBoolean (mapGen.randomBoolValueD)) {
						mapGen.map [tile.tileX, tile.tileY] = 8;
						droppersPos.Add (tile);
					}

				}
			}
			//COINS
			foreach (MapGenerator.Coord tile in room.ReturnTiles()) {
				if (isOccupied (tile.tileX, tile.tileY)) {
					continue;
				}
				if (mapGen.IsInMapRange (tile.tileX, tile.tileY - 2) && mapGen.IsInMapRange (tile.tileX, tile.tileY - 1) &&
					mapGen.IsInMapRange (tile.tileX, tile.tileY)) {
					if (mapGen.map [tile.tileX, tile.tileY - 2] == 1 && mapGen.map [tile.tileX, tile.tileY - 1] == 0 &&
						mapGen.map [tile.tileX, tile.tileY] == 0) {
						if (randomBoolean (mapGen.randomBoolValueC)) {
							mapGen.map [tile.tileX, tile.tileY] = 10;
							coinsPos.Add (tile);
						}

					}
				}
			}
		}
		foreach (MapGenerator.Coord coord in mapGen.survivingPassageCoords) {

			if (isOccupied (coord.tileX, coord.tileY)) {
				continue;
			}
			//SPIKES
			if (mapGen.map [coord.tileX, coord.tileY-1] == 1 && mapGen.map [coord.tileX, coord.tileY] == 0) {
				if (randomBoolean (mapGen.randomBoolValueS)) {
					mapGen.map [coord.tileX, coord.tileY] = 4;
					spikesPos.Add (coord);
				}

			}
			//WALKERS
			if (mapGen.map [coord.tileX, coord.tileY] == 0 && mapGen.map [coord.tileX+1, coord.tileY] == 0&&
				mapGen.map [coord.tileX+2, coord.tileY] == 0 &&mapGen.map [coord.tileX, coord.tileY-1] == 1&&
				mapGen.map [coord.tileX+1, coord.tileY-1] == 1&&mapGen.map [coord.tileX+2, coord.tileY-1] == 1) {
				if (randomBoolean (mapGen.randomBoolValueW)) {
					mapGen.map [coord.tileX, coord.tileY] = 7;
					mapGen.map [coord.tileX + 1, coord.tileY] = 7;
					mapGen.map [coord.tileX + 2, coord.tileY] = 7;
					walkersPos.Add (coord);
				}
			}
			//FLYERS
			if (mapGen.map [coord.tileX, coord.tileY]==0&&randomBoolean (mapGen.randomBoolValueF)) {
				mapGen.map [coord.tileX, coord.tileY] = 5;
				flyersPos.Add (coord);
			}
			//JUMPERS
			if (mapGen.map [coord.tileX, coord.tileY-1] == 1 && mapGen.map [coord.tileX, coord.tileY] == 0) {
				if (randomBoolean (mapGen.randomBoolValueJ)) {
					mapGen.map [coord.tileX, coord.tileY] = 6;
					jumpersPos.Add (coord);
				}

			}
			//DROPPERS
			if (mapGen.map [coord.tileX, coord.tileY+1] == 1 && mapGen.map [coord.tileX, coord.tileY] == 0) {
				if (randomBoolean (mapGen.randomBoolValueD)) {
					mapGen.map [coord.tileX, coord.tileY] = 8;
					droppersPos.Add (coord);
				}

			}
			//COINS
			if (mapGen.map [coord.tileX, coord.tileY-2] == 1&&mapGen.map [coord.tileX, coord.tileY-1] == 0 && mapGen.map [coord.tileX, coord.tileY] == 0) {
				if (randomBoolean (mapGen.randomBoolValueC)) {
					mapGen.map [coord.tileX, coord.tileY] = 10;
					coinsPos.Add (coord);
				}

			}
		}
	}
	//Create area around checkpoint that enemies can't spawn
	void CreateArea(int i,int j){
		for (int neighbourX = i - checkpointRadius; neighbourX <= i + checkpointRadius; neighbourX++) {
			for (int neighbourY = j - checkpointRadius; neighbourY <= j + checkpointRadius; neighbourY++) {
				if(!mapGen.IsInMapRange(neighbourX,neighbourY)){
					continue;
				}
				if (mapGen.map [neighbourX, neighbourY] == 1) {
					continue;
				}
				if (mapGen.map [neighbourX, neighbourY] == 0&&mapGen.IsInMapRange(neighbourX,neighbourY)) {
					mapGen.map [neighbourX, neighbourY] = 9;
				}
			}
		}
	}

	bool randomBoolean (float randomBoolValue){
		if (Random.value >= randomBoolValue)
		{
			return true;
		}
		return false;
	}
	bool isOccupied(int x,int y){
		if ((x == startX && y == startY) || (x == checkX && y == checkY) || (x == goalX && y == goalY)) {
			return true;
		}
		return false;
	}
}
