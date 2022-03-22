using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChecker : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner cubeSpawner;

    private CubeController[] touchCubes;

    private void Awake()
    {
        touchCubes = GetComponentsInChildren<CubeController>();

        for(int i = 0; i < touchCubes.Length; ++i)
        {
            touchCubes[i].Setup(cubeSpawner);
        }
    }
}
