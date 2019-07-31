namespace SoundVisualizer
{
    class Transformation
    {
        public static float lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float map(float x, float x0, float x1, float a, float b)
        {
            float t = (x - x0) / (x1 - x0);
            return lerp(a, b, t);
        }
    }
}
