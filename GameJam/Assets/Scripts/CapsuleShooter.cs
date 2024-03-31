using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CapsuleShooter : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject space_man;
    [SerializeField] private SpaceMan space_shooter;
    public bool ship_exit = false;
    private Ship_Controller controller;
    public Transform current_space_manPos;
    public bool created = false;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponentInParent<Ship_Controller>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           SecondPlayerCreate();
           space_shooter.TurnOn();
            
        }

        transform.position = transform.parent.position;

    }

    public void SecondPlayerCreate()
    {
        created= true;
    }

    public void ReEnter()
    {
        space_man.transform.position = gameObject.transform.position;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player" && !ship_exit)
        {
            ship_exit = true;       
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && ship_exit)
        {
            created = false;
            space_shooter.ReEnterShip();
            space_shooter.TurnOff();
            controller.TurnOnShip();
        }
    }
}
