using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instantiator : MonoBehaviour {



	int width;
	int height;
	public int[,] map;

	public MapGenerator mapGen;
	public LevelLayoutHandler lvlHandler;

	public PlayerController player;
	public GameObject mainCamera;
	void Awake(){
		//Debug.LogError("Instantiator Awake started");

		map = mapGen.map;
		width = mapGen.width;
		height = mapGen.height;
		InstantiateGameObjects();
		//Debug.LogError("Instantiator Awake finished");
	}
	
	
	void InstantiateGameObjects(){

		SpawnBackground ();

		SpawnGround ();

		SpawnStartPoint ();

		SpawnCheckPoint ();

		SpawnGoal ();

		SpawnSpikes();

		SpawnWalkers ();

		SpawnFlyers();

		SpawnJumpers();

		SpawnDroppers();

		SpawnCoins ();
	}

	void SpawnBackground(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Instantiate (Resources.Load ("Background"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
			}
		}
	}
	void SpawnGround(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				//Debug.Log ("i: "+i+" j: "+j+" value : "+map [i, j]);
				if (map [i, j] == 1) {
					//Spawn ground outside of the map
					if(i==0||j==0||i==width-1||j==height-1){
						//Left
						if (i == 0) {
							if (map [i+1, j] != 1) {
								Instantiate (Resources.Load ("Ground_middle_right_1"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							} else {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							}
							for (int k = 1; k <= 10; k++) {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i - k, j, 0), Quaternion.Euler (0, 0, 0));
							}

						}
						//Bottom
						else if (j == 0) {
							if (map [i, j+1] != 1) {
								Instantiate (Resources.Load ("Ground_top"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							} else {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							}
							for (int k = 1; k <= 10; k++) {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j - k, 0), Quaternion.Euler (0, 0, 0));
							}
						} 
						//Right
						else if (i == width - 1) {
							if (map [i-1, j] != 1) {
								Instantiate (Resources.Load ("Ground_middle_left_1"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							} else {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							}
							for (int k = 1; k <= 10; k++) {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i + k, j, 0), Quaternion.Euler (0, 0, 0));
							}
						}
						//Top
						else if (j == height - 1) {
							if (map [i, j-1] != 1) {
								Instantiate (Resources.Load ("Ground_bottom"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							} else {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
							}
							for (int k = 1; k <= 10; k++) {
								Instantiate (Resources.Load ("Ground_middle"),
									new Vector3 (i, j + k, 0), Quaternion.Euler (0, 0, 0));
							}
						} 
						//Bottom-left
						if (i == 0 && j == 0) {
							for (int k = 0; k <= 10; k++) {
								for (int l = 0; l <= 10; l++) {
									Instantiate (Resources.Load ("Ground_middle"),
										new Vector3 (i - k, j - l, 0), Quaternion.Euler (0, 0, 0));
								}
							}
						}
						//Top-left
						if (i == 0 && j == height-1) {
							for (int k = 0; k <= 10; k++) {
								for (int l = 0; l <= 10; l++) {
									Instantiate (Resources.Load ("Ground_middle"),
										new Vector3 (i - k, j + l, 0), Quaternion.Euler (0, 0, 0));
								}
							}
						}
						//Bottom-right
						if (i == width-1 && j == 0) {
							for (int k = 0; k <= 10; k++) {
								for (int l = 0; l <= 10; l++) {
									Instantiate (Resources.Load ("Ground_middle"),
										new Vector3 (i + k, j - l, 0), Quaternion.Euler (0, 0, 0));
								}
							}
						}
						//Top-right
						if (i == width-1&& j == height-1) {
							for (int k = 0; k <= 10; k++) {
								for (int l = 0; l <= 10; l++) {
									Instantiate (Resources.Load ("Ground_middle"),
										new Vector3 (i + k, j + l, 0), Quaternion.Euler (0, 0, 0));
								}
							}
						}
						continue;
					}
					//Spawn the ground with corresponding sprite
					if (map [i-1, j] != 1 && map [i+1, j] == 1  && map [i, j-1] == 1 && map [i, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_top_left"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] == 1 && map [i, j-1] == 1 && map [i, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_top"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] != 1 && map [i+1, j] != 1 && map [i, j-1] == 1 && map [i, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_top_2"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] != 1 && map [i, j-1] == 1 && map [i, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_top_right"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] != 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] == 1) {
						Instantiate (Resources.Load ("Ground_middle_left_1"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] == 1&&map [i-1, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_middle_left_2"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] != 1 && map [i, j+1] == 1 && map [i, j-1] == 1) {
						Instantiate (Resources.Load ("Ground_middle_right_1"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] == 1&&map [i+1, j+1] != 1) {
						Instantiate (Resources.Load ("Ground_middle_right_2"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] == 1) {
						Instantiate (Resources.Load ("Ground_middle"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] != 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] != 1) {
						Instantiate (Resources.Load ("Ground_bottom_left"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] == 1 && map [i, j+1] == 1 && map [i, j-1] != 1) {
						Instantiate (Resources.Load ("Ground_bottom"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
					if (map [i-1, j] == 1 && map [i+1, j] != 1 && map [i, j+1] == 1 &&map [i, j-1] != 1) {
						Instantiate (Resources.Load ("Ground_bottom_right"), new Vector3 (i, j, 0), Quaternion.Euler (0, 0, 0));
						continue;
					}
				}
			}
		}

	}
	void SpawnStartPoint(){
		player.transform.localPosition = new Vector2 (lvlHandler.startX, lvlHandler.startY);
		mainCamera.transform.localPosition = new Vector3 (lvlHandler.startX, lvlHandler.startY,-10);
		Instantiate (Resources.Load ("Startpoint"),
			new Vector3 (lvlHandler.startX, lvlHandler.startY, 0), Quaternion.Euler (0, 0, 0));
	}
	void SpawnCheckPoint(){
		Instantiate (Resources.Load ("Checkpoint"),
			new Vector3 (lvlHandler.checkX+0.2f, lvlHandler.checkY+0.45f, 0), Quaternion.Euler (0, 0, 0));
		for (int k = 0; k < lvlHandler.checkpointsPos.Count; k++) {
			Instantiate (Resources.Load ("Checkpoint"),
				new Vector3 (lvlHandler.checkpointsPos[k].tileX+0.2f,  lvlHandler.checkpointsPos[k].tileY+0.45f, 0),
				Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnGoal(){
		Instantiate (Resources.Load ("Goal"),
			new Vector3 (lvlHandler.goalX, lvlHandler.goalY, 0), Quaternion.Euler (0, 0, 0));
	}
	void SpawnSpikes(){
		for (int i = 0; i < lvlHandler.spikesPos.Count; i++) {
			Instantiate (Resources.Load ("Spikes"),
				new Vector3 (lvlHandler.spikesPos[i].tileX,  lvlHandler.spikesPos[i].tileY-0.1f, 0), Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnWalkers(){
		for (int i = 0; i < lvlHandler.walkersPos.Count; i++) {
			Instantiate (Resources.Load ("Enemy_walker"),
				new Vector3 (lvlHandler.walkersPos[i].tileX,  lvlHandler.walkersPos[i].tileY, 0), Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnFlyers(){
		for (int i = 0; i < lvlHandler.flyersPos.Count; i++) {
			Instantiate (Resources.Load ("Enemy_flyer"),
				new Vector3 (lvlHandler.flyersPos[i].tileX,  lvlHandler.flyersPos[i].tileY, 0), Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnJumpers(){
		for (int i = 0; i < lvlHandler.jumpersPos.Count; i++) {
			Instantiate (Resources.Load ("Enemy_jumper"),
				new Vector3 (lvlHandler.jumpersPos[i].tileX,  lvlHandler.jumpersPos[i].tileY, 0), Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnDroppers(){
		for (int i = 0; i < lvlHandler.droppersPos.Count; i++) {
			Instantiate (Resources.Load ("Enemy_dropper"),
				new Vector3 (lvlHandler.droppersPos[i].tileX,  lvlHandler.droppersPos[i].tileY+0.5f, 0), Quaternion.Euler (0, 0, 0));
		}
	}
	void SpawnCoins(){
		for (int i = 0; i < lvlHandler.coinsPos.Count; i++) {
			Instantiate (Resources.Load ("Coin"),
				new Vector3 (lvlHandler.coinsPos[i].tileX,  lvlHandler.coinsPos[i].tileY, 0), Quaternion.Euler (0, 0, 0));
		}
	}

}
