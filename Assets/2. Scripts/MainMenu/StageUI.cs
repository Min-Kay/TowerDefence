using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    private MainMenuUiCtrl ui;

    private void Start()
    {
        ui = GetComponent<MainMenuUiCtrl>();
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

        if (hit && hit.transform.tag == "Stage")
        {
            //GameManager.instance.targetTower = hit.transform.GetComponent<TowerCtrl>();
            //ui.ShowTowerUi();
        }
    }
}
