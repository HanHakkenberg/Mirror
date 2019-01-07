using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float gravity = 9.81f;
    private Rigidbody myRB;
    [SerializeField] float moveSpeed;

    private void Awake() {
        myRB = GetComponent<Rigidbody>();
    }

    void Update() {
        UpdateMovement();
    }

    float newMovPosition;
    private void UpdateMovement() {
        newMovPosition = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed;

        myRB.MovePosition(new Vector3(newMovPosition, transform.position.y));
    }

    private void FixedUpdate() {
        Gravity();
    }

    private void Gravity() {
        myRB.AddForce(new Vector3(0, gravity, 0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Damage")) {
            Debug.Log("Damage");
        }
        else {
            Debug.Log("other");
        }
    }
}