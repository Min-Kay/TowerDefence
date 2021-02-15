using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    public Transform name;
    public GameObject press;

    public Transform enemy;
    public Transform fireball;
    public Transform fireball1;
    public Transform fireball2;

    private bool isLoaded;
    private bool sceneChange;

    private void Start()
    {
        StartCoroutine(MoveName(name));
        StartCoroutine(MoveEnemy(enemy, 0.5f));
        StartCoroutine(MoveBall(fireball));
        StartCoroutine(MoveBall(fireball1));
        StartCoroutine(MoveBall(fireball2));
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isLoaded)
        {
            sceneChange = true;
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator MoveName(Transform rt)
    {
        var origin = rt.position.y;
        rt.position = new Vector3(0, -7);
        press.SetActive(false);

        while (rt.position.y < origin)
        {
            rt.Translate(0, 0.5f, 0);
            if (rt.position.y >= origin)
            {
                isLoaded = true;
                press.SetActive(true);
                StartCoroutine(FadeColor(press.GetComponent<Text>()));
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    IEnumerator FadeColor(Text text)
    {
        var i = 0.5f;
        var isHalf = true;
        while (!sceneChange)
        {
            if (isHalf)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i += 0.05f);
                if (i >= 1)
                    isHalf = false;
                yield return new WaitForSeconds(0.1f);
            }
            else if(!isHalf)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i -= 0.05f);
                if (i <= 0.5)
                    isHalf = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }

    IEnumerator MoveEnemy(Transform tr, float y)
   {
        var i = 0f;
        var isZero = false;
        while(!sceneChange)
        {
            if(isZero == false)
            {
                tr.Translate(0, 0.01f, 0);
                i += 0.01f;
                if(i>=y)
                {
                    isZero = true;
                }
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                tr.Translate(0, -0.01f, 0);
                i -= 0.01f;
                if(i<=0)
                {
                    isZero = false;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
   }

    IEnumerator MoveBall(Transform tr)
    {
        var i = 0;
        var state = 0;

        while(!sceneChange)
        {
            switch (state)
            {
                case 0:
                    tr.Translate(Mathf.Cos(i), Mathf.Sin(i++), 0);
                    if (i%90 == 0)
                        state = 1;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 1:
                    tr.Translate(Mathf.Cos(i), Mathf.Sin(i++), 0);
                    if (i % 90 == 0)
                        state = 2;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 2:
                    tr.Translate(Mathf.Cos(i), Mathf.Sin(i++), 0);
                    if (i % 90 == 0)
                        state = 3;
                    yield return new WaitForSeconds(0.1f);
                    break;
                case 3:
                    tr.Translate(Mathf.Cos(i), Mathf.Sin(i++), 0);
                    if (i % 90 == 0)
                        state = 0;
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
        yield return null;
    }
}
