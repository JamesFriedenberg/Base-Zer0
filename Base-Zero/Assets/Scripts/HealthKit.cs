using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour {

    public GameObject player;
    private GameObject gameManager;
    private GameManager gm;
    private int currentPlayerHealth;


    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        //take stats from gm at start of scene
        gameManager = GameObject.FindGameObjectWithTag("gm");
        gm = gameManager.GetComponent<GameManager>();
        currentPlayerHealth = gm.currentPlayerHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(currentPlayerHealth <= 90)
        {
            currentPlayerHealth += 10;
        }
        else
        {
            currentPlayerHealth += 100 - currentPlayerHealth;
        }

        Destroy(this.gameObject);
    }
}
