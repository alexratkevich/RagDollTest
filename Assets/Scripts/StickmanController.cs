using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StickmanController : MonoBehaviour
{
    [SerializeField]
    private int _kickForce = 400;
    private Animator _animator;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private Rigidbody[] _allRigidBodies;

    public int kickForce => _kickForce;
    
    public void Initialize()
    {        
        _animator = GetComponent<Animator>();
        transform.GetPositionAndRotation(out _defaultPosition, out _defaultRotation);
        _allRigidBodies = GetComponentsInChildren<Rigidbody>();

        // setup children ragdoll objects
        foreach (var rb in _allRigidBodies)
        {
            rb.isKinematic = true;
            rb.gameObject.AddComponent<KickController>().Initialize(this);
        }
    }

    public void SetDefault()
    {
        transform.SetPositionAndRotation(_defaultPosition, _defaultRotation);        
        _animator.enabled = true;

        foreach (var rb in _allRigidBodies)
            rb.isKinematic = true;
    }

    public void SetRagDoll()
    {
        _animator.enabled = false;

        foreach (var rb in _allRigidBodies)
            rb.isKinematic = false;        
    }
}
