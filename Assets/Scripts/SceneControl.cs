using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public GameObject BlockPrefab;
    public StackBlockControl StackControl;
    public Material[] BlockMaterials;

    private void Awake()
    {
        Block.Materials = BlockMaterials;
        StackControl = new StackBlockControl();
        StackControl.BlockPrefab = BlockPrefab;
        StackControl.Create(transform);
    }
}
