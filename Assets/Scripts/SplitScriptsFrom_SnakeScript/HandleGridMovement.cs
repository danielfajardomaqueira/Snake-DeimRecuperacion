using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGridMovement : MonoBehaviour
{

    public Snake snakeScript;

    public void HandleGridMovementSnake() // Relativo al movimiento en 2D
    {
        snakeScript.gridMoveTimer += Time.deltaTime;
        if (snakeScript.gridMoveTimer >= snakeScript.gridMoveTimerMax)
        {
            snakeScript.gridMoveTimer -= snakeScript.gridMoveTimerMax; // Se reinicia el temporizador

            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeScript.snakeMovePositionsList.Count > 0)
            {
                previousSnakeMovePosition = snakeScript.snakeMovePositionsList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, snakeScript.gridPosition, snakeScript.gridMoveDirection);
            snakeScript.snakeMovePositionsList.Insert(0, snakeMovePosition);

            // Relación entre enum Direction y vectores left, right, down y up
            Vector2Int gridMoveDirectionVector;
            switch (snakeScript.gridMoveDirection)
            {
                default:
                case Snake.Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    break;
                case Snake.Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(1, 0);
                    break;
                case Snake.Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    break;
                case Snake.Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, 1);
                    break;
            }

            snakeScript.gridPosition += gridMoveDirectionVector; // Mueve la posición 2D de la cabeza de la serpiente
            snakeScript.gridPosition = snakeScript.levelGrid.ValidateGridPosition(snakeScript.gridPosition);

            // ¿He comido comida?
            bool snakeAteFood = snakeScript.levelGrid.TrySnakeEatFood(snakeScript.gridPosition);
            if (snakeAteFood)
            {
                // El cuerpo crece
                snakeScript.snakeBodySize++;
                snakeScript.CreateBodyPart();
            }

            if (snakeScript.snakeMovePositionsList.Count > snakeScript.snakeBodySize)
            {
                snakeScript.snakeMovePositionsList.
                    RemoveAt(snakeScript.snakeMovePositionsList.Count - 1);
            }

            // Comprobamos el Game Over aquí porque tenemos la posición de la cabeza y la lista snakeMovePositionsList actualizadas para poder comprobar la muerte
            foreach (SnakeMovePosition movePosition in snakeScript.snakeMovePositionsList)
            {
                if (snakeScript.gridPosition == movePosition.GetGridPosition()) // Posición de la cabeza coincide con alguna parte del cuerpo
                {
                    // GAME OVER
                    snakeScript.state = Snake.State.Dead;
                    GameManager.Instance.SnakeDied();
                }
            }

            transform.position = new Vector3(snakeScript.gridPosition.x, snakeScript.gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, snakeScript.GetAngleFromVector(gridMoveDirectionVector));
            snakeScript.UpdateBodyParts();
        }
    }


    //SetUp para conectar los script Snake a HandleGridMovement
    /*
    public void Setup(Snake snake) {
        this.snakeScript = snake;
    }
    */
}
