using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizard_Explosion : MonoBehaviour
{

    public int power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().HP -= power;
            Destroy(this.gameObject, 1);
        }
    }
}
