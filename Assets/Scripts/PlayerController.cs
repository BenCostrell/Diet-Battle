using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int playerNum;
	public float speed;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
