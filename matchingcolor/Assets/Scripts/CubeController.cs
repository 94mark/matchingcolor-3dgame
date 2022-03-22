using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private CubeSpawner cubeSpawner;
    private MeshRenderer meshRenderer;
    private int colorIndex;

    public void Setup(CubeSpawner cubeSpawner)
    {
        this.cubeSpawner = cubeSpawner;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = this.cubeSpawner.CubeColors[0];
        colorIndex = 0;
    }

    public void ChangeColor()
    {
        if(colorIndex < cubeSpawner.CubeColors.Length-1)
        {
            colorIndex++;
        }
        else
        {
            colorIndex = 0;
        }

        meshRenderer.material.color = cubeSpawner.CubeColors[colorIndex];
    }
}
