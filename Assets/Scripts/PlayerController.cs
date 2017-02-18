using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float macroToUIScaleFactor;
	public int playerNum;
	public float speed;
	private Rigidbody2D rb;
	public GameObject platePrefab;
	public float plateOffset;
	public float plateSpeed;
	public int platesInHolster;
	public GameObject plate1UI;
	public GameObject plate2UI;
	public GameObject fatUI;
	public GameObject carbUI;
	public GameObject proteinUI;
	private int calorieDrainRate;
	private float timeBetweenCalorieDrain;
	private float timeUntilCalorieDrain;
	private int totalCaloriesNeeded;
	private float wiggleRoom;

	public GameManager gameManager;

	private int fat;
	private int carbs;
	private int protein;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();	
		platesInHolster = 2;
		fat = 0;
		carbs = 0;
		protein = 0;
		macroToUIScaleFactor = 0.02f;
		timeBetweenCalorieDrain = 2f;
		calorieDrainRate = 1;
		totalCaloriesNeeded = 300;
		wiggleRoom = 0.05f;
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Shoot_P" + playerNum) && (platesInHolster > 0)) {
			ShootPlate ();
		}
		if (timeUntilCalorieDrain > 0) {
			timeUntilCalorieDrain -= Time.deltaTime;
		} else {
			DrainCalories ();
			timeUntilCalorieDrain = timeBetweenCalorieDrain;
		}
		CheckDiet ();
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

	void DrainCalories(){
		if (fat > 0) {
			fat -= calorieDrainRate;
		}
		if (carbs > 0) {
			carbs -= calorieDrainRate;
		}
		if (protein > 0) {
			protein -= calorieDrainRate;
		}
		UpdateCalorieUI ();
	}

	void CheckDiet(){
		bool balancedDiet = true;
		int totalCalories = fat + carbs + protein;
		float oneThird = 1f / 3f;
		if (totalCalories >= totalCaloriesNeeded) {
			float fatFloat = fat;
			float fatProportion = fatFloat / totalCalories;
			if ((fatProportion < oneThird - wiggleRoom) || (fatProportion > oneThird + wiggleRoom)) {
				balancedDiet = false;
			}
			float carbFloat = carbs;
			float carbProportion = carbFloat / totalCalories;
			if ((carbProportion < oneThird - wiggleRoom) || (carbProportion > oneThird + wiggleRoom)) {
				balancedDiet = false;
			}
			float proteinFloat = protein;
			float proteinProportion = proteinFloat / totalCalories;
			if ((proteinProportion < oneThird - wiggleRoom) || (proteinProportion > oneThird + wiggleRoom)) {
				balancedDiet = false;
			}
			if (balancedDiet) {
				gameManager.GameWin (playerNum);
			} else {
				fat = fat / 2;
				carbs = carbs / 2;
				protein = protein / 2;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject go = collider.gameObject;
		if (go.tag == "Plate") {
			Plate plate = go.GetComponent<Plate> ();
			if (plate.loaded) {
				EatFoodOnPlate (plate);
			}
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

	void EatFoodOnPlate(Plate plate){
		Food food = plate.GetComponentInChildren<Food> ();
		fat += food.nutrition.fatCalories;
		carbs += food.nutrition.carbCalories;
		protein += food.nutrition.proteinCalories;
		UpdateCalorieUI ();
		plate.loaded = false;
		Destroy (food.gameObject);
	}

	public void UpdateCalorieUI(){
		fatUI.transform.localScale = new Vector3 (1, fat * macroToUIScaleFactor, 1);
		carbUI.transform.localScale = new Vector3 (1, carbs * macroToUIScaleFactor, 1);
		proteinUI.transform.localScale = new Vector3 (1, protein * macroToUIScaleFactor, 1);

		carbUI.transform.position = new Vector3(carbUI.transform.position.x, 
			fatUI.GetComponent<SpriteRenderer> ().bounds.max.y, carbUI.transform.position.z);
		proteinUI.transform.position = new Vector3 (proteinUI.transform.position.x, 
			carbUI.GetComponent<SpriteRenderer> ().bounds.max.y, proteinUI.transform.position.z);
	}

}
