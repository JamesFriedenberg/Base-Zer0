using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearnaceRandomizer : MonoBehaviour {
	public GameObject[] torsoItems;
	public GameObject[] legItems;
	public GameObject[] headItems;
	public GameObject[] accItems;


	// Use this for initialization
	void Start () {
		TorsoChoice ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Debug.Log ("works");
			foreach(GameObject g in torsoItems){
				g.SetActive (false);
			}
			foreach(GameObject f in legItems){
				f.SetActive (false);
			}
			foreach(GameObject j in headItems){
				j.SetActive (false);
			}
			foreach(GameObject k in accItems){
				k.SetActive (false);
			}
			TorsoChoice ();
		}
	}

	void TorsoChoice(){
		int choiceIndex = Random.Range (1, 5);
		int rigChoice = Random.Range (0, 2);
		//Enable rig
		if (rigChoice == 1) {
			torsoItems [0].SetActive (true);
		}
		//Choose chest
		switch (choiceIndex) {
		//Shortsleeve snow
		case 1:
			torsoItems [1].SetActive (true);
			torsoItems [5].SetActive (true);
			LegsChoice (1);
			break;
		//Shortsleeve wood
		case 2:
			torsoItems [2].SetActive (true);
			torsoItems [5].SetActive (true);
			LegsChoice (2);
			break;
		//Longleeve snow
		case 3:
			torsoItems [3].SetActive (true);
			LegsChoice (3);
			break;
		//Longleeve wood
		case 4:
			torsoItems [4].SetActive (true);
			LegsChoice (4);
			break;
		default:
			break;
			
		}
	}

	void LegsChoice(int choice){
		int choiceIndex = 0;
		switch (choice) {
		//Shortsleeve snow
		case 1:
			legItems [0].SetActive (true);
			HeadgearChoice (1);
			break;
			//Shortsleeve wood
		case 2:
			legItems [1].SetActive (true);
			HeadgearChoice (2);
			break;
			//Longleeve snow
		case 3:
			choiceIndex = Random.Range (1, 3);
			switch (choiceIndex) {
			//Pants long snow
			case 1:
				legItems [0].SetActive (true);
				HeadgearChoice (3);
				break;
			//Pants bloused snow
			case 2:
				legItems [2].SetActive (true);
				HeadgearChoice (3);
				break;
			default:
				break;
			}
			break;
			//Longleeve wood
		case 4:
			choiceIndex = Random.Range (1, 3);
			switch (choiceIndex) {
			//Pants long snow
			case 1:
				legItems [1].SetActive (true);
				HeadgearChoice (4);
				break;
				//Pants long wood
			case 2:
				legItems [3].SetActive (true);
				HeadgearChoice (4);
				break;
			default:
				break;
			}
			break;
		default:
			break;

		}
	}

	void HeadgearChoice(int choice){
		int choiceIndex = 0;
		switch (choice) {
		//Snow short sleeve
		//snow pants bloused
		case 1:
			choiceIndex = Random.Range (1, 6);
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [6].SetActive (true);
				AccessoryChoice (1);
				break;
				//patrol cap
			case 2:
				headItems [0].SetActive (true);
				AccessoryChoice (1);
				break;
				//boonie hat
			case 3:
				headItems [4].SetActive (true);
				AccessoryChoice (1);
				break;
				//beanie hat
			case 4:
				headItems [7].SetActive (true);
				AccessoryChoice (2);
				break;
			default:
				AccessoryChoice (3);
				break;
			}
			break;
		//wood short sleeve
		//wood pants bloused
		case 2:
			choiceIndex = Random.Range (1, 6);
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [6].SetActive (true);
				AccessoryChoice (5);
				break;
				//patrol cap
			case 2:
				headItems [1].SetActive (true);
				AccessoryChoice (5);
				break;
				//boonie hat
			case 3:
				headItems [5].SetActive (true);
				AccessoryChoice (5);
				break;
				//beanie hat
			case 4:
				headItems [7].SetActive (true);
				AccessoryChoice (2);
				break;
			default:
				AccessoryChoice (3);
				break;
			}
			break;
		//snow long
		//pants
		case 3:
			choiceIndex = Random.Range (1, 8);
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [6].SetActive (true);
				AccessoryChoice (1);
				break;
				//patrol cap
			case 2:
				headItems [0].SetActive (true);
				AccessoryChoice (1);
				break;
				//boonie hat
			case 3:
				headItems [4].SetActive (true);
				AccessoryChoice (1);
				break;
				//beanie hat
			case 4:
				headItems [7].SetActive (true);
				AccessoryChoice (2);
				break;
				//mask
			case 5:
				headItems [2].SetActive (true);
				AccessoryChoice (3);
				break;
			case 6:
				headItems [8].SetActive (true);
				headItems [2].SetActive (true);
				break;
			default:
				AccessoryChoice (3);
				break;
			}
			break;
		case 4:
			choiceIndex = Random.Range (1, 8);
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [6].SetActive (true);
				AccessoryChoice (5);
				break;
				//patrol cap
			case 2:
				headItems [1].SetActive (true);
				AccessoryChoice (5);
				break;
				//boonie hat
			case 3:
				headItems [5].SetActive (true);
				AccessoryChoice (5);
				break;
				//beanie hat
			case 4:
				headItems [7].SetActive (true);
				AccessoryChoice (2);
				break;
				//mask
			case 5:
				headItems [3].SetActive (true);
				AccessoryChoice (4);
				break;
			case 6:
				headItems [8].SetActive (true);
				headItems [3].SetActive (true);
				break;
			default:
				AccessoryChoice (3);
				break;
			}
			break;
		default:
			break;

		}
	}

	void AccessoryChoice(int choice){
		int choiceIndex = 0;
		int altChoiceIndex = 0;
		switch (choice) {
		//Snow short sleeve
		//snow pants bloused
		case 1:
			choiceIndex = Random.Range (1, 3);
			altChoiceIndex = Random.Range (1, 3);
			if (choiceIndex == 1) {
				accItems [0].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [3].SetActive (true);
			}
			break;
		case 2:
			choiceIndex = Random.Range (1, 3);
			altChoiceIndex = Random.Range (1, 3);
			if (choiceIndex == 1) {
				accItems [5].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [2].SetActive (true);
			}
			break;
		case 3:
			choiceIndex = Random.Range (1, 3);
			if (choiceIndex == 1) {
				accItems [3].SetActive (true);
			}
			break;
		case 4:
			choiceIndex = Random.Range (1, 3);
			altChoiceIndex = Random.Range (1, 3);
			if (choiceIndex == 1) {
				accItems [4].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [3].SetActive (true);
			}
			break;
		case 5:
			choiceIndex = Random.Range (1, 3);
			altChoiceIndex = Random.Range (1, 3);
			if (choiceIndex == 1) {
				accItems [1].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [3].SetActive (true);
			}
			break;
		default:
			break;

		}
	}
}
