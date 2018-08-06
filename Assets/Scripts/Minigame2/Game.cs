using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class Game : MonoBehaviour {

	private TurretManager TurretManager;
	private int ENEMY_CAP;
	private List<String> EnemiesToSpawn;
    public GameObject player;
    public Timer roundTimer;
    public int currentRound;
	public GameObject ground;
	public GameObject left;
	public GameObject right;
    public GameObject healthBar;

    private bool fading;

	// Use this for initialization
	public void Start () {
		SetPowers ();
		TurretManager = this.GetComponent<TurretManager>();

		//Default enemy cap
		ENEMY_CAP = 5;

		//Create enemies list
		EnemiesToSpawn = new List<String>();
        currentRound = 1;
        roundTimer = new Timer(5f);
		ReadRoundData(currentRound);

        fading = false;

    }
	
	// Update is called once per frame
	public void Update () {
		for(int i = 0; i < (ENEMY_CAP - TurretManager.NumAlive()); i++){
			//Spawn stuff here
			if(i < EnemiesToSpawn.Count){
				TurretManager.PlaceTurret(TurretManager.turret_types[EnemiesToSpawn[0]], GetRandomValidPosition(EnemiesToSpawn[0]));
				EnemiesToSpawn.RemoveAt(0);
			}
		}
		
		if(player.GetComponent<Player>().won){
			if (Input.GetButtonDown("Fire1")) {
				Application.LoadLevel("Minigame 1");
			} else if (Input.GetButtonDown("Fire2")) {
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				System.Diagnostics.Process.GetCurrentProcess().Kill();
				#endif
			}
		}

        //Check if player is dead
        if (!player.GetComponent<Player>().IsAlive()) {
            if (!fading) {
                fading = true;
                StartCoroutine(player.GetComponent<Player>().FadeToBlack());
                //Disable turrets that are still alive
                StartCoroutine(TurretManager.KillAll());

                //Disable powers
                GameObject[] hands = new GameObject[2];
                hands[0] = left;
                hands[1] = right;

                //remove preset powers
                foreach (GameObject hand in hands) {
                    foreach (Component comp in hand.GetComponents(typeof(MonoBehaviour))) {
                        Destroy(comp);
                    }
                }
            }

            if (Input.GetButtonDown("Fire1")) {
                Application.LoadLevel("Minigame 1");
            } else if (Input.GetButtonDown("Fire2")) {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                #endif
            }
        } else {
            //Check if the round is over

            if (EnemiesToSpawn.Count == 0 && TurretManager.NumAlive() == 0 && !roundTimer.active) {
                roundTimer.Start();
            }

            roundTimer.Update();

            //Check if we are done waiting
            if (roundTimer.Complete()) {
                roundTimer.Stop();
                roundTimer.Restart();

                this.currentRound++;

                TurretManager.ClearSpawned();

                player.GetComponent<Player>().health = 25f;

                this.ReadRoundData(currentRound);

                if (TurretManager.NumAlive() == 0 && EnemiesToSpawn.Count == 0) {
                    player.GetComponent<Player>().won = true;
                    healthBar.SetActive(false);

                    if (!fading) {
                        fading = true;
                        StartCoroutine(player.GetComponent<Player>().WinAnimation());

                        //Disable turrets that are still alive
                        StartCoroutine(TurretManager.KillAll());

                        //Disable powers
                        GameObject[] hands = new GameObject[2];
                        hands[0] = left;
                        hands[1] = right;

                        //remove preset powers
                        foreach (GameObject hand in hands) {
                            foreach (Component comp in hand.GetComponents(typeof(MonoBehaviour))) {
                                Destroy(comp);
                            }
                        }
                    }
                }
            }
        }
	}

	private void ReadRoundData(int roundNumber) {
        int counter = 1;
        XmlTextReader reader = new XmlTextReader (Application.dataPath + "/Resources/RoundData.xml");
        reader.ReadToFollowing("round");
        int NumToSpawn = 0;

        EnemiesToSpawn.Clear();

        while (reader.Read())
        {
        	if (counter == roundNumber) {
	      		if(reader.NodeType == XmlNodeType.Element) 
	    		{
	    			switch (reader.Name) 
	           		{
	           	      	case "cap":
	           	      		reader.Read();
	            			ENEMY_CAP = int.Parse(reader.Value);
	            			break;
	                	case "basic":
	                		reader.Read();
	            			NumToSpawn = int.Parse(reader.Value);
	            			AddEnemyToList("basic", NumToSpawn);
	            			break;
	            		case "spiral":
	            			reader.Read();
	    	        		NumToSpawn = int.Parse(reader.Value);
	    	        		AddEnemyToList("spiral", NumToSpawn);
	            			break;
                        case "tracking":
                            reader.Read();
                            NumToSpawn = int.Parse(reader.Value);
                            AddEnemyToList("tracking", NumToSpawn);
                            break;
                        case "boss":
                            reader.Read();
                            NumToSpawn = int.Parse(reader.Value);
                            AddEnemyToList("boss", NumToSpawn);
                            break;
                        case "round":
	            			counter++;
	            			break;
	            	}		
	    		}
        	}
        	else{
	        	reader.ReadToFollowing("round");
	            counter++;
	        	continue;
        	}
        }
	}

	private void AddEnemyToList(String EnemyType, int NUM){
		for(int i = 0; i < NUM; i++){
			this.EnemiesToSpawn.Add(EnemyType);
		}
	}

	private Vector3 GetRandomValidPosition(String EnemyType){
		bool foundPosition = false;
		float floorLevel = ground.transform.lossyScale.y / 2;
		while(!foundPosition) 
		{
			Vector3 pos = new Vector3(UnityEngine.Random.Range(-14.0f, 14.0f), floorLevel + 0.7f + UnityEngine.Random.Range(0.2f, 0.6f), UnityEngine.Random.Range(-14.0f, 14.0f));
			foreach (GameObject obj in TurretManager.turrets)
			{
				if (Vector3.Distance(pos, obj.transform.position) <= 3.0f)
				{
					continue;
				}
			}
			if (Vector3.Distance(pos, TurretManager.target.transform.position) >= 10.0f)
			{
				if (EnemyType == "spiral") {
					pos = new Vector3(pos.x, floorLevel + 0.7f, pos.z);
				}
				else if (EnemyType == "boss"){
					pos = new Vector3(pos.x, floorLevel + 1.7f + 0.5f, pos.z);
				}
				else if (EnemyType == "tracking"){
					pos = new Vector3(pos.x, floorLevel + 0.7f + UnityEngine.Random.Range(0.1f, 0.5f), pos.z);
				}
				return pos;
			}
		}
		return Vector3.zero;
	}

	public void RemoveWalls() {
		foreach(Transform child in ground.transform)
		{
			child.gameObject.SetActive (false);
		}
	}

	public void SetPowers() {
		bool debugging = false;
		if (!debugging) {
			
			GameObject[] hands = new GameObject[2];
			hands [0] = left;
			hands [1] = right;

			//remove preset powers
			foreach (GameObject hand in hands) {
				foreach (Component comp in hand.GetComponents(typeof(MonoBehaviour))) {
					Destroy (comp);
				}
			}

			//add powers from betweenScenes
			left.AddComponent(Type.GetType(betweenScenes.defenseChosen));
			right.AddComponent (Type.GetType(betweenScenes.offenseChosen));
		}
	}
}
