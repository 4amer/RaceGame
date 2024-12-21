using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Road
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        public void PositionChanged(float speed)
        {
            _rb.MovePosition(new Vector3(0, 0, transform.position.z + speed * (-1)));
        }
    }
}
