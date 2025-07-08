using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float speedInAir = 3.5f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float dashForce = 5f;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform cameraTransform;
    [SerializeField] bool canRevive = false;
    [SerializeField] bool useJoystick = true;
    public float currentSpeed;
    private Rigidbody rb;
    private FloatingJoystick floatingJoystick;
    private PlayUI playUI;
    private CapsuleCollider capsuleCollider;
    private const string AnimRun = "isRun";
    private const string AnimJum = "isJump";
    private const string AnimSlip = "isJump2";
    private const string AnimFall = "TriggerFall";
    private float _rotationVelocity;
    private float _verticalVelocity;
    private Vector3 posRevive;
    Vector3 dir;
    bool canJump = true;
    int jumpCount = 0;
    int maxJump = 1;
    float originHeightCollider = 1.84f;
    public bool canControl = true;
    
    void OnEnable()
    {
        GameEvent.eventWinGame+=WinGame;
        GameEvent.eventLoseGame+=LoseGame;
    }

    void OnDisable()
    {
        GameEvent.eventWinGame-=WinGame;
        GameEvent.eventLoseGame-=LoseGame;
    }

    public void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        playUI = FindFirstObjectByType<PlayUI>();
        floatingJoystick = playUI.floatingJoystick;
        playUI.onJump = ()=>Jump();
        posRevive = transform.position;
        currentSpeed = speed;
    }

    public void Update()
    {
        if(canControl){
            MoveAndRotate();
            ControlAnim();
            if(Input.GetKeyDown(KeyCode.Space)){
                Jump();
            }
        }
        
    }

    private void WinGame()
    {
        // gameObject.SetActive(false);
    }
    private void LoseGame()
    {
        gameObject.SetActive(false);
    }
    //Add event Anim
    public void ScaleCollider(){
        capsuleCollider.height = 1.2f; //old 0.9
    }
    public void ScaleOriginCollider(){
        capsuleCollider.height = originHeightCollider;
    }
    public void SetControl(){
        canControl = true;
    }
    //Add event anim
    public void StandingUp(){
        // ScaleOriginCollider();
        StartCoroutine(ChangeHeightAndPositionCoroutine(0.9f, originHeightCollider, 0.2f, 0.2f));
    }
    private IEnumerator ChangeHeightAndPositionCoroutine(float startHeight, float endHeight, float increment, float duration)
    {
        float elapsedTime = 0f;

        // Trạng thái ban đầu
        capsuleCollider.height = startHeight;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, increment, 0);

        while (elapsedTime < duration)
        {
            // Tính toán tỷ lệ
            float t = elapsedTime / duration;
            capsuleCollider.height = Mathf.Lerp(startHeight, endHeight, t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null; // Đợi cho frame tiếp theo
        }

        // Đảm bảo chiều cao và vị trí cuối cùng
        capsuleCollider.height = endHeight;
        transform.position = targetPosition;
    }

    private Action Jump()
    {
        if(!canControl) return null;
        if (canJump)
        {
            canJump = false;
            _verticalVelocity = jumpForce;
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            Debug.Log("Jump");
            jumpCount++;
        }
        else{
            if(jumpCount<maxJump){
                jumpCount++;
                animator.SetBool(AnimSlip,true);
                Debug.Log("Dash");
                //control dash
                rb.AddForce(transform.forward*dashForce,ForceMode.Impulse);
            }
        }
        
        return null;
    }
    public void ControlAnim(){
        if(dir.magnitude>0.1f){
            animator.SetBool(AnimRun,true);
        }
        else{
            animator.SetBool(AnimRun,false);
        }
        if (IsGrounded())
        {
            currentSpeed = speed;
            ScaleOriginCollider();
            jumpCount = 0;
            _verticalVelocity = 0f;
            animator.SetBool(AnimJum,false);
            animator.SetBool(AnimSlip,false);
            canJump = true;
        }
        else{
            currentSpeed = speedInAir;
            animator.SetBool(AnimJum,true);
            canJump = false;
        }
    }
    public void Slip(){
        animator.SetBool(AnimSlip,true);
        Debug.Log("Slip");
    }
    public void ChangeMode(){
        useJoystick = !useJoystick;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint")){
            posRevive = other.transform.position;
        }
        if(other.CompareTag("Kill")){
            // Dead();
            if(canRevive) Revive();
            else{
                GameEvent.eventLoseGame?.Invoke();
                // Lose();
            }
        }
        if(other.CompareTag("Win")){
            GameEvent.eventWinGame?.Invoke();
            GameEvent.eventFinish?.Invoke();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Slip")){
            Slip();
        }
        if(other.gameObject.CompareTag("Bounce")){
            canControl = false;
            ScaleCollider();
            animator.SetTrigger(AnimFall);
        }
    }
    public void MoveAndRotate(){
        float dirX;
        float dirZ;
        if(useJoystick){

            dirX = floatingJoystick.Horizontal;
            dirZ = floatingJoystick.Vertical;
        }
        else{
            dirX = Input.GetAxis("Horizontal");
            dirZ = Input.GetAxis("Vertical");
        }
        

        dir = new Vector3(dirX,0,dirZ);

        if(dir.magnitude>0.1f){
            // Move();
            MoveRotate();

        }
    }
    // private void Move()
    // {
    //     //old move
    //     Vector3 newPos = transform.position+dir;
    //     transform.position = Vector3.Lerp(transform.position,newPos,speed*Time.deltaTime);

    // }
    public void MoveRotate(){
        //rotation
        float targetRotation = Mathf.Atan2(dir.x,dir.z)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                    0.12f);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        //move
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        
        Vector3 newPosition = transform.position + (targetDirection.normalized * currentSpeed *10* Time.deltaTime) +
                            new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
    
        // Vector3 rotate = Vector3.RotateTowards(transform.forward,dir,rotateSpeed*Time.deltaTime,0f);
        // transform.rotation = Quaternion.LookRotation(rotate);


    }
    private bool IsGrounded()
    {
        // return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);
    
        return Physics.CheckSphere(checkPosition, 0.25f, groundLayer);
    }

    [ContextMenu("Revive")]
    public async void Revive(){
        await Task.Delay(200);
        animator.SetBool(AnimRun,false);
        animator.SetBool(AnimJum,false);
        transform.position = posRevive + new Vector3(0,1f,0);
        rb.linearVelocity = Vector3.zero;
    }
    public void Dead(){
        //CallEffect or load UI lose
        Debug.Log("Dead");
    }
}
