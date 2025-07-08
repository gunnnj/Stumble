using System;
using System.Collections;
using DiasGames.Abilities;
using DiasGames.Components;
using UnityEngine;
using UnityEngine.Events;

public class BounceAbility : AbstractAbility
{

    [Header("Animation State")]
    [SerializeField] private string animJumpState = "Air.Jump";
    [SerializeField] private string animFallState = "Air.Falling";
    // [SerializeField] private string animHardLandState = "Air.Hard Land";
    [SerializeField] private string animDashState = "Air.Dash"; 

    [Header("Push Parameters")]
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float airControl = 0.5f; 
    [SerializeField] private float speedOnAir = 6f;
    [SerializeField] private float dashDistance = 2f; 
    [SerializeField] private float dashSpeed = 10f; 


    [Header("Sound FX")]
    // [SerializeField] private AudioClip pushSound; // Âm thanh khi đẩy
    // [SerializeField] private AudioClip hardLandClip; // Âm thanh khi hạ cánh mạnh

    [Header("Event")]
    [SerializeField] private UnityEvent OnLanded = null; // Sự kiện khi hạ cánh

    private IMover _mover = null;
    private IDamage _damage = null;
    private CharacterAudioPlayer _audioPlayer = null;
    private Transform _camera = null;
    private float _startSpeed;

    private Vector2 _inputVel;
    private Vector2 _startInput;
    private float _angleVel;
    private float _targetRotation;
    private float _highestPosition = 0;
    private bool _hardLanding = false;
    private Vector3 _pushVelocity; // Lưu vận tốc đẩy
    private bool _isDashing = false;

    private void Awake()
    {
        _mover = GetComponent<IMover>();
        _damage = GetComponent<IDamage>();
        _audioPlayer = GetComponent<CharacterAudioPlayer>();
        _camera = Camera.main.transform;
    }

    public override bool ReadyToRun()
    {
        return _action.bounce;
    }

    public override void OnStartAbility()
    {
        Debug.Log("Bounce");
        _startInput = _action.move;
        _targetRotation = _camera.eulerAngles.y;

        if (_action.bounce)
                PerformBounce();
        else{
            SetAnimationState(animFallState, 0.25f);
            _startSpeed = Vector3.Scale(_mover.GetVelocity(), new Vector3(1, 0, 1)).magnitude;

            _startInput.x = Vector3.Dot(_camera.right, transform.forward);
            _startInput.y = Vector3.Dot(Vector3.Scale(_camera.forward, new Vector3(1, 0, 1)), transform.forward);

            if (_startSpeed > 3.5f)
                _startSpeed = speedOnAir;
        }
    }



    public override void UpdateAbility()
    {
        if(_action.jump){
            Debug.Log("Dash in bounce");
            Dash();
        }

        if (_mover.IsGrounded())

            StopAbility();


        if (transform.position.y > _highestPosition)
            _highestPosition = transform.position.y;

        // // Điều chỉnh đầu vào với độ nhạy airControl
        _startInput = Vector2.SmoothDamp(_startInput, _action.move, ref _inputVel, airControl);


        // Xoay nhân vật
        RotateCharacter();
    }

    private void RotateCharacter()
    {
        if (_action.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_startInput.x, _startInput.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _angleVel, airControl);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }

    public override void OnStopAbility()
    {
        base.OnStopAbility();

        if (_mover.IsGrounded() && !_hardLanding && _mover.GetVelocity().y < -3f)
            OnLanded.Invoke();

        _hardLanding = false;
        _highestPosition = 0;
        _pushVelocity = Vector3.zero;
        _mover.StopRootMotion();
    }

    private void PerformBounce()
    {
        Vector3 velocity = _mover.GetVelocity();
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * _mover.GetGravity());

        _mover.SetVelocity(velocity);
        _animator.CrossFadeInFixedTime(animJumpState, 0.1f);
        _startSpeed = speedOnAir;

        if (_startInput.magnitude > 0.1f)
            _startInput.Normalize();

    }
    private void Dash()
    {
        _isDashing = true;
        _mover.DisableGravity(); 
        StartCoroutine(PerformDash());
        _animator.CrossFadeInFixedTime(animDashState, 0.25f);
    }

    public IEnumerator PerformDash(){
        float dashTime = dashDistance / dashSpeed; 
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < dashTime)
        {

            Vector3 dashDirection = transform.forward;
            _mover.Move(dashDirection * dashSpeed); 

            elapsedTime += Time.deltaTime;

            float traveledDistance = Vector3.Distance(startPosition, transform.position);
            if (traveledDistance >= dashDistance)
                break;

            yield return null;
        }

        _mover.EnableGravity(); 
        _isDashing = false; 
    }
    

}
