// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class ActionButton : MonoBehaviour , IPointerClickHandler
// {
    
//     private IUseable useable;

//     public ActionButton MyButton {get; private set;}

//     // Start is called before the first frame update
//     void Start()
//     {
//         MyButton = GetComponent<Button>();
//         MyButton.OnClick.AddListenter(OnClick);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void OnClick()
//     {
//         if(useable != null)
//         {
//             useable.Use();
//         }
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {

//     }
// }
