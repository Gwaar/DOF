
using UnityEngine;
using UnityEngine.UI;
//using RPG.CameraUI;



namespace RPG.Character
{


    public class Energy : MonoBehaviour
    {
        [SerializeField]        RawImage            EnergyBar;
        [SerializeField]        float               MaxEnergyPoint              = 100f;
        [SerializeField]        int                 pointsOfEnergyPerHit        = 10;

        public                  float               currentEnergyPoints;
                                CameraRaycaster     cameraRaycaster;
  

        // Use this for initialization
        void Start()
        {
            cameraRaycaster                         = Camera.main.GetComponent<CameraRaycaster>()   ;
            currentEnergyPoints                     = MaxEnergyPoint                                ;   
            cameraRaycaster.onOverPotentiallyEnemy += OnOverPotentiallyEnemy;

        }

        //------------------------------------------------------------------------------------------//
        //     if player click on Enemy with the <Enemy>() script attatched                         //
        //------------------------------------------------------------------------------------------//
        void OnOverPotentiallyEnemy(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UpdateEnergyBar();
                UpdateEnergyPoints();
            }
        }
        //------------------------------------------------------------------------------------------//
        //      Updates Enery Bar after clicked on enemy                                            //
        //------------------------------------------------------------------------------------------//

        private void UpdateEnergyBar()
        {
            float newEnergyPoint    = currentEnergyPoints - pointsOfEnergyPerHit;
            currentEnergyPoints     = Mathf.Clamp(newEnergyPoint, 0, MaxEnergyPoint);
        }
      
        void Update()
        {
            UpdateEnergyPoints();
        }

        private void UpdateEnergyPoints()
        {         
            float xValue        =           -(EnergyAsProcent() / 2f) - 0.5f;
            EnergyBar.uvRect    = new       Rect(xValue, 0f, 0.5f, 1f);
        }
        float       EnergyAsProcent()
        {       
            return currentEnergyPoints / MaxEnergyPoint;
        }
    }


}


