using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float distance;
    [SerializeField] private float height;

    private void Start()
    {
        transform.localPosition = new Vector3(0, height, -distance);
        transform.LookAt(target.transform);
    }
}
