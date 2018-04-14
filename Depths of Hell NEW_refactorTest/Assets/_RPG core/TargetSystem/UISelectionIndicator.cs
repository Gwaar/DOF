// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class UISelectionIndicator : MonoBehaviour {


// 	 MouseManager mm;

// 	// Use this for initialization
// 	void Start () {
// 		  mm = GameObject.FindObjectOfType<MouseManager>();
// 	}
	
// 	// Update is called once per frame
// 	void Update () {


//         if(mm.selectedObject != null)
//         {
//             GetComponentInChildren<Renderer>().enabled = true;




//             Bounds bigBound = mm.selectedObject.GetComponentInChildren<Renderer>().bounds;

//             float diameter = bigBound.size.z;

//             diameter *= 1.25f;



//             this.transform.position = new Vector3(bigBound.center.x, 0, bigBound.center.z);
//             this.transform.localScale = new Vector3(bigBound.size.x, 1, bigBound.size.y);

		
// 	}
// }}
