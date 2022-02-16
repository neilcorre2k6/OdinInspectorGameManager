using Unity.Mathematics;

/**
 * Class for comparing floating point values 
 */
public static class FloatExtensions {
	/**
	 * Returns whether or not a == b
	 */
	public static bool TolerantEquals(this float a, float b) {
		return math.abs(a - b) < 0.0001f;
	}

	/**
	 * Returns whether or not a >= b
	 */
	public static bool TolerantGreaterThanOrEquals(this float a, float b) {
		return a > b || TolerantEquals(a, b);
	}

	/**
	 * Returns whether or not a <= b
	 */
	public static bool TolerantLesserThanOrEquals(this float a, float b) {
		return a < b || TolerantEquals(a, b);
	}

    /// <summary>
    /// Returns whether or not the specified floating value is zero
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsZero(this float value) {
        return TolerantEquals(value, 0.0f);
    }
}
