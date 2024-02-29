using UnityEngine;
using UnityEngine.UI;

public class UICrosshair : MonoSingleton<UICrosshair>
{
    public Image defaultCrosshair;
    [SerializeField] private Image reloadCrosshair;
    [SerializeField] private float fillAmountIncrease;
    [SerializeField] private bool isReloading;
    float currentFillAmount = 0;

    void Start()
    {
        Cursor.visible = false;
        defaultCrosshair.gameObject.SetActive(true);
        reloadCrosshair.gameObject.SetActive(false);
        currentFillAmount = 0;
        isReloading = false;
        RifleController.Instance.isReloading = isReloading;
    }

    void Update()
    {
        reloadCrosshair.transform.position = Input.mousePosition;
        defaultCrosshair.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && Cursor.visible && UIManager.Instance.isPlay)
        {
            Cursor.visible = false;
        }
        if (isReloading)
        {
            Reloading();
        }
    }


    public void EndGame()
    {
        defaultCrosshair.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    public void ReloadRifle()
    {
        defaultCrosshair.gameObject.SetActive(false);
        reloadCrosshair.gameObject.SetActive(true);
        isReloading = true;
        RifleController.Instance.isReloading = isReloading;

    }

    private void Reloading()
    {
        currentFillAmount += fillAmountIncrease * Time.deltaTime;
        reloadCrosshair.fillAmount = currentFillAmount;
        if (currentFillAmount >= 1)
        {
            currentFillAmount = 0;
            reloadCrosshair.fillAmount = currentFillAmount;
            isReloading = false;
            RifleController.Instance.isReloading = isReloading;
            reloadCrosshair.gameObject.SetActive(false);
            defaultCrosshair.gameObject.SetActive(true);
            UIManager.Instance.ReloadedRifle();
            RifleController.Instance.ReloadedRifle();
        }
    }
}