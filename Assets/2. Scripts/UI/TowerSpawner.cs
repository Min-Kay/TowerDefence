using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public void spawnTower(Transform tileTransform, GameObject tower)
    {
        Tile tileInfo = tileTransform.GetComponent<Tile>();

        if (!tileInfo.isAllow)
        {
            return;
        }
        tileInfo.isAllow = false;
        Instantiate(tower, tileTransform.position, Quaternion.identity);
    }
}
