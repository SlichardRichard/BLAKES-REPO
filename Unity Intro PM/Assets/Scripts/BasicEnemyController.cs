using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{

        public PlayerController player;
        public NavMeshAgent Agent;

        //public GameObject playerRef;

        //public LayerMask targetMask;
        //public LayerMask obstructionMask;

        //public bool canSeePlayer;

        [Header("enemyStats")]
        public int health = 6;
        public int maxHealth = 9;
        public int damagegiven = 1;
        public int damagerecived = 1;
        public float pushBackForce = 10000;

        /*
        [Header("FovStats")]
        public float radius;
        [Range(0, 360)]
        public float angle;


        private void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        /*private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    */


        void Start()
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
