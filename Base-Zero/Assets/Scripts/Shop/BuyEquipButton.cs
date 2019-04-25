using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyEquipButton : MonoBehaviour {
    public GameObject attachmentList;
    public GameObject shopManager;
	public WeaponInfo gunInfo;
	// Use this for initialization
	void Start () {
		GetComponentInChildren<Text> ().text = "$" + gunInfo.weaponCost.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buyEquipWeapon(GameObject weapon)
    {
        WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();

        if (!weaponRef.purchased)
        {
            if (shopManager.GetComponent<ShopManager>().scrap >= weaponRef.weaponCost)
            {
                shopManager.GetComponent<ShopManager>().scrap -= weaponRef.weaponCost;
                weaponRef.purchased = true;
                if(weapon.tag != "pistol" && attachmentList != null)
                {
                    attachmentList.SetActive(true);
                }
                
                shopManager.GetComponent<ShopManager>().UpdateCashScrap();
                shopManager.GetComponent<ShopManager>().EnableEquipSlots();
                Destroy(gameObject);
            }
        }
    }
}
