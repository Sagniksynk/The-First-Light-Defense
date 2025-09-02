using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image constructionProgeressImage;
    private void Awake()
    {
        constructionProgeressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }
    private void Update()
    {
        constructionProgeressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
