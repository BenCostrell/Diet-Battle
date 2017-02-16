using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int playerNum;
	public float speed;
	private Rigidbody2D rb;
	public GameObject platePrefab;
	public float plateOffset;
	public float plateSpeed;
	public int platesInHolster;
	public GameObject plate1UI;
	public GameObject plate2UI;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();	
		platesInHolster = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Shoot_P" + playerNum) && (platesInHolster > 0)) {
			ShootPlate ();
		}
	}

	void FixedUpdate() {
		Move ();
	}

	void Move(){
		float y = Input.GetAxis ("Vertical_P" + playerNum);
		float x = Input.GetAxis ("Horizontal_P" + playerNum);
		Vector2 inputDirection = new Vector2 (x, y);
		rb.velocity = speed * inputDirection;
		float angle = Mathf.Atan2 (y, x);
		transform.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
	}

	void ShootPlate(){
		platesInHolster -= 1;
		UpdatePlateUI ();
		float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		Vector3 rotationVector = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
		GameObject plate = Instantiate (platePrefab, transform.position + (rotationVector * plateOffset), Quaternion.identity);
		plate.GetComponent<Rigidbody2D> ().velocity = plateSpeed * rotationVector;
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject go = collider.gameObject;
		if (go.tag == "Plate") {
			if (platesInHolster < 2) {
				platesInHolster += 1;
				UpdatePlateUI ();
				Destroy (go);
			}
		}
	}

	void UpdatePlateUI(){
		if (platesInHolster == 2) {
			plate1UI.SetActive (true);
			plate2UI.SetActive (true);
		} else if (platesInHolster == 1) {
			plate1UI.SetActive (true);
			plate2UI.SetActive (false);
		} else {
			plate1UI.SetActive (false);
			plate2UI.SetActive (false);
		}
	}

}
