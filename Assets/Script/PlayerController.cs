using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class PlayerController : MonoBehaviour
{
    public float speedMovement;
    public float rotationSpeed;
    public float jumpForce;
    public float jumpDelay;
    public Vector3 turn;
    Rigidbody rb;
    Animator animator;
    public PlayerState playerState = PlayerState.IDLE;
    public bool isPushing = false;
    private Rig rig;
    
    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rig = GetComponent<Rig>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        RotatePlayer();
      

    }
    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical") * speedMovement;
        transform.Translate(new Vector3(horizontal, 0, vertical) * (Time.deltaTime));
        if(horizontal != 0)
        {
            animator.SetBool("isTurn", true);
        }else
        {
            animator.SetBool("isTurn", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(jumpCoroutine(jumpDelay));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMovement = 20f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || vertical < 0)
        {
            speedMovement = 5f;
        }
        animator.SetFloat("Speed", vertical);
        animator.SetFloat("turn", horizontal);
        //transform.Rotate(new Vector3(0,horizontal * rotationSpeed * Time.deltaTime,0));
    }
    public void RotatePlayer()
    {
        if(playerState == PlayerState.IDLE || playerState == PlayerState.WALK)
        {
            float speedY = Input.GetAxis("Mouse X");
            float speedX = Input.GetAxis("Mouse Y");
            this.transform.Rotate(new Vector3(0, speedY, 0));
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void BeginPush(Transform pushObject)
    {
        animator.SetBool("Push", true);
        playerState = PlayerState.PUSH;
        speedMovement = 4f;
        pushObject.transform.parent = gameObject.transform;
    }
    public void EndPush(Transform pushObject)
    {
        animator.SetBool("Push", false);
        playerState = PlayerState.IDLE;
        speedMovement = 5f;
        pushObject.transform.parent = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            animator.SetBool("isJumping", false);
        }
    }
    IEnumerator jumpCoroutine(float delay)
    {
        animator.SetBool("isJumping", true);
        yield return new WaitForSeconds(delay);
        Jump();
    }
}
public enum PlayerState
{
    IDLE,
    WALK,
    WALKBACKWARD,
    RUN,
    PUSH
}