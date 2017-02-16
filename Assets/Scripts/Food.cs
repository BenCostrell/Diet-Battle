﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public Nutrition nutrition;
	private FoodManager foodManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize(Sprite foodSprite, Nutrition nutritionFacts, FoodManager fm){
		nutrition = nutritionFacts;
		GetComponent<SpriteRenderer> ().sprite = foodSprite;
		foodManager = fm;
	}

	public void LoadOntoPlate(Plate plate){
		transform.SetParent (plate.transform);
		transform.localPosition = Vector3.zero;
		foodManager.RemoveFood (this);
	}

}
