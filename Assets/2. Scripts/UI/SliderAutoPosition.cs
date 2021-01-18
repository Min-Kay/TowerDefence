using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAutoPosition : MonoBehaviour
{
    private Vector3 distance = Vector3.down * 20.0f;//거리
    private Transform targetTransform;
    private RectTransform rectTramsform;


    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTramsform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(targetTransform== null)//적대상이 사라지면 삭제
        {
            Destroy(gameObject);
            return;
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTramsform.position = screenPosition + distance;
    }
}
