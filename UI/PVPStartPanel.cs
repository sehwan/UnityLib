using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PVPStartPanel : MonoBehaviour
{
    public Text txt_name;
    public Text txt_mmr;

    public async void ShowAndHide_async(string name, int mmr)
    {
        gameObject.SetActive(true);
        txt_name.text = name;
        txt_mmr.text = $"{mmr:n0}pt";
        await Task.Delay(5000);
        gameObject.SetActive(false);
    }
}
