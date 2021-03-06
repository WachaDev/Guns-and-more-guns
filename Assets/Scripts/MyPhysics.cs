using UnityEngine;

public class MyPhysics : MonoBehaviour
{

    [Header("Properties")]

    [SerializeField] private CharacterController characterController;
    public float gravity = -9.81f;
    [Range(0, 100)] public float mass = 1;
    public Vector3 velocity;

    [Header("Ground Analyzer")]
    
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _groundChecker;
    public bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
    }

    public void Gravity()
    {
        isGrounded = UnityEngine.Physics.CheckSphere(_groundChecker.position, _radius, _ground);

        velocity.y += (gravity * mass * Time.deltaTime);

        characterController.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
}