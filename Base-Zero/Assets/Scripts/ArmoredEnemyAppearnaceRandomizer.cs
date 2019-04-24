using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemyAppearnaceRandomizer : MonoBehaviour {
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
		int choiceIndex = Random.Range (3, 5);
		//int rigChoice = Random.Range (0, 2);
		//Enable rig
		//if (rigChoice == 1) {
			
		//}
		torsoItems [0].SetActive (true);
		//Choose chest
		switch (choiceIndex) {
		//Longleeve snow
		case 3:
			torsoItems [1].SetActive (true);
			LegsChoice (3);
			break;
		//Longleeve wood
		case 4:
			torsoItems [2].SetActive (true);
			LegsChoice (4);
			break;
		default:
			break;
			
		}
	}

	void LegsChoice(int choice){
		int choiceIndex = 0;
		switch (choice) {
			//Longleeve snow
		case 3:
			choiceIndex = Random.Range (1, 5);
			switch (choiceIndex) {
			//Pants long snow
			case 1:
				legItems [0].SetActive (true);
				HeadgearChoice (1);
				break;
			//Pants bloused snow
			case 2:
				legItems [2].SetActive (true);
				HeadgearChoice (1);
				break;
			case 3:
				legItems [1].SetActive (true);
				HeadgearChoice (1);
				break;
				//Pants long wood
			case 4:
				legItems [3].SetActive (true);
				HeadgearChoice (1);
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
				HeadgearChoice (2);
				break;
				//Pants long wood
			case 2:
				legItems [3].SetActive (true);
				HeadgearChoice (2);
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
			choiceIndex = 1;
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [0].SetActive (true);
				AccessoryChoice (1);
				break;
			default:
				break;
			}
			break;
		//wood short sleeve
		//wood pants bloused
		case 2:
			choiceIndex = Random.Range (1, 3);
			switch (choiceIndex) {
			//cap
			case 1:
				headItems [1].SetActive (true);
				AccessoryChoice (2);
				break;
				//patrol cap
			case 2:
				headItems [2].SetActive (true);
				AccessoryChoice (2);
				break;
				break;
			default:
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
			choiceIndex = Random.Range (1, 4);
			altChoiceIndex = Random.Range (1, 4);
			if (choiceIndex == 1) {
				accItems [0].SetActive (true);
				accItems [1].SetActive (true);
			} else if (choiceIndex == 2) {
				accItems [1].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [3].SetActive (true);
			} else if (altChoiceIndex == 2 && choiceIndex != 1) {
				accItems [4].SetActive (true);
			}
			break;
		case 2:
			choiceIndex = Random.Range (1, 4);
			altChoiceIndex = Random.Range (1, 4);
			if (choiceIndex == 1) {
				accItems [0].SetActive (true);
				accItems [2].SetActive (true);
			} else if (choiceIndex == 2) {
				accItems [2].SetActive (true);
			}
			if (altChoiceIndex == 1) {
				accItems [3].SetActive (true);
			} else if (altChoiceIndex == 2 && choiceIndex != 1) {
				accItems [4].SetActive (true);
			}
			break;
		default:
			break;

		}
	}
}
