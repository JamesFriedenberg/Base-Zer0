using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour {
	

	public class QuestData{
		public int currentQuestIndex;
		public int questItemOne;
		public int questItemTwo;
		public int questItemThree;
		public int questItemFour;
		public int questItemFive;
		public int questItemSix;
	}

	public class PlayerData{
		public int scrapCount;
		public int equippedWeaponOne;
		public int equippedWeaponTwo;
		public int equippedWeaponThree;
		public int ammoPistol;
		public int ammoSMG;
		public int ammoShotgun;
		public int ammoSniper;
		public int ammoAR;
		public int ammoMR;
		public int ammoLMG;
		public int ammoEXP;
	}

	public class WeaponData{
		public bool purchased;
		public bool magUpgraded;
		public bool stockUpgraded;
		public bool barrelUpgraded;
		public bool[] scopeUpgraded;
		public int activeScope;
		public bool[] receiverUpgraded;
		public int activeReceiver;
        public int scopeImageNum;
		public string datapath;
	}

	public QuestData questInfo;
	public PlayerData playerInfo;
	public WeaponData[] gunInfo;
	string questPath;
	string playerPath;
	void Start ()
	{
		questInfo = new QuestData();
		playerInfo = new PlayerData ();
		gunInfo = new WeaponData[44];

		QuestManager qm = GetComponent<QuestManager> ();
		questInfo.currentQuestIndex = qm.questIndex;
		questPath = Path.Combine(Application.persistentDataPath, "QuestData.txt");

		GameManager gm = GetComponent<GameManager> ();
		playerInfo.scrapCount = gm.playerScraps;
		playerInfo.equippedWeaponOne = gm.playerWeapons [0];
		playerInfo.equippedWeaponTwo = gm.playerWeapons [1];
		playerInfo.equippedWeaponThree = gm.playerWeapons [2];
		playerPath = Path.Combine(Application.persistentDataPath, "PlayerData.txt");

		gm.updateWeapons ();
		for (int i = 0; i < gunInfo.Length; i++) {
			
			if (gm.weaponsList[i].valid) {
				Debug.Log (gm.weaponsList [i].purchased);
				gunInfo [i] = new WeaponData ();
				gunInfo [i].purchased = gm.weaponsList [i].purchased;
				gunInfo [i].magUpgraded = gm.weaponsList [i].magazineUpgraded;
				gunInfo [i].stockUpgraded = gm.weaponsList [i].stockUpgraded;
				gunInfo [i].barrelUpgraded = gm.weaponsList [i].barrelUpgraded;
				gunInfo [i].scopeUpgraded = gm.weaponsList [i].scopeUpgraded;
				gunInfo [i].activeScope = gm.weaponsList [i].activeScope;
				gunInfo [i].receiverUpgraded = gm.weaponsList [i].receiverUpgraded;
				gunInfo [i].activeReceiver = gm.weaponsList [i].activeReceiver;
                gunInfo[i].scopeImageNum = gm.weaponsList[i].scopeImageNum;
                string gunString = gm.weaponsList [i].name + "Data.txt";
				gunInfo[i].datapath = Path.Combine(Application.persistentDataPath, gunString);
			}
		}
		//dataPath = Path.Combine(Application.persistentDataPath, "BZData.txt");
	}

	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("loading");
            LoadGame();
        }

           
               
	}

	public void SaveGame(){
		QuestManager qm = GetComponent<QuestManager> ();
		questInfo.currentQuestIndex = qm.questIndex;

		GameManager gm = GetComponent<GameManager> ();
		playerInfo.scrapCount = gm.playerScraps;
		playerInfo.equippedWeaponOne = gm.playerWeapons [0];
		playerInfo.equippedWeaponTwo = gm.playerWeapons [1];
		playerInfo.equippedWeaponThree = gm.playerWeapons [2];
		for (int i = 0; i < gunInfo.Length; i++) {
			if (gm.weaponsList[i].valid) {
				gunInfo [i].purchased = gm.weaponsList [i].purchased;
				gunInfo [i].magUpgraded = gm.weaponsList [i].magazineUpgraded;
				gunInfo [i].stockUpgraded = gm.weaponsList [i].stockUpgraded;
				gunInfo [i].barrelUpgraded = gm.weaponsList [i].barrelUpgraded;
				gunInfo [i].scopeUpgraded = gm.weaponsList [i].scopeUpgraded;
				gunInfo [i].activeScope = gm.weaponsList [i].activeScope;
                gunInfo[i].scopeImageNum = gm.weaponsList[i].scopeImageNum;
                gunInfo [i].receiverUpgraded = gm.weaponsList [i].receiverUpgraded;
				gunInfo [i].activeReceiver = gm.weaponsList [i].activeReceiver;
			}
		}

		SaveQuest (questInfo, questPath);
		SavePlayer (playerInfo, playerPath);

		for (int i = 0; i < gunInfo.Length; i++) {
			if (gunInfo [i] != null) {
				SaveWeapon (gunInfo [i], gunInfo [i].datapath);
			}
		}
	}

	public void LoadGame(){
		questInfo = LoadQuestData (questPath);
		QuestManager qm = GetComponent<QuestManager> ();
		qm.questIndex = questInfo.currentQuestIndex;

		playerInfo = LoadPlayerData (playerPath);
		GameManager gm = GetComponent<GameManager> ();
		gm.playerScraps = playerInfo.scrapCount;
		gm.playerWeapons [0] = playerInfo.equippedWeaponOne;
		gm.playerWeapons [1] = playerInfo.equippedWeaponTwo;
		gm.playerWeapons [2] = playerInfo.equippedWeaponThree;

		for (int i = 0; i < gunInfo.Length; i++) {
			if (gunInfo [i] != null) {
				Debug.Log ("Loading gun " + 1);
				gunInfo [i] = LoadWeaponData (gunInfo [i].datapath);
				gm.weaponsList [i].purchased = gunInfo [i].purchased;
				gm.weaponsList [i].magazineUpgraded = gunInfo [i].magUpgraded;
				gm.weaponsList [i].stockUpgraded = gunInfo [i].stockUpgraded;
				gm.weaponsList [i].barrelUpgraded = gunInfo [i].barrelUpgraded;
				gm.weaponsList [i].scopeUpgraded = gunInfo [i].scopeUpgraded;
				gm.weaponsList [i].activeScope = gunInfo [i].activeScope;
                gm.weaponsList[i].scopeImageNum = gunInfo[i].scopeImageNum;
                gm.weaponsList [i].receiverUpgraded = gunInfo [i].receiverUpgraded;
				gm.weaponsList [i].activeReceiver = gunInfo [i].activeReceiver;
			}
		}

		gm.fromSceneWeapons ();
	}
	static void SaveQuest (QuestData data, string path)
	{
		string jsonString = JsonUtility.ToJson (data);

		using (StreamWriter streamWriter = File.CreateText (path))
		{
			streamWriter.Write (jsonString);
		}
	}

	static void SavePlayer (PlayerData data, string path)
	{
		string jsonString = JsonUtility.ToJson (data);

		using (StreamWriter streamWriter = File.CreateText (path))
		{
			streamWriter.Write (jsonString);
		}
	}

	static void SaveWeapon (WeaponData data, string path)
	{
		string jsonString = JsonUtility.ToJson (data);

		using (StreamWriter streamWriter = File.CreateText (path))
		{
			streamWriter.Write (jsonString);
		}
	}

	static QuestData LoadQuestData (string path)
	{
		using (StreamReader streamReader = File.OpenText (path))
		{
			string jsonString = streamReader.ReadToEnd ();
			return JsonUtility.FromJson<QuestData> (jsonString);
		}
	}

	static PlayerData LoadPlayerData (string path)
	{
		using (StreamReader streamReader = File.OpenText (path))
		{
			string jsonString = streamReader.ReadToEnd ();
			return JsonUtility.FromJson<PlayerData> (jsonString);
		}
	}

	static WeaponData LoadWeaponData (string path)
	{
		using (StreamReader streamReader = File.OpenText (path))
		{
			string jsonString = streamReader.ReadToEnd ();
			return JsonUtility.FromJson<WeaponData> (jsonString);
		}
	}


}
