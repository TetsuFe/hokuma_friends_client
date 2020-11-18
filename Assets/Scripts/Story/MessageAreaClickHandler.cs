using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Story
{
    public class MessageAreaClickHandler : MonoBehaviour, IPointerClickHandler
    {
     public UnityEvent onClick;
     
         public void OnPointerClick(PointerEventData pointerEventData)
         {
             //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
             Debug.Log(name + " Game Object Clicked!", this);
     
             // invoke your event
             onClick.Invoke();
         }

    }
}