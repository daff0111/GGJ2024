using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshModifier : MonoBehaviour
{
    SkinnedMeshRenderer skinnedRenderer;
    float TargetWeight;
    float CurrentValue;

    private void Awake()
    {
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if(CurrentValue != TargetWeight)
        {
            CurrentValue = Mathf.Lerp(CurrentValue, TargetWeight, 3*Time.deltaTime);
            skinnedRenderer.SetBlendShapeWeight(0, CurrentValue);
        }
    }

    public void SetSliderValue(float value)
    {
        TargetWeight = value;
    }
}
