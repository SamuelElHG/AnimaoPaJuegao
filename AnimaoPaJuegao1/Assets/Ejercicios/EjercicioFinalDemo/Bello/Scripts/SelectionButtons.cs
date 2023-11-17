using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject firstEffect;
    [SerializeField]
    private GameObject secondEffect;
    [SerializeField]
    private GameObject thirdEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateEffect(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateEffect(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateEffect(3);
        }
    }

    public void ActivateEffect(int numberAssigned)
    {
        Debug.Log("pressed button");
        switch (numberAssigned)
        {
            case 1:
                if(secondEffect.activeSelf == false && thirdEffect.activeSelf == false)
                {
                    firstEffect.SetActive(true);
                }
                else if(secondEffect.activeSelf == true || thirdEffect.activeSelf == true)
                {
                    secondEffect.SetActive(false);
                    thirdEffect.SetActive(false);

                    firstEffect.SetActive(true);
                }
                break;
            case 2:
                if (firstEffect.activeSelf == false && thirdEffect.activeSelf == false)
                {
                    secondEffect.SetActive(true);
                }
                else if (firstEffect.activeSelf == true || thirdEffect.activeSelf == true)
                {
                    firstEffect.SetActive(false);
                    thirdEffect.SetActive(false);

                    secondEffect.SetActive(true);
                }
                break;
            case 3:
                if (secondEffect.activeSelf == false && firstEffect.activeSelf == false)
                {
                    thirdEffect.SetActive(true);
                }
                else if (secondEffect.activeSelf == true || firstEffect.activeSelf == true)
                {
                    secondEffect.SetActive(false);
                    firstEffect.SetActive(false);

                    thirdEffect.SetActive(true);
                }
                break;
            default:
                Debug.Log("default");
                break;
        }
    }
}
