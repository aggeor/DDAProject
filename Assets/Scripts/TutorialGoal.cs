using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialGoal : MonoBehaviour {
	public Text text;
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene(0);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			if (this.name == "Tutorial Goal (1)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -18.95f, 0);
				text.text = "To jump over enemies and obstacles press the ↑ button."
						+"\n Pick up coins to increase your score.";
			}else if (this.name == "Tutorial Goal (2)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -33.95f, 0);
				text.text = "Use your Double-jump ability to avoid enemies by pressing  the ↑ button twice." 
						+"\n You can also lure enemies over the spikes to kill them.";
			}else if (this.name == "Tutorial Goal (3)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -44.95f, 0);
				text.text = "See that rock over there? It will start falling once you pass under it." 
						+"\n It will kill anything it hits.";
			}else if (this.name == "Tutorial Goal (4)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -59.95f, 0);
				text.text = "You can use your Gravity-flip ability by pressing the S button to reach high grounds.";
			}else if (this.name == "Tutorial Goal (5)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -74.95f, 0);
				text.text = "Those wasps over there seem dangerous!" 
					+"\n Use your Run ability to run past them by pressing the A button.";
				
			}else if (this.name == "Tutorial Goal (6)") {
				other.gameObject.transform.localPosition = new Vector3 (-9.07f, -89f, 0);
				text.text = "This is the last part of the tutorial." 
					+"\n Use what you have learned to reach the open door at the end!";
			}else if (this.name == "Tutorial Goal (7)") {
				SceneManager.LoadScene (0);
			}
		}
	}
}
