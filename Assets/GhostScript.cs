using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class GhostScript : MonoBehaviour

{
    public Animator animator;
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float speed = 3.5f; // Speed of the ghost

    public float eatingDistance = 1f; // Distance to consider the ghost as "eating" the orb





    void Start()
    {
        
    }

    public GameObject getClosestOrb()
    {
        
        GameObject closestOrb = null;
        float closestDistance = Mathf.Infinity;

        List<GameObject> orbs = orbSpawner.instance.spawnedOrbs;

        foreach (GameObject orb in orbs)
        {
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0;
            Vector3 orbPosition = orb.transform.position;
            orbPosition.y = 0;




            float distance = Vector3.Distance(ghostPosition, orbPosition);
            if (distance < closestDistance)
            {
                Debug.Log("Orb Distance: " + orbPosition);
                Debug.Log("Ghost Distance: " + ghostPosition);
                closestDistance = distance;
                closestOrb = orb;
            }
        }

        if (closestDistance < eatingDistance)
        {
            Debug.LogError("Destroying orb");
            orbSpawner.instance.RemoveOrb(closestOrb);
            closestOrb = null;
        }

        return closestOrb;
    }


    void Update()
    {
        if (!agent.enabled)
        {
            return;
        }

        GameObject closestOrb = getClosestOrb();

        if (closestOrb)
        {
            Vector3 targetPosition = closestOrb.transform.position;

            agent.SetDestination(targetPosition);
            agent.speed = speed;

        }


        

    }

    public void kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");
 
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject); 
    }
}
