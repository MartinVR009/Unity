using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceMan : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 smooth_Movement;
    private Vector2 smooth_Update;
    private Vector2 movement_value;
    private Vector2 movement;
    [SerializeField] private float speed_move;
    [SerializeField] private float force_mode;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] private float impulse_start;
    [SerializeField] private Ship_Controller ship_main;
    [SerializeField] private Transform capsule;
    [SerializeField] private CapsuleCollider2D capsule_collider;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float direction_change_rate = 0.1f;
    [SerializeField] private Camera cam;
    [SerializeField] private float bullet_timer;
    public int score = 0;
    public int uses = 1;
    private float timer_restart;
    private bool attack_enable = false;
    public bool enable_move = false;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClip2;
    private Vector2 mouse_Pos;
    
    private Vector2 mouse_Rot;
    private float angle;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();;
        capsule_collider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        TurnOff();
        timer_restart = bullet_timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship_main.canMove == false)
        {
            enable_move = true;
        } else
        {
            transform.position = transform.parent.position;
        }
        if (enable_move && !ship_main.dead)
        {
            movement = InputTakeMini();
            smooth_Movement = Vector2.SmoothDamp(smooth_Movement, InputTakeMini(), ref smooth_Update, direction_change_rate);
            mouse_Pos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouse_Rot = mouse_Pos - rb.position;
            angle = Mathf.Atan2(mouse_Rot.y, mouse_Rot.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }
        if (bullet_timer > 0)
        {
            attack_enable = true;
            bullet_timer -= Time.deltaTime;
            
        }else if(bullet_timer < 0 && uses == 1) 
            {
                attack_enable = false;
                bullet_timer = 0;
                uses = 0;
            } 
    }
    private void FixedUpdate()
    {
        if (enable_move)
        {
            rb.AddForce(movement * Time.fixedDeltaTime * force_mode, ForceMode2D.Impulse);
        }
    }


    private Vector2 InputTakeMini()
    {
        return movement = movement_value.normalized;
    }

    private void OnMove(InputValue inputValue)
    {
        movement_value = inputValue.Get<Vector2>();
    }

    public void TurnOn()
    {
        PLaySound2();
        sprite_renderer.enabled = true;
        capsule_collider.enabled = true;
        if (uses > 1)
        {           
            rb.velocity = ship_main.last_input * impulse_start * Time.fixedDeltaTime;
            uses = 1;
        }
        if(uses > 0)
        {
            bullet_timer = timer_restart;
            attack_enable = true;
        }

    }
    public void TurnOff()
    {
        sprite_renderer.enabled = false;
        capsule_collider.enabled= false;
        uses = 2;
    }
    public void ReEnterShip()
    {
        enable_move = false;
        transform.position = capsule.position; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && attack_enable){
            Destroy(collision.gameObject, 0.15f);
            PlaySound();
            FindObjectOfType<AudioManager>().Play("Hit34");
            score += 1;
        }
    }
    private void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
    private void PLaySound2()
    {
        audioSource.PlayOneShot(audioClip2);
    }

}

    