using UnityEngine;

public class RifleController : MonoSingleton<RifleController>
{
    public bool IsCursorVisible { get; set; }
    [SerializeField] private GameObject crosshair;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rifleOffsetX;

    private RaycastHit2D raycastHit;
    private float lastOffsetX;
    private Vector3 mousePos;
    private Vector3 riflePos;

    private bool IsClicked => Input.GetKeyDown(KeyCode.Mouse0);

    void Start()
    {
        IsCursorVisible = false;
        SetCursorVisible();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlay)
        {
            SetCrosshairPosition();
            ChangeLookingPos();
            if (IsClicked)
            {
                OnClickLeftButton();
            }
        }
    }

    private void SetCrosshairPosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        crosshair.transform.position = mousePos;
    }

    private void ChangeLookingPos()
    {
        riflePos = transform.position;
        riflePos.x = mousePos.x;
        if (riflePos.x > 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            lastOffsetX = -rifleOffsetX;
        }
        else if (riflePos.x < -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            lastOffsetX = rifleOffsetX;
        }
        riflePos.x = Mathf.Lerp(riflePos.x, riflePos.x + lastOffsetX, 0.5f);
        transform.position = riflePos;
    }

    private void OnClickLeftButton()
    {
        raycastHit = Physics2D.Raycast(mousePos, mousePos.z * Vector3.forward, 10f, mask);
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