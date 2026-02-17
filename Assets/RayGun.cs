using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{

    [SerializeField] private InputActionReference triggerReference;


    public LayerMask layerMask; 
    public LineRenderer lineRenderer;
    public Transform shootingPoint;
    public float maxLineDistance = 5; 
    public float lineDuration = 0.3f;

    public GameObject hitEffectPrefab;

    // Update is called once per frame
    void Update()
    {
        if (triggerReference.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint = Vector3.zero;

        if (hitSomething)
        {
            endPoint = hit.point;
            Debug.Log("Shoot() called");
            DoorTargetRay doorTarget = hit.collider.GetComponent<DoorTargetRay>();
            Debug.Log("Hit: " + hit.collider.name);
            if (doorTarget != null)
            {
                doorTarget.Trigger();
            }

            GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(-hit.normal));
            Destroy(hitEffect, 1f);

        } else {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        }

        LineRenderer line = Instantiate(lineRenderer); 
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineDuration);
    }
}
