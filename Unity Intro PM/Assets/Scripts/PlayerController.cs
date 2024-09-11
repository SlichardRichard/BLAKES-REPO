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
    public bool sprintMode = false;

    public float TimeSprint;

    [Header("Sprint Accelaration")]
    public float STARTacceltime = 10;
    public float ENDacceltime = 15;


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



        
        if(sprintMode)
        {
            speed = Mathf.Lerp(STARTacceltime, ENDacceltime, TimeSprint / 5);
            TimeSprint += Time.deltaTime;
        }
        else
        {
            TimeSprint = 0;
            speed = 10;
        }

        Vector3 temp = myRB.velocity;

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
    
        
    }
}
