using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
            {
                GetComponent<NavMeshAgent>().SetDestination(raycastHit.point);
                GetComponent<NavMeshAgent>().isStopped = false;
            }


            
        }
        
        if (GetComponent<NavMeshAgent>().path.status == NavMeshPathStatus.PathComplete)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
}
