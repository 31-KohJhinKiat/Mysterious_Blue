using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static JoyStickManager instance;
    public RectTransform pad;

    private Vector2 directionVec;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);
        directionVec = new Vector3(transform.localPosition.x, transform.localPosition.y, 0).normalized;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        directionVec = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return directionVec;
    }

}
