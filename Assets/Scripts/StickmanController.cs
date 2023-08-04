using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(Animator))]
public class StickmanController : MonoBehaviour
{
    [SerializeField]
    private int _kickForce = 400;
    private Animator _animator;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private Rigidbody[] _allRigidBodies;
    private bool _isRagDoll = false;
    private Vector3 _deltaStickPosition;

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

        _deltaStickPosition = transform.position - _allRigidBodies[0].transform.position;        
    }

    public void SetDefault()
    {
        _isRagDoll = false;
        transform.SetPositionAndRotation(_defaultPosition, _defaultRotation);        
        _animator.enabled = true;

        foreach (var rb in _allRigidBodies)
            rb.isKinematic = true;
    }

    public void SetAnimated()
    {
        _isRagDoll = false;
        
        foreach (var rb in _allRigidBodies)
            rb.isKinematic = true;

        Vector3 newPos = _allRigidBodies[0].transform.position + _deltaStickPosition;
        transform.position = new Vector3(newPos.x, 0, newPos.z);

        _animator.enabled = true;
    }

    public async void SetRagDoll()
    {        
        _animator.enabled = false;

        foreach (var rb in _allRigidBodies)
            rb.isKinematic = false;
        // so so
        await Task.Delay(1000);

        _isRagDoll = true;
    }

    private void FixedUpdate()
    {
        // so so
        if (_isRagDoll)
        {
            foreach (var rb in _allRigidBodies)
            {                
                if (rb.velocity.sqrMagnitude > 0.1f)
                    return;
            }

            SetAnimated();
        }
    }
}
