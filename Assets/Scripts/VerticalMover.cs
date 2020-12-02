using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Vector2 startpoint;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y - startpoint.y > range && direction > 0)
        {
            direction *= -1;
        }
        else if(startpoint.y - transform.position.y > range && direction < 0)
        {
            direction *= -1;
        }
        transform.Translate(0, speed * direction * Time.deltaTime, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, range * 2, 0));
    }
}
