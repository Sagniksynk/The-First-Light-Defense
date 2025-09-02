using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilidingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Transform btnTemplate;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuilidingtypeList;
    private ListOfBulidngTypeSO listOfBulidngType;

    private Transform currentlySelectedButton;

    void Awake()
    {
        btnTemplate.gameObject.SetActive(false);
        listOfBulidngType = Resources.Load<ListOfBulidngTypeSO>(typeof(ListOfBulidngTypeSO).Name);

        int index = 0;
        float offsetAmount = 160f;

        // --- Add default Arrow button ---
        Transform arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
        arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null); // deselect
            UpdateSelectedButton(arrowBtn);
        });
        UpdateSelectedButton(arrowBtn); // select arrow by default
        MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };
 
        index++;

        // --- Add real building buttons ---
        foreach (BuildingTypeSO buildingType in listOfBulidngType.buildingTypes)
        {
            if (ignoreBuilidingtypeList.Contains(buildingType)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            BuildingTypeSO buildingTypeCopy = buildingType; // avoid closure issue
            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingTypeCopy);
                UpdateSelectedButton(btnTransform);
            });
            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.name +"\n" + buildingType.GetConstructionCostResourceString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };
            index++;
        }
    }

    private void UpdateSelectedButton(Transform newSelectedButton)
    {
        // Turn off old highlight
        if (currentlySelectedButton != null)
        {
            currentlySelectedButton.Find("Highlight").gameObject.SetActive(false);
        }

        // Turn on new highlight
        currentlySelectedButton = newSelectedButton;
        currentlySelectedButton.Find("Highlight").gameObject.SetActive(true);
        }
}
