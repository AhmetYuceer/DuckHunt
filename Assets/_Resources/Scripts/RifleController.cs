using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : MonoSingleton<RifleController>
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rifleOffsetX;
    private RaycastHit2D raycastHit;
    private float lastOffsetX;
    private Vector3 mousePos;
    private Vector3 riflePos;
    private bool IsClicked => Input.GetKeyDown(KeyCode.Mouse0);
    private bool IsPressedKeyR => Input.GetKeyDown(KeyCode.R);

    //Mods
    private const int bulletsCount = 3;
    private int currentBulletCount;
    public bool isReloading;

    void Start()
    {
        currentBulletCount = bulletsCount;
    }

    private void Update()
    {
        if (GameManager.Instance.isPlay)
        {
            SetMousePosition();
            ChangeLookingPos();
            if (IsClicked)
            {
                OnClickLeftButton();
            }
            if (IsPressedKeyR && GameManager.Instance.threeBulletMod)
            {
                UICrosshair.Instance.ReloadRifle();
            }
        }
    }

    public void ReloadedRifle()
    {
        currentBulletCount = bulletsCount;
    }

    private void Fire()
    {
        if (GameMods.activeMod == GameMods.Mods.threeBullets && currentBulletCount > 0 && !isReloading)
        {
            currentBulletCount -= 1;
            UIManager.Instance.DisableNextActiveBullet();

            if (currentBulletCount <= 0)
                UICrosshair.Instance.ReloadRifle();
        }
    }
    public Vector3 SetMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        return mousePos;
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
        if (GameManager.Instance.threeBulletMod && currentBulletCount <= 0)
            return;

        Fire();
        raycastHit = Physics2D.Raycast(mousePos, mousePos.z * Vector3.forward, 10f, mask);

        if (raycastHit.collider == null)
            return;

        raycastHit.collider.gameObject.GetComponent<Duck>().Disable();
    }
}