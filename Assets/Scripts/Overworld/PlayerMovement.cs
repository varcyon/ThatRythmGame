using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float RunSpeed;
    public float StrafeSpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    Rigidbody rigidBody;

    void Start() {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();

        float horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * StrafeSpeed;
        float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * RunSpeed;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * StrafeSpeed * Time.deltaTime);
        }       
    }
    void OnTriggerStay(Collider other) {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
        }
    }
    
    public void PlayerMove()    {        
        if (Input.GetKey(KeyCode.LeftShift)) {
            RunSpeed = 18f;
            StrafeSpeed = 18f;
        } else {
            RunSpeed = 5f;
            StrafeSpeed = 5f;
        }
    }
}
