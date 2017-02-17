using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {

	public bool loaded;

	// Use this for initialization
	void Start () {
		loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject go = collider.gameObject;
		if ((go.tag == "Food") && !loaded) {
			Food food = go.GetComponent<Food> ();
			if (!food.onPlate) {
				loaded = true;
				go.GetComponent<Food> ().LoadOntoPlate (this);
			}
		}
	}


}
