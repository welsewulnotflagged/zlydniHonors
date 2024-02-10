using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlay : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isTalking", true);
        }
    }
    
    void OnTriggerExit (Collider other)
    {
        animator.SetBool("isTalking", false);
        Debug.Log("No longer in contact");
    }
}
