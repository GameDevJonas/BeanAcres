using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SoilSeedChecker : MonoBehaviour
{
    public bool canPlant;

    public SoilBehaviour activeSoil;

    public float overlapRadius;
    public LayerMask soilMask;

    public GameObject activeParticlePrefab, particles;

    public GameObject placementIndicator;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    public bool placementPoseIsValid = false;

    public SwapTools tools;

    void Start()
    {
        tools = FindObjectOfType<SwapTools>();
        canPlant = false;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        return;
#endif
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        Collider[] soils = Physics.OverlapSphere(placementIndicator.transform.position, overlapRadius, soilMask);
        if (soils.Length > 0)
        {
            foreach (Collider soil in soils)
            {
                if (soil.GetComponent<SoilBehaviour>().myStage == SoilBehaviour.SoilStage.empty)
                {
                    activeSoil = soil.GetComponent<SoilBehaviour>();
                }
            }
        }
        else
        {
            activeSoil = null;
        }

        if (activeSoil != null && particles == null)
        {
            particles = Instantiate(activeParticlePrefab, activeSoil.transform.position, Quaternion.identity);
        }
        else if (particles != null && activeSoil == null)
        {
            Destroy(particles);
        }

        if (particles.transform.position != activeSoil.transform.position && activeSoil != null)
        {
            particles.transform.position = activeSoil.transform.position;
        }

    }

    public void PlantSeed()
    {
        if (activeSoil != null)
        {
            //Check for current seed
            activeSoil.PlantSeed(tools.currentSeed);
        }
    }

    public void SwitchSeed(SwapTools.Plants plant)
    {
        tools.currentSeed = plant;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(placementIndicator.transform.position, overlapRadius);
    }


    private void OnDisable()
    {
        Destroy(particles);
        activeSoil = null;
    }
}
