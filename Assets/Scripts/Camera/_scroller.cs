using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _scroller : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _scrollSpeed = 1.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0 ,_scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLife.Instance.IsDead)
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
