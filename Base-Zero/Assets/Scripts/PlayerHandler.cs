using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    //To move player between scenes use "SceneManager.MoveGameObjectToScene(Gameobject, Sceneto);

    public int startingPlayerHealth;
    public int currentPlayerHealth;
    private GameObject gameManager;
    public GameObject[] playerWeapons = new GameObject[3];
    public List<GameObject> allWeapons;
    public int[] weaponsFromGM;
    public int currentWeapon = 0;


    private float damageTimer = 0;
    private float healthAddTimer = 0;
    private GameManager gm;

    public static PlayerHandler instance;

    void Start()
    {
		//Debug.Log ("1");
        //take stats from gm at start of scene
        gameManager = GameObject.FindGameObjectWithTag("gm");
        gm = gameManager.GetComponent<GameManager>();
        currentWeapon = gm.currentWeapon;
        startingPlayerHealth = gm.startingPlayerHealth;
        currentPlayerHealth = gm.currentPlayerHealth;
        weaponsFromGM = gm.playerWeapons;
        if(!gm.GetComponent<GameManager>().startPosition.Equals(Vector3.zero)){
            this.transform.position = gm.GetComponent<GameManager>().startPosition;
        }

        if(gm.ammoInWeapons == null || gm.ammoInWeapons.Length == 0){
            gm.ammoInWeapons = new int[allWeapons.Count];
            for(int i = 0; i < gm.ammoInWeapons.Length; i++){
				if (allWeapons [i] == null) {
					continue;
				}

                gm.ammoInWeapons[i] = allWeapons[i].GetComponentInChildren<weapon>().currentAmmoCount;
            }
        }
		//Debug.Log ("2");

        if(weaponsFromGM[0] != -1){
            playerWeapons[0] = allWeapons[weaponsFromGM[0]];
        }
        if(weaponsFromGM[1] != -1){
            playerWeapons[1] = allWeapons[weaponsFromGM[1]];
        }
        if(weaponsFromGM[2] != -1){
            playerWeapons[2] = allWeapons[weaponsFromGM[2]];
        }
		//Debug.Log ("3");
        for (int i = 0; i < playerWeapons.Length; i++)
        {
            if (!playerWeapons[i]) continue;
            playerWeapons[i].SetActive(false);
        }
		//Debug.Log ("4");
        playerWeapons[currentWeapon].SetActive(true);
		//Debug.Log (playerWeapons [currentWeapon].name);
    }
    void Update()
    {
        
        if (Input.GetButton("Fire2") || playerWeapons[currentWeapon].GetComponentInChildren<weapon>().WillFire()) return;
        if (Input.GetKeyDown("1"))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            SwitchWeapon(2);
        }
        if(Input.GetKeyDown(KeyCode.L)){
            TakeDamage(100);
        }
        doHeal();
        if (currentPlayerHealth <= 0) onDeath();
    }
    void doHeal(){
        damageTimer += Time.deltaTime;
        healthAddTimer += Time.deltaTime;

        if(damageTimer < 5) return;
        if(currentPlayerHealth >= startingPlayerHealth) return;
        if(healthAddTimer >= 1){
            currentPlayerHealth += startingPlayerHealth / 50;
            if(currentPlayerHealth >= startingPlayerHealth){
                currentPlayerHealth = startingPlayerHealth;
            }
            gm.currentPlayerHealth = currentPlayerHealth;
            healthAddTimer = 0;
        }
    }
    private void SwitchWeapon(int weaponNumber)
    {
		
		if (currentWeapon == weaponNumber ||
		          !playerWeapons [weaponNumber]) {
			return;
		}
        if(playerWeapons[currentWeapon].GetComponentInChildren<weapon>().IsReloading()) return;
        playerWeapons[currentWeapon].SetActive(false);
        currentWeapon = weaponNumber;
        gm.currentWeapon = currentWeapon;
        playerWeapons[currentWeapon].SetActive(true);
        // playerWeapons[currentWeapon].GetComponentInChildren<weapon>().doEnable();

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
		//Debug.Log(mode);
		GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>().fromSceneWeapons ();
	}

    public int GetHealth()
    {
        return currentPlayerHealth;
    }
    public Transform getPlayerPosition()
    {
        return this.transform;
    }
    public void TakeDamage(int damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        gm.currentPlayerHealth = currentPlayerHealth;
        damageTimer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NormalAmmo")
        {
            gm.AddAmmo("AR", other.GetComponent<PickupHandler>().normalAmmoAmount);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ScrapBox")
        {

            gm.AddScraps(other.GetComponent<PickupHandler>().scrapBoxAmount);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "SmallCash")
        {
            gm.AddCash(other.GetComponent<PickupHandler>().smallCashAmount);
            Destroy(other.gameObject);
        }
    }
    private void onDeath()
    {
        SceneManager.LoadScene(3);
    }
}
