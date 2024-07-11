using Assets.Scripts.Spawner;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ShowerStatistic<T> : MonoBehaviour where T : IStatisticSpawner
    {
        [SerializeField] private T _statistic;
        [SerializeField] private TextMeshProUGUI _textActive;
        [SerializeField] private TextMeshProUGUI _textAll;

        private bool _isCanShow = false;

        private void Awake()
        {
            _isCanShow = _statistic != null && _textActive && _textAll;
            
            if (_isCanShow == false)
                Debug.LogWarning("The component is not assigned.", gameObject);
        }

        private void OnEnable()
        {
            _statistic.StatChanged += ShowStat;
        }

        private void OnDisable()
        {
            _statistic.StatChanged -= ShowStat;
        }

        private void Start()
        {
            if (_isCanShow)
                ShowStat();
        }

        private void ShowStat()
        {
            _textActive.text = $"Актиынх: \n {_statistic.CountActive}";
            _textAll.text = $"Всего создано: \n {_statistic.CountAll}"; 
        }
    }
}