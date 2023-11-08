using UnityEngine;
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private JoystickController joystickController;
        [SerializeField] private float moveSpeed;
        [SerializeField] private PlayerAnimator playerAnimator;
        
        private CharacterController _characterController;
        private Vector3 _moveVector;
        private float _gravity = -9.81f;
        private float _gravityMultiplier = 3f;
        private float _gravityVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            _moveVector = joystickController.GetMovePosition()*moveSpeed*Time.deltaTime/Screen.width;
            _moveVector.z = _moveVector.y;
            _moveVector.y = 0;
            
            playerAnimator.ManageAnimations(_moveVector);
            ApplyGravity();
            _characterController.Move(_moveVector);
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded&&_gravityVelocity<0f)
            {
                _gravityVelocity = -1f;
            }
            else
            {
                _gravityVelocity += _gravity * _gravityMultiplier * Time.deltaTime;

            }

            _moveVector.y = _gravityVelocity;
        }
    }
