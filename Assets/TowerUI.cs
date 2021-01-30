using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    private UiCtrl ui;

    private void Start()
    {
        ui = GetComponent<UiCtrl>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CastRay();
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if(hit && hit.transform.tag == "Tower")
        {
            GameManager.instance.targetTower = hit.transform.GetComponent<TowerCtrl>();
            ui.ShowTowerUi();
        }
    }
}
