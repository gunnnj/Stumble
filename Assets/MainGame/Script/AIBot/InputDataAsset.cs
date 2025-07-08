using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InputDataAsset", menuName = "ScriptableObjects/InputDataAsset", order = 1)]
public class InputDataAsset : ScriptableObject
{
    public List<InputData> inputs = new List<InputData>();
}

[System.Serializable]
public struct InputData
{
    public float time;
    public float moveX, moveY, mouseX;
    public bool isJumping;
    public Vector3 position;
    public Quaternion rotation;
}
