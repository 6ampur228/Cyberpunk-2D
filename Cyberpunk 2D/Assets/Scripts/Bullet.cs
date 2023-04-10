using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterTime(_lifeTime));
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.right, Space.Self);
    }

    private IEnumerator DestroyAfterTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
