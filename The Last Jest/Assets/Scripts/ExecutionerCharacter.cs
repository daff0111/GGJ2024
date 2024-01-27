using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionerCharacter : Character
{
    public GameObject ExecutionerAxe;
    public JesterCharacter Jester;

    public void KillJester()
    {
        ExecutionerAxe.transform.rotation = Quaternion.Euler(0, 0, -80);
        ExecutionerAxe.transform.localPosition = new Vector3(-0.1f, 0.6f, -0.475f);
        Jester.DropHead();
    }

    public void PrepareKill(bool prepare)
    {
        if(prepare)
            ExecutionerAxe.transform.localRotation = Quaternion.Euler(0, 0, 40);
        else
            ExecutionerAxe.transform.localRotation = Quaternion.Euler(0, 0, -10);
    }

    public void RevealAngryFace()
    {
        //FaceMesh.material = MaterialAngryFace;
        //FaceMesh.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
