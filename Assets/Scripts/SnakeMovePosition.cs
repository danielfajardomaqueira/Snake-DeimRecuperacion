using UnityEngine;

public class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Snake.Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Snake.Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Snake.Direction GetDirection()
        {
            return direction;
        }

        public Snake.Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Snake.Direction.Right;
            }
            return previousSnakeMovePosition.GetDirection();
        }   
    }