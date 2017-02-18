using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public GameObject foodPrefab;
	private Dictionary<Sprite, Nutrition> foodDict;
	public List<Sprite> foodSprites;
	private List<Food> activeFoodList;
	public Vector2 xBounds;
	public Vector2 yBounds;
	public float minAcceptableDistance;
	public int initialNumFood;

	// Use this for initialization
	void Start () {
		InitializeFoodDict ();
		InitializeFoodList ();
		GenerateInitialFoodSetup ();
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void InitializeFoodDict(){
		foodDict = new Dictionary<Sprite, Nutrition> ();
		foodDict.Add (foodSprites [0], new Nutrition (0, 10, 0));
		foodDict.Add (foodSprites [1], new Nutrition (0, 0, 10));
		foodDict.Add (foodSprites [2], new Nutrition (10, 0, 0));
	}

	void InitializeFoodList(){
		activeFoodList = new List<Food> ();
	}

	void GenerateInitialFoodSetup(){
		for (int i = 0; i < initialNumFood; i++) {
			GenerateFood ();
		}
	}


	void GenerateFood(){
		int foodIndex = Random.Range (0, foodSprites.Count);
		Sprite foodSprite = foodSprites [foodIndex];
		Vector3 location = GenerateValidLocation ();
		GameObject food = Instantiate (foodPrefab, location, Quaternion.identity);
		Nutrition nutritionalFacts;
		foodDict.TryGetValue (foodSprite, out nutritionalFacts);
		Food foodScript = food.GetComponent<Food> ();
		foodScript.Initialize (foodSprite, nutritionalFacts, this);
		activeFoodList.Add (foodScript);
	}

	Vector2 GenerateValidLocation(){
		Vector2 location = GenerateRandomLocation ();
		bool isValid = ValidateLocation (location);
		while (!isValid) {
			location = GenerateRandomLocation ();
			isValid = ValidateLocation (location);
		}
		return location;
	}

	bool ValidateLocation(Vector2 location){
		if (activeFoodList.Count > 0) {
			foreach (Food food in activeFoodList) {
				if (Vector2.Distance (location, food.transform.position) < minAcceptableDistance) {
					return false;
				}
			}
		}
		return true;
	}

	Vector2 GenerateRandomLocation(){
		float x = Random.Range (xBounds.x, xBounds.y);
		float y = Random.Range (yBounds.x, yBounds.y);
		Vector2 location = new Vector2 (x, y);
		return location;
	}

	public void RemoveFood(Food food){
		activeFoodList.Remove (food);
		GenerateFood ();
	}
}
