using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameOver gameover;
    public Slider slider;
    public int health;

    public void Update()
    {
        Debug.Log("Mensaje");
        if(health <= 0)
        {
            gameover.GameOverScreen();
            Debug.Log("Se activo!!");
        }
    }
    // Start is called before the first frame update
    public void SetMaxHealt(int health)
    {
        slider.maxValue= health;
        slider.value = health;
    }
    public void SetHealt(int health)
    {
        slider.value = health;
        this.health = health;
    }
    public void TakeHealt(int damage)
    {
        health -= damage;
        slider.value = health;
    }
}
