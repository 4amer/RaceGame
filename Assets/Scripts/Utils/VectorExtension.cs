using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class VectorExtension
    {
        public static float GetLargestNumber(this Vector2Int vector)
        {
            float result = vector.x > vector.y ? vector.x : vector.y;
            return result;
        }

        public static float GetLargestNumber(this Vector2 vector)
        {
            float result = vector.x > vector.y ? vector.x : vector.y;
            return result;
        }

        public static float GetLargestNumberWithSign(this Vector2Int vector)
        {
            float x = Mathf.Abs(vector.x);
            float y = Mathf.Abs(vector.y);
            float result = x > y ? vector.x : vector.y;
            return result;
        }

        public static float GetLargestNumberWithSign(this Vector2 vector)
        {
            float x = Mathf.Abs(vector.x);
            float y = Mathf.Abs(vector.y);
            float result = x > y ? vector.x : vector.y;
            return result;
        }

        public static float GetAbsLargestNumber(this Vector2Int vector)
        {
            float x = Mathf.Abs(vector.x);
            float y = Mathf.Abs(vector.y);
            float result = x > y ? x : y;
            return result;
        }

        public static float GetAbsLargestNumber(this Vector2 vector)
        {
            float x = Mathf.Abs(vector.x);
            float y = Mathf.Abs(vector.y);
            float result = x > y ? x : y;
            return result;
        }

        public static List<Vector2Int> GetNeighboringCells(this Vector2Int vector, Vector2Int fieldSize)
        {
            Vector2Int[] directions = { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1),
                                        new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1)};

            List<Vector2Int> allPositions = new List<Vector2Int>();

            foreach (Vector2Int direction in directions)
            {
                Vector2Int position = vector + direction;

                if (!((position.x >= fieldSize.x || position.x < 0) || (position.y >= fieldSize.y || position.y < 0)))
                {
                    allPositions.Add(position);
                }
            }

            return allPositions;
        }
    }
}