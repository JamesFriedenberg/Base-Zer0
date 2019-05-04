using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour {

    public GameObject player;

    public int normalAmmoAmount = 20;
    public int scrapBoxAmount = 5;
    public int smallCashAmount = 10;
    public string ammoType;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");

	}
}
