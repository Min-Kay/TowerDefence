using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSkillList : MonoBehaviour
{
    public GameObject skillList;
   public void Close()
    {
        skillList.gameObject.SetActive(false);
    }
}
