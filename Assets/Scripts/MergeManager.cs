using System.Collections.Generic;using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private List<Fruit> _fruit = new List<Fruit>();
    public void AddFruit(Fruit fruit)
    {
        _fruit.Add(fruit);
        fruit.OnFruitCollision += HandleCollision;
    }

    private void HandleCollision(Fruit fruit, Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Fruit>(out var otherfruit);
        if (otherfruit != null)
        {
            if (otherfruit.Size == fruit.Size)
            {
                RemoveFruit(fruit);
                RemoveFruit(otherfruit);
            }
        }
    }

    private void RemoveFruit(Fruit fruit)
    {
        _fruit.Remove(fruit);
        fruit.OnFruitCollision -= HandleCollision;
        Destroy(fruit.gameObject);
    }

}
