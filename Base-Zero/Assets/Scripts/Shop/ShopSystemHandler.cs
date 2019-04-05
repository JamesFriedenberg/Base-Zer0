using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSystemHandler : MonoBehaviour {
    
    public struct ShopWeapon
    {
        public bool stockUpgraded;
        public bool scopeUpgraded;
        public bool barrelUpgraded;
        public bool magazineUpgraded;
        public bool purchased;
        public string name;
        public string objName;
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
		Debug.Log("Level Loaded");
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
		for(int j = 0; j < 3; j++){
			shopRef.equippedWeapons [j] = gm.playerWeapons [j];
		}

		for(int i = 0; i < gm.weaponsList.Length; i++)
        {
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
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockButton);
            }
			/*if (gm.weaponsList[i].scopeUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().scope != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scope.SetActive(false);
				}                
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeButton);
            }*/
			if (gm.weaponsList[i].magazineUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().magazine != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazine.SetActive(false);
				}    
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineButton);
            }
			if (gm.weaponsList[i].barrelUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded = true;
				if (shopRef.weaponRefArray [i].GetComponent<WeaponInfo> ().barrel != null) {
					shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrel.SetActive(false);
				}    
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelButton);
            }
        }
    }

    public void updateWeapons()
    {
		for(int i =0; i <gm.weaponsList.Length; i++)
        {
			if (i != 1 && i != 10 && i != 16 && i != 18 && i != 19 && i != 20 && i != 21 && i != 35) {
				continue;
			}

			gm.weaponsList[i].purchased = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchased;
			gm.weaponsList[i].magazineUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded;
			//gm.weaponsList[i].scopeUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgraded;
			gm.weaponsList[i].stockUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgraded;
			gm.weaponsList[i].barrelUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded;
        }
        for(int j = 0; j < 3; j++){
			equippedWeapons[j] = shopRef.equippedWeapons[j];
			gm.playerWeapons[j] = equippedWeapons[j];
        }
    }
}
