using UnityEngine;

namespace Data{
    public class TypeSkin : MonoBehaviour
    {
        [SerializeField] Type type;

        public int index = 0;

        void Update()
        {
            index = (int)type;
        }
    }
    public enum Type{
        Head,
        Body,
        Pant,
        Leg,
        Hair
    }
}

