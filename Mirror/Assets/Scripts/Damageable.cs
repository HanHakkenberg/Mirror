using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : InteractionBase {
    public override void Interact(Movement interactor) {
        interactor.Die();
    }
}