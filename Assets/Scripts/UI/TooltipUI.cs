using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    private TextMeshProUGUI textMeshProUGUI;
    private RectTransform backGroundRectTransform;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform canvasRectTransform;
    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backGroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backGroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backGroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backGroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backGroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void SetText(string tooltipText)
    {
        textMeshProUGUI.SetText(tooltipText);
        textMeshProUGUI.ForceMeshUpdate();
        Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
        Vector2 padding = new Vector2(16, 25);
        backGroundRectTransform.sizeDelta = textSize + padding;
    }
    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
