using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject tank_prefab;
    [SerializeField] private float Interval;
    [SerializeField] private float x_offset;
    [SerializeField] private float y_offset;

    void Start()
    {
        StartCoroutine(SpawnTanks(Interval, tank_prefab));
    }


    private IEnumerator SpawnTanks(float interval, GameObject tank)
    {
        float raycastDistance = 50f;
        yield return new WaitForSeconds(interval);
        Vector3 spawn_pos = new Vector3(Random.Range(x_offset, -x_offset), Random.Range(y_offset, -y_offset), 0f);
        Instantiate(tank_prefab, spawn_pos, Quaternion.identity);
        RaycastHit2D hit = Physics2D.Raycast(spawn_pos, Vector2.zero, raycastDistance);
        /*if(hit.collider.tag == "Respawn")
         {
             Instantiate(tank_prefab, spawn_pos, Quaternion.identity);
         }*/
        StartCoroutine(SpawnTanks(interval, tank_prefab));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(x_offset, y_offset, 0)); 
    }
}
