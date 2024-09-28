using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [Header("weapon Stats")]
    public float fireRate = 0;
    protected PlayerController player { get => PlayerController.instance; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void HeldUpdate()
    {
        
    }
}
