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

    public ShopManager shopRef;
    public int[] equippedWeapons = new int[3];
    public ShopWeapon[] weaponsList = new ShopWeapon[7];
    private GameObject player;
    public GameObject canvas;
    public GameObject questObj;

    private GameManager gm;

    // Use this for initialization
    void Awake () {    
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        questObj = GameObject.FindGameObjectWithTag("questobj");
		if(SceneManager.GetActiveScene().name == "Shoptest")
        {
            shopRef = GameObject.FindGameObjectWithTag("shopmanager").GetComponent<ShopManager>();
            
            updateWeapons();
        }
    }
    void Start(){
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        //print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            if(!Cursor.visible){
                Cursor.visible = true;
            }
            shopRef = GameObject.FindGameObjectWithTag("shopmanager").GetComponent<ShopManager>();
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<QuestManager>().enabled = false;
            questObj.SetActive(false);
            if(canvas){
                canvas.SetActive(false);
            }
            if (player)
            {
                player.SetActive(false);
            }
        }else{
            if(Cursor.visible){
                Cursor.visible = false;
            }
            gameObject.GetComponent<QuestManager>().enabled = true;
            questObj.SetActive(true);
            if (player)
            {
                player.SetActive(true);
            }
            if(canvas){
                canvas.SetActive(true);
            }
        }
	}

    public void updateShop()
    {
        for(int i = 0; i < weaponsList.Length; i++)
        {
            if (weaponsList[i].purchased)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchased = true;
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().WeaponAttachments.SetActive(true);
                if(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchaseButton != null)
                {
                    Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().purchaseButton);
                }
                
            }
            if (weaponsList[i].stockUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgraded = true;
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stock.SetActive(false);
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockButton);
            }
            if (weaponsList[i].scopeUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgraded = true;
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scope.SetActive(false);
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeButton);
            }
            if (weaponsList[i].magazineUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded = true;
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazine.SetActive(false);
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineButton);
            }
            if (weaponsList[i].barrelUpgraded)
            {
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded = true;
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrel.SetActive(false);
                shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgrade.SetActive(true);
                Destroy(shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelButton);
            }
        }
    }

    public void updateWeapons()
    {
        for(int i =0; i < weaponsList.Length; i++)
        {
            weaponsList[i].name = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().name;
            weaponsList[i].objName = shopRef.weaponRefArray[i].name;

            //temporay to fill in the gaps where these weapons aren't active on the player
            //TODO: Add these weapons to the player and remove this switch statement
            switch(shopRef.weaponRefArray[i].name){
                case "Heavy_SMG":
                    weaponsList[i].objName = "MAG_LMG";
                    break;
                case "AR_Rifle":
                    weaponsList[i].objName = "AK_Rifle";
                    break;
                default:
                    break;
            }
            weaponsList[i].magazineUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().magazineUpgraded;
            weaponsList[i].scopeUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().scopeUpgraded;
            weaponsList[i].stockUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().stockUpgraded;
            weaponsList[i].barrelUpgraded = shopRef.weaponRefArray[i].GetComponent<WeaponInfo>().barrelUpgraded;
        }
        for(int j = 0; j < 3; j++){
            if(shopRef.equippedWeapons[j] != -1){
                 equippedWeapons[j] = shopRef.equippedWeapons[j];
                 gm.playerWeapons[j] = equippedWeapons[j];
            }
           
        }

    }
}
