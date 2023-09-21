using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;


public class IKClassController : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint izquierda;
    [SerializeField] TwoBoneIKConstraint derecha;
    [SerializeField] Slider leftSlider;
    [SerializeField] Slider rightSlider;

    // Start is called before the first frame update
    void Start()
    {
        derecha.data.targetPositionWeight = rightSlider.value;
        izquierda.data.targetPositionWeight = leftSlider.value;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeLeft()
    {
        izquierda.data.targetPositionWeight = leftSlider.value;
    }
    public void changeRight() {
        derecha.data.targetPositionWeight = rightSlider.value;

    }
}
