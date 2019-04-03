using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {

    public GameObject playerRef;

    public GameObject enemyType;
    public float timeBetweenSpawns = 2f;

    private bool coroutineFired = false;

    public bool spawnLimitedEnemies = false;
    public int enemiesToSpawn;

    public float minSpawnDistance = 15f;
    public float maxSpawnDistance = 250f;
	// Use this for initialization
	void Start () {

        playerRef = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
        if (!coroutineFired)
        {
            coroutineFired = true;

            if (!spawnLimitedEnemies)
            {

                StartCoroutine(Spawn());
            }
            else if (spawnLimitedEnemies)
            {
                StartCoroutine(SpawnLimited());
            }

        }

    }
    IEnumerator Spawn()
    {

        //if (Vector3.Distance(playerRef.transform.position, this.gameObject.transform.position) < maxSpawnDistance && Vector3.Distance(playerRef.transform.position, this.gameObject.transform.position) > minSpawnDistance)
        //{



            Instantiate(enemyType, transform.position + transform.forward, transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
            Debug.Log("lalalala");
            coroutineFired = false;



            





        //}




    }
    IEnumerator SpawnLimited()
    {

        
        //if (Vector3.Distance(playerRef.transform.position, this.gameObject.transform.position) < maxSpawnDistance && Vector3.Distance(playerRef.transform.position, this.gameObject.transform.position) > minSpawnDistance)
        //{
            for (int i = 0; i < enemiesToSpawn; i++)
            {

                //if (Vector3.Dot(playerRef.transform.forward, (this.transform.position - playerRef.transform.position).normalized) < 0f)
                //{
                    Instantiate(enemyType, transform.position + transform.forward, transform.rotation);
                    yield return new WaitForSeconds(timeBetweenSpawns);

               // }
                //Debug.Log("3hunnit");
            }

        //}     
        
    }
}
