using ezutils.Core;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class FruitDropper : MonoBehaviour
{
    [SerializeField] private MergeManager _mergeManager;
    [Header("Inputs")]
    [SerializeField] private Vector3Variable _touchPosition;
    [SerializeField] private BoolVariable _isTouching;

    [Header("Droppers")]
    [SerializeField] private GameObject _fruitPrefab;
    [SerializeField] private GameObject _currentFruit;
    [SerializeField] private Rigidbody2D _currentRigidbody;
    [SerializeField] private readonly float _dropHeight;
    [SerializeField] private Vector3 _dropPosition;
    [SerializeField] private bool _shouldDrop;
    [Header("Respawn Timer")]
    [SerializeField] private float _currentTimer;
    private const float RESPAWN_TIME = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNextFruit();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTouching.Value && _currentFruit)
        {
            var pos = _currentFruit.transform.position;
            pos.x = _touchPosition.Value.x;
            _currentFruit.transform.position = pos;
            if (!_shouldDrop)
            {
                _shouldDrop = true;
            }
        }
        else
        {
            if (_shouldDrop)
            {
                Drop();
            }
        }

        if (_currentTimer >= 0)
        {
            _currentTimer -= Time.deltaTime;
        }
        else if (_currentFruit == null)
        {
            SpawnNextFruit();
        }
    }

    private void SpawnNextFruit()
    {
        _currentFruit = Instantiate(_fruitPrefab, _dropPosition, Quaternion.identity);
        _currentFruit.TryGetComponent<Fruit>(out var fruit);
        if (fruit != null)
        {
            _mergeManager.AddFruit(fruit);
        }
        else
        {
            Debug.LogWarning("Spawned Fruit has no Fruit Script");
        }
        _currentFruit.transform.position = _dropPosition;

        Rigidbody2D rb;
        _currentFruit.TryGetComponent<Rigidbody2D>(out rb);
        if (rb != null)
        {
            _currentRigidbody = rb;
        }
    }

    private void Drop()
    {
        _shouldDrop = false;
        _currentTimer = RESPAWN_TIME;
        if (!_currentRigidbody) return;

        _currentRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _currentFruit = null;
        _currentRigidbody = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_dropPosition, 1f);
    }
}
