using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachine_controller;
    [SerializeField] private GameObject current_target;
    [SerializeField] private GameObject mini_ship;
    [SerializeField] private Ship_Controller ship_controller;
    [SerializeField] private CapsuleShooter capsule_Shooter;
    // Start is called before the first frame update
    void Start()
    {
        cinemachine_controller = GetComponent<CinemachineVirtualCamera>();
        cinemachine_controller.Follow = current_target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            cinemachine_controller.Follow = mini_ship.transform;
        }
        if(ship_controller.canMove || Input.GetKeyDown(KeyCode.E))
        {
            cinemachine_controller.Follow = current_target.transform;
        }
        
    }

}
