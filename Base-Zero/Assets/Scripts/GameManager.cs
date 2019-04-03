using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public struct ShopWeapon
	{
		public bool stockUpgraded;
		public bool scopeUpgraded;
		public bool barrelUpgraded;
		public bool magazineUpgraded;
		public bool purchased;
		public string name;
	}

    private Dictionary<string, int> weaponAmmo = new Dictionary<string, int>();
    public int playerCash = 0;
    public int playerScraps = 0;
    public int startingAmmo = 500;

    public int startingPlayerHealth = 1000;
    public int currentPlayerHealth = 1000;
    public int currentWeapon = 0;
    public int[] ammoInWeapons;
	public bool[] stockList = new bool[44];
    public Vector3 startPosition = Vector3.zero;

    //list of weapon indices from player weapon array
    public int[] playerWeapons = new int[3];

    public static GameManager instance;

    void Start(){
        weaponAmmo.Add("LMG", startingAmmo);
        weaponAmmo.Add("AR", startingAmmo);
		//updateWeapons ();
    }
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            DestroyImmediate(gameObject);
        }

    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            //SceneManager.LoadScene("Shop");
        }
    }

	void updateWeapons(){
		for (int i = 0; i < 44; i++) {
			stockList [i] = false;
			//weaponsList [i].scopeUpgraded = false;
			//weaponsList [i].barrelUpgraded = false;
			//weaponsList [i].magazineUpgraded = false;
			//weaponsList[i].purchased =false;
		}
		//weaponsList [1].purchased = true;
	}

	void fromSceneWeapons(){
		if (GameObject.FindGameObjectWithTag ("Player") == null) {
			return;
		}

		PlayerHandler playerGuns = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHandler> ();
			for (int i = 0; i < 44; i++) {
			if (playerGuns.allWeapons [i] == null) {
				continue;
			}
			//playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[2] = weaponsList [i].scopeUpgraded;
			//playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[3] = weaponsList [i].barrelUpgraded;
			playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[0] = stockList [i];
			playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().FindStats (playerGuns.allWeapons [i]);
			}

	}

    public int GetPlayerCash(){
        return playerCash;
    }
    public int GetPlayerScraps(){
        return playerScraps;
    }
    public void AddScraps(int scraps){
        playerScraps += scraps;
    }
    public void AddCash(int cash){
        playerCash += cash;
    }
    public int CheckAmmo(string ammoType){
        return weaponAmmo[ammoType];
    }
    public void AddAmmo(string ammoType, int ammoCount){
        weaponAmmo[ammoType] += ammoCount;
        Mathf.Clamp(weaponAmmo[ammoType], 0, 2000);
    }
}
