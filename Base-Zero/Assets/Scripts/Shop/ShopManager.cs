using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    //Main Shop Screen Array
    public GameObject[] weaponRefArray;

    public GameObject scrapText;
    public int scrap;
    public GameObject currentWeapon;
    public int[] equippedWeapons;
    public GameObject[] equipSlots;
	// Use this for initialization
	void Start () {
        //GameManager gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
        //equippedWeapons = gm.playerWeapons;
        UpdateCashScrap();
    }
	
	// Update is called once per frame
	void Update () {

        
		
	}

    public void hideScreen(GameObject screenToHide)
    {
        screenToHide.SetActive(false);
    }
    public void unhideScreen(GameObject screenToUnhide)
    {
        screenToUnhide.SetActive(true);
    }


    public void UpdateCashScrap()
    {
        //cashText.GetComponent<Text>().text = "$" + cash;
        scrapText.GetComponent<Text>().text = "%" + scrap;
    }

    public void ToggleEquips()
    {
        for(int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].SetActive(false);
        }
    }

    public void CheckWeaponSlots()
    {
        if (currentWeapon == null || currentWeapon.GetComponent<WeaponInfo>().purchased == false)
        {
            ToggleEquips();
        } else
        {
            EnableEquipSlots();
        }
    }

    public void ClearWeaponSlot()
    {
        currentWeapon = null;
    }

    public void EnableEquipSlots()
    {
        if(currentWeapon == null)
        {
            return;
        }
        if(currentWeapon.tag == "pistol")
        {
            if(equippedWeapons[0] == -1 || weaponRefArray[equippedWeapons[0]] == null || currentWeapon.GetComponent<WeaponInfo>().name != weaponRefArray[equippedWeapons[0]].GetComponent<WeaponInfo>().name)
            {
                equipSlots[0].SetActive(true);
            }
        } else
        {
            //currentWeapon.GetComponent<WeaponInfo>().name != equippedWeapons[1].GetComponent<WeaponInfo>().name && currentWeapon.GetComponent<WeaponInfo>().name != equippedWeapons[2].GetComponent<WeaponInfo>().name
            if ((equippedWeapons[1] == -1 && equippedWeapons[2] == -1) || 
            (equippedWeapons[1] == -1 && currentWeapon.GetComponent<WeaponInfo>().name != weaponRefArray[equippedWeapons[2]].GetComponent<WeaponInfo>().name) || 
            (equippedWeapons[2] == -1 && currentWeapon.GetComponent<WeaponInfo>().name != weaponRefArray[equippedWeapons[1]].GetComponent<WeaponInfo>().name) || 
            currentWeapon.GetComponent<WeaponInfo>().name != weaponRefArray[equippedWeapons[1]].GetComponent<WeaponInfo>().name && currentWeapon.GetComponent<WeaponInfo>().name != weaponRefArray[equippedWeapons[2]].GetComponent<WeaponInfo>().name)
            {
                equipSlots[1].SetActive(true);
                equipSlots[2].SetActive(true);
            }
        }
    }
}
