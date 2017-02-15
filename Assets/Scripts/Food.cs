using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public int fatCalories;
	public int carbCalories;
	public int proteinCalories;
	public enum FoodType {Bread};


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Initialize(int fat, int carbs, int protein, Sprite foodSprite){
		fatCalories = fat;
		carbCalories = carbs;
		proteinCalories = protein;
		GetComponent<SpriteRenderer> ().sprite = foodSprite;
	}

}
