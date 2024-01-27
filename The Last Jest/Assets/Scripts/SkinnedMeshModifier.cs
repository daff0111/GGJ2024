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

    private void Update()
    {
        if (skinnedRenderer != null)
        {
            skinnedRenderer.SetBlendShapeWeight(0, 100.0f * (1.0f + Mathf.Sin(Time.time * 10.0f)) / 2.0f );
        }
    }
}
