using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] public Animator BotAnimator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float JumpDelay;
    [SerializeField] private float dashForce;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.J)) //walk backwards
            { BotAnimator.SetInteger("TheInput", 1);
                transform.position = transform.position + new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.L)) //walk foward
            { BotAnimator.SetInteger("TheInput", 1);
                transform.position = transform.position + new Vector3(1 * moveSpeed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.I))//jump
            {
                StartCoroutine(jumpCoru());
               // BotAnimator.SetInteger("TheInput", 2);
               // transform.position = transform.position + new Vector3(0, JumpForce, 0);
            }

            if (Input.GetKeyDown(KeyCode.H)) //Punch
            { BotAnimator.SetInteger("TheInput", 3);
                transform.Rotate(0,40, 0);
                    }

            if (Input.GetKeyDown(KeyCode.M))//Dash
            { BotAnimator.SetInteger("TheInput", 4);
                transform.position = transform.position + new Vector3(dashForce, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.K)) //Crouch
            { BotAnimator.SetInteger("TheInput", 5); }


            if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.M))//dash izquierda
            {
                BotAnimator.SetInteger("TheInput", 4);
                transform.position = transform.position + new Vector3(-dashForce, 0, 0);
            }
            if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.M))//dash derecha
            {
                BotAnimator.SetInteger("TheInput", 4);
                transform.position = transform.position + new Vector3(dashForce, 0, 0);
            }

        }
        else BotAnimator.SetInteger("TheInput", 0);


    }
    IEnumerator jumpCoru()
    {
        BotAnimator.SetInteger("TheInput", 2);
        yield return new WaitForSeconds(JumpDelay);
        transform.position = transform.position + new Vector3(0, JumpForce, 0);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Impacto");
        BotAnimator.SetInteger("TheInput", 6);


    }
}
