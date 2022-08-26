using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    public Transform Target;
    List<PointOfInterest> Pois;
    public float Radius;
    public Transform targetDefault;
    // Start is called before the first frame update
    void Start()
    {
        Pois = FindObjectsOfType<PointOfInterest>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        Transform tracking = null;
        foreach (PointOfInterest poi in Pois)
        {
            Vector3 delta = poi.transform.position - transform.position;
            if(delta.magnitude < Radius)
            {
                tracking = poi.transform;
                break;
            }
        }
        if (tracking != null)
        {
            Target.position = tracking.position;
        }
        else
        {
            Target.position = targetDefault.position;
        }

    }
}
