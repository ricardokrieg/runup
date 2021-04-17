using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] points;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * points[0].position +
                             3 * Mathf.Pow(1 - t, 2) * t * points[1].position +
                             3 * (1 - t) * Mathf.Pow(t, 2) * points[2].position +
                             Mathf.Pow(t, 3) * points[3].position;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(gizmosPosition, 0.05f);
        }
        
        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[2].position, points[3].position);
    }
}
