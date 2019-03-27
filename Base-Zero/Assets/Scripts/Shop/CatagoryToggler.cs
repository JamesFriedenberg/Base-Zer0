using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryToggler : MonoBehaviour {
	GameObject[] weapons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ToggleActiveWeapons(int index){
		for (int i = 0; i < weapons.Length; i+= 2) {
			if (i != index) {
				weapons [i].SetActive (false);
				weapons [i + 1].SetActive (false);
			}
		}

		weapons [index].SetActive (true);
		weapons [index + 1].SetActive (true);
	}
}
