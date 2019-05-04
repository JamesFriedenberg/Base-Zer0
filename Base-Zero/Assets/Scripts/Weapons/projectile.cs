using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public float speed = 10;
    public int damage = 100;
    public float explosionRange = 3.0f;
    public GameObject particleTrail;
    public GameObject impactExplosion;
    public Transform emitter;

    private float destroyTimer;

    void Start()
    {
        particleTrail.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        this.transform.position = this.transform.position + (this.transform.forward * speed) * Time.deltaTime;

        if(destroyTimer > 15){
            DoDestroy();
        }
    }
    void DoDestroy(){
        GameObject explosion = Instantiate(impactExplosion, gameObject.transform.position, Quaternion.identity);
        emitter.GetComponent<ParticleSystem>().Stop();
        emitter.parent = null;
        Destroy(emitter.gameObject, 5.0f);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Magnitude(enemies[i].transform.position - this.transform.position);
            if (distance <= explosionRange)
            {
                float myDamage = ((Mathf.Pow(explosionRange, 2) - Mathf.Pow(distance, 2)) / Mathf.Pow(explosionRange, 2)) * damage;
                if (enemies[i].GetComponent<ZombieController>())
                {
                    enemies[i].GetComponent<ZombieController>().takeDamage(myDamage);
                }
            }
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float pDistance = Vector3.Magnitude(player.transform.position - this.transform.position);
        if (pDistance <= explosionRange * 1.5f)
        {
            float myDamage = ((Mathf.Pow(explosionRange * 1.5f, 2) - Mathf.Pow(pDistance, 2)) / Mathf.Pow(explosionRange * 1.5f, 2)) * damage * 4;
            if (player.GetComponent<PlayerHandler>())
            {
                player.GetComponent<PlayerHandler>().TakeDamage((int)myDamage);
            }
        }
        DoDestroy();
    }
}
