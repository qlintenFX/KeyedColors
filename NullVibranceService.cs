using System;

namespace KeyedColors
{
    /// <summary>
    /// No-op vibrance implementation used when no GPU vendor API is available.
    /// </summary>
    public sealed class NullVibranceService : IVibranceService
    {
        public static NullVibranceService Instance { get; } = new NullVibranceService();

        private NullVibranceService()
        {
        }

        public bool IsSupported => false;

        public int MinValue => 0;

        public int MaxValue => 100;

        public int DefaultValue => 50;

        public bool ApplyVibrance(int value)
        {
            // Intentionally a no-op so that gamma/contrast adjustments still succeed.
            return true;
        }

        public void ResetVibrance()
        {
            // Nothing to reset.
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
