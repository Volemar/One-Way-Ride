using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

	
		private PlayerInput _playerInput;
		private CharacterController _controller;
		private PlayerControls _controls;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		//Crouch variables
		private Vector3 _defaultColliderDimensions = new Vector3(0,1f,0);
		private Vector3 _crouchColliderDimensions = new Vector3(0,0.5f,0);
		private float _crouchCharControllerHeight = 1;
		private float _defaultCharControllerHeight = 2;
		private Vector3 _defaultCameraHolderPosition = Vector3.zero;
		private Vector3 _crouchCameraHolderPosition = new Vector3(0,-0.5f,0);
		public bool isCrouching = false;
		[SerializeField] private Transform _cameraHolder;
		private float crouchSpeed = 2f;
		private HeadBob _headbob;

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
			_headbob = GetComponentInChildren<HeadBob>();
		}

		public void CrouchToggle()
		{
			if(!_controls.GetPlayerCrouchThisFrame) return;
			if(isCrouching)
			{
				_controls.GetPlayerCrouchThisFrame = false;
				isCrouching = false;
				_controller.height = _defaultCharControllerHeight;
				_controller.center = _defaultColliderDimensions;
				_headbob.SetDefaultPositionForCamera(0);
				StartCoroutine(MoveCameraHolder(_defaultCameraHolderPosition));
				return;
			}
			_controls.GetPlayerCrouchThisFrame = false;
			isCrouching = true;
			_controller.height = _crouchCharControllerHeight;
			_controller.center = _crouchColliderDimensions;
			_headbob.SetDefaultPositionForCamera(-.5f);
			StartCoroutine(MoveCameraHolder(_crouchCameraHolderPosition));
		}

		public IEnumerator MoveCameraHolder(Vector3 destination)
		{
			float totalMovementTime = .5f; //the amount of time you want the movement to take
			float currentMovementTime = 0f;//The amount of time that has passed
			while (Vector3.Distance(_cameraHolder.localPosition, destination) > 0 && totalMovementTime < currentMovementTime) 
			{
				currentMovementTime += Time.deltaTime;
				_cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, destination, currentMovementTime / totalMovementTime);
				yield return null;
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_controls = FindObjectOfType<PlayerControls>();
			_playerInput = GetComponent<PlayerInput>();

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			CrouchToggle();
			Move();
		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_controls.GetMouseDelta.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				//float deltaTimeMultiplier = _playerInput.controlSchemes[0]. ? 1.0f : Time.deltaTime;
				float deltaTimeMultiplier = 1.0f;
				
				_cinemachineTargetPitch += _controls.GetMouseDelta.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _controls.GetMouseDelta.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _controls.GetPlayerSprintThisFrame ? SprintSpeed : MoveSpeed;
			if(isCrouching) targetSpeed = crouchSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_controls.GetPlayerMovement == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * _controls.GetPlayerMovement.magnitude, Time.unscaledDeltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_controls.GetPlayerMovement.x, 0.0f, _controls.GetPlayerMovement.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_controls.GetPlayerMovement != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _controls.GetPlayerMovement.x + transform.forward * _controls.GetPlayerMovement.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.unscaledDeltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.unscaledDeltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded && !isCrouching)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_controls.GetPlayerJumpedThisFrame && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.unscaledDeltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.unscaledDeltaTime;
				}

				// if we are not grounded, do not jump
				//_controls.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.unscaledDeltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}