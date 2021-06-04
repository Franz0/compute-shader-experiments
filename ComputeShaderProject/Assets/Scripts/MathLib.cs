using UnityEngine;

namespace ComputeShaderExp.Math
{
    public static class MathLib
    {
        public static Vector2 GetSquareRandomPosition(int width, int height)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            return new Vector2(x, y);
        }

        public static Vector2 GetCircleRandomPosition(int radius, int posX, int posY)
        {
            Vector2 randUnitPos = Random.insideUnitCircle * radius;
            Vector2 randCirclePos = randUnitPos + new Vector2(posX, posY);
            return randCirclePos;
        }

        public static Vector2Int GetCenterRect(int width, int height)
        {
            return new Vector2Int((int)(width * .5f), (int)(height * .5f));
        }
    }
}
