using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider slider;
    public void SetMaxHealth(int maxHealth)
    {

       slider.maxValue = maxHealth; 
    
    
    }

    public void SetHealth(int health)
    {

        slider.value = health; 
    
    }
}
