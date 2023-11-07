using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] public Animator BotAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.J)) //walk
            { BotAnimator.SetInteger("TheInput", 1); }

            if (Input.GetKeyDown(KeyCode.I))//jump
            { BotAnimator.SetInteger("TheInput", 2); }

            if (Input.GetKeyDown(KeyCode.H)) //Punch
            { BotAnimator.SetInteger("TheInput", 3); }

            if (Input.GetKeyDown(KeyCode.L))//Dash
            { BotAnimator.SetInteger("TheInput", 4); }

            if (Input.GetKeyDown(KeyCode.K)) //Crouch
            { BotAnimator.SetInteger("TheInput", 5); }

        }
        else BotAnimator.SetInteger("TheInput", 0);


    }
}
