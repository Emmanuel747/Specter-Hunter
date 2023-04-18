using UnityEngine;

public class AddPrefabToPellets : MonoBehaviour
{
    public GameObject prefab;
    public GameObject lightObject;

    private void Start()
    {
        // Find all Pellet objects in the scene
        GameObject[] pellets = GameObject.FindGameObjectsWithTag("Pellet");

        // Add the prefab and light object as child to each Pellet object
        foreach (GameObject pellet in pellets)
        {
            GameObject newPrefab = Instantiate(prefab, pellet.transform);
            GameObject newLight = Instantiate(lightObject, pellet.transform);
            newPrefab.transform.position = pellet.transform.position;
            newLight.transform.position = pellet.transform.position;
        }
    }
}
