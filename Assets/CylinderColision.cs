using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderColision : MonoBehaviour
{
    public GameEvent score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Stick"))
        {
            gameObject.SetActive(false); // Remove Object.
            score.AddPoint(3);
        }
    }
}