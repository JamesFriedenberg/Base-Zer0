using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Image blood = this.gameObject.GetComponent<Image>();

        float opacity = 1.0f - (player.GetComponent<PlayerHandler>().currentPlayerHealth / 1000.0f);
        blood.color = new Color(blood.color.r,blood.color.g,blood.color.b,opacity);
	}
}
