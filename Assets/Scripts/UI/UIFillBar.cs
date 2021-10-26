using UnityEngine;
using UnityEngine.UI;
namespace PZS
{
    public class UIFillBar : MonoBehaviour
    {
        Image _filledBar;
        [SerializeField] SharedInt _totalValue;
        [SerializeField] SharedInt _currentValue;
        void Awake()
        {
            _filledBar = GetComponent<Image>();

            _filledBar.fillAmount = FillAmount();
        }
        void OnEnable()
        {
            _currentValue.OnValueChanged += UpdateBar;
        }

        void OnDisable()
        {
            _currentValue.OnValueChanged -= UpdateBar;
        }
        float FillAmount()
        {
            return (float)_currentValue.Value / _totalValue.Value;
        }
        public void UpdateBar()
        {
            _filledBar.fillAmount = FillAmount();
        }
    }
}
