using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpawnTEST : MonoBehaviour {

    public GameObject zombiePrefab;
    public float rad = 200;
    public int maxEnemiesInScene = 25;
    public float timeBetweenSpawns = 1.5f;
    private GameObject player;
    private bool flag;

    private GameObject[] enemies;
	// Use this for initialization
	void Start () {
        flag = true;
        player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
       // Debug.Log(enemies.Length);

        if (flag)
        {
            StartCoroutine(spawnEm());
            flag = false;
        }
		
	}

    public Vector3 randomPointOnCircleEdge(float radius)
    {
        // Vector3 vec = Random.insideUnitCircle.normalized * radius;
        Vector2 randPos = Random.insideUnitCircle.normalized * radius;
        Vector3 vec = player.transform.position + new Vector3(randPos.x, player.transform.position.y, randPos.y);
        
        return vec;
    }
    IEnumerator spawnEm()
    {

        if(enemies.Length < maxEnemiesInScene)
        {
            Instantiate(zombiePrefab, randomPointOnCircleEdge(rad), transform.rotation);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
            flag = true;
      

    }
}
