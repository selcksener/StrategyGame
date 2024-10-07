using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExtension
{
    private Camera cam = Camera.main;
    public static RaycastHit2D GetRaycastHit2D(Vector2 point, Vector2 normal, float distance,LayerMask layer)
    { 
        return Physics2D.Raycast(point, normal,
            Mathf.Infinity, layer);
    }
    public static RaycastHit2D GetRaycastHit2DFromCamera( LayerMask layer)
    { 
        return Physics2D.Raycast(GameManager.Instance.Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
            Mathf.Infinity, layer);
    }

   
}
