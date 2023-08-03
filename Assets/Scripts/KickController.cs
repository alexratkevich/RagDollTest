using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class KickController : MonoBehaviour, IPointerDownHandler
{            
    private Rigidbody _rigidbody;
    private StickmanController _stickmanController;
    private int _kickForce;

    public void Initialize(StickmanController stickmanController)
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _stickmanController = stickmanController;
        _kickForce = stickmanController.kickForce;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _stickmanController.SetRagDoll();
        _rigidbody.AddForce(Vector3.forward * _kickForce, ForceMode.Impulse);
    }   
}
