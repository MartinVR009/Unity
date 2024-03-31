using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour
{
    private float timer = 1.5f;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject other_target;
    [SerializeField] private Ship_Controller controller;
    [SerializeField] private SpaceMan miniship;
    [SerializeField] private GameObject prefab;
    [SerializeField] private  float speed;
    [SerializeField] private float duration;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Tank tank;
    [SerializeField] private float distance_max;
    [SerializeField] private int bullet_damage;
    [SerializeField] private Health slider;
    private int target_toKill;
    private Vector2 movementDirection;
    private Vector2 rotation;
    // Start is called before the first frame update
    void Start()
    {
        timer = duration;
        slider = GameObject.Find("HealtBar").GetComponent<Health>();
        target = GameObject.Find("Ship");
        controller = GameObject.Find("Ship").GetComponent<Ship_Controller>();
        other_target = GameObject.Find("MiniShip");
        miniship = GameObject.Find("MiniShip").GetComponent<SpaceMan>();
    }

    // Update is called once per frame
    void Update()
    {

        if (miniship.enable_move)
        {
           rotation = (other_target.transform.position - transform.position).normalized;
            target_toKill = 1;
        }

        if (controller.canMove)
        {
            rotation = (target.transform.position - transform.position).normalized;
            target_toKill = 2;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (target_toKill == 1)
            {
                controller.player_healt -= bullet_damage * 2;
                slider.TakeHealt(bullet_damage * 2);
            }
            if (target_toKill == 2)
            {
                controller.player_healt -= bullet_damage;
                slider.TakeHealt(bullet_damage);
            }
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb2D.velocity = rotation * speed * Time.fixedDeltaTime;
    }
}


