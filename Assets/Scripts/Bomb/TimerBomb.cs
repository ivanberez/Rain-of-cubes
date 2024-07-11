using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Bomb
{
    public class TimerBomb : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _minTime;
        [SerializeField, Min(0.1f)] private float _maxTime;

        private Coroutine _coroutine;

        private float _randomWorkTime => UnityEngine.Random.Range(_minTime, _maxTime);

        private void OnValidate()
        {
            if(_minTime > _maxTime)
                _maxTime = _minTime + 0.1f;
        }

        public void Run(Action<float> routineAction, Action endAction)
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(WorkRoutine(routineAction, endAction));
        }

        private IEnumerator WorkRoutine(Action<float> routineAction, Action endAction)
        {
            float workTime = _randomWorkTime;
            float passTime = 0;

            while(passTime < workTime)
            {
                passTime += Time.deltaTime;
                routineAction(passTime/workTime);
                yield return null;
            }
                
            endAction();
            _coroutine = null;
        }
    }
}