using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : InteractionBase {
    public override void Interact(Movement interactor) {
        StartCoroutine(interactor.FinishAnimation(transform.position));
    }
}