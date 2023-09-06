using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointClickController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100000f, 1 << 3);
            Debug.Log(hit.point);
            GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }

    }
}
