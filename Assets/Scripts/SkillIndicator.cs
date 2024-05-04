using UnityEngine;

public class SkillIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject skillParticlesPrefab;
    [SerializeField] private float verticalMoveDistance = 0.5f;
    [SerializeField] private float verticalMoveSpeed = 0.5f;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float minimumDistanceToFollow = 1.5f;

    private bool movingDown = true;

    private void Start() => transform.SetParent(null);
    
    private void Update()
    {
        MoveUpAndDown();
        FollowTarget();
    }

    private void MoveUpAndDown()
    {
        float step = verticalMoveSpeed * Time.deltaTime;

        if (movingDown)
        {
            transform.Translate(Vector3.down * step);

            if (transform.position.y <= target.position.y - verticalMoveDistance)
                movingDown = false;
        }
        else
        {
            transform.Translate(Vector3.up * step);

            if (transform.position.y >= target.position.y)
                movingDown = true;
        }
    }

    private void FollowTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > minimumDistanceToFollow)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    public void SpawnParticles() => Instantiate(skillParticlesPrefab, transform.position, Quaternion.identity);
}
