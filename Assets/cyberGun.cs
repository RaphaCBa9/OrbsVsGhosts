//using Oculus.Interaction.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyberGun : MonoBehaviour
{
    public OVRInput.RawButton triggerButton;
    public LineRenderer bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public float maxLineLength = 5;
    public float bulletLifeTime = 2f;
    public AudioSource bulletSound;
    public AudioClip bulletClip;

    public string enemyTag;

    public LayerMask hitLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot()
    {
        bulletSound.PlayOneShot(bulletClip);
        Debug.Log("Trigger pressed");

        Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);

        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, maxLineLength, hitLayerMask);

        Vector3 endPoint = Vector3.zero;

        if (hit)
        {
            Debug.Log("Hit: " + hitInfo.collider.name);

            endPoint = hitInfo.point;

            GhostScript ghostScript = hitInfo.transform.GetComponentInParent<GhostScript>();

            if (ghostScript != null)
            {
                // Handle ghost hit logic here
                hitInfo.collider.enabled = false;
                ghostScript.kill();
            }

            if (hitInfo.collider.CompareTag(enemyTag))
            {
                // Handle enemy hit logic here
                Debug.Log("Hit an enemy!");
                Destroy(hitInfo.collider.gameObject);
            }
        }
        else
        {
            endPoint = bulletSpawnPoint.position + bulletSpawnPoint.forward * maxLineLength;
        }


        LineRenderer line = Instantiate(bulletPrefab);
        line.positionCount = 2;
        line.SetPosition(0, bulletSpawnPoint.position);

        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, bulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(triggerButton))
        {

            Shoot();

        }

    }
}
