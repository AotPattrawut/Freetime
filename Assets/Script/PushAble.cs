using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    public float grabRange = 1f;
    public float lookingAngle = 5f;
    public float gravity = -20f;

    private Transform player;
    //Rigidbody rb;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


    void Update()
    {
        DetectPush();
    }


    private void DetectPush()
    {
        if (!CheckIfLookAt()) return;
        if (!CheckDistance()) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<PlayerController>().BeginPush(this.transform);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            player.GetComponent<PlayerController>().EndPush(this.transform);
        }
    }

    private bool CheckDistance()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = this.transform.position;
        myPos.y = playerPos.y;
        float distance = Vector3.Distance(playerPos, myPos);
        return distance < grabRange;
    }

    private bool CheckIfLookAt()
    {
        Vector3 playerForward = player.transform.forward;
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = this.transform.position;
        myPos.y = playerPos.y = playerForward.y;
        float angle = Vector3.Angle(playerForward, myPos - playerPos);

        Debug.Log(angle);
        return angle <= lookingAngle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
           
        }
    }
}
