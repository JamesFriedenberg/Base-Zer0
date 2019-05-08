using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

	public float health = 50f;
    public GameObject zombie;
    private GameObject player;
    private GameManager gm;

    public List<GameObject> ammo;
    public GameObject scraps;

    private bool flag = true;

    public GameObject slowZombieRef;
    private ZombieController zombieController;
    private DroneController droneController;

    private bool zombIsHere = false;
    private bool droneIsHere = false;
    private void setKinematic(bool val)
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = val;        
        }
    }
    void Start()
    {
        if(slowZombieRef.GetComponent<ZombieController>()){
            zombieController = slowZombieRef.GetComponent<ZombieController>();
            droneController = null;
            zombIsHere = true;
        }
        else if (slowZombieRef.GetComponent<DroneController>()){
            droneController = slowZombieRef.GetComponent<DroneController>();
            Debug.Log("TAKEYH");

            zombieController = null;
            droneIsHere = true;
        }

        setKinematic(true);
        player = GameObject.FindGameObjectWithTag("gm");
        gm = player.GetComponent<GameManager>();
    }

    public void TakeDamage(float amount){
        if (droneIsHere) {
            droneController.takeDamage(amount);
        }
        else if (zombIsHere)
        {
            zombieController.takeDamage(amount);

        }
    }

    void DoSpawn(GameObject spawn){
        if(spawn == null) return;
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.y -= .85f;
        Instantiate(spawn, spawnPosition, Quaternion.identity);
    }
    void SpawnResource(){
        DoSpawn(scraps);
        return;
        int spawnState = (int)(Random.Range(0f,3f));
        switch(spawnState){
            case 1:
                if(ammo.Count == 0) return;
                int ammoType = (int)(Random.Range(0f, (float)ammo.Count));
                DoSpawn(ammo[ammoType]);
                break;
            case 2:
                if(scraps == null) return;
                DoSpawn(scraps);
                break;
            default:
                break;
        }
    }
}
