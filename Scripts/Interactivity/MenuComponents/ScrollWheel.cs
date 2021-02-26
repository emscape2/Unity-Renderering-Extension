using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWheel : MonoBehaviour
{
    public bool activated;
    public float yMin, yMax;
    public Transform tomove;
    public float yMinTM, yMaxTM;
    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer rend = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && Input.GetMouseButton(0))
        {
            if (startpos == null)
            {
                startpos = tomove.position;
            }
            var mousePosition = MouseBehavior.MousePos();
            if (mousePosition.x < transform.position.x - 1.0f || mousePosition.x > transform.position.x + 1.0f) 
                return;
            var y = mousePosition.y;
            if (y < yMin)
                y = yMin;
            else if (y > yMax)
                y = yMax;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            Vector3 newposition;
            float ratio = (y - yMin) / (yMax - yMin);
            float ynew = startpos.y + ((yMaxTM - yMinTM) * ratio);

            newposition = new Vector3(tomove.position.x, ynew, tomove.position.z);
            tomove.position = newposition;
        }
    }
}
