using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;

using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    private GameObject gm;
    public GameObject player;
    public NavMeshAgent zombie;
    public GameObject zombieRef;
    public float wanderRadius;
    public float wanderTimer;

    public List<GameObject> ammo;
    public GameObject scraps;

    private float wanderTime;
    private float timer;

    private bool isDead;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 200;
    public bool playerInRange;

    public float distanceToSeekPlayer = 180f;

    private bool deathFlag = true;
    public float health = 40f;
    private void Start()
    {
        isDead = false;
        this.GetComponent<NavMeshAgent>().speed = Random.Range(9, 10);

        zombie = this.GetComponent<NavMeshAgent>();
        gm = GameObject.FindGameObjectWithTag("gm");
        wanderRadius = 50f;
        wanderTime = wanderTimer;
        player = GameObject.FindGameObjectWithTag("Player");
        zombie.stoppingDistance = 2f;
        zombie.angularSpeed = 40f;
        zombie.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        if (this.GetComponent<NavMeshAgent>().isOnNavMesh == false || Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 30f)
        {
            Destroy(gameObject);
        }

    }
    public void takeDamage(float damage)
    {
        health -= damage;
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject == player)
        //{
            
        //        playerInRange = true;

            

        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    public static Vector3 randomWanderDirection(Vector3 origin, float dist, int layer)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;

        randDir += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layer);

        return navHit.position;
    }
    void Update () {

        zombie.updateRotation = false;
        float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        int playerHealth = player.GetComponent<PlayerHandler>().GetHealth();
        //Debug.Log(distanceToPlayer);
        wanderTime += Time.deltaTime;
        timer += Time.deltaTime;

    

    

        if(distanceToPlayer > 35f)
        {
            zombie.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        }
        else
        {
            zombie.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;

        }
        if (distanceToPlayer < 2f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        if (playerInRange)
        {
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 2.5f;
            player.GetComponent<FirstPersonController>().m_RunSpeed = 5f;

        }
        else
        {
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 5f;
            player.GetComponent<FirstPersonController>().m_RunSpeed = 10f;
        }
        if (timer >= timeBetweenAttacks && playerInRange && playerHealth > 0 && isDead == false)
        {
            Attack();
            
            timer = 0f;
        }

        if (deathFlag == true)
        {
            transform.LookAt(player.transform);
            zombie.SetDestination(player.transform.localPosition);

            if (health <= 0f)
            {
                Die();

                deathFlag = false;
            }
        }
        //// player.GetComponent<PlayerHandler>().TakeDamage(100f);    

        // if(gm.GetComponent<QuestManager>().currentQuests[gm.GetComponent<QuestManager>().questIndex].tag == "q_defense")
        // {

        //     if(gm.GetComponent<QuestManager>().currentQuests[gm.GetComponent<QuestManager>().questIndex].GetComponent<DefendQuest>().getQuestStatus() == "defendTarget")
        //     {
        //         distanceToSeekPlayer = 180f;

        //     }
        // }
        // else
        // {
        //     distanceToSeekPlayer = 180f;
        // }

        // if (distanceToPlayer > distanceToSeekPlayer)
        // {

        //     if (wanderTime >= wanderTimer)
        //     {

        //         Vector3 newDir = randomWanderDirection(transform.position, wanderRadius, 1);
        //         zombie.SetDestination(newDir);
        //         wanderTime = 0;
        //     }
        // }
        // else if (distanceToPlayer < distanceToSeekPlayer)
        // {

        //     zombie.SetDestination(player.transform.localPosition);
        // }



    }
    private void Die()

    {
        isDead = true;
        Debug.Log(GetComponent<Transform>().position);
        Debug.Log(gameObject.name);
        SpawnResource();
        this.GetComponent<NavMeshAgent>().enabled = false;
        
        Debug.Log(GetComponent<Transform>().position);
        //Debug.Log(zombie.GetComponent<Transform>().position);
        //setKinematic(false);
        GetComponentInChildren<Animator>().SetInteger("death", (int)Random.Range(1, 6));
        GetComponent<Transform>().position = new Vector3(zombie.GetComponent<Transform>().position.x, zombie.GetComponent<Transform>().position.y - 0.8f, zombie.GetComponent<Transform>().position.z);

        //this.GetComponent<Transform>().localPosition = new Vector3(zombie.GetComponent<Transform>().localPosition.x, 30f, zombie.GetComponent<Transform>().localPosition.z);

        //CapsuleCollider capCollider = this.GetComponent<CapsuleCollider>();
        //if (capCollider != null)
        //{
        //    capCollider.enabled = false;
        //}

        //SpawnResource();
        Debug.Log(GetComponent<Transform>().position);
        Destroy(gameObject, 5);
    }
    private void Attack()
    {
        //if(gameObject.GetComponent<Target>().health <= 0) return;
        if(player.GetComponent<PlayerHandler>().GetHealth() > 0)
        {
            zombie.isStopped = true;

            Debug.Log("ATTACKING!!!!!!!");
            Vector3 playerPosition = player.GetComponent<Transform>().position;
            Vector3 zombiePosition = this.gameObject.GetComponent<Transform>().position;
            GetComponentInChildren<Animator>().Play("Attack");

            player.GetComponent<Rigidbody>().AddForce( Vector3.Normalize(playerPosition - zombiePosition) * 500);
            this.gameObject.GetComponent<AudioSource>().Play();


            player.GetComponent<PlayerHandler>().TakeDamage(attackDamage);
            zombie.isStopped = false;
        }
    }
    IEnumerator AttackEnum()
    {
        if (player.GetComponent<PlayerHandler>().GetHealth() > 0)
        {
            while (playerInRange)
            {
                zombie.enabled = false;
                Debug.Log("ATTACKING!!!!!!!");
                Vector3 playerPosition = player.GetComponent<Transform>().position;
                Vector3 zombiePosition = this.gameObject.GetComponent<Transform>().position;
                GetComponentInChildren<Animator>().Play("Attack");
                yield return new WaitForSeconds(1f);
                zombie.enabled = true;
                GetComponentInChildren<Animator>().Play("Walking");

                player.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(playerPosition - zombiePosition) * 800);
                this.gameObject.GetComponent<AudioSource>().Play();

                player.GetComponent<PlayerHandler>().TakeDamage(attackDamage / 2);


            }
            
        }
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
