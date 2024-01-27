using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterCharacter : Character
{
    public Rigidbody Head;

    public void DropHead()
    {
        Head.useGravity = true;
        Head.AddForce(transform.right * 100);
    }
}
