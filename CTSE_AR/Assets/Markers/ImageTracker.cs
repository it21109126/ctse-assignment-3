using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;

    List<GameObject> ARObjects = new List<GameObject>();


    //[SerializeField]
    //private GameObject[] placeablePrefabs;
    //private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string,
    //GameObject>();
    //private ARTrackedImageManager trackedImageManager;

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
        //trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        //foreach (GameObject prefab in placeablePrefabs)
        //{ 
        //    GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //    newPrefab.name = prefab.name;
        //    spawnedPrefabs.Add(prefab.name, newPrefab);
        //}
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
        //trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
        //trackedImageManager.trackedImagesChanged += ImageChanged;
    }


    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Create object based on image tracked
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            foreach (var arPrefab in ArPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name)
                {
                    var newPrefab = Instantiate(arPrefab, trackedImage.transform);
                    ARObjects.Add(newPrefab);
                }
            }
        }

        //Update tracking position
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            foreach (var gameObject in ARObjects)
            {
                if (gameObject.name == trackedImage.name)
                {
                    gameObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                }
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            foreach (var gameObject in ARObjects)
            {
                if (gameObject.name == trackedImage.name)
                {
                    gameObject.SetActive(false);
                }
            }
        }

    }

    //private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    //{
    //    foreach (ARTrackedImage trackedImage in eventArgs.added)
    //    {
    //        UpdateImage(trackedImage);
    //    }
    //    foreach (ARTrackedImage trackedImage in eventArgs.updated) {
    //        UpdateImage(trackedImage);
    //    }

    //    foreach (ARTrackedImage trackedImage in eventArgs.removed) {
    //        spawnedPrefabs[trackedImage.name].SetActive(false);
    //    }
    //}


    //private void UpdateImage(ARTrackedImage trackedImage)
    //{
    //    string name = trackedImage.referenceImage.name;
    //    Vector3 posistion = trackedImage.transform.position;

    //    GameObject prefab = spawnedPrefabs[name];
    //    prefab.transform.position = posistion;
    //    prefab.SetActive(true);

    //    foreach(GameObject go in spawnedPrefabs.Values)
    //    {
    //        if(go.name != name)
    //        {
    //            go.SetActive(false);
    //        }
    //    }
    //}
}