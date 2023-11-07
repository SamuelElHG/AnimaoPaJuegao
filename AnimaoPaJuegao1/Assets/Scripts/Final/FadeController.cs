using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] public Animator FadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.D)) //walk
            { FadeAnimator.SetInteger("TheInput", 1);}

            if (Input.GetKeyDown(KeyCode.W))//jump
            {FadeAnimator.SetInteger("TheInput", 2);}

            if (Input.GetKeyDown(KeyCode.F)) //Punch
            { FadeAnimator.SetInteger("TheInput", 3); }

            if (Input.GetKeyDown(KeyCode.G))//Dash
            { FadeAnimator.SetInteger("TheInput", 4); }

            if (Input.GetKeyDown(KeyCode.S)) //Crouch
            { FadeAnimator.SetInteger("TheInput", 5); }

        }
        else FadeAnimator.SetInteger("TheInput", 0);
        

    }
}
