using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public LineRenderer line;

    public GameObject stick;

    public GameObject feet;
    public GameObject head;

    private Vector3[] points = new Vector3[2];

    private Color noHitColor = Color.magenta;
    private Color hitColor = Color.green;

    public void ShowLine(bool on)
    {
        line.enabled = on;
        stick.SetActive(!on);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Point at hand.
        points[0] = gameObject.transform.position;

        // Point 4 units forward from hand.
        points[1] = gameObject.transform.position + gameObject.transform.forward * 4;

        // Give the line render this positions this frame.
        line.SetPositions(points);

        if (haveCollision())
        {
            line.startColor = hitColor;
            line.endColor = hitColor;
        }
        else
        {
            line.startColor = noHitColor;
            line.endColor = noHitColor;
        }

        //teleport();
    }

    private RaycastHit hit;
    public void teleport()
    {
        if (hit.collider != null)
        {
            // Should teleport happen?
            if (hit.collider.gameObject.name.Equals("Plane"))
            {
                // The offset between the play area and the head location.
                Vector3 difference = feet.transform.position - head.transform.position;

                // Ignore changes in y right now, to keep the head at the same height!
                difference.y = 0;

                // Final position.
                feet.transform.position = hit.point + difference;
            }
        }
    }

    public bool haveCollision()
    {
        Vector3 origin = points[0];
        Vector3 direction = points[1] - origin;
        float distance = direction.magnitude;

        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            return true;
        }
        return false;
    }

}
