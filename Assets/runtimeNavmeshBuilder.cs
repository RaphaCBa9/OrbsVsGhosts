using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class runtimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;

    // Start is called before the first frame update
    void Start()
    {

        navmeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);
        if (navmeshSurface == null)
        {
            Debug.LogError("NavMeshSurface component not found on this GameObject.");
            return;
        }
        

    }

    public void BuildNavmesh()
    {
        // Start the coroutine to build the NavMesh
        StartCoroutine(BuildNavmeshRoutine());
    }

    public IEnumerator BuildNavmeshRoutine()
    {
        yield return new WaitForEndOfFrame();
        navmeshSurface.BuildNavMesh();
    }

}
