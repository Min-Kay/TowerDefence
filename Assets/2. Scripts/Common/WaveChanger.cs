using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveChanger : MonoBehaviour
{
    private Text WaveText;
    private void Start()
    {
        WaveText = GetComponentInChildren<Text>();
    }
    public void updateWave(int MaxWave, int Wavecount)
    {
        //Wave.text = Wave.ToString();
        WaveText.text = (Wavecount+1+"/"+MaxWave).ToString();
        
    }
}
