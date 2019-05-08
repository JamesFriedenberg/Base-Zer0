using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    private GameObject player;
    private GameObject gameManager;
    private int playerHealth;
    private int playerAmmo;
    private int playerScraps;
    private int playerCash;

    public Text healthUIText;
    public Text ammoUIText;

    public GameObject resourceMenuBackground;
    public Text scrapsText;
    public Text cashText;

    public static UIHandler instance;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("gm");
        resourceMenuBackground.SetActive(false);    

    }

    // Update is called once per frame
    void Update () {

        player = GameObject.FindGameObjectWithTag("Player");
        if (Input.GetKey(KeyCode.Tab))
        {
            resourceMenuBackground.SetActive(true);
        }
        else
        {
            resourceMenuBackground.SetActive(false);
        }        
        PlayerHandler playerHandler = player.GetComponent<PlayerHandler>();
        playerAmmo = gameManager.GetComponent<GameManager>().CheckAmmo("AR");
        playerHealth = playerHandler.GetHealth();
        playerScraps = gameManager.GetComponent<GameManager>().GetPlayerScraps();
        playerCash = gameManager.GetComponent<GameManager>().GetPlayerCash();
        string currentAmmo = playerHandler.playerWeapons[playerHandler.currentWeapon].GetComponentInChildren<weapon>().currentAmmoCount.ToString();

        ammoUIText.text = currentAmmo + "/" + (playerAmmo).ToString();
        ammoUIText.fontSize = 75;
        healthUIText.text = (playerHealth / 10).ToString();
        scrapsText.text = (playerScraps).ToString();
        cashText.text = (playerCash).ToString();

	}
}
