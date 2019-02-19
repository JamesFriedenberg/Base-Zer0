using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public float speed = 10;
    public int damage = 100;
    public float explosionRange = 2.0f;
    public GameObject particleTrail;
    public GameObject impactExplosion;
	
    void Start(){
        particleTrail.GetComponent<ParticleSystem>().Play();
    }

	// Update is called once per frame
	void Update () {
		this.transform.position = this.transform.position + (this.transform.forward * speed) * Time.deltaTime;
	}
    void OnCollisionEnter(Collision collision)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("target");
        for(int i = 0; i < enemies.Length; i++){
            float distance = Vector3.Magnitude(enemies[i].transform.position - this.transform.position);
            if(distance <= explosionRange){
                float myDamage = ((Mathf.Pow(explosionRange,2) - Mathf.Pow(distance,2)) / Mathf.Pow(explosionRange,2)) * damage;
                enemies[i].GetComponent<Target>().TakeDamage(myDamage);
            }
        }
        GameObject explosion = Instantiate(impactExplosion, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
