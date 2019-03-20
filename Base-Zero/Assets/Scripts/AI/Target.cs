using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

	public float health = 50f;

    private GameObject player;
    private GameManager gm;

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
        Destroy(gameObject, 5);
    }
}
