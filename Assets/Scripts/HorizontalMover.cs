using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
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
        if (transform.position.x - startpoint.x > range && direction > 0)
        {
            direction *= -1;
        }
        else if (startpoint.x - transform.position.x > range && direction < 0)
        {
            direction *= -1;
        }
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, 0.5f, 0));
    }
}
