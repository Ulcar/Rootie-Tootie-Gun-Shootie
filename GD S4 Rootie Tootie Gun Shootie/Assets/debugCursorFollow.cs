using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugCursorFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Camera camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0);
    }
}
