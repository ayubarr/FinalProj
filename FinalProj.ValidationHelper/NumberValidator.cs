namespace FinallApp.ValidationHelper
{
    public static class NumberValidator<T> where T : struct, IComparable, IConvertible
    {
        /// <summary>
        /// Checks if the value is not zero.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsNotZero(T n)
        {
            if (n.CompareTo(default(T)) == 0)
            {
                throw new ArgumentException("Значение не должно быть равно нулю.", nameof(n));
            }
        }

        /// <summary>
        /// Checks if the value is positive.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsPositive(T n)
        {
            if (n.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException("Значение должно быть положительным.", nameof(n));
            }
        }

        /// <summary>
        /// Checks if the value is negative.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsNegative(T n)
        {
            if (n.CompareTo(default(T)) >= 0)
            {
                throw new ArgumentException("Значение должно быть отрицательным.", nameof(n));
            }
        }

        /// <summary>
        /// Checks if the value is odd.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsOdd(T n)
        {
            if (Convert.ToInt32(n) % 2 == 0)
            {
                throw new ArgumentException("Значение должно быть нечетным.", nameof(n));
            }
        }

        /// <summary>
        /// Checks if the value is even.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsEven(T n)
        {
            if (Convert.ToInt32(n) % 2 != 0)
            {
                throw new ArgumentException("Значение должно быть четным.", nameof(n));
            }
        }

        /// <summary>
        /// Checks if the value is within the specified range.
        /// </summary>
        /// <param name="n">The value to be checked.</param>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void IsRange(T n, T minValue, T maxValue)
        {
            if (n.CompareTo(minValue) < 0 || n.CompareTo(maxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n,
                    $"Значение должно быть в диапазоне от {minValue} до {maxValue}.");
            }
        }
    }
    
}
