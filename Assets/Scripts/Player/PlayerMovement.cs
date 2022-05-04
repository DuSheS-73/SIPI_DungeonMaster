using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _footstepSfxFrequency;

    private Player _player;

    private Rigidbody _rb;
    private Animator _animator;

    private Vector3 _move;

    private float _footstepDistanceCounter = 0f;

    private void Start()
    {
        _player = gameObject.GetComponent<Player>();

        _rb = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * _moveSpeed * Time.fixedDeltaTime);

        if (_footstepDistanceCounter >= 1f / _footstepSfxFrequency)
        {
            _footstepDistanceCounter = 0f;
            _player.PlayFootstepSfx();
        }

        _footstepDistanceCounter += _move.magnitude * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Sets the player movement vector
    /// </summary>
    /// <param name="move">New movement vector</param>
    public void Move(Vector3 move)
    {
        _move = move;
    }

    /// <summary>
    /// Rotates the vector
    /// </summary>
    /// <param name="rotation">New rotation</param>
    /// <param name="origin">Rotation origin</param>
    public void Rotate(Vector2 rotation, float origin = 0f)
    {
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg + origin;
        transform.localRotation = Quaternion.Euler(angle * Vector3.up);
    }
}
