using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SwordAttack : MonoBehaviour
{
    private Vector2 offset;
    private float x_offset = -0.13f;
    private Collider2D poly_collider;
    // Start is called before the first frame update
    private void Start()
    {
        poly_collider= GetComponent<Collider2D>();
        poly_collider.enabled = false;
        offset = poly_collider.offset;
    }

    public void AttackRight()
    {
        poly_collider.enabled= true;
        transform.localScale = Vector3.one;
        poly_collider.offset= offset;
    }

    public void AttackLeft()
    {
        poly_collider.enabled= true;
        poly_collider.offset = new Vector2(x_offset, offset.y);
    }

    public void AttackStop()
    {
        poly_collider.enabled= false;
    }
   
}
