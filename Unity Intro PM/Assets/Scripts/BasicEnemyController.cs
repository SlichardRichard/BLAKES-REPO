using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{

    public PlayerController player;
    public NavMeshAgent Agent;

    [Header("enemyStats")]
    public int health = 3;
    public int maxHealth = 3;
    public int damagegiven = 1;
    public int damagerecived = 1;
    public float pushBackForce = 10000;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Agent = GetComponent<NavMeshAgent>();    
    }


    // Update is called once per frame
    void Update()
    {
        Agent.destination = player.transform.position;

        if (health <= 0)
            Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            health -= damagerecived;
            Destroy(gameObject);

        }
    }
}


