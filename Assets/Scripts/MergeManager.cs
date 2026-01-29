using System.Collections.Generic;
using UnityEngine;
using ezutils.Core;
using System;

public class MergeManager : MonoBehaviour
{
    [SerializeField] private GameObjectRepository _alllFruit;
    private List<Fruit> _fruit = new List<Fruit>();

    public void AddFruit(Fruit fruit)
    {
        _fruit.Add(fruit);
        fruit.OnFruitCollision += HandleCollision;
    }

    private void HandleCollision(Fruit fruit, Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Fruit>(out var otherfruit);
        if (otherfruit == null) return;

        if (otherfruit.Size == fruit.Size)
        {
            RemoveFruit(fruit);
            RemoveFruit(otherfruit);
            if (fruit.Size + 1 < _alllFruit.Length )
            {
                SpawnNewFruit(fruit.Size + 1, collision.contacts[0].point);
            }
            else
            {
                Debug.Log("Max limit of merged fruit");
            }
        }
    }

    private void RemoveFruit(Fruit fruit)
    {
        _fruit.Remove(fruit);
        fruit.OnFruitCollision -= HandleCollision;
        Destroy(fruit.gameObject);
    }
    private void SpawnNewFruit(int size, Vector2 position)
    {
        var newFruitGO = Instantiate(_alllFruit[size], new Vector3(position.x, position.y, 0f), Quaternion.identity);
        newFruitGO.TryGetComponent<Fruit>(out var fruit);
        if (fruit != null)
        {
            AddFruit(fruit);
        }
        newFruitGO.TryGetComponent<Rigidbody2D>(out var rb);
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
