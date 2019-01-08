using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    float gravity = 9.81f;
    private Rigidbody myRB;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] bool gravityUp;
    [SerializeField] LayerMask groundRay;
    [SerializeField] Animator myAnimator;

    [SerializeField] GameEvent deathEvent;
    [SerializeField] GameEvent finishEvent;
    static bool finish;
    bool canMove = true;

    private void Awake() {
        myRB = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    void Update() {
        if (canMove) {
            UpdateMovement();
        }
    }

    float newMovPosition;
    private void UpdateMovement() {
        jump();

        newMovPosition = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed;
        myRB.MovePosition(new Vector3(newMovPosition, transform.position.y));

        myAnimator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
    }

    private void jump() {
        if (Physics.Raycast(transform.position, -transform.up, 0.6f, groundRay)) {
            myAnimator.SetBool("Grounded", true);

            if (Input.GetButtonDown("Jump")) {
                myAnimator.SetBool("Jump", true);
                if (gravityUp) {
                    myRB.AddForce(new Vector3(0, -jumpHeight), ForceMode.Impulse);
                }
                else {
                    myRB.AddForce(new Vector3(0, jumpHeight), ForceMode.Impulse);
                }
            }
            else {
                myAnimator.SetBool("Jump", false);
            }
        }
        else {
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("Grounded", false);
        }
    }

    private void FixedUpdate() {
        if (canMove) {
            Gravity();
        }
    }

    private void Gravity() {
        if (gravityUp) {
            myRB.AddForce(new Vector3(0, gravity, 0));
        }
        else {
            myRB.AddForce(new Vector3(0, -gravity, 0));
        }
    }

    public void Die() {
        Time.timeScale = 0;
        deathEvent.Raise();
    }

    public void AddForce(Vector3 force) {
        AddForce(force);
    }

    private void OnTriggerEnter(Collider other) {
        if (canMove) {
            other.GetComponent<InteractionBase>().Interact(this);
        }
    }

    [SerializeField] float finishSpeed;
    public IEnumerator FinishAnimation(Vector3 newPos) {
        StopCoroutine(FinishAnimation(Vector3.zero));
        canMove = false;
        myAnimator.SetTrigger("Finish");

        while (Vector3.Distance(transform.position, newPos) > 0.1) {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPos.x, Time.deltaTime * finishSpeed), Mathf.Lerp(transform.position.y, newPos.y, Time.deltaTime * finishSpeed), transform.position.z);
            yield return null;
        }

        if (finish) {
            finishEvent.Raise();
        }
        else {
            finish = true;
        }
    }
}