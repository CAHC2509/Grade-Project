using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int projectilesAmount;
    [SerializeField] private Transform projectilesParent;

    [Space, Header("Projectiles particles")]
    [SerializeField] private GameObject greenParticlesPrefab;
    [SerializeField] private GameObject blueParticlesPrefab;
    [SerializeField] private Transform projectilesParticlesParent;

    private List<GameObject> projectilesPool = new List<GameObject>();
    private List<GameObject> greenParticlesPool = new List<GameObject>();
    private List<GameObject> blueParticlesPool = new List<GameObject>();

    public enum ObjectType
    {
        NONE,
        PROJECTILE,
        GREEN_PARTICLES,
        BLUE_PARTICLES
    }

    public static ObjectPool Instance;

    private void Awake() => Instance = Instance == null ? this : Instance;

    private void Start()
    {
        CreatePool(projectilePrefab.gameObject, projectilesAmount, projectilesParent, projectilesPool);
        CreatePool(greenParticlesPrefab, projectilesAmount, projectilesParticlesParent, greenParticlesPool);
        CreatePool(blueParticlesPrefab, projectilesAmount, projectilesParticlesParent, blueParticlesPool);
    }

    private void CreatePool(GameObject objectPrefab, int objectsAmount, Transform targetParent, List<GameObject> targetPool)
    {
        for (int i = 0; i < objectsAmount; i++)
        {
            GameObject instanciatedObject = Instantiate(objectPrefab, targetParent);
            instanciatedObject.SetActive(false);
            targetPool.Add(instanciatedObject);
        }
    }

    public GameObject GetPooledObject(ObjectType objectType)
    {
        List<GameObject> targetPool = new List<GameObject>();

        switch (objectType)
        {
            case ObjectType.PROJECTILE:
                targetPool = projectilesPool;
                break;

            case ObjectType.GREEN_PARTICLES:
                targetPool = greenParticlesPool;
                break;

            case ObjectType.BLUE_PARTICLES:
                targetPool = blueParticlesPool;
                break;

            default:
                break;
        }

        foreach (GameObject pooledObject in targetPool)
        {
            if (!pooledObject.activeInHierarchy)
                return pooledObject;
        }

        return null;
    }
}
