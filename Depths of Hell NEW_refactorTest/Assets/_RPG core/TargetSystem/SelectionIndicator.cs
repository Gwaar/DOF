using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character;
public class SelectionIndicator : MonoBehaviour {
[SerializeField] float offsetMax  =100;
                    
                    [SerializeField] float OffsetHeight;
                    [SerializeField ] float OffsetWidth;
                    [SerializeField] float Offsetmin;
                   
    MouseManager mm;

    CameraRaycaster cameraRaycaster;
    // Use this for initialization
    void Start ()
    { 

        cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster>();

        mm = GameObject.FindObjectOfType<MouseManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (cameraRaycaster.clickedEnemy == true){

        

        
       
        if(mm.selectedObject != null)
        {
            for(int i =0;i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }   
                    Rect visualRect = RendererBoundInScreenSpace(mm.selectedObject.GetComponentInChildren<Renderer>());
                    RectTransform rt  = GetComponent<RectTransform>();

                    


                    rt.position     = new Vector2 (visualRect.xMin+Offsetmin,visualRect.yMin+offsetMax);
                    rt.sizeDelta    = new Vector2 (visualRect.width+OffsetWidth,visualRect.height+OffsetHeight);
                      
                    }else{
                            for(int i =0;i< this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);           
            }
        }
    }
     }
    
    
      static Rect RendererBoundInScreenSpace (Renderer r)
            {

                Bounds bigBound = r.bounds;
               

            Vector3[] ScreenSpaceorners = new Vector3[8];
             Camera mainCamera = Camera.main;

                    // for each of the 8 corners of the renderers wwoundingb box,
                    // convert those corners into screen space.
                ScreenSpaceorners[0]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   + bigBound.extents.x,   bigBound.center.y   + bigBound.extents.y,   bigBound.center.z   + bigBound.extents.z ));
                ScreenSpaceorners[1]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   + bigBound.extents.x,   bigBound.center.y   + bigBound.extents.y,   bigBound.center.z   - bigBound.extents.z ));  
                ScreenSpaceorners[2]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   + bigBound.extents.x,   bigBound.center.y   - bigBound.extents.y,   bigBound.center.z   + bigBound.extents.z ));  
                ScreenSpaceorners[3]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   + bigBound.extents.x,   bigBound.center.y   - bigBound.extents.y,   bigBound.center.z   - bigBound.extents.z )); 
                ScreenSpaceorners[4]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   - bigBound.extents.x,   bigBound.center.y   + bigBound.extents.y,   bigBound.center.z   + bigBound.extents.z ));
                ScreenSpaceorners[5]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   - bigBound.extents.x,   bigBound.center.y   + bigBound.extents.y,   bigBound.center.z   - bigBound.extents.z ));  
                ScreenSpaceorners[6]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   - bigBound.extents.x,   bigBound.center.y   - bigBound.extents.y,   bigBound.center.z   + bigBound.extents.z ));  
                ScreenSpaceorners[7]   = mainCamera.WorldToScreenPoint (new Vector3(   bigBound.center.x   - bigBound.extents.x,   bigBound.center.y   - bigBound.extents.y,   bigBound.center.z   - bigBound.extents.z )); 

                // find the min/max X & Y of these screen space corners
                float min_x  =  ScreenSpaceorners[0].x;
                float min_y  =  ScreenSpaceorners[0].y;
                float max_x  =  ScreenSpaceorners[0].x;
                float max_y  =  ScreenSpaceorners[0].y;
                       
                       for (int i= 1; i< 8;i++)
                       {
                            if(ScreenSpaceorners[i].x       < min_x){min_x        = ScreenSpaceorners[i].x     ;}
                            if(ScreenSpaceorners[i].y       < min_y){min_y        = ScreenSpaceorners[i].y      ;}
                            if(ScreenSpaceorners[i].x       > max_x){max_x        = ScreenSpaceorners[i].x      ;}
                            if(ScreenSpaceorners[i].y       > max_x){max_y        = ScreenSpaceorners[i].y      ;}     
      }
      return  Rect.MinMaxRect(min_x,min_y,max_x,max_y);


    
    }
    

    
}


    