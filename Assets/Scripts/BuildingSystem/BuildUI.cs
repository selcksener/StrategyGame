using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public int itemID;
    public void ClickedItem()
    {
        EventBus.TriggerEvent<int>(EventName.StartPlacement,itemID); 
    }
}
