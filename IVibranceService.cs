using System;

namespace KeyedColors
{
    /// <summary>
    /// Abstraction for vendor-specific vibrance/saturation controls.
    /// </summary>
    public interface IVibranceService : IDisposable
    {
        /// <summary>
        /// True when the underlying GPU API is available and ready.
        /// </summary>
        bool IsSupported { get; }

        /// <summary>
        /// Minimum vibrance value supported by the implementation.
        /// </summary>
        int MinValue { get; }

        /// <summary>
        /// Maximum vibrance value supported by the implementation.
        /// </summary>
        int MaxValue { get; }

        /// <summary>
        /// Baseline vibrance value that represents drivers defaults.
        /// </summary>
        int DefaultValue { get; }

        /// <summary>
        /// Applies a vibrance value within the supported range.
        /// </summary>
        bool ApplyVibrance(int value);

        /// <summary>
        /// Restores the driver default vibrance level.
        /// </summary>
        void ResetVibrance();
    }
}
