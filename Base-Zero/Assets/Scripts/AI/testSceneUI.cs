using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class testSceneUI : MonoBehaviour {

    // Use this for initialization

    public GameObject dummy;

    public GameObject[] testEnemies;

    public InputField distToSeek;
    public InputField wanderRad;
    public InputField zombieSpeed;
    public Text godeModeStatus;

    public bool godMode;
	void Start () {

        godMode = false;
        wanderRad.text = "50";
        zombieSpeed.text = "3.5";
       
        distToSeek.text = "20";

	}
	
	// Update is called once per frame
	void Update () {

        testEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i <testEnemies.Length; i++)
        {
            testEnemies[i].GetComponent<ZombieController>().wanderRadius = float.Parse(wanderRad.text);

            testEnemies[i].GetComponent<NavMeshAgent>().speed = float.Parse(zombieSpeed.text);
            testEnemies[i].GetComponent<ZombieController>().distanceToSeekPlayer = float.Parse(distToSeek.text);

            if (Input.GetKeyDown(KeyCode.G))
            {
                godMode = true;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                godMode = false;
            }

            if (godMode)
            {
                testEnemies[i].GetComponent<ZombieController>().player = dummy;
                godeModeStatus.text = "God Mode Enabled (H Disables)";
            }
            if(!godMode)
            {
                testEnemies[i].GetComponent<ZombieController>().player = GameObject.FindGameObjectWithTag("Player");
                godeModeStatus.text = "God Mode Disabled (G Enables)";

            }

        }


    }
}
