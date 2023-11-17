using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zawarudocontr : MonoBehaviour
{
    [SerializeField] FullScreenPassRendererFeature zawardo;
    [SerializeField] Material zawaMaterial;
    [SerializeField] Material NorMaterial;
    [SerializeField] Slider zawardoSlider;
    string mat;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(zawaMaterial.GetFloat("_Valor"));
        zawardo.passMaterial = NorMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            zawardo.passMaterial = zawaMaterial;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            zawardo.passMaterial = NorMaterial;
        }
    }
    public void slideChange()
    {
        zawaMaterial.SetFloat("_Valor", zawardoSlider.value);
    }
}
