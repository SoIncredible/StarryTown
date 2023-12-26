using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _startPos;
        [SerializeField] private Transform _endPos;

        private void Start()
        {
            transform.position = _startPos.position;
            StartCoroutine(TickMove());
        }

        private IEnumerator TickMove()
        {
            yield return new WaitForSeconds(3f);
            var length = (_startPos.position - _endPos.position).magnitude;
            var curLength = 0f;
            while (curLength < length)
            {
                var pos = Vector3.Lerp(_startPos.position, _endPos.position, curLength / length);
                curLength += _speed * Time.deltaTime;
                if (curLength > length)
                {
                    transform.position = _endPos.position;
                    yield break;
                }

                transform.position = pos;
                yield return null;
            }
        }
    }
}