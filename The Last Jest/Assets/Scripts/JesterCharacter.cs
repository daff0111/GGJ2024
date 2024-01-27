using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterCharacter : Character
{
    public Rigidbody Head;

    public void DropHead()
    {
        Head.gameObject.GetComponent<Collider>().enabled = true;
        Head.useGravity = true;
        Head.AddForce(transform.right * 100);
        IsAlive = false;
    }
}