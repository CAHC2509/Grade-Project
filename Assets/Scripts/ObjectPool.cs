using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private Projectile blueProjectilePrefab;
    [SerializeField] private Projectile greenProjectilePrefab;
    [SerializeField] private Projectile ovalProjectilePrefab;
    [SerializeField] private Projectile greenLaserPrefab;
    [SerializeField] private Projectile missilePrefab;
    [SerializeField] private int blueProjectilesAmount;
    [SerializeField] private int greenProjectilesAmount;
    [SerializeField] private int ovalProjectilesAmount;
    [SerializeField] private int greenLasersAmount;
    [SerializeField] private int missilesAmount;
    [SerializeField] private Transform projectilesParent;

    [Space, Header("Projectiles particles")]
    [SerializeField] private GameObject greenParticlesPrefab;
    [SerializeField] private GameObject blueParticlesPrefab;
    [SerializeField] private GameObject missileParticlesPrefab;
    [SerializeField] private Transform projectilesParticlesParent;

    private List<GameObject> blueProjectilesPool = new List<GameObject>();
    private List<GameObject> greenProjectilesPool = new List<GameObject>();
    private List<GameObject> ovalProjectilesPool = new List<GameObject>();
    private List<GameObject> greenLasersPool = new List<GameObject>();
    private List<GameObject> missilesPool = new List<GameObject>();
    private List<GameObject> greenParticlesPool = new List<GameObject>();
    private List<GameObject> blueParticlesPool = new List<GameObject>();
    private List<GameObject> missileParticlesPool = new List<GameObject>();

    public enum ObjectType
    {
        NONE,
        BLUE_PROJECTILE,
        GREEN_PROJECTILE,
        OVAL_PROJECTILE,
        GREEN_LASER,
        MISSILE,
        BLUE_PARTICLES,
        GREEN_PARTICLES,
        MISSILE_PARTICLES
    }

    public static ObjectPool Instance;

    private void Awake() => Instance = Instance == null ? this : Instance;

    private void Start()
    {
        CreatePool(blueProjectilePrefab.gameObject, blueProjectilesAmount, projectilesParent, blueProjectilesPool);
        CreatePool(greenProjectilePrefab.gameObject, greenProjectilesAmount, projectilesParent, greenProjectilesPool);
        CreatePool(ovalProjectilePrefab.gameObject, ovalProjectilesAmount, projectilesParent, ovalProjectilesPool);
        CreatePool(greenLaserPrefab.gameObject, greenLasersAmount, projectilesParent, greenLasersPool);
        CreatePool(missilePrefab.gameObject, missilesAmount, projectilesParent, missilesPool);
        CreatePool(greenParticlesPrefab, blueProjectilesAmount, projectilesParticlesParent, greenParticlesPool);
        CreatePool(blueParticlesPrefab, blueProjectilesAmount, projectilesParticlesParent, blueParticlesPool);
        CreatePool(missileParticlesPrefab, missilesAmount, projectilesParticlesParent, missileParticlesPool);
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
            case ObjectType.BLUE_PROJECTILE:
                targetPool = blueProjectilesPool;
                break;

            case ObjectType.GREEN_PROJECTILE:
                targetPool = greenProjectilesPool;
                break;
            
            case ObjectType.OVAL_PROJECTILE:
                targetPool = ovalProjectilesPool;
                break;
            
            case ObjectType.GREEN_LASER:
                targetPool = greenLasersPool;
                break;

            case ObjectType.MISSILE:
                targetPool = missilesPool;
                break;

            case ObjectType.BLUE_PARTICLES:
                targetPool = blueParticlesPool;
                break;

            case ObjectType.GREEN_PARTICLES:
                targetPool = greenParticlesPool;
                break;

            case ObjectType.MISSILE_PARTICLES:
                targetPool = missileParticlesPool;
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
