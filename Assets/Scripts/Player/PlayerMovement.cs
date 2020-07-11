using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection { Up, Down, Left, Right, Dash}

public class PlayerMovement : PlayerHealthComponent
{
    #region Inspector
    [SerializeField] internal MovementDirection direction;
    [SerializeField] internal KeyCode[] viableInputs;
    #endregion

    public Vector2 Value { get; private set; }

    internal virtual void Update()
    {
        foreach(var key in viableInputs)
        {
            if (Input.GetKey(key))
            {
                Value = GetDirection(direction);
                return;
            }
        }

        Value = Vector2.zero;
    }

    internal Vector2 GetDirection (MovementDirection direction)
    {
        switch (direction)
        {
            case MovementDirection.Up:
                return new Vector2(0f, 1f);
            case MovementDirection.Down:
                return new Vector2(0f, -1f);
            case MovementDirection.Left:
                return new Vector2(-1f, 0f);
            case MovementDirection.Right:
                return new Vector2(1f, 0f);

        }

        throw new System.Exception("Invalid movement direction");
    }
}
