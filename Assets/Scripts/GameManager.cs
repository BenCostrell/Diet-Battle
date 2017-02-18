using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject platePrefab;

	public Vector3 spawnP1;
	public Vector3 spawnP2;

	public GameObject plate1_p1;
	public GameObject plate2_p1;
	public GameObject plate1_p2;
	public GameObject plate2_p2;

	public GameObject calorieUI_p1;
	public GameObject calorieUI_p2;

	public GameObject bigUIText;
	public bool gameOver;


	// Use this for initialization
	void Start () {
		InitializePlayers ();
		bigUIText.SetActive (false);
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Reset")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	void InitializePlayers(){
		InitializePlayer (spawnP1, 1, Color.red, plate1_p1, plate2_p1, calorieUI_p1);
		InitializePlayer (spawnP2, 2, Color.blue, plate1_p2, plate2_p2, calorieUI_p2);
	}

	void InitializePlayer(Vector3 spawnLocation, int playerNum, Color spriteColor, GameObject plate1, GameObject plate2, GameObject calorieUI){
		GameObject player = Instantiate (playerPrefab, spawnLocation, Quaternion.identity) as GameObject;
		PlayerController pc = player.GetComponent<PlayerController> ();
		pc.playerNum = playerNum;
		player.GetComponent<SpriteRenderer> ().color = spriteColor;
		pc.platePrefab = platePrefab;
		pc.plate1UI = plate1;
		pc.plate2UI = plate2;
		Transform[] macroUIArray = calorieUI.GetComponentsInChildren<Transform> ();
		pc.fatUI = macroUIArray [1].gameObject;
		pc.carbUI = macroUIArray [2].gameObject;
		pc.proteinUI = macroUIArray [3].gameObject;
		pc.UpdateCalorieUI ();
	}

	public void GameWin(int playerNum){
		bigUIText.SetActive (true);
		bigUIText.GetComponent<Text> ().text = "PLAYER " + playerNum + " WINS";
		gameOver = true;
	}

	public void UnbalancedDietMessage(int playerNum){
		bigUIText.SetActive (true);
		bigUIText.GetComponent<Text> ().text = "UNBALANCED DIET";
		Invoke ("ResetUIText", 1f);
	}

	void ResetUIText(){
		bigUIText.SetActive (false);
	}
}
