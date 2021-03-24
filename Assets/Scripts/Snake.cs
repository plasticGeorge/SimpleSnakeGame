using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [Range(0, 30), SerializeField] private float _moveSpeed;
    [Range(0, 100), SerializeField] private float _rotateSpeed;


    private void Update ()
    {
        MoveHeade(_moveSpeed);
        MoveTail();
        Rotate(_rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Food food))
        {
            Destroy(other.gameObject);

            GameObject bone = Instantiate(_bonePrefab);
            _tails.Add(bone.transform);

            Instantiate(_foodPrefab,
                        new Vector3(Random.Range(transform.position.x + 3, transform.position.x + 5), 0.3f, Random.Range(transform.position.x + 3, transform.position.x + 5)),
                        new Quaternion(0, 0, 0, 0));
        }
        else if(other.TryGetComponent(out Food border))
        {
            // reset
        }
    }

    private void MoveHeade(float speed)
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void MoveTail()
    {
        float sqrDistance = Mathf.Sqrt(_bonesDistance);
        Vector3 previousPosition = transform.position;

        foreach(var bone in _tails)
        {
            if((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 currentBonePosition = bone.position;
                bone.position = previousPosition;
                previousPosition = currentBonePosition;
            }
            else
            {
                break;
            }
        }
    }

    private void Rotate(float speed)
    {
        float angle = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Rotate(0, angle, 0);
    }
}
