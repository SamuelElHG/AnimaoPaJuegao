using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControCompu : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audi;
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            animator.SetBool("Input", true);
            audi.PlayOneShot(clip, 10f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Input", false);
        }

    }
}
