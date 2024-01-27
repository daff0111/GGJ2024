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
        Jester.DropHead();
    }
}
