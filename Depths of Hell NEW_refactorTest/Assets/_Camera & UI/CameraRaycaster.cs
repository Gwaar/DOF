using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using System;
using RPG.Character; // so can detect by type; 

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField]
    Texture2D unknownCursor = null;

    [SerializeField]
    Texture2D targetCursor = null;


    [SerializeField]
    Texture2D walkCursor = null;

    [SerializeField]
    Vector2 cursorHotspot = new Vector2(96, 96);


    const int   POTENTIALLY_WALKABLE_LAYER_NUMBER   = 8 ;
    const int   POTENTIALLY_ENEMY_LAYER_NUMBER      = 9 ; 




    // INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
    [SerializeField] int[] layerPriorities = null;

    float maxRaycastDepth = 100f; // Hard coded value
	int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

	// Setup delegates for broadcasting layer changes to other classes
    public delegate void OnCursorLayerChange(int newLayer); // declare new delegate type
    public event OnCursorLayerChange notifyLayerChangeObservers; // instantiate an observer set




    public delegate void OnOverPotentiallyEnemy(Enemy enemy);
    public event OnOverPotentiallyEnemy onOverPotentiallyEnemy;


    public delegate void OnOverPotentiallyWalkable(Vector3 destination);
    public event OnOverPotentiallyWalkable onOverPotentiallyWalkable;

    //REMOVE
    public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit); // declare new delegate type
    //REMOVE                                                                               //REMOVE
    public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set
    //REMOVE
    
    public delegate void OnRightClick(RaycastHit raycastHit, int layerHit); // declare new delegate type
    //REMOVE
    public event OnRightClick notifyRightClickObservers; // instantiate an observer set
    //REMOVE


    void Update()
    {
        // Check if pointer is over an interactable UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
           
        }
        else
        {
            PreforemRayCasts();
        }

    }
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

    private bool RaycastForPotentionallyWalkable(Ray ray)
    {
        RaycastHit hitinfo;
        LayerMask potatiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER_NUMBER;

        bool potentiallyWalkableHit = Physics.Raycast(ray, out hitinfo, maxRaycastDepth, potatiallyWalkableLayer);
        if (potentiallyWalkableHit)
        {
            Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
            onOverPotentiallyWalkable(hitinfo.point);
            return true;
        }

        return false;
    }
  
}




