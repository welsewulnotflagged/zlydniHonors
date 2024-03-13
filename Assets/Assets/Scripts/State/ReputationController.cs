using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationController : MonoBehaviour
{
    private float overallRep = 0;

    private float approvalRate = 0;
    private float approvalMax = 1;

    public GameObject repCanvas;

    public Slider slider;

    public bool questFinished;
    public bool isApproved;
    
    // Start is called before the first frame 
    void Start()
    {
        questFinished = false;
        isApproved = false;
    }

    public void HandleApproval()
    {
        if (questFinished && isApproved && approvalRate<approvalMax)
        {
            slider.value =+ 1;
            Debug.Log("rep increased");
        }
        else if (!questFinished && isApproved && approvalRate>=0)
        {
            slider.value =- 1;
            Debug.Log("rep decreased");
        }
    }
}
