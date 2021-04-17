using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Transform[] getRoutes()
    {
        Transform[] transforms = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform child in transform)
        {
            transforms[i++] = child;
        }

        return transforms;
    }
}
