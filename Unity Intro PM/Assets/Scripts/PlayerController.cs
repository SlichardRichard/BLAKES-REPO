using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody myRB;
    Camera playerCam;

    Vector2 camRotation;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpheight = 5.0f;
    public float GrounddetectDistance = 1.0f;
    public bool Sprintmode = false;

    [Header("User Settings")]
    public bool sprintToggleOption = false;
    public float mouseSensetivity = 1.0f;
    public float Xsensetivity = 2.0f;
    public float Ysensetivity = 2.0f;
    public float CamRotationLimit = 90;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();

        playerCam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible= false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensetivity;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensetivity;
        camRotation.y = Mathf.Clamp(camRotation.y, - CamRotationLimit, CamRotationLimit);

        playerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);




        Vector3 temp = myRB.velocity;

        if(!sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                Sprintmode = true;
           
            if (Input.GetKeyUp(KeyCode.LeftShift))
                Sprintmode = false;
        }
       
       
        if (!Sprintmode)
            temp.x = Input.GetAxisRaw("Vertical") * speed;
        
        if (Sprintmode)
            temp.x = Input.GetAxisRaw("Vertical") * speed * sprintMultiplier;
        
        if (sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") > 0)
                Sprintmode= true;  
            if (Input.GetKeyUp(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") <= 0)
                    Sprintmode = false;
        }
       

        temp.z= Input.GetAxisRaw("Horizontal") * speed;

       

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, GrounddetectDistance))
            temp.y = jumpheight;
        
        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    
        
    }
}
