using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject other_target;
    [SerializeField] private Ship_Controller controller;
    [SerializeField] private SpaceMan miniship;
    [SerializeField] private GameObject prefab;
    [SerializeField] float shootTimer;
    [SerializeField] private Transform shootPos;
    private Rigidbody2D rb;
    private Vector3 tank_Rot;
    private Vector2 target_Vector;
    private float angle;
    private float timer;
    private bool enable = true;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public Quaternion rotation;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Ship");
        controller = GameObject.Find("Ship").GetComponent<Ship_Controller>();
        other_target = GameObject.Find("MiniShip");
        miniship = GameObject.Find("MiniShip").GetComponent<SpaceMan>();
    }
    void Update()
    {

        if (miniship.enable_move)
        {
            target_Vector = other_target.transform.position;
            tank_Rot = target_Vector - rb.position;
            angle = Mathf.Atan2(tank_Rot.y, tank_Rot.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 270f));
        }

        if (controller.canMove)
        {
            target_Vector = target.transform.position;
            tank_Rot = target_Vector - rb.position;
            angle = Mathf.Atan2(tank_Rot.y, tank_Rot.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 270f));
        }

        if (timer < shootTimer)
        {
            enable = false;
            timer += Time.deltaTime;
        }
        else
        {
            enable = true;
        }
        if (enable)
        {
            timer = 0;
            Shoot();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(yourOverlapCirclePosition, yourOverlapCircleRange);
    }

    private void Shoot()
    {
        rotation = Quaternion.Euler(new Vector3(0, 0, angle - 270f));
        Instantiate(prefab, shootPos.position, rotation);
        PlaySound();
    }
    private void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
