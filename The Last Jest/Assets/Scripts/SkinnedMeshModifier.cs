using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshModifier : MonoBehaviour
{
    SkinnedMeshRenderer skinnedRenderer;

    private void Awake()
    {
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void SetSliderValue(float value)
    {
        skinnedRenderer.SetBlendShapeWeight(0, value);
    }
}
