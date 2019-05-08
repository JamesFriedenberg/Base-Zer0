using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
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

    private Dictionary<string, int> weaponAmmo = new Dictionary<string, int>();
    public int playerCash = 0;
    public int playerScraps = 5000;
    public int startingAmmo = 500;

    public int startingPlayerHealth = 1000;
    public int currentPlayerHealth = 1000;
    public int currentWeapon = 0;
    public int[] ammoInWeapons;
	public ShopWeapon[] weaponsList = new ShopWeapon[44];
    public Vector3 startPosition = Vector3.zero;

    //list of weapon indices from player weapon array
    public int[] playerWeapons = new int[3];

    public static GameManager instance;

    void Start(){
        weaponAmmo.Add("LMG", startingAmmo);
        weaponAmmo.Add("AR", startingAmmo);
		Debug.Log ("Game manager");
		//updateWeapons ();
    }
    void Awake()
    {

		//fromSceneWeapons ();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            DestroyImmediate(gameObject);
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
		//fromSceneWeapons ();
	}
		
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene("Shop");
        }
    }

	public void updateWeapons(){
		PlayerHandler playerGuns = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHandler> ();
		for (int i = 0; i < 44; i++) {
			if (playerGuns.allWeapons [i] != null) {
				weaponsList [i].stockUpgraded = false;
				//weaponsList [i].scopeUpgraded = false;
				weaponsList [i].barrelUpgraded = false;
				weaponsList [i].magazineUpgraded = false;
				weaponsList [i].purchased = false;
				weaponsList [i].valid = true;
				weaponsList [i].name = playerGuns.allWeapons [i].name;
			}
		}
		//weaponsList [1].purchased = true;
	}

	public void fromSceneWeapons(){
		if (GameObject.FindGameObjectWithTag ("Player") == null) {
			return;
		}
		PlayerHandler playerGuns = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHandler> ();
			for (int i = 0; i < 44; i++) {
			if (playerGuns.allWeapons [i] == null) {
				continue;
			}
			//Debug.Log ("Updating guns");
			//playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[2] = weaponsList [i].scopeUpgraded;
			playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[3] = weaponsList [i].barrelUpgraded;
			playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[0] = weaponsList [i].stockUpgraded;
			playerGuns.allWeapons[i].GetComponentInChildren<weapon>().myUpgrades[1] = weaponsList [i].magazineUpgraded;
            Debug.Log(weaponsList[i].name + " : " + weaponsList[i].scopeImageNum);
			switch (weaponsList [i].scopeImageNum) {
			case 0:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.None;
				break;
			case 1:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.HoloSight;
				break;
			case 2:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.KobraSight;
				break;
			case 3:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.CCOSight;
				break;
			case 4:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.ACOG;
				break;
			case 5:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.PKA;
				break;
			case 6:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.SniperScope;
				break;
			case 7:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myScope = weapon.Scope.PSO;
				break;
			default:
				break;
			}

			switch (weaponsList [i].activeReceiver) {
			case 0:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myReciever = weapon.Reciever.Default;
				break;
			case 1:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myReciever = weapon.Reciever.FasterFire;
				break;
			case 2:
				playerGuns.allWeapons [i].GetComponentInChildren<weapon> ().myReciever = weapon.Reciever.HigherDamage;
				break;
			default:
				break;
			}

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
