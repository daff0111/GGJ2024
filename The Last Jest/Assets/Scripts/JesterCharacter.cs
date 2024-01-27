using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterCharacter : Character
{
    public Rigidbody Head;

    public void DropHead()
    {
        Head.useGravity = true;
        //Head.transform.position += transform.right * 1;
        Head.AddForce(transform.right * 100);
    }
}
