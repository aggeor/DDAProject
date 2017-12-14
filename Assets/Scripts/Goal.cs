using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
	
	public GameManager gm;

	void Awake(){
		//Debug.LogError ("Goal awake started");
		gm = FindObjectOfType<GameManager> ();
		gm.previousTimeLeft = gm.timeLeft;
		Invoke("IncreaseLvl", 0.1f);
		//Debug.LogError ("Goal awake finished");
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			Invoke("IncreaseLvl", 0.1f);
			SceneManager.LoadScene(2);
		}
	}
	void IncreaseLvl()
	{
		gm.lvlCount+=1;
		if (gm.mapLayout == "VeryEasy") {
			gm.timeLeft += 15;
		}else if (gm.mapLayout == "Easy") {
			gm.timeLeft += 30;
		}else if (gm.mapLayout == "Medium") {
			gm.timeLeft += 45;
		}else if (gm.mapLayout == "Hard") {
			gm.timeLeft += 45;
		}else if (gm.mapLayout == "VeryHard") {
			gm.timeLeft += 60;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			
			SceneManager.LoadScene (2);
		}
	}

}
