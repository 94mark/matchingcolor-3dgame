using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    private ObjectDetector objectDetector;

    private void Awake()
    {
        objectDetector = GetComponent<ObjectDetector>();
        objectDetector.raycastEvent.AddListener(SelectCube);
    }

    public void SelectCube(Transform hit)
    {
        hit.GetComponent<CubeController>().ChangeColor();
    }
}
