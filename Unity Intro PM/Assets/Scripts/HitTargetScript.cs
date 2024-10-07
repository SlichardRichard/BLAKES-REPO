using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargetScript : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject ExitDoor;

    [Header("enemyStats")]
    public int health = 6;
    public int maxHealth = 9;
    public int damagegiven = 1;
    public int damagerecived = 1;
    public float pushBackForce = 10000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            StartCoroutine(GameManager.TargetElim());
            Destroy(gameObject);
            ExitDoor.SetActive(true);
        }
        //punch
        if (other.gameObject.tag == "Fists")
        {
            health -= damagerecived;
        }


    }
}

