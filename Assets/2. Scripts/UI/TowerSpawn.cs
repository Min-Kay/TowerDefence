using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [Header("TowerSpawner Object")]
    public TowerSpawner towerSpawner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    [HideInInspector]
    public GameObject currentTower = null;

    private void Awake()
    {
        gameObject.SetActive(false);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile") && !hit.transform.CompareTag("Tower") && currentTower != null)
                {
                    towerSpawner.spawnTower(hit.transform, currentTower);
                    gameObject.SetActive(false);
                }
            }
        }
    }
   
}
