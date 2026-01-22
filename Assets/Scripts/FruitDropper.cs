using ezutils.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FruitDropper : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private Vector3Variable _touchPosition;
    [SerializeField] private BoolVariable _isTouching;

    [Header("Droppers")]
    [SerializeField] private GameObject _fruitPrefab;
    [SerializeField] private GameObject _previewFruitPrefab;
    [SerializeField]private GameObject _previewFruit;
    [SerializeField] private readonly float _dropHeight;
    [SerializeField] private Vector3 _dropPosition;
    //private Vector3 _lastPosition;
    //[SerializeField] private Vector3 _move;
    //[SerializeField] private float _moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previewFruit = Instantiate(_previewFruitPrefab, _dropPosition, Quaternion.identity);
        _previewFruit.transform.position = _dropPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTouching.Value)
        {
            var pos = _previewFruit.transform.position;
            pos.x = _touchPosition.Value.x;
            _previewFruit.transform.position = pos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_dropPosition, 1f);
    }
}
