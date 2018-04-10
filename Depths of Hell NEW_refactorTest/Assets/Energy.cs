
using UnityEngine;
using UnityEngine.UI;




namespace RPG.Character
{
    public class Energy : MonoBehaviour
    {
        [SerializeField]        RawImage            EnergyBar                   = null  ;
        [SerializeField]        float               MaxEnergyPoint              = 100f  ;


        [SerializeField]
        float RegenPointsPerSec = 1f;



        public                  float               currentEnergyPoints;                                   
   
        void Start()
        {           
            currentEnergyPoints   = MaxEnergyPoint  ;          
        }

        //------------------------------------------------------------------------------------------//
        //     if player click on Enemy with the <Enemy>() script attatched                         //
        //------------------------------------------------------------------------------------------//

        public bool IsEnergyAvailable(float amount)
        {
            return amount <= currentEnergyPoints;       
        }

        public void ConsumeEnergy(float amount)
        {

            float newEnergyPoint    = currentEnergyPoints - amount;
            currentEnergyPoints     = Mathf.Clamp(newEnergyPoint, 0, MaxEnergyPoint);
            UpdateEnergyBar();
        }
        private void UpdateEnergyBar()
        {         
            float xValue        =           -(EnergyAsProcent() / 2f) - 0.5f;
            EnergyBar.uvRect    = new       Rect(xValue, 0f, 0.5f, 1f);
        }
        float       EnergyAsProcent()
        {       
            return currentEnergyPoints / MaxEnergyPoint;
        }




        private void Update()
        {
           // UpdateEnergyBar();
            AddenergyPoints();
            
        }




       private void  AddenergyPoints()
        {
            float pointsToAdd =  + RegenPointsPerSec * Time.deltaTime;
            currentEnergyPoints = Mathf.Clamp(currentEnergyPoints + pointsToAdd, 0, MaxEnergyPoint);

        }
    }
    


}


