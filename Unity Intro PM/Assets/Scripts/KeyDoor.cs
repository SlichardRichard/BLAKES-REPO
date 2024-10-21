using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    public PlayerController playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Opendoor()
    {
        if (playerData.HasKey == true && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            playerData.HasKey = false;
        }
    
    
        
    }
    
}
