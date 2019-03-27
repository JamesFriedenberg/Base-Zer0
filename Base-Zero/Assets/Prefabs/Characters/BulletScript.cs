using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private GameObject player;
    public float speed;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
      
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y  -1 , player.transform.position.z);
        transform.rotation = Quaternion.LookRotation(playerPos);
        transform.LookAt(player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(player.transform.position);
        this.transform.position += transform.forward * speed * Time.deltaTime;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHandler>().TakeDamage(100);
            Destroy(this.gameObject);
        }
    }
}
