using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject currentCheckpoint;

	public PlayerController player;

	private EnemyFlyer flyer;

	public GameObject deathParticle;
	public GameObject respawnParticle;

	public float respawnDelay;
	public bool deathCheck;


	public GameObject[] enemies;

	public MapGenerator mapGen;
	public LevelLayoutHandler lvlHandler;

	public Camera camera;
	public Camera camera2;

	void Start () {
		deathCheck=false;
		if (camera2 != null) {
			camera2.transform.localPosition = new Vector3 (mapGen.width / 2, mapGen.height / 2, -10);
		}
	}

	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		if (Input.GetKeyDown(KeyCode.RightShift)&&deathCheck==false){
			RespawnPlayer ();
			RespawnEnemies ();
		}
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			camera.enabled = !camera.enabled;
			camera2.enabled = !camera2.enabled;
			player.gameObject.SetActive(!player.isActiveAndEnabled);
		}

	}
	public void RespawnPlayer(){
		StartCoroutine ("RespawnPlayerCo");
	}
	public IEnumerator RespawnPlayerCo(){
		Instantiate (deathParticle, player.transform.localPosition, player.transform.localRotation);
		player.gameObject.SetActive (false);
		player.GetComponent<Renderer>().enabled = false;
		player.GetComponent<BoxCollider2D> ().enabled = false;
		deathCheck = true;

		yield return new WaitForSeconds (respawnDelay);
		player.transform.localPosition = currentCheckpoint.transform.localPosition;
		Instantiate (respawnParticle, player.transform.localPosition, player.transform.localRotation);
		player.gameObject.SetActive (true);
		player.angle=new Vector3(0,0,0);
		player.GetComponent<Renderer>().enabled = true;
		player.GetComponent<BoxCollider2D> ().enabled = true;
		deathCheck = false;


	}
	public void RespawnEnemies(){
		StartCoroutine ("RespawnEnemiesCo");
	}
	public IEnumerator RespawnEnemiesCo(){
		
		if(deathCheck==true){
			int i=0,j=0,k=0,l=0;
			yield return new WaitForSeconds (respawnDelay);
			foreach (GameObject enemy in enemies) {
				if (enemy.layer == 28) {
					enemy.transform.localPosition = enemy.GetComponent<EnemyWalker> ().startPos;
					i++;
				}else if (enemy.layer==29){
					enemy.transform.localPosition = enemy.GetComponent<EnemyFlyer> ().startPos;
					j++;
				}else if (enemy.layer==30){
					enemy.transform.localPosition = enemy.GetComponent<EnemyJumper> ().startPos;
					k++;

				}else if (enemy.layer==31){
					enemy.transform.localPosition = enemy.GetComponent<EnemyDropper> ().startPos;
					l++;
				}
			}
		}
	}

}
