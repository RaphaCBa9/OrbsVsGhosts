using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class orbSpawner : MonoBehaviour
{
    public int numberOfOrbs = 10;
    public GameObject orbPrefab;
    public float hight;

    public List<GameObject> spawnedOrbs = new List<GameObject>();

    public int maxNumberOfTries = 100;
    private int currentTries = 0;

    public static orbSpawner instance;


    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);

    }

    public void SpawnOrbs()
    {
        for (int i = 0; i < numberOfOrbs; i++)
        {
            Vector3 randomPosition = Vector3.zero;

            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            while (currentTries < maxNumberOfTries)
            {
                bool isValidPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,
                1,
                LabelFilter.Included(MRUKAnchor.SceneLabels.FLOOR),
                out randomPosition,
                out Vector3 n
                );

                if (isValidPosition) {
                    break;
                }
                currentTries++;
            }

            

            randomPosition.y = hight;

            GameObject spawned = Instantiate(orbPrefab, randomPosition, Quaternion.identity);
            spawnedOrbs.Add(spawned);
        }

    }

    public void RemoveOrb(GameObject orb)
    {
        spawnedOrbs.Remove(orb);
        Destroy(orb);

        if (spawnedOrbs.Count == 0)
        {
            Debug.LogError("All orbs collected!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

    }


}
