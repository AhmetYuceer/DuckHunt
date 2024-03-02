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
        third = false;
        second = false;
        first = false;
        defaultCrosshair.gameObject.SetActive(false);
        reloadCrosshair.gameObject.SetActive(true);
        isReloading = true;
        RifleController.Instance.isReloading = isReloading;
    }

    bool first, second, third;

    private void Reloading()
    {
        currentFillAmount += fillAmountIncrease * Time.deltaTime;
        reloadCrosshair.fillAmount = currentFillAmount;

        if (currentFillAmount >= 0.3f && currentFillAmount <= 0.4f && !first)
        {
            UIManager.Instance.ReloadingRifle();
            first = true;
            Debug.Log("1");
        }
        else if (currentFillAmount >= 0.6f && currentFillAmount <= 0.7f && !second)
        {
            UIManager.Instance.ReloadingRifle();
            second = true;
            Debug.Log("2");

        }
        else if (currentFillAmount >= 0.9f && currentFillAmount <= 1f && !third)
        {
            UIManager.Instance.ReloadingRifle();
            third = true;
            Debug.Log("3");
        }

        if (currentFillAmount >= 1)
        {
            third = false;
            second = false;
            first = false;

            isReloading = false;
            currentFillAmount = 0;
            reloadCrosshair.fillAmount = currentFillAmount;
            RifleController.Instance.isReloading = isReloading;
            reloadCrosshair.gameObject.SetActive(false);
            defaultCrosshair.gameObject.SetActive(true);
            RifleController.Instance.ReloadedRifle();
        }
    }
}