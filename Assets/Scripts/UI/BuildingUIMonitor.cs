using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildingUIMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingNameText;
    [SerializeField] private Image buildingIcon;
    [SerializeField] private List<UnitUI> _productionItems = new List<UnitUI>();
    [SerializeField] private GameObject buildingUIPanel;
    [SerializeField] private GameObject productionInfoPanel;
    [SerializeField] private GameObject uiProductionPrefab,uiProductParent,uiBuildingPanel,uiBuildingArrowPanel;
    public UnitObjectDataSO UnitObjectDataSo;

    private void Start()
    {
        EventBus.RegisterEvent<int>(EventName.SelectedBuildingEvent, ShowBuildingInfo);
        EventBus.RegisterEvent(EventName.ObjectPlacer, ObjectPlacer);
    }

    private void OnDestroy()
    {
        EventBus.UnregisterEvent<int>(EventName.SelectedBuildingEvent, ShowBuildingInfo);
        EventBus.UnregisterEvent(EventName.ObjectPlacer, ObjectPlacer);
    }

    public void ShowBuildingInfo(int id)
    {
        if (id == -1)
        {
            Debug.Log("Closing building info ui");
            
        }
        else
        {
            Debug.Log("showing building info "+id);
            ObjectData showingData = GameManager.Instance.ObjectDataSO.objectsData[id];
            buildingNameText.text = showingData.Name;
            buildingIcon.sprite = showingData.Image;
            for (int i = 0; i < showingData.buildDependency.Count; i++)
            {
                if (_productionItems.Count > i)
                {
                    _productionItems[i].SetUI(showingData.buildDependency[i].productionName,
                        UnitObjectDataSo.unitsData.Find(x=>x.Name ==showingData.buildDependency[i].productionName).UnitUIImage) ;
                    
                }
                else
                {
                    GameObject newProductUI = Instantiate(uiProductionPrefab, uiProductParent.transform);
                    UnitUI newUI = newProductUI.GetComponent<UnitUI>();
                    newUI.SetUI(showingData.buildDependency[i].productionName,
                        UnitObjectDataSo.unitsData.Find(x=>x.Name ==showingData.buildDependency[i].productionName).UnitUIImage) ;
                    _productionItems.Add(newUI);
                }
                
            }
            productionInfoPanel.SetActive(showingData.buildDependency.Count>0);
        }
        buildingUIPanel.SetActive(id != -1);
        
    }

    public void ObjectPlacer()
    {
        uiBuildingPanel.SetActive(false);
        uiBuildingArrowPanel.SetActive(true);
    }
}

