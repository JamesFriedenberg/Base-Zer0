using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

	public float health = 50f;

    private GameObject player;
    private GameManager gm;

    public List<GameObject> ammo;
    public GameObject scraps;

    private bool flag = true;

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
        setKinematic(true);
        player = GameObject.FindGameObjectWithTag("gm");
        gm = player.GetComponent<GameManager>();
    }

    public void TakeDamage(float amount){
        health -= amount;

        if (flag)
        {
            if (health <= 0)
            {
                Die();
                flag = false;
            }
        }
        
    }
    private void Die()
    {
        this.GetComponent<NavMeshAgent>().speed = 0f;
        setKinematic(false);
        GetComponentInChildren<Animator>().enabled = false;
        CapsuleCollider capCollider = this.GetComponent<CapsuleCollider>();
        if(capCollider != null){
            capCollider.enabled = false;
        }
        SpawnResource();
        Destroy(gameObject, 5);
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
