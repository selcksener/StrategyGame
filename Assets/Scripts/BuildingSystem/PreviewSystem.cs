using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewOffsetY = 0f;

    [SerializeField] private GameObject previewObject;
    private PoolObjectType currentPreviewObjectType;
    
    /// <summary>
    /// start of the preview
    /// </summary>
    /// <param name="poolObjectType"></param>
    /// <param name="size"></param>
    public void StartShowingPreview(PoolObjectType poolObjectType, Vector2Int size)
    {
        //Get the object from pool
        previewObject = PoolManager.Instance.GetObject(poolObjectType);//Instantiate(prefab);
        currentPreviewObjectType=poolObjectType;
        PreparePreview(previewObject);
    }

    /// <summary>
    /// preview
    /// </summary>
    /// <param name="previewObject"></param>
    private void PreparePreview(GameObject previewObject)
    {
        Color col = previewObject.GetComponent<SpriteRenderer>().color;
        col = Color.green; ;
        col.a = 0.5f;
        previewObject.GetComponent<SpriteRenderer>().color = col;
    }

    /// <summary>
    /// stop the preview
    /// </summary>
    public void StopShowingPreview()
    {
        if (previewObject != null)
        {
            PoolManager.Instance.AddObjectInPool(currentPreviewObjectType,previewObject);
        }
    }
    /// <summary>
    /// updating the position of the preview object
    /// </summary>
    /// <param name="position"></param>
    /// <param name="validity"></param>
    public void UpdatePosition(Vector2 position, bool validity)
    {
        if (previewObject != null)
        {
            //moving the preview object
            MovePreview(position);
           
        }

        ApplyFeedbackToCursor(validity);
    }
    /// <summary>
    /// change of color according to  availability
    /// </summary>
    /// <param name="validity"></param>
    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.green : Color.red;
        c.a = .5f;
        previewObject.GetComponent<SpriteRenderer>().color = c;
    }
    /// <summary>
    /// moving the preview object
    /// </summary>
    /// <param name="position"></param>
    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y + previewOffsetY, position.z);
    }

}
