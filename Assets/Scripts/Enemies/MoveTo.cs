// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {
    
  public Transform goal;
  

  void Start () {
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    agent.destination = goal.position; 
  }

  void Update () {
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    if (agent.isOnNavMesh == false)
    {                  
      agent = GetComponent<NavMeshAgent>();
    }
    agent.SetDestination(goal.position);
  }
  // void Update () {
  //   NavMeshAgent agent = GetComponent<NavMeshAgent>();
  //   if (agent.isOnNavMesh == false)
  //   {                  
  //     agent.Warp(goal.position);
  //   }
  //   agent.SetDestination(goal.position);
  // }
}


