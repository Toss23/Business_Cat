using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private CameraState[] states;

    private Vector3 cameraPosition;
    private Vector3 targetOffset, targetOffsetCurrent;

    private void Start()
    {
        transform.localPosition = new Vector3(0, states[0].Height, -states[0].Distance);
        cameraPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.LookAt(target.transform.position + targetOffsetCurrent);
        targetOffsetCurrent = Vector3.MoveTowards(targetOffsetCurrent, targetOffset, cameraSpeed * Time.deltaTime);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, cameraPosition, cameraSpeed * Time.deltaTime);
    }

    public void SelectState(string identifier)
    {
        foreach (CameraState state in states)
        {
            if (state.Identifier == identifier)
            {
                cameraPosition = new Vector3(0, state.Height, -state.Distance);
                targetOffset = state.TargetOffset;
            }
        }
    }

    [System.Serializable]
    private class CameraState
    {
        [SerializeField] public string Identifier;
        [SerializeField] public float Distance;
        [SerializeField] public float Height;
        [SerializeField] public Vector3 TargetOffset;
    }
}