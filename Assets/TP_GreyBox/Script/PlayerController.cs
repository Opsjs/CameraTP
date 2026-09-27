using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum CONTROLS {
        WORLD,
        CAMERA,
    }

    public float speed = 10.0f;
    public CONTROLS controls = CONTROLS.CAMERA;

    Rigidbody _rigidbody = null;
    protected bool IsActive { get; private set; }

    public void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 direction = Vector3.zero;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        switch (controls) {
            case CONTROLS.CAMERA:
                forward = Camera.main.transform.forward;
                forward.y = 0;
                forward.Normalize();

                right = Camera.main.transform.right;
                right.y = 0;
                right.Normalize();

                break;
        }
        direction += ((Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0)) * right;
        direction += ((Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)) * forward;
        direction.Normalize();
        _rigidbody.linearVelocity = direction * speed + Vector3.up * _rigidbody.linearVelocity.y;
    }
}
