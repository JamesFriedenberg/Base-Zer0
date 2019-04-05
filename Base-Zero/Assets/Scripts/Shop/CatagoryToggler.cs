using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryToggler : MonoBehaviour {
	public GameObject[] weapons;
    public GameObject[] catagories;
    public GameObject[] equips;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleActiveWeapons(int index){
		for (int i = 0; i < weapons.Length; i+= 2) {
			if (i != index) {
				if (weapons [i] != null) {
					weapons [i].SetActive (false);
				}
				if (weapons [i+1] != null) {
					weapons [i+1].SetActive (false);
				}					
			}
		}
        if(index != -1)
        {
            weapons[index].SetActive(true);
            weapons[index + 1].SetActive(true);
        }
		
	}

    public void ToggleCatagories(int index)
    {
        for (int i = 0; i < catagories.Length; i++)
        {
            if (i != index)
            {
                if (catagories[i] != null)
                {
                    catagories[i].GetComponent<CatagoryToggler>().ToggleActiveWeapons(-1);
                    catagories[i].SetActive(false);
                }
            }
        }

        if (index != -1 && catagories[index] != null)
        {
            catagories[index].SetActive(true);
        }
    }

    public void ResetEquips()
    {
        foreach(GameObject g in equips)
        {
            g.SetActive(false);
        }
    }
}
