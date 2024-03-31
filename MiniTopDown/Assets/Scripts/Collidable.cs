using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
   [SerializeField] private ContactFilter2D contactFilter;
   private BoxCollider2D boxCollider;
    private Collider2D[] hit = new Collider2D[10];
}
