using UnityEngine;
using System;
namespace PZS
{
    [CreateAssetMenu(fileName ="New Float Value", menuName ="Shared Variable/Int")]
    public class SharedInt : ScriptableObject
    {
        [SerializeField] int _initialValue, _currentValue;
        [SerializeField] bool _constant;
        [TextArea] public string description;
        public event Action OnValueChanged;

        public int Value
        {
            get => _currentValue;
            set
            {
                if (_constant)
                    return;
                _currentValue = value;
                OnValueChanged?.Invoke();
            }
        }

        void OnEnable()
        {
            _currentValue = _initialValue;
        }
    }
}
