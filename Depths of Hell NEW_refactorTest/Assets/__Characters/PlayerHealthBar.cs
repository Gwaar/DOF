using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character;

[RequireComponent(typeof(Image))]
public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] Image healthOrb;
    Player player;

   
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthOrb = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthOrb.fillAmount = player.healthAsPercentage;

    }
}
