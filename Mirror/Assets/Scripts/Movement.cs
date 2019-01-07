using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Rigidbody myRB;

    void Awake() {
        myRB = GetComponent<Rigidbody>();
    }
}