using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Wall") || (other.CompareTag("Ground")) || (other.CompareTag("basic enemy")) || (other.CompareTag("KeyDoor")))
            Destroy(gameObject);
    }
}
