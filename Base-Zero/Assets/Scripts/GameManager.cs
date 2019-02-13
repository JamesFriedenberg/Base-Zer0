using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Dictionary<string, int> weaponAmmo = new Dictionary<string, int>();
    public int playerCash = 0;
    public int playerScraps = 0;
    public int startingAmmo = 500;

    public int startingPlayerHealth = 1000;
    public int currentPlayerHealth = 1000;
    public int currentWeapon = 0;
    public int[] ammoInWeapons;

    //list of weapon indices from player weapon array
    public int[] playerWeapons = new int[3];

    public static GameManager instance;

    void Start(){
        weaponAmmo.Add("LMG", startingAmmo);
        weaponAmmo.Add("AR", startingAmmo);
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
            SceneManager.LoadScene(5);
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
