using UnityEngine;

namespace Objects
{
    public abstract class ObjectView : MonoBehaviour
    {
        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}