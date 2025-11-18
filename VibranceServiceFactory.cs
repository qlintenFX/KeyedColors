using System;

namespace KeyedColors
{
    /// <summary>
    /// Responsible for selecting the best vibrance implementation available on the system.
    /// </summary>
    public static class VibranceServiceFactory
    {
        public static IVibranceService Create()
        {
            // Future: attempt vendor-specific services (NVAPI, ADL, Intel, etc.).
            // For now we only have the null implementation which keeps the rest
            // of the application logic decoupled from the GPU API surface.
            return NullVibranceService.Instance;
        }
    }
}
