using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ExecutionerCharacter : Character
{
    public GameObject ExecutionerAxe;
    public JesterCharacter Jester;
    public Animator JesterAnimator;
    public StudioEventEmitter KillSound;

    public IEnumerator KillJester()
    {
        yield return new WaitForSeconds(1);
        //Play Music
        KillSound.Play();
        yield return new WaitForSeconds(0.15f);
        ExecutionerAxe.transform.rotation = Quaternion.Euler(0, 0, -80);
        ExecutionerAxe.transform.localPosition = new Vector3(-0.1f, 0.6f, -0.475f);
        Jester.DropHead();
        if (JesterAnimator)
            JesterAnimator.SetBool("Died", true);
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
