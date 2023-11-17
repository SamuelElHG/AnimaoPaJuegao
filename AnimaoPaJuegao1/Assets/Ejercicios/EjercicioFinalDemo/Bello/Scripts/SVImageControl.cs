using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Image pickerImage;
    private RawImage SVimage;
    private ColourPcikerControl CC;
    private RectTransform rectTransform, pickerTransform;

    private void Awake()
    {
        SVimage = GetComponent<RawImage>();
        CC = GetComponent<ColourPcikerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = pickerImage.GetComponent<RectTransform>();
        pickerTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    // Update is called once per frame
    void UpdateColour(PointerEventData eventData)
    {
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);
        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;
        pos.x = Mathf.Clamp(pos.x, -deltaX, deltaX);
        pos.y = Mathf.Clamp(pos.y, -deltaY, deltaY);

        float x= pos.x + deltaX;
        float y= pos.y + deltaY;

        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = pos;
        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);

        CC.SetSV(xNorm, yNorm);
    }
   

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }
}
