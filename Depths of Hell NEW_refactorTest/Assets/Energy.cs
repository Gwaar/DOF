using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;



namespace RPG.Character
{


    public class Energy : MonoBehaviour
    {
        [SerializeField]  RawImage EnergyBar;
        [SerializeField] float MaxEnergyPoint = 100f;
        [SerializeField] int pointsOfEnergyPerHit = 10;



       public float currentEnergyPoints;
        CameraRaycaster cameraRayCaster;


        // Use this for initialization
        void Start()
        {
            cameraRayCaster = Camera.main.GetComponent<CameraRaycaster>();
            currentEnergyPoints = MaxEnergyPoint;

            cameraRayCaster.notifyRightClickObservers += ProcessRightClick;

        }

        // Update is called once per frame
        void Update()
        {

            UpdateEnergyPoints();
        }

        void ProcessRightClick ( RaycastHit raycastHit,int layerhit)
        {
            float newEnergyPoint = currentEnergyPoints - pointsOfEnergyPerHit;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoint, 0, MaxEnergyPoint);
         
            UpdateEnergyPoints();
        }

        private void UpdateEnergyPoints()
        {
            
            float xValue = -(EnergyAsProcent() / 2f) - 0.5f;
            EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
        float EnergyAsProcent()
        {
          
            return currentEnergyPoints / MaxEnergyPoint;
        }
    }


}


