using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Ship_Controller : MonoBehaviour
{

    private Vector2 smooth_Movement;
    private Vector2 smooth_Update;
    private Vector2 movement_value;
    public Vector2 last_input;
    public int player_healt;
    [SerializeField] private Health slider;
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private CapsuleShooter shooter;
    [SerializeField] private GameObject mini_ship;
    [SerializeField] private float direction_change_rate = 0.1f;
    [SerializeField] private GameOver game_over;
    public bool canMove = true;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mouse_Pos;
    [SerializeField] private Camera cam;
    private Vector2 mouse_Rot;
    public bool dead = false;
    private float angle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        slider.SetMaxHealt(player_healt);
        slider.SetHealt(player_healt);
    }
    private void Update()
    {     
        if(player_healt <= 0)
        {
            dead = true;
            canMove= false;
            FindObjectOfType<AudioManager>().Play("Die");
        }
        if (movement != Vector2.zero) {
            last_input = movement;
        }
        
        if(shooter.created)             
        {
            canMove = false;
        }
        else if(!dead)
        {
            canMove = true;
            shooter.ship_exit = false;
            smooth_Movement = Vector2.SmoothDamp(smooth_Movement, InputTake(), ref smooth_Update, direction_change_rate); //Smooth Movement

                mouse_Pos = cam.ScreenToWorldPoint(Input.mousePosition); //Mouse Position to rotation****
                mouse_Rot = mouse_Pos - rb.position;
                angle = Mathf.Atan2(mouse_Rot.y, mouse_Rot.x) * Mathf.Rad2Deg;
                rb.rotation = angle - 90f; //Until Here****
        }
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + (new Vector2(smooth_Movement.x * speedX, smooth_Movement.y * speedY) * Time.deltaTime));
        }
    }
    
    private Vector3 InputTake()
    {
       return movement = (movement_value).normalized;
    }
    private void RotateShip(Vector3 vector)
    {
        transform.rotation = Quaternion.Euler(vector);
    }
    
    private void OnMove(InputValue inputValue)
    {
        movement_value = inputValue.Get<Vector2>();
    }
    public void TurnOnShip()
    {
        canMove = true;
    }
}
