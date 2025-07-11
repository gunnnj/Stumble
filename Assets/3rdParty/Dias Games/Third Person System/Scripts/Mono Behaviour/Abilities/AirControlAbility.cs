using UnityEngine;
using UnityEngine.Events;
using DiasGames.Components;
using System.Collections;

namespace DiasGames.Abilities
{
    public class AirControlAbility : AbstractAbility
    {
        [Header("Animation State")]
        [SerializeField] private string animJumpState = "Air.Jump";
        [SerializeField] private string animFallState = "Air.Falling";
        [SerializeField] private string animHardLandState = "Air.Hard Land";
        
        [Header("Jump parameters")]
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float speedOnAir = 6f;
        [SerializeField] private float airControl = 0.5f;
        // [SerializeField] private float rayLength = 2f;
        // [SerializeField] private LayerMask layerMask;

        [Header("Dash parameters")] // Thêm tham số cho lướt
        [SerializeField] private float dashDistance = 2f; // Khoảng cách lướt
        [SerializeField] private float dashSpeed = 10f;   // Tốc độ lướt
        [SerializeField] private string animDashState = "Air.Dash"; // Tên state hoạt hình cho lướt (nếu có)

        [Header("Landing")]
        // [SerializeField] private float heightForHardLand = 3f;
        // [SerializeField] private float heightForKillOnLand = 7f;
        [Header("Sound FX")]
        [SerializeField] private AudioClip jumpEffort;
        // [SerializeField] private AudioClip hardLandClip;
        [Header("Event")]
        [SerializeField] private UnityEvent OnLanded = null;
        private IMover _mover = null;
        private IDamage _damage;
        private CharacterAudioPlayer _audioPlayer;

        private float _startSpeed;
        private Vector2 _startInput;

        private Vector2 _inputVel;
        private float _angleVel;

        private float _targetRotation;
        private Transform _camera;

        // vars to control landing
        private float _highestPosition = 0;
        private bool _hardLanding = false;
        private int jumpcount;
        private GameObject player;
        private bool _isDashing = false;
        float _timeStart;
        float _timeToDash;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _damage = GetComponent<IDamage>();
            _audioPlayer = GetComponent<CharacterAudioPlayer>();
            _camera = Camera.main.transform;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public override bool ReadyToRun()
        {
            return !_mover.IsGrounded() || _action.jump;
        }

        public override void OnStartAbility()
        {
            jumpcount = 2;
            _startInput = _action.move;
            _targetRotation = _camera.eulerAngles.y;
            _timeStart = Time.time;
            _timeToDash = 0;

            if (_action.jump && _mover.IsGrounded())
                PerformJump();
            else
            {
                // SetAnimationState(animFallState, 0.25f);
                SetAnimationState(animDashState, 0.25f);
                _startSpeed = Vector3.Scale(_mover.GetVelocity(), new Vector3(1, 0, 1)).magnitude;

                _startInput.x = Vector3.Dot(_camera.right, transform.forward);
                _startInput.y = Vector3.Dot(Vector3.Scale(_camera.forward, new Vector3(1, 0, 1)), transform.forward);

                if (_startSpeed > 3.5f)
                    _startSpeed = speedOnAir;
            }

            _highestPosition = transform.position.y;
            _hardLanding = false;
        }

        public override void UpdateAbility()
        {
            // if (_isDashing) return;
            // CheckDoubleJump();
            if (_hardLanding)
            {
                // apply root motion
                _mover.ApplyRootMotion(Vector3.one, false);

                // wait animation finish
                if (HasFinishedAnimation(animHardLandState))
                
                    StopAbility();

                return;
            }

            // if(transform.position.y >= heightForHardLand){
            //     _animator.SetBool(ParamDash,true);
            // }
            _timeToDash = Time.time - _timeStart;
            if(_timeToDash>0.8f){
                SetAnimationState(animDashState, 0.25f);
            }

            if (_mover.IsGrounded())
            {
                // if(_highestPosition - transform.position.y >= heightForHardLand)
                // {
                //     _hardLanding = true;
                //     // SetAnimationState(animHardLandState, 0.02f);
                //     SetAnimationState(animDashState, 0.02f);

                //     // call event
                //     OnLanded.Invoke();

                // //     // call damage clip
                // //     if(_audioPlayer)
                // //         _audioPlayer.PlayVoice(hardLandClip);

                // //     // cause damage
                // //     if(_damage != null)
                // //     {
                // //         // calculate damage
                // //         float currentHeight = _highestPosition - transform.position.y - heightForHardLand;
                // //         float ratio = currentHeight / (heightForKillOnLand - heightForHardLand);

                // //         _damage.Damage((int)(200 * ratio));
                // //     }

                //     return;
                // }

                // _animator.SetBool(ParamDash,false);

                StopAbility();
            }

            if (transform.position.y > _highestPosition)
                _highestPosition = transform.position.y;

            _startInput = Vector2.SmoothDamp(_startInput, _action.move, ref _inputVel, airControl);
            _mover.Move(_startInput, _startSpeed, false);

            RotateCharacter();

            
        }

        private void RotateCharacter()
        {
            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_action.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(_startInput.x, _startInput.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _angleVel, airControl);

                // rotate to face input direction relative to camera position
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
            _mover.StopRootMotion();
        }

        /// <summary>
        /// Adds force to rigidbody to allow jump
        /// </summary>
        private void PerformJump()
        {
            // Debug.Log(CheckInBounce());
            Vector3 velocity = _mover.GetVelocity();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _mover.GetGravity());

            _mover.SetVelocity(velocity);
            _animator.CrossFadeInFixedTime(animJumpState, 0.1f);
            _startSpeed = speedOnAir;

            if (_startInput.magnitude > 0.1f)
                _startInput.Normalize();

            if (_audioPlayer)
                _audioPlayer.PlayVoice(jumpEffort);
        }
        public void CheckDoubleJump(){
        
            if(_action.jump && jumpcount>0){
                Debug.Log("Jump");
                
                jumpcount --;

                if (jumpcount == 1) // Lần nhảy đầu tiên (nhảy bình thường)
                {
                    Vector3 velocity = _mover.GetVelocity();
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * _mover.GetGravity());
                    _mover.SetVelocity(velocity);
                    _animator.CrossFadeInFixedTime(animJumpState, 0.1f);
                }
                else if (jumpcount == 0) // Lần nhảy thứ hai (lướt)
                {
                    _isDashing = true; // Đánh dấu trạng thái lướt
                    _mover.DisableGravity(); // Tắt trọng lực để lướt mượt mà
                    StartCoroutine(PerformDash());
                    _animator.CrossFadeInFixedTime(animDashState, 0.25f); // Sử dụng hoạt hình lướt

                    // if (_audioPlayer)
                    //     _audioPlayer.PlayVoice(jumpEffort); // Có thể thay bằng âm thanh lướt riêng
                }

            }   

        }

        private void HardLand()
        {

        }

        public IEnumerator PerformDash(){
            float dashTime = dashDistance / dashSpeed; // Thời gian lướt
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < dashTime)
            {
                // Tính toán vận tốc lướt theo hướng transform.up
                Vector3 dashDirection = transform.forward;
                _mover.Move(dashDirection * dashSpeed); // Di chuyển với tốc độ lướt

                elapsedTime += Time.deltaTime;

                // Kiểm tra khoảng cách đã lướt
                float traveledDistance = Vector3.Distance(startPosition, transform.position);
                if (traveledDistance >= dashDistance)
                    break;

                yield return null;
            }

            _mover.EnableGravity(); // Bật lại trọng lực
            _isDashing = false; // Kết thúc lướt
        }

    }
}