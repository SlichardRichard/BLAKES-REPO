using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{

        public PlayerController player;
        public NavMeshAgent Agent;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool canSeePlayer;

        [Header("enemyStats")]
        public int health = 6;
        public int maxHealth = 9;
        public int damagegiven = 1;
        public int damagerecived = 1;
        public float pushBackForce = 10000;

        
        [Header("FovStats")]
        public float radius;
        [Range(0, 360)]
        public float angle;


        private void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();

            
            Agent = GetComponent<NavMeshAgent>();
        }

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
   
        private void OnTriggerEnter(Collider other)
        {
            //shoot
            if (other.gameObject.tag == "shot")
            {
                health -= damagerecived;

            }
            if (health <= 0)
            {
                Destroy(gameObject);

            }
            //punch
            if (other.gameObject.tag == "Fists")
            {
                health -= damagerecived;
            }


        }
}
