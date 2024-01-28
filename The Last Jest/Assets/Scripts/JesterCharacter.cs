using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JesterCharacter : Character
{
    public Rigidbody Head;
    public VisualEffect BloodSplatter;
    public GameObject FakeHead;

    public void DropHead()
    {
        FakeHead.SetActive(false);
        Head.gameObject.transform.localPosition = new Vector3(0, 1.5f, -0.1f);
        Head.gameObject.GetComponent<Collider>().enabled = true;
        Head.useGravity = true;
        Head.AddForce(transform.right * 100);
        BloodSplatter.enabled = true;
        //GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().AddForce(transform.forward * -10);
    }
}