using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] public VariableJoystick joystick;
    [SerializeField] public FloatingJoystick floatingJoystick;
    [SerializeField] Button btnJump;
    public Action onJump;

    void Start()
    {
        // btnJump.onClick.AddListener(OnJump);
    }

    public void OnJump()
    {
        onJump?.Invoke();
    }
}
