using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JesterCharacter : Character
{
    public Rigidbody Head;
    public VisualEffect BloodSplatter;

    public void DropHead()
    {
        Head.gameObject.GetComponent<Collider>().enabled = true;
        Head.useGravity = true;
        Head.AddForce(transform.right * 100);
        BloodSplatter.enabled = true;
    }
}