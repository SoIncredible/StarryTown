using System.Collections;
using UnityEngine;

namespace Movement
{
    public class Rotate : MonoBehaviour
    {
        private float _angularVelocity = 20f;
        private Vector3 _pos;
        [SerializeField] private Transform _trans;

        private void Awake()
        {
            _pos = _trans.position;
        }

        private void Start()
        {
            StartCoroutine(RotateTest());
        }


        private IEnumerator RotateTest()
        {
            float time = 0f;
            while (time < 3f)
            {
                _trans.Rotate(0, 0, _angularVelocity * Time.deltaTime);
                time += Time.deltaTime;
                yield return null;
            }

            float angle = 0f;
            while (angle < 45f)
            {
                transform.rotation = angle + Time.deltaTime * _angularVelocity / 2 > 90f
                    ? Quaternion.Euler(90f, 0, 0)
                    : Quaternion.Euler(angle + Time.deltaTime * _angularVelocity, 0, 0);

                _trans.position = _pos;
                _trans.Rotate(0, 0, _angularVelocity * Time.deltaTime);
                angle += Time.deltaTime * _angularVelocity;
                yield return null;
            }

            while (true)
            {
                _trans.Rotate(0, 0, _angularVelocity * Time.deltaTime);
                yield return null;
            }
        }
    }
}