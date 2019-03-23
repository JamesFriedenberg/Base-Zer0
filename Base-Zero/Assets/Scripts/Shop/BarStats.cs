using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStats : MonoBehaviour {
	public Image statBar;
	public float stat;
	// Use this for initialization
	void Start () {
		statBar.fillAmount = (1.0f * (stat / 100.0f));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
