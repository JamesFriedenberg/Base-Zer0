using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UnityStandardAssets.Characters.FirstPerson;

public class DroneController : MonoBehaviour
{

    private GameObject gm;
    public GameObject player;
    public NavMeshAgent zombie;

    public GameObject bullet;

    public float wanderRadius;
    public float wanderTimer;

    private float wanderTime;
    private float timer;

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 200;
    public bool playerInRange;

    public float distanceToSeekPlayer = 30f;
    public float height = 70f;
    private float firingRange = 30f;

    public bool shootFlag = true;
    private float offsetTimer = 1.5f;
    private float offsetValue = 2f;

    private bool deathFlag = true;
    public float health = 50f;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gm");
        wanderRadius = 80f;
        wanderTime = wanderTimer;
        player = GameObject.FindWithTag("Player");
        zombie = this.GetComponent<NavMeshAgent>();
        zombie.stoppingDistance = 2f;
        //zombie.baseOffset = Random.Range(30f, 40f);
        zombie.baseOffset = 15f;
        if (this.GetComponent<NavMeshAgent>().isOnNavMesh == false || Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 30f)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
      
    }
    private void OnTriggerExit(Collider other)
    {
       
    }
    public void takeDamage(float damage)
    {
        health -= damage;
    }
    public static Vector3 randomWanderDirection(Vector3 origin, float dist, int layer)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;

        randDir += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layer);

        return navHit.position;
    }
    void Update()
    {

        this.transform.LookAt(player.transform);

        if (deathFlag == true)
        {
            if (health <= 0f)
            {
                Die();
                deathFlag = false;
            }
        }
        wanderTime += Time.deltaTime;
        timer += Time.deltaTime;

        
        offsetTimer -= Time.deltaTime;

        //zombie.baseOffset += offsetValue * Time.deltaTime;
        //if (offsetTimer <= 0f)
        //{
        //    //if (this.gameObject.transform.position.y < 2f)
        //    //{
        //    //    offsetValue = (Random.Range(7f, 9f));
        //    //    offsetTimer = 2f;

        //    //}
        //    if (Random.Range(0.0f, 1.0f) > 0.5f)
        //    {
        //        offsetValue = -(Random.Range(7f, 9f));
        //        offsetTimer = 2f;
        //    }
        //    else
        //    {
        //        offsetValue = (Random.Range(7f, 9f));
        //        offsetTimer = 2f;

        //    }
        //}



        float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        int playerHealth = player.GetComponent<PlayerHandler>().GetHealth();

        zombie.SetDestination(player.transform.localPosition);
        //if (distanceToPlayer > distanceToSeekPlayer)
        //{

        //    if (wanderTime >= wanderTimer)
        //    {

        //        Vector3 newDir = randomWanderDirection(transform.position, wanderRadius, 1);
        //        zombie.SetDestination(newDir);
        //        wanderTime = 0;
        //    }
        //}
        if (distanceToPlayer < distanceToSeekPlayer)
        {

            zombie.SetDestination(player.transform.localPosition);
            if (distanceToPlayer < 20f)
            {
                if (shootFlag)
                {
                    Debug.Log("ShootyPooty");
                    StartCoroutine(shoot());
                    shootFlag = false;
                }
            }
        }

    }
    public void Die()
    {
        Destroy(gameObject, 1);

    }
    IEnumerator shoot()
    {
        if (player.GetComponent<PlayerHandler>().GetHealth() > 0)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(3f);
            shootFlag = true;
        }
    }

}