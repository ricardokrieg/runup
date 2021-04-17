using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public RouteManager routeManager;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool moving = false;
    [SerializeField] private int routeIndex = 0;

    private Transform[] routes;
    private Vector2 position;
    private Vector2 nextPosition;

    private float delta;

    void Start()
    {
        routes = routeManager.getRoutes();
        delta = 0f;
        transform.position = routes[0].GetChild(0).position;
        
        StartCoroutine(FollowRoutes());
    }

    private IEnumerator FollowRoutes()
    {
        Vector2[] points = new Vector2[4];
        
        while (routeIndex < routes.Length)
        {
            for (int i = 0; i < 4; i++)
            {
                points[i] = routes[routeIndex].GetChild(i).position;
            }
            
            while (delta < (routeIndex + 1))
            {
                if (moving)
                {
                    delta += Time.deltaTime * speed;
                    float nextDelta = delta + Time.deltaTime * speed;

                    position = getPosition(delta - routeIndex, points);
                    nextPosition = getPosition(nextDelta - routeIndex, points);

                    transform.position = position;
                    transform.rotation = getRotation(position, nextPosition);
            
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    yield return null;
                }
            }

            routeIndex++;
        }
    }

    private Vector2 getPosition(float t, Vector2[] points)
    {
        return Mathf.Pow(1 - t, 3) * points[0] +
               3 * Mathf.Pow(1 - t, 2) * t * points[1] +
               3 * (1 - t) * Mathf.Pow(t, 2) * points[2] +
               Mathf.Pow(t, 3) * points[3];
    }

    private Quaternion getRotation(Vector2 p1, Vector2 p2)
    {
        Vector2 difference = p1 - p2;
        float sign = 1.0f;
        float angle = Vector2.Angle(Vector2.down, difference) * sign;
            
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
