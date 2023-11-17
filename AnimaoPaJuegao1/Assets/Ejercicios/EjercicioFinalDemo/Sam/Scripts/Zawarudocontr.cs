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

    [SerializeField] AudioSource audi;
    [SerializeField] AudioClip clip;

    private float time = 4.0f;

    string mat;
    public bool sequencing = false;
    // Start is called before the first frame update
    void Start()
    {
        zawaMaterial.SetVector("_Position", transform.position);
        Debug.Log(zawaMaterial.GetVector("_Position"));
        //Debug.Log(zawaMaterial.GetFloat("_Valor"));
        zawardo.passMaterial = NorMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sequencing == false  )
        {
            StartCoroutine(sequence());
        }
        zawaMaterial.SetVector("_Position", transform.position);

    }
    public void slideChange()
    {
        //zawaMaterial.SetFloat("_Valor", zawardoSlider.value);
        time = zawardoSlider.value;
    }

    IEnumerator sequence()
    {
        sequencing = true;
        zawardo.passMaterial=zawaMaterial;
        audi.PlayOneShot(clip, 10f);
        yield return new WaitForSeconds(time);
        zawardo.passMaterial = NorMaterial;
        sequencing = false;
    }
}
