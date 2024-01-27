using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionerCharacter : Character
{
    public GameObject ExecutionerAxe;
    public JesterCharacter Jester;

    public void KillJester()
    {
        ExecutionerAxe.transform.rotation = Quaternion.Euler(0, 0, -90);
        ExecutionerAxe.transform.localPosition = new Vector3(1.5f, 0.5f, 0);
        Jester.DropHead();
    }

    public void RevealAngryFace()
    {
        FaceMesh.material = MaterialAngryFace;
        FaceMesh.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
