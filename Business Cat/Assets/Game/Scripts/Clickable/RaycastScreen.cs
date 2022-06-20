using UnityEngine;

public class RaycastScreen : Clickable2D
{
    [Header("Raycast")]
    [SerializeField] private Camera raycastCamera;
    [SerializeField] private LayerMask layer;

    protected override void OnSingleClick()
    {
        ContentUI.HideAll();
        Clickable3D clickable = Cast();
        if (clickable != null)
            clickable.OnClick();
    }

    public Clickable3D Cast()
    {
        Ray ray = raycastCamera.ScreenPointToRay(TouchPositionUnfixed);
        if (Physics.Raycast(ray, out RaycastHit hit, layer))
        {
            return hit.transform.GetComponent<Clickable3D>();
        }
        return null;
    }

    protected override void OnTouchDown() { }

    protected override void OnTouchUp() { }
}