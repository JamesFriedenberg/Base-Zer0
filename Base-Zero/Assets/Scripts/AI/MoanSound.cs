using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoanSound : MonoBehaviour {

    public AudioClip moanSound;

    private AudioSource source;
    private float volLowRange = .75f;
    private float volHighRange = 1.0f;
    private int moanChance;



	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 10f){
            moanChance = Random.Range(0, 10);
            float vol = Random.Range(volLowRange, volHighRange);
            if(moanChance > 5)
            {
                source.PlayOneShot(moanSound, vol);
            }
        }
	}
}
