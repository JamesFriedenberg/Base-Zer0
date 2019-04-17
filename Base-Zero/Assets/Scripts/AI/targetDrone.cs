using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class targetDrone : MonoBehaviour {

    // Use this for initialization
    public float health = 50f;
    public GameObject zombie;
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

    public void TakeDamage(float amount)
    {
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
        if (zombie == null) return;
        Debug.Log(zombie.GetComponent<Transform>().position);
        Debug.Log("here");
        this.GetComponent<NavMeshAgent>().speed = 0f;
        zombie.GetComponent<Transform>().localPosition = new Vector3(zombie.GetComponent<Transform>().localPosition.x, -0.7f, zombie.GetComponent<Transform>().localPosition.z);
        Debug.Log(zombie.GetComponent<Transform>().position);
        //setKinematic(false);
        //GetComponentInChildren<Animator>().SetInteger("death", (int)Random.Range(1, 6));
        CapsuleCollider capCollider = this.GetComponent<CapsuleCollider>();
        if (capCollider != null)
        {
            capCollider.enabled = false;
        }

        SpawnResource();
        Destroy(gameObject, 2);
    }
    void DoSpawn(GameObject spawn)
    {
        if (spawn == null) return;
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.y -= .85f;
        Instantiate(spawn, spawnPosition, Quaternion.identity);
    }
    void SpawnResource()
    {
        DoSpawn(scraps);
        return;
        int spawnState = (int)(Random.Range(0f, 3f));
        switch (spawnState)
        {
            case 1:
                if (ammo.Count == 0) return;
                int ammoType = (int)(Random.Range(0f, (float)ammo.Count));
                DoSpawn(ammo[ammoType]);
                break;
            case 2:
                if (scraps == null) return;
                DoSpawn(scraps);
                break;
            default:
                break;
        }
    }
}
