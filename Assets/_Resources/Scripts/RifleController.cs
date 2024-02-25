using UnityEngine;

public class RifleController : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private LayerMask mask;
    private RaycastHit2D raycastHit;

    public bool IsCursorVisible { get; set; }
    private bool IsClicked => Input.GetKeyDown(KeyCode.Mouse0);

    void Start()
    {
        IsCursorVisible = false;
        SetCursorVisible();
    }

    Vector3 mousePos;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        crosshair.transform.position = mousePos;
        if (IsClicked)
        {
            OnClickLeftButton();
        }
    }

    private void OnClickLeftButton()
    {
        raycastHit = Physics2D.Raycast(mousePos, mousePos.z * Vector3.forward, 100f, mask);
        if (raycastHit.collider == null)
        {
            return;
        }
        raycastHit.collider.gameObject.GetComponent<Duck>().Disable();
    }

    private void SetCursorVisible()
    {
        Cursor.visible = IsCursorVisible;
    }
}