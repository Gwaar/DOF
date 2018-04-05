using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using RPG.Character; // so can detect by type; 

public class CameraRaycaster : MonoBehaviour
{

    //------------------------------------------------------------------------------------------//
    //      SerializeField                                                                      //
    //------------------------------------------------------------------------------------------//

    [SerializeField]    Texture2D   unknownCursor   = null               ;
    [SerializeField]    Texture2D   targetCursor    = null               ;
    [SerializeField]    Texture2D   walkCursor      = null               ;
    [SerializeField]    Vector2     cursorHotspot   = new Vector2(96, 96);
    //------------------------------------------------------------------------------------------//
    //      Constants                                                                           //
    //------------------------------------------------------------------------------------------//

    const int   POTENTIALLY_WALKABLE_LAYER_NUMBER   = 8 ;
    const int   POTENTIALLY_ENEMY_LAYER_NUMBER      = 9 ;


    //------------------------------------------------------------------------------------------//
    //     Layer Prios                                                                          //
    //------------------------------------------------------------------------------------------//
    [SerializeField] int[] layerPriorities = null;

    float       maxRaycastDepth             = 100f; 
	int         topPriorityLayerLastFrame   = -1;
    //------------------------------------------------------------------------------------------//
    //      Sets Delegates for other classes                                                    //
    //------------------------------------------------------------------------------------------//

   // public      delegate void OnCursorLayerChange(int newLayer)      ;

    public      delegate void OnOverPotentiallyEnemy(Enemy enemy)    ;
    public      event OnOverPotentiallyEnemy onOverPotentiallyEnemy  ;

    public      delegate void OnOverPotentiallyWalkable(Vector3 destination);
    public      event OnOverPotentiallyWalkable onOverPotentiallyWalkable;


    //------------------------------------------------------------------------------------------//
    //     Check if pointer is over an Ui element                                               //
    //------------------------------------------------------------------------------------------//
    void Update()
    {      
        if (EventSystem.current.IsPointerOverGameObject())        {}        else       { PreforemRayCasts();       }    }


    void PreforemRayCasts()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (RaycastForEnemy     (ray)) {    return; }
        if (RaycastForPotentionallyWalkable(ray)) { return; }

    }

     bool RaycastForEnemy(Ray ray)
    {
        RaycastHit hitinfo;
        Physics.Raycast(ray, out hitinfo, maxRaycastDepth);
        var gameObjectHit = hitinfo.collider.gameObject;
        var enemyHit = gameObjectHit.GetComponent<Enemy>();

        if (enemyHit)
        {
            Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
            onOverPotentiallyEnemy(enemyHit);
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------------------//
    //     Check if target is Walkable                                                          //
    //------------------------------------------------------------------------------------------//
    private bool RaycastForPotentionallyWalkable(Ray ray)
    {
        RaycastHit  hitinfo;
        LayerMask   potatiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER_NUMBER;

        bool        potentiallyWalkableHit  = Physics.Raycast(ray, out hitinfo, maxRaycastDepth, potatiallyWalkableLayer);
        if         (potentiallyWalkableHit)
        {
            Cursor.SetCursor            (walkCursor, cursorHotspot, CursorMode.Auto);
            onOverPotentiallyWalkable   (hitinfo.point);

            return true;
        }

        return false;
    }
  
}




