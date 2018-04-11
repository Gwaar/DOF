using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour

{


    [SerializeField] float  minDistance = 1.0f;
    [SerializeField]
    float maxDistance = 4f;
    
    Vector3 dollyDir;
    [SerializeField]float smooth = 10f;
    [SerializeField]
    Vector3 dollyDirAdjusted;

    [SerializeField]
    float distance;




    // Use this for initialization
    void Awake ()
    {


        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 disierdCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, disierdCameraPos, out hit))
        {
            distance = Mathf.Clamp(hit.distance * 0.9f, minDistance, maxDistance);
        }


        else
        {
            distance = maxDistance;
        }
            transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        }	
	}

