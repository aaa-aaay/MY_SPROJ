
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Transform target;
    [SerializeField] private float _maxSpeed;
    [SerializeField] [Range(0,1)] private float _seekWeight;
    [SerializeField] [Range(0,1)] private float _obstacleAvoidanceWeight;
    [SerializeField] private float steeringTime;
    [SerializeField] private float avoidDistance;
    [SerializeField] private float _lookAheadDistance;
    private Rigidbody2D _rb;
    private Vector2 _velocity;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private Vector2 Seek()
    {
        var disiredVelocity = Vector2.ClampMagnitude( target.position - transform.position, _maxSpeed);
        var steeringVector = disiredVelocity - _rb.linearVelocity;

        return steeringVector * _seekWeight;
    }

    private Vector2 ObstacleAvoidance()
    {
        Vector2 steeringVector = Vector2.zero;

        if (_rb.linearVelocity == Vector2.zero) return steeringVector;

        RaycastHit2D hitInfo = Physics2D.CircleCast(
            origin: transform.position,
            radius: 1f,
            direction: _rb.linearVelocity.normalized,
            distance: _lookAheadDistance,
            layerMask: LayerMask.GetMask("Ground")
        );

        if (!hitInfo) return steeringVector;


        Vector2 avoidDirection = new Vector2(-_rb.linearVelocity.y, _rb.linearVelocity.x).normalized;


        Vector2 targetPosition = hitInfo.point + avoidDirection * avoidDistance;
        Vector2 desiredVelocity = Vector2.ClampMagnitude(targetPosition - (Vector2)transform.position, _maxSpeed);

        steeringVector = desiredVelocity - _rb.linearVelocity;
        return steeringVector * _obstacleAvoidanceWeight;
    }

    private void FixedUpdate()
    {
        if(target == null) return;

        Vector2 steeringVector = Seek() + ObstacleAvoidance();
        var expectedEndPos = transform.position + Vector3.ClampMagnitude(_rb.linearVelocity + steeringVector, _maxSpeed);

        Vector2.SmoothDamp(transform.position, expectedEndPos, ref _velocity, steeringTime); 
        _rb.linearVelocity = _velocity;


        if (_rb.linearVelocity != Vector2.zero)
            transform.right = _rb.linearVelocity.normalized;


        Debug.DrawRay(transform.position, _rb.linearVelocity.normalized * _lookAheadDistance, Color.red);
    }
}
