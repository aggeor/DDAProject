using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIAnimator : MonoBehaviour {

	public GameManager gm;

	public Button tutorialBtn;
	public Button startBtn;
	public Button exitBtn;

	public Button veryEasyBtn;
	public Button easyBtn;
	public Button mediumBtn;
	public Button hardBtn;
	public Button veryHardBtn;
	public Button backBtn;

	void Start () {
		gm = FindObjectOfType<GameManager> ();
		if (SceneManager.GetActiveScene ().buildIndex==0) {
			tutorialBtn = GameObject.Find ("Tutorial").GetComponent<UnityEngine.UI.Button> ();
			startBtn = GameObject.Find ("Start").GetComponent<UnityEngine.UI.Button> ();
			exitBtn = GameObject.Find ("Exit").GetComponent<UnityEngine.UI.Button> ();
		}
		if (SceneManager.GetActiveScene ().buildIndex==1) {
			veryEasyBtn = GameObject.Find ("VeryEasy").GetComponent<UnityEngine.UI.Button> ();
			easyBtn = GameObject.Find ("Easy").GetComponent<UnityEngine.UI.Button> ();
			mediumBtn = GameObject.Find ("Medium").GetComponent<UnityEngine.UI.Button> ();
			hardBtn = GameObject.Find ("Hard").GetComponent<UnityEngine.UI.Button> ();
			veryHardBtn = GameObject.Find ("VeryHard").GetComponent<UnityEngine.UI.Button> ();
			backBtn = GameObject.Find ("Back").GetComponent<UnityEngine.UI.Button> ();
		}
		if (SceneManager.GetActiveScene ().buildIndex == 4) {
			backBtn = GameObject.Find ("Back").GetComponent<UnityEngine.UI.Button> ();
		}

	}
	public void ShowTutorialOpen(){
		tutorialBtn.animator.SetBool ("Open", true);
	}
	public void ShowTutorialClose(){
		tutorialBtn.animator.SetBool ("Open", false);
	}
	public void ShowStartOpen(){
		startBtn.animator.SetBool ("Open", true);
	}
	public void ShowStartClose(){
		startBtn.animator.SetBool ("Open", false);
	}
	public void ShowExitOpen(){
		exitBtn.animator.SetBool ("Open", true);
	}
	public void ShowExitClose(){
		exitBtn.animator.SetBool ("Open", false);
	}
	public void ShowVeryEasyOpen(){
		veryEasyBtn.animator.SetBool ("Open", true);
	}
	public void ShowVeryEasyClose(){
		veryEasyBtn.animator.SetBool ("Open", false);
	}
	public void ShowEasyOpen(){
		easyBtn.animator.SetBool ("Open", true);
	}
	public void ShowEasyClose(){
		easyBtn.animator.SetBool ("Open", false);
	}
	public void ShowMediumOpen(){
		mediumBtn.animator.SetBool ("Open", true);
	}
	public void ShowMediumClose(){
		mediumBtn.animator.SetBool ("Open", false);
	}
	public void ShowHardOpen(){
		hardBtn.animator.SetBool ("Open", true);
	}
	public void ShowHardClose(){
		hardBtn.animator.SetBool ("Open", false);
	}
	public void ShowVeryHardOpen(){
		veryHardBtn.animator.SetBool ("Open", true);
	}
	public void ShowVeryHardClose(){
		veryHardBtn.animator.SetBool ("Open", false);
	}
	public void ShowBackOpen(){
		backBtn.animator.SetBool ("Open", true);
	}
	public void ShowBackClose(){
		backBtn.animator.SetBool ("Open", false);
	}
}
