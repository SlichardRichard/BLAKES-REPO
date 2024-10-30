using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [NonSerialized]public List<Weapon> weapons = new List<Weapon>();
    public int heldIndex { get => _heldIndex; set
        {
            _heldIndex = value;
            RefreshWeapons();
        } }
    private int _heldIndex;
    public GameObject fists;
    public Weapon currentWeapon { get => weapons[heldIndex]; }


    [NonSerialized]public Rigidbody myRB;
    [NonSerialized]public Camera playerCam;

    Vector2 camRotation;
    public Transform Weaponslot;
    public LayerMask Keydoor;

    [Header("Player Stats")]
    public GameManager gameManager;
    public int maxHealth = 5;
    public int Health = 5;
    public int HealthRestore = 1;
    LayerMask layerMask;
    public bool canFire = true;
    public bool HasKey = false;

    [Header("Movement Settings")]
    public float speed = 8.0f;
    public float sprintMultiplier = 1.5f;
    public float jumpheight = 5.0f;
    public float GrounddetectDistance = 1.0f;
    public bool sprintMode = false;

    public float TimeSprint;

    [Header("Sprint Accelaration")]
    public float STARTacceltime = 8;
    public float ENDacceltime = 10;


    [Header("User Settings")]
    public bool sprintToggleOption = false;
    public float mouseSensetivity = 1.0f;
    public float Xsensetivity = 2.0f;
    public float Ysensetivity = 2.0f;
    public float CamRotationLimit = 90;

    private void Awake()
    {
        instance = this;

        // adds a fist
        GameObject fistObject = new GameObject();
        fistObject.transform.parent = this.transform;
        fistObject.transform.SetPositionAndRotation(Weaponslot.position, Weaponslot.rotation);
        fistObject.transform.SetParent(Weaponslot);
        fistObject.AddComponent(typeof(Fists));
        weapons.Add(fistObject.GetComponent<Fists>());
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Wall");

        myRB = GetComponent<Rigidbody>();

        playerCam = transform.GetChild(0).GetComponent<Camera>();


        camRotation = Vector2.zero;
        Cursor.visible= false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensetivity;
            camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensetivity;
            camRotation.y = Mathf.Clamp(camRotation.y, -CamRotationLimit, CamRotationLimit);

            playerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
            transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

            // Sprinting
            if (sprintMode)
            {
                speed = Mathf.Lerp(STARTacceltime, ENDacceltime, TimeSprint / 5);
                TimeSprint += Time.deltaTime;
            }
            else
            {
                TimeSprint = 0;
                speed = 8;
            }

            Vector3 temp = myRB.velocity;

            //movement
            float verticalMove = Input.GetAxisRaw("Vertical");
            float horizontalMove = Input.GetAxisRaw("Horizontal");

            //Sprint Toggle Option start
            if (!sprintToggleOption)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    sprintMode = true;

                if (Input.GetKeyUp(KeyCode.LeftShift))
                    sprintMode = false;
            }

            if (sprintToggleOption)
            {
                if (Input.GetKey(KeyCode.LeftShift) && verticalMove > 0)
                    sprintMode = true;

                if (verticalMove <= 0)
                    sprintMode = false;
            }
            //Sprint Toggle Option End

            if (!sprintMode)
                temp.x = verticalMove * speed;

            if (sprintMode)
                temp.x = verticalMove * speed * sprintMultiplier;

            temp.z = horizontalMove * speed;


            if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, GrounddetectDistance))
                temp.y = jumpheight;
     


            myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

            // weapons
            for (int i = 0; i < weapons.Count; i++)
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    heldIndex = i;
                    return;
                }

            currentWeapon.HeldUpdate();
        }

        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 5f, Keydoor))
        {
            hit.collider.gameObject.GetComponent<KeyDoor>().Opendoor();
        }
      
        
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward);
    
    
    
    
    
    }


   //Pickups
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "exitDoor")
            gameManager.LoadLevel(1);

        if (other.gameObject.tag == "Key")
        { 
            HasKey = true;
            Destroy(other.gameObject);
        }

        if ((Health < maxHealth) && other.gameObject.tag == "HealthPickup")
        {
            Health += HealthRestore;

            if (Health > maxHealth)
                Health = maxHealth;


            Destroy(other.gameObject);
        }


        
  
        if (currentWeapon as Gun != null)
        {
            Gun gun = currentWeapon as Gun;

            if ((gun.currentAmmo < gun.maxAmmo) && other.gameObject.tag == "ammoPickup")
            {
                gun.currentAmmo += gun.ReloadAmount;

                if (gun.currentAmmo > gun.maxAmmo)
                    gun.currentAmmo = gun.maxAmmo;


                Destroy(other.gameObject);
            }
        }

        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            weapons.Add(weapon);
            heldIndex = weapons.Count - 1;
            weapon.gameObject.transform.SetPositionAndRotation(Weaponslot.position, Weaponslot.rotation);
            weapon.gameObject.transform.SetParent(Weaponslot);
       

        }
        
    }
    IEnumerator cooldownFire()
    {
        yield return new WaitForSeconds(currentWeapon.fireRate);
        canFire = true;
    }
    void RefreshWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == heldIndex)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }
}
