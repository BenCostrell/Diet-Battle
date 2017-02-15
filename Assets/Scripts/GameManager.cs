using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Vector3 spawnP1;
	public Vector3 spawnP2;

	// Use this for initialization
	void Start () {
		InitializePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializePlayers(){
		GameObject player1 = Instantiate (playerPrefab, spawnP1, Quaternion.identity) as GameObject;
		PlayerController pc1 = player1.GetComponent<PlayerController> ();
		pc1.playerNum = 1;
		player1.GetComponent<SpriteRenderer> ().color = Color.red;

		GameObject player2 = Instantiate (playerPrefab, spawnP2, Quaternion.identity) as GameObject;
		PlayerController pc2 = player2.GetComponent<PlayerController> ();
		pc2.playerNum = 2;
		player2.GetComponent<SpriteRenderer> ().color = Color.blue;
	}
}
