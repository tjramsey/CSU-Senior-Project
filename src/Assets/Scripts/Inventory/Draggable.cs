using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;
    public GameObject temp;


    public void DragHandler(BaseEventData data)
    {
        temp = GameObject.Find("Canvas");
        canvas = temp.GetComponent<Canvas>();
        if(canvas == null){
            Debug.Log("Could not locate Canvas component");
        }
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position
        );

        transform.position = canvas.transform.TransformPoint(position);
    }
}