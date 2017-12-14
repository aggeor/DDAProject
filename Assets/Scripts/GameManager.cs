using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int width;
	public int height;
	public int randomFillPercent;
	//////////////////////////////////////
	public string mapLayout;
	public int minSpikes,maxSpikes;
	public int minWalker,maxWalker;
	public int minFlyerX,maxFlyerX,minFlyerY,maxFlyerY;
	public int minJumper,maxJumper;
	public int minDropper,maxDropper;
	public int minCoin, maxCoin;

	public float randomBoolValueS;
	public float randomBoolValueW;
	public float randomBoolValueF;
	public float randomBoolValueJ;
	public float randomBoolValueD;
	public float randomBoolValueC;

	public int lvlCount;
	public int abilityCount;
	public int deathCount;
	public int enemyCount;

	public bool spikesEnabled;
	public bool walkersEnabled;
	public bool flyersEnabled;
	public bool jumpersEnabled;
	public bool droppersEnabled;

	//Jumper
	public float jumperJumpSpeed;
	public float jumperHorSpeed;
	public float jumperPlayerRange;

	//Flyer
	public float flyerMoveSpeed;
	public float flyerPlayerRange;

	//Dropper
	public float dropSpeed;

	//Walker
	public float walkSpeed;
	//////////////////////////////////////
	public float score;
	public float previousScore;
	public float highScore;
	public int coinCount;

	public MapGenerator mapGen;

	public Button veryEasyBtn;
	public Button easyBtn;
	public Button mediumBtn;
	public Button hardBtn;
	public Button veryHardBtn;

	public Text scoreText;
	public Text highScoreText;
	public Text coinText;
	public Text timerText;

	public float timeLeft;
	public float previousTimeLeft;

	public GameObject[] enemies;

	static int instanceId;

	public static GameManager instance;

	public LevelManager lvlManager;
	public GameObject deathParticle;
	void Awake()
	{	
		//Debug.LogError ("GM awake started");
		instanceId = GetInstanceID ();
		//Singleton for Game Manager
		if (instance != null && instance != this)
		{
			if(SceneManager.GetActiveScene().buildIndex==0&&instanceId!=GetInstanceID()){
				Debug.Log(instanceId+GetInstanceID());
				DestroyImmediate (instance.gameObject);
			}
		} else {
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}
		highScore = 0;
		//Debug.LogError ("GM awake finished");
	}
	public void ChangeToScene(int sceneToChangeTo){
		SceneManager.LoadScene (sceneToChangeTo);
	}
	public void ExitApplication(){
		Application.Quit ();
	}

	void Start () {
		//Debug.LogError ("GM start started");
		lvlCount=1;
		previousScore=0;
		TutorialValues();
		//Debug.LogError ("GM start finished");
	}

	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			TutorialValues();
		}
		if (SceneManager.GetActiveScene().buildIndex==1) {
			coinCount = 0;
			veryEasyBtn = GameObject.Find ("VeryEasy").GetComponent<UnityEngine.UI.Button> ();
			veryEasyBtn.onClick.AddListener (() => VeryEasyValues ());
			easyBtn = GameObject.Find ("Easy").GetComponent<UnityEngine.UI.Button> ();
			easyBtn.onClick.AddListener (() => EasyValues ());
			mediumBtn = GameObject.Find ("Medium").GetComponent<UnityEngine.UI.Button> ();
			mediumBtn.onClick.AddListener (() => MediumValues ());
			hardBtn = GameObject.Find ("Hard").GetComponent<UnityEngine.UI.Button> ();
			hardBtn.onClick.AddListener (() => HardValues ());
			veryHardBtn = GameObject.Find ("VeryHard").GetComponent<UnityEngine.UI.Button> ();
			veryHardBtn.onClick.AddListener (() => VeryHardValues ());
		}

		if(SceneManager.GetActiveScene().buildIndex!=2){
			deathCount = 0;
			lvlCount=1;
			previousScore = 0;
			score = 0;
			previousTimeLeft = 0f;
		}
		if(SceneManager.GetActiveScene().buildIndex==2){
			if (mapGen == null) {
				mapGen = FindObjectOfType<MapGenerator> ();
			}

			TimeCounter ();
			coinText=GameObject.Find ("CoinText").GetComponent<UnityEngine.UI.Text> ();
			coinText.text = coinCount.ToString ();
			timerText=GameObject.Find ("Timer").GetComponent<UnityEngine.UI.Text> ();
			timerText.text = "Time Left : " + Mathf.RoundToInt(timeLeft);
			if (coinCount >= highScore) {
				highScore = coinCount;
			}
		}

		if(SceneManager.GetActiveScene().buildIndex==4){
			scoreText=GameObject.Find ("Score").GetComponent<UnityEngine.UI.Text> ();
			scoreText.text ="Current Score : "+ coinCount.ToString ();
			highScoreText=GameObject.Find ("HighScore").GetComponent<UnityEngine.UI.Text> ();
			highScoreText.text = "Highscore : " + highScore.ToString();


		}
	}
	public void TutorialValues(){
		width=0;
		height=0;
		randomFillPercent=0;
		mapLayout="";
		minSpikes=0;
		maxSpikes=0;
		minWalker=0;
		maxWalker=0;
		minFlyerX=0;maxFlyerX=0;
		minFlyerY=0;
		maxFlyerY=0;
		minJumper=0;
		maxJumper=0;
		minDropper=0;
		maxDropper=0;
		minCoin = 0;
		maxCoin = 0;

		jumperJumpSpeed=11;
		jumperHorSpeed=3;
		jumperPlayerRange=4;

		flyerMoveSpeed=2;
		flyerPlayerRange=4;

		dropSpeed=1;

		walkSpeed = 0.5f;

		randomBoolValueS=0;
		randomBoolValueW=0;
		randomBoolValueF=0;
		randomBoolValueJ=0;
		randomBoolValueD=0;
		randomBoolValueC=0;
	}
	public void VeryEasyEnemies(){
		if (spikesEnabled) {

			minSpikes = 5;
			maxSpikes = 10;
		} else {

			minSpikes = width;
			maxSpikes = width;
		}
		if (walkersEnabled) {

			minWalker = 8;
			maxWalker = 10;
		} else {

			minWalker = width;
			maxWalker = width;
		}
		if (flyersEnabled) {

			minFlyerX = 4;
			maxFlyerX = 10;
			minFlyerY = 2;
			maxFlyerY = 6;
		} else {

			minFlyerX = width;
			maxFlyerX = width;
			minFlyerY = height;
			maxFlyerY = height;
		}
		if (jumpersEnabled) {

			minJumper=5;
			maxJumper = 10;
		} else {

			minJumper = width;
			maxJumper = width;
		}
		if (droppersEnabled) {

			minDropper = 5;
			maxDropper = 8;
		} else {

			minDropper = width;
			maxDropper = width;
		}
		minCoin = 5;
		maxCoin = 10;

	}
	public void VeryEasyValues(){
		width = UnityEngine.Random.Range(60, 70);
		height = UnityEngine.Random.Range (10, 12);
		randomFillPercent = UnityEngine.Random.Range(0, 15);
		mapLayout = "VeryEasy";

		spikesEnabled = false;
		walkersEnabled = false;
		flyersEnabled = false;
		jumpersEnabled = false;
		droppersEnabled = false;

		jumperJumpSpeed=11;
		jumperHorSpeed=2;
		jumperPlayerRange=4;
		flyerMoveSpeed=2;
		flyerPlayerRange=4;
		dropSpeed=1;
		walkSpeed = 0.5f;
		VeryEasyEnemies ();
		timeLeft = 15f;

	}
	public void EasyEnemies (){
		if (spikesEnabled) {

			minSpikes = 5;
			maxSpikes = 10;
		} else {

			minSpikes = width;
			maxSpikes = width;
		}
		if (walkersEnabled) {

			minWalker = 8;
			maxWalker = 10;
		} else {

			minWalker = width;
			maxWalker = width;
		}
		if (flyersEnabled) {

			minFlyerX = 6;
			maxFlyerX = 10;
			minFlyerY = 2;
			maxFlyerY = 6;
		} else {

			minFlyerX = width;
			maxFlyerX = width;
			minFlyerY = height;
			maxFlyerY = height;
		}
		if (jumpersEnabled) {

			minJumper=5;
			maxJumper = 10;
		} else {

			minJumper = width;
			maxJumper = width;
		}
		if (droppersEnabled) {

			minDropper = 5;
			maxDropper = 8;
		} else {

			minDropper = width;
			maxDropper = width;
		}
		minCoin = 5;
		maxCoin = 10;
	}
	public void EasyValues(){
		width = UnityEngine.Random.Range(80, 120);
		height = UnityEngine.Random.Range (10, 20);
		randomFillPercent = UnityEngine.Random.Range(25, 35);
		mapLayout = "Easy";

		spikesEnabled = true;
		walkersEnabled = true;
		flyersEnabled = false;
		jumpersEnabled = false;
		droppersEnabled = false;

		jumperJumpSpeed=11;
		jumperHorSpeed=2;
		jumperPlayerRange=4;
		flyerMoveSpeed=2;
		flyerPlayerRange=4;
		dropSpeed=1;
		walkSpeed = 0.5f;
		EasyEnemies ();
		timeLeft = 30f;
	} 
	public void MediumEnemies (){
		if (spikesEnabled) {

			randomBoolValueS=0.8f;
		} else {

			randomBoolValueS=1f;
		}
		if (walkersEnabled) {

			randomBoolValueW=0.5f;
		} else {

			randomBoolValueW=1f;
		}
		if (flyersEnabled) {

			randomBoolValueF=0.995f;
		} else {

			randomBoolValueF=1f;
		}
		if (jumpersEnabled) {

			randomBoolValueJ=0.9f;
		} else {

			randomBoolValueJ=1f;
		}
		if (droppersEnabled) {

			randomBoolValueD=0.9f;
		} else {

			randomBoolValueD=1f;
		}

		randomBoolValueC=0.7f;
	}
	public void MediumValues(){
		width = UnityEngine.Random.Range(30, 50);
		height = UnityEngine.Random.Range (80, 120);
		randomFillPercent = UnityEngine.Random.Range(45, 52);
		mapLayout = "Medium";

		spikesEnabled = false;
		walkersEnabled = false;
		flyersEnabled = false;
		jumpersEnabled = true;
		droppersEnabled = true;

		jumperJumpSpeed=11;
		jumperHorSpeed=2;
		jumperPlayerRange=4;
		flyerMoveSpeed=2;
		flyerPlayerRange=4;
		dropSpeed=1;
		walkSpeed = 0.5f;
		MediumEnemies ();
		timeLeft = 45f;
	} 

	public void HardEnemies(){
		if (spikesEnabled) {

			randomBoolValueS=0.9f;
		} else {

			randomBoolValueS=1f;
		}
		if (walkersEnabled) {

			randomBoolValueW=0.5f;
		} else {

			randomBoolValueW=1f;
		}
		if (flyersEnabled) {

			randomBoolValueF=0.999f;
		} else {

			randomBoolValueF=1f;
		}
		if (jumpersEnabled) {

			randomBoolValueJ=0.9f;
		} else {

			randomBoolValueJ=1f;
		}
		if (droppersEnabled) {

			randomBoolValueD=0.999f;
		} else {

			randomBoolValueD=1f;
		}

		randomBoolValueC=0.8f;
	}
	public void HardValues(){
		width = UnityEngine.Random.Range(80, 100);
		height = UnityEngine.Random.Range (80, 100);
		randomFillPercent = UnityEngine.Random.Range(50, 55);
		mapLayout = "Hard";

		spikesEnabled = true;
		flyersEnabled = false;
		jumpersEnabled = true;
		droppersEnabled = true;
		walkersEnabled = true;

		jumperJumpSpeed=11;
		jumperHorSpeed=2;
		jumperPlayerRange=4;
		flyerMoveSpeed=2;
		flyerPlayerRange=4;
		dropSpeed=1;
		walkSpeed = 0.5f;
		HardEnemies ();
		timeLeft = 75f;
	} 
	public void VeryHardEnemies(){
		if (spikesEnabled) {

			randomBoolValueS=0.8f;
		} else {

			randomBoolValueS=1f;
		}
		if (walkersEnabled) {

			randomBoolValueW=0.5f;
		} else {

			randomBoolValueW=1f;
		}
		if (flyersEnabled) {

			randomBoolValueF=0.999f;
		} else {

			randomBoolValueF=1f;
		}
		if (jumpersEnabled) {

			randomBoolValueJ=0.9f;
		} else {

			randomBoolValueJ=1f;
		}
		if (droppersEnabled) {

			randomBoolValueD=0.9f;
		} else {

			randomBoolValueD=1f;
		}
		randomBoolValueC=0.8f;
	}
	public void VeryHardValues(){
		width = UnityEngine.Random.Range(100, 120);
		height = UnityEngine.Random.Range (100, 120);
		randomFillPercent = UnityEngine.Random.Range(50, 55);
		mapLayout = "VeryHard";

		spikesEnabled = true;
		flyersEnabled = true;
		jumpersEnabled = true;
		droppersEnabled = true;
		walkersEnabled = true;

		jumperJumpSpeed=11;
		jumperHorSpeed=2;
		jumperPlayerRange=4;
		flyerMoveSpeed=2;
		flyerPlayerRange=4;
		dropSpeed=1;
		walkSpeed = 0.5f;
		VeryHardEnemies ();
		timeLeft = 120f;
	}

	 
	public void DynamicDifficultyAdjustment(){
		//Debug.LogError ("DDA started");
		//previousScore = score;

		//score = score + lvlCount-deathCount;
		//score = score + lvlCount+abilityCount+enemyCount-100*deathCount;
		//score = score + lvlCount+abilityCount+enemyCount-3*deathCount;
		//score = score + coinCount-deathCount;
		/*
		if (lvlCount == 10 && mapLayout == "VeryEasy") {
			EasyValues ();
		}
		if (lvlCount == 15 && mapLayout == "Easy") {
			MediumValues ();
		}
		if (lvlCount == 20 && mapLayout == "Medium") {
			HardValues ();
		}
		if (lvlCount == 25 && mapLayout == "Hard") {
			VeryHardValues ();
		}
		*/

		/*
		Increase difficulty by reaching a certain score
		if (score>=100&& mapLayout == "VeryEasy") {
			EasyValues ();
		}
		if (score>=200 && mapLayout == "Easy") {
			MediumValues ();
		}
		if (score>=400 && mapLayout == "Medium") {
			HardValues ();
		}
		if (score>=800 && mapLayout == "Hard") {
			VeryHardValues ();
		}
		*/

		//Debug.LogError ("Level : " + lvlCount + " abilities : " + abilityCount + " coinCount :	 " + coinCount+ "deathCount : " + deathCount);
		//print ("Level : " + lvlCount + "deathCount : " + deathCount);
		//Debug.LogError("Previous Score : "+previousScore);
		//Debug.LogError("Score : "+score);


		//Debug.LogError("Level : " + lvlCount +" Previous time: "+previousTimeLeft+" timeleft: "+timeLeft);

		float rand=Random.Range(0,4);
		if(timeLeft>previousTimeLeft){
		//if (score >= previousScore) {
			//Make it harder
			Debug.LogError("Level : " + lvlCount +" Previous time: "+previousTimeLeft+" timeleft: "+timeLeft);
			if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
				if (minCoin != 1 && maxCoin != 1) {
					if (randomBoolean (0.5f)||minCoin==maxCoin) {
						minCoin -= 1;
					} else {
						maxCoin -= 1;
					}
				}
				if (minCoin == 1 && maxCoin != 1) {
					maxCoin -= 1;
				}
			}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
				randomBoolValueC-=0.01f;
			}
			if(randomBoolean(0.25f)){
				//Increase density
				rand=Random.Range(0,5);
				if (rand == 0) {
					if (spikesEnabled) {
						Debug.LogError ("Increase spike density");
						if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
							if (minSpikes != 1 && maxSpikes != 1) {
								if (randomBoolean (0.5f)||minSpikes==maxSpikes) {
									minSpikes -= 1;
								} else {
									maxSpikes -= 1;
								}
							}
							if (minSpikes == 1 && maxSpikes != 1) {
								maxSpikes -= 1;
							}
						}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
							randomBoolValueS-=0.01f;
						}
					} else {
						spikesEnabled = true;
						Debug.LogError ("Enable spikes");
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 1) {
					if (flyersEnabled) {
						Debug.LogError("Increase flyer density");
						if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
							if (minFlyerX != 1 && maxFlyerX != 1) {
								if (randomBoolean (0.5f)||minFlyerX==maxFlyerX) {
									minFlyerX -= 1;
								} else {
									maxFlyerX -= 1;
								}
							}
							if (minFlyerX == 1 && maxFlyerX != 1) {
								maxFlyerX -= 1;
							}
						}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
							randomBoolValueF-=0.01f;
						}
					} else {
						Debug.LogError ("Enable flyers");
						flyersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 2) {
					if (jumpersEnabled) {
						Debug.LogError("Increase jumper density");
						if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
							if (minJumper != 1 && maxJumper != 1) {
								if (randomBoolean (0.5f)||minJumper==maxJumper) {

									minJumper -= 1;
								} else {
									maxJumper -= 1;
								}
							}
							if (minJumper == 1 && maxJumper != 1) {
								maxJumper -= 1;
							}
						}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
							randomBoolValueJ-=0.01f;
						}
					} else {
						Debug.LogError ("Enable jumpers");
						jumpersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 3) {
					if (droppersEnabled) {
						Debug.LogError("Increase dropper density");
						if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
							if (minDropper != 1 && maxDropper != 1) {
								if (randomBoolean (0.5f)||minDropper==maxDropper) {
									minDropper -= 1;
								} else {
									maxDropper -= 1;
								}
							}
							if (minDropper == 1 && maxDropper != 1) {
								maxDropper -= 1;
							}
						}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
							randomBoolValueD-=0.01f;
						}
					} else {
						Debug.LogError ("Enable droppers");
						droppersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}

				}
				else if (rand == 4) {
					if (walkersEnabled) {
						Debug.LogError("Increase walker density");
						if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
							if (minWalker != 1 && maxWalker != 1) {
								if (randomBoolean (0.5f)||minWalker==maxWalker) {
									minWalker -= 1;
								} else {
									maxWalker -= 1;
								}
							}
							if (minWalker == 1 && maxWalker != 1) {
								maxWalker -= 1;
							}
						}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
							randomBoolValueW-=0.01f;
						}
					} else {
						Debug.LogError ("Enable walkers");
						walkersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				}
			}else{
				//Increase enemy ability 
				rand=Random.Range(0,4);
				if (rand == 0) {
					if (flyersEnabled) {
						if (randomBoolean (0.5f)) {
							Debug.LogError("Increase flyer ability:Movespeed");
							flyerMoveSpeed += 0.1f;
						} else {
							Debug.LogError("Increase flyer ability:Range");
							flyerPlayerRange += 0.1f;
						}
					} else {
						Debug.LogError ("Enable flyers");
						flyersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 1) {
					if (jumpersEnabled) {
						if (randomBoolean (0.5f)) {
							if (randomBoolean (0.5f)) {
								Debug.LogError("Increase jumper ability:Hor Movespeed");
								jumperHorSpeed += 0.1f;
							} else {
								Debug.LogError("Increase jumper ability:Jump speed");
								jumperJumpSpeed += 0.1f;
							}
						} else {
							Debug.LogError("Increase jumper ability:Range");
							jumperPlayerRange += 0.1f;
						}
					} else {
						Debug.LogError ("Enable jumpers");
						jumpersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 2) {
					if (droppersEnabled) {
						Debug.LogError("Increase dropper ability:Movespeed");
						dropSpeed += 0.1f;
					} else {
						Debug.LogError ("Enable droppers");
						droppersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} else if (rand == 3) {
					if (walkersEnabled) {
						Debug.LogError("Increase walker ability:Movespeed");
						walkSpeed += 0.01f;
					} else {
						Debug.LogError ("Enable walkers");
						walkersEnabled = true;
						if (mapLayout == "VeryEasy") {
							VeryEasyEnemies ();
						}else if(mapLayout == "Easy"){
							EasyEnemies ();
						}else if(mapLayout == "Medium"){
							MediumEnemies ();
						}else if(mapLayout == "Hard"){
							HardEnemies ();
						}else if(mapLayout == "VeryHard"){
							VeryHardEnemies ();
						}
					}
				} 
			}
		} else if(timeLeft<previousTimeLeft) {
			//Make it easier
			Debug.LogError("Level : " + lvlCount +" Previous time: "+previousTimeLeft+" timeleft: "+timeLeft);
			if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
				if (randomBoolean (0.5f)||minCoin==maxCoin) {
					maxCoin += 1;
				} else {
					minCoin += 1;
				}
			}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
				randomBoolValueC+=0.01f;
			}
			if(randomBoolean(0.5f)){
				//Decrease density
				rand=Random.Range(0,5);
				if (rand == 0) {
					Debug.LogError ("Decrease spike density");
					if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
						if (randomBoolean (0.5f) || minSpikes == maxSpikes) {
							maxSpikes += 1;
						} else {
							minSpikes += 1;
						}
					
					} else if (mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard") {
						randomBoolValueS += 0.01f;
					}
				}
				if (rand == 1) {
					Debug.LogError("Decrease flyer density");
					if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
						if (randomBoolean (0.5f)||minFlyerX==maxFlyerX) {
							maxFlyerX += 1;
						} else {
							minFlyerX += 1;
						}
					}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
						randomBoolValueF+=0.01f;
					}
				} else if (rand == 2) {
					Debug.LogError("Decrease jumper density");
					if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
						if (randomBoolean (0.5f)||minJumper==maxJumper) {
							maxJumper += 1;
						} else {
							minJumper += 1;
						}
					}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
						randomBoolValueJ+=0.01f;
					}
				} else if (rand == 3) {
					Debug.LogError("Decrease dropper density");
					if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
						if (randomBoolean (0.5f)||minDropper==maxDropper) {
							maxDropper += 1;
						} else {
							minDropper += 1;
						}

					}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
						randomBoolValueD+=0.01f;
					}
				} else if (rand == 4) {
					Debug.LogError("Decrease walker density");
					if (mapLayout == "VeryEasy"||mapLayout == "Easy") {
						if (randomBoolean (0.5f)||minDropper==maxDropper) {
							maxWalker += 1;
						} else {
							minWalker += 1;
						}

					}else if(mapLayout == "Medium"||mapLayout == "Hard"||mapLayout == "VeryHard"){
						randomBoolValueW+=0.01f;
					}
				}
			}else{
				//Decrease ability
				rand=Random.Range(0,4);
				if (rand == 0) {
					if (randomBoolean (0.5f)) {
						Debug.LogError("Decrease flyer ability:Movespeed");
						flyerMoveSpeed -= 0.1f;
					} else {
						Debug.LogError("Decrease flyer ability:Range");
						flyerPlayerRange -= 0.1f;
					}
				} else if (rand == 1) {
					
					if (randomBoolean (0.5f)) {
						if (randomBoolean (0.5f)) {
							Debug.LogError("Decrease jumper ability:Hor Movespeed");
							jumperHorSpeed -= 0.1f;
						} else {
							Debug.LogError("Decrease jumper ability:Jump speed");
							jumperJumpSpeed -= 0.1f;
						}
					} else {
						Debug.LogError("Range");
						jumperPlayerRange -= 0.1f;
					}
				} else if (rand == 2) {
					Debug.LogError("Decrease dropper ability:Movespeed");
					dropSpeed -= 0.1f;
				} else if (rand == 3) {
					Debug.LogError("Decrease walker ability:Movespeed");
					walkSpeed -= 0.01f;
				} 
			}
		}
		//deathCount = 0;
		//coinCount = 0;

		/*
		if (score >= highScore) {
			highScore = score;
		}
		*/
		//Debug.LogError ("DDA finished");
	}

	bool randomBoolean (float randomBoolValue){
		if (Random.value >= randomBoolValue)
		{
			return true;
		}
		return false;
	}

	void TimeCounter(){
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			GameOver ();
		}
	}
	void GameOver(){
		SceneManager.LoadScene(4);
	}
}