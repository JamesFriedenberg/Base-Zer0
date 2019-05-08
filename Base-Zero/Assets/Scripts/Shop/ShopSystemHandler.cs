using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSystemHandler : MonoBehaviour {
	public struct ShopWeapon
	{
		public bool[] scopeUpgraded;
		public int activeScope;
		public int scopeImageNum;
		public bool[] receiverUpgraded;
		public int activeReceiver;
		public bool stockUpgraded;
		public bool barrelUpgraded;
		public bool magazineUpgraded;
		public bool purchased;
		public string name;
		public string objName;
		public bool valid;
	}

	int timesAwoken = 0;
    public ShopManager shopRef;
    public int[] equippedWeapons = new int[3];
	public ShopWeapon[] weaponsList = new ShopWeapon[44];
    private GameObject player;
    public GameObject canvas;
    public GameObject questObj;

    private GameManager gm;

    // Use this for initialization
    void Awake () {
		timesAwoken++;
		Debug.Log ("ss" + timesAwoken);
		if(SceneManager.GetActiveScene().name == "Shop")
        {
            shopRef = GameObject.FindGameObjectWithTag("shopmanager").GetComponent<ShopManager>();
            
            //updateShop();
        }
    }
    void Start(){
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            if(!Cursor.visible){
                Cursor.visible = true;
            }
            shopRef = GameObject.FindGameObjectWithTag("shopmanager").GetComponent<ShopManager>();
            Cursor.lockState = CursorLockMode.None;

        }else{
            if(Cursor.visible){
                Cursor.visible = false;
            }


        }
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//Debug.Log("Level Loaded");
		Debug.Log(scene.name);
		Debug.Log(mode);
		if(SceneManager.GetActiveScene().name == "Shop")
		{
			shopRef = GameObject.FindGameObjectWithTag("shopmanager").GetComponent<ShopManager>();

			updateShop();
		}
	}

    public void updateShop()
    {
		Debug.Log (gm.playerScraps);
		shopRef.scrap = gm.playerScraps;
		shopRef.UpdateCashScrap ();
		for(int j = 0; j < 3; j++){
            if(gm.playerWeapons[j] == -1)
            {
                continue;
            }
			shopRef.equippedWeapons [j] = gm.playerWeapons [j];
			shopRef.equipSlots [j].GetComponent<EquipWeapon>().weaponSlot.GetComponentInChildren<Text>().text = shopRef.weaponRefArray [shopRef.equippedWeapons [j]].GetComponent<WeaponInfo> ().name;
		}

		for(int i = 0; i < gm.weaponsList.Length; i++)
        {
			
			if ( shopRef.weaponRefArray[i] == null) {
				continue;
			}
			//Debug.Log (i + " " + gm.weaponsList[i].purchased);
			if (gm.weaponsList[i].purchased)
            {
				
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchased = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().WeaponAttachments != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().WeaponAttachments.SetActive(true);
				}
                
                if(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchaseButton != null)
                {
                    Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchaseButton);
                }
                
            }
			if (shopRef.weaponRefArray [i] == null || shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().WeaponAttachments == null) {
				continue;
			}
			if (gm.weaponsList[i].stockUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().stock != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stock.SetActive(false);
				}    
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgrade.SetActive(true);
				Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockButton.GetComponent<WeaponButton>().upgradeText);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockButton);
            }
			if (gm.weaponsList[i].magazineUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().magazine != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazine.SetActive(false);
				}    
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgrade.SetActive(true);
				Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineButton.GetComponent<WeaponButton>().upgradeText);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineButton);
            }
			if (gm.weaponsList[i].barrelUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().barrel != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrel.SetActive(false);
				}    
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgrade.SetActive(true);
				Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelButton.GetComponent<WeaponButton>().upgradeText);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelButton);
            }

			if (gm.weaponsList [i].scopeUpgraded != null && shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeButton.Length != 0) {
				shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeUpgraded = gm.weaponsList [i].scopeUpgraded;
                Debug.Log(gm.weaponsList[i].name);
				shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeButton[gm.weaponsList[i].activeScope].GetComponent<WeaponButton>().UpdateScope(shopRef.weaponRefArray [i], gm.weaponsList[i].activeScope);
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeActiveNum = gm.weaponsList[i].activeScope;
			}
			if (gm.weaponsList [i].receiverUpgraded != null) {
				shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().receiverUpgraded = gm.weaponsList [i].receiverUpgraded;
				shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().receiverButton[gm.weaponsList[i].activeReceiver].GetComponent<WeaponButton>().UpdateReceiver(shopRef.weaponRefArray [i], gm.weaponsList[i].activeReceiver);
			}
	

        }
    }

    public void updateWeapons()
    {
		gm.playerScraps = shopRef.scrap;
		for(int i =0; i <gm.weaponsList.Length; i++)
        {
			if ( shopRef.weaponRefArray[i] == null) {
				continue;
			}
				
			gm.weaponsList[i].purchased = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchased;
			gm.weaponsList[i].magazineUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded;
			gm.weaponsList[i].stockUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgraded;
			gm.weaponsList[i].barrelUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded;

			gm.weaponsList [i].scopeUpgraded = shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeUpgraded;
			
			gm.weaponsList [i].activeScope = shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeActiveNum;
			gm.weaponsList [i].scopeImageNum = shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeNum [shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scopeActiveNum];


			gm.weaponsList [i].receiverUpgraded = shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().receiverUpgraded;
			gm.weaponsList [i].activeReceiver = shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().activeReceiver;
        }
        for(int j = 0; j < 3; j++){
			equippedWeapons[j] = shopRef.equippedWeapons[j];
			gm.playerWeapons[j] = equippedWeapons[j];
        }
    }
}
