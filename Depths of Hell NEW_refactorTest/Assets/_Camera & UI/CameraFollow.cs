using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //GameObject player;
    [SerializeField]                float       CameraMoveSpeed  = 120f ;

    [SerializeField]                GameObject  cameraFollowobj         ;
    [SerializeField]                Vector3     followPos               ;

    [SerializeField]                float       clamAngle        = 80f  ;
    [SerializeField]                float       inputSensitivity = 150f ;

    [SerializeField]                GameObject  cameraObj               ;
    [SerializeField]                GameObject  playerObj               ;

    [SerializeField]                float       camDistanceXtoPlayer    ;
    [SerializeField]                float       camDistanceYtoPlayer    ;
    [SerializeField]                float       camDistanceZtoPlayer    ;
    [SerializeField]                float       MouseX                  ;
    [SerializeField]                float       MouseY                  ;
    [SerializeField]                float       finalInputX             ;
    [SerializeField]                float       finalInputZ             ;

    [SerializeField]                float       smoothX                 ;
    [SerializeField]                float       smoothY                 ;
    
                                    float rotY                  = 0f    ;
                                    float rotX                  = 0f    ;
   

    
    void Start ()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;




        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	void Update ()
    {

        if (Input.GetMouseButton(1))
        {

        
            //setup ROtation of 
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");

        

        rotY += MouseX * inputSensitivity * Time.deltaTime;
        rotX += MouseY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clamAngle, clamAngle);

            

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;


        }
        //transform.position = player.transform.position;
    }


     void LateUpdate()
    {
       
        
            CameraUpdater();

            
    }


    void CameraUpdater()
    {
        // set target to follow
        Transform target = cameraFollowobj.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
}
