using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] public Animator FadeAnimator;
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
            if (Input.GetKey(KeyCode.A)) //walk foward
            { FadeAnimator.SetInteger("TheInput", 1);
            transform.position = transform.position+new Vector3(1*moveSpeed*Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.D)) //walk backwards
            { FadeAnimator.SetInteger("TheInput", 1);
                transform.position = transform.position + new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.W))//jump
            {
                StartCoroutine(jumpCoru());
               // FadeAnimator.SetInteger("TheInput", 2);
                //transform.position = transform.position + new Vector3(0,1* JumpForce * Time.deltaTime, 0);
            }

            if (Input.GetKeyDown(KeyCode.F)) //Punch
            { FadeAnimator.SetInteger("TheInput", 3); }

            if (Input.GetKeyDown(KeyCode.G))//Dash
            { FadeAnimator.SetInteger("TheInput", 4);
                transform.position = transform.position + new Vector3(-dashForce,0, 0);
            }

            if (Input.GetKeyDown(KeyCode.S)) //Crouch
            { FadeAnimator.SetInteger("TheInput", 5); }

        }
        else FadeAnimator.SetInteger("TheInput", 0);
        

    }
    IEnumerator jumpCoru()
    {
        FadeAnimator.SetInteger("TheInput", 2);
        yield return new WaitForSeconds(JumpDelay);
        transform.position = transform.position + new Vector3(0,JumpForce, 0);
    }
}
