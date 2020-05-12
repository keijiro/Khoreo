using UnityEngine;

namespace Khoreo
{
    [System.Serializable]
    public sealed class ObjectSwitcher
    {
        public enum TargetType { GameObject, Behaviour, Subtree }

        public Transform Owner = null;

        [SerializeField] TargetType _targetType = TargetType.Subtree;
        [SerializeField] GameObject _targetGameObject = null;
        [SerializeField] Behaviour _targetBehaviour = null;

        public bool Active {
            get => CheckActive();
            set => SetActive(value);
        }

        public bool CheckActive()
        {
            if (_targetType == TargetType.GameObject)
            {
                if (_targetGameObject == null) return false;
                return _targetGameObject.activeSelf;
            }
            else if (_targetType == TargetType.Behaviour)
            {
                if (_targetBehaviour == null) return false;
                return _targetBehaviour.enabled;
            }
            else // TargetType.Subtree
            {
                if (Owner.childCount == 0) return false;
                return Owner.GetChild(0).gameObject.activeSelf;
            }
        }

        public void SetActive(bool active)
        {
            if (_targetType == TargetType.GameObject)
            {
                if (_targetGameObject != null)
                    _targetGameObject.SetActive(active);
            }
            else if (_targetType == TargetType.Behaviour)
            {
                if (_targetBehaviour != null)
                    _targetBehaviour.enabled = active;
            }
            else // TargetType.Subtree
            {
                foreach (Transform t in Owner)
                  t.gameObject.SetActive(active);
            }
        }
    }
}
