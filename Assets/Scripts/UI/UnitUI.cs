using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitUI : MonoBehaviour
{
    public string unitID;
    public Image productImage;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void SetUI(string id,Sprite img)
    {
        unitID = id;
        productImage.sprite = img;
    }

    private void OnClick()
    {
        Debug.Log("Clicking "+unitID);
        EventBus.TriggerEvent<string>(EventName.CreateUnitEvent,unitID);
    }
}
