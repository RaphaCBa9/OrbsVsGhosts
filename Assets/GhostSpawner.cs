using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{

    public GameObject ghostPrefab; 
    public float spawnInterval = 2f;

    public float minEdgeDistance = 0.5f;
    public MRUKAnchor.SceneLabels spawnLabel;
    public float normalOffset = 0.5f; 

    public int maxTries = 10;

    private float timer;




    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGhost();
            timer = 0f;
        }

    }

    void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();


        int spawnTryCount = 0;

        while (spawnTryCount < maxTries)
        {


            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabel), out Vector3 pos, out Vector3 norm); ;

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0f;

                GameObject ghost = Instantiate(ghostPrefab, randomPositionNormalOffset, Quaternion.identity);

                ghost.transform.parent = transform;

                return;
            }
            else
            {
                spawnTryCount++;
            }

        }

            

    }
}
