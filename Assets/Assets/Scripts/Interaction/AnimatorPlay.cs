using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlay : MonoBehaviour
{
   public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
       // animator = GetComponent<Animator>();
       animator.SetBool("isTalking", false);
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
