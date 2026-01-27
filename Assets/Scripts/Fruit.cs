using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    public MergeManager MergeManager { get => _mergeManager; set { _mergeManager = value; } }
    private MergeManager _mergeManager;

    public delegate void FruitCollision(Fruit fruit, Collision2D collision);
    public FruitCollision OnFruitCollision;

    public int Size { get => _size; }
    [SerializeField] private int _size;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnFruitCollision(this, collision);
    }
    private void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }
}
