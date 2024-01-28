using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimationManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;
    void ArriveIn()
    {
        uiManager.OnPanelIn();
    }

    void ArriveOut()
    {
        uiManager.OnPanelOut();
    }
}
