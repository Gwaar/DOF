using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
    [RequireComponent(typeof(CameraRaycaster))]

    public class CursorAffordance : MonoBehaviour

    {


        [SerializeField]
        Texture2D unknownCursor = null;

        [SerializeField]
        Texture2D targetCursor = null;

        [SerializeField]
        const int walkableLayerNumber = 8;
        [SerializeField]
        const int enemybleLayerNumber = 9;

        CameraRaycaster cameraRaycaster;

        // Use this for initialization
        void Start()
        {
            cameraRaycaster = GetComponent<CameraRaycaster>();
          //  cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // register ourself
        }

       
    }
    //void OnLayerChanged(int newLayer)
    //{

    //    switch (newLayer)
    //    {
    //        case walkableLayerNumber:

    //            break;
    //        case enemybleLayerNumber:
    //            Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
    //            break;
    //        default:
    //            Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
    //            return;
    //    }
    //    }
    //}
}