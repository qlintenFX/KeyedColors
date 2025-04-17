using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyedColors
{
    public class DisplayManager
    {
        // Windows API declarations
        [DllImport("gdi32.dll")]
        private static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP ramp);

        [DllImport("gdi32.dll")]
        private static extern bool GetDeviceGammaRamp(IntPtr hDC, ref RAMP ramp);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Blue;
        }

        // Default values
        private const double DefaultGamma = 1.0;
        private const double DefaultContrast = 1.0;

        // Store original gamma to restore later
        private RAMP originalRamp;
        private bool hasOriginalRamp = false;
        private bool apiAvailable = true;

        public bool IsApiAvailable => apiAvailable;

        public DisplayManager()
        {
            try
            {
                // Initialize the original ramp values
                originalRamp = new RAMP
                {
                    Red = new ushort[256],
                    Green = new ushort[256],
                    Blue = new ushort[256]
                };

                // Store the original gamma settings
                IntPtr hDC = GetDC(IntPtr.Zero);
                if (hDC != IntPtr.Zero)
                {
                    if (GetDeviceGammaRamp(hDC, ref originalRamp))
                    {
                        hasOriginalRamp = true;
                    }
                    else
                    {
                        // API call failed - might not have proper permissions
                        apiAvailable = false;
                    }
                    ReleaseDC(IntPtr.Zero, hDC);
                }
                else
                {
                    apiAvailable = false;
                }

                if (!apiAvailable)
                {
                    MessageBox.Show("Unable to access display settings. The application might need to be run with administrator privileges.", 
                        "Limited Functionality", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apiAvailable = false;
                MessageBox.Show($"Error initializing display settings: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Apply gamma and contrast settings
        public bool ApplySettings(double gamma, double contrast)
        {
            if (!apiAvailable)
                return false;

            try
            {
                // Validate input
                if (gamma < 0.30 || gamma > 2.80 || contrast < 0 || contrast > 1.0)
                    return false;

                RAMP ramp = new RAMP
                {
                    Red = new ushort[256],
                    Green = new ushort[256],
                    Blue = new ushort[256]
                };

                // Calculate gamma ramp values
                for (int i = 0; i < 256; i++)
                {
                    // Apply gamma correction
                    double value = Math.Pow(i / 255.0, 1.0 / gamma) * 65535.0;

                    // Apply contrast - scale from 0-1 to 0-2
                    // 0   = minimum contrast (0.0)
                    // 0.5 = normal contrast (1.0)
                    // 1.0 = maximum contrast (2.0)
                    double adjustedContrast = contrast * 2.0;
                    value = ((value / 65535.0) - 0.5) * adjustedContrast + 0.5;
                    value = Math.Max(0, Math.Min(1, value)) * 65535.0;

                    ushort val = (ushort)Math.Round(value);
                    ramp.Red[i] = val;
                    ramp.Green[i] = val;
                    ramp.Blue[i] = val;
                }

                // Apply the new settings
                IntPtr hDC = GetDC(IntPtr.Zero);
                bool success = false;
                
                if (hDC != IntPtr.Zero)
                {
                    success = SetDeviceGammaRamp(hDC, ref ramp);
                    ReleaseDC(IntPtr.Zero, hDC);
                }

                return success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Reset to original settings
        public bool ResetToDefault()
        {
            if (!apiAvailable || !hasOriginalRamp)
                return false;

            try
            {
                IntPtr hDC = GetDC(IntPtr.Zero);
                bool success = false;
                
                if (hDC != IntPtr.Zero)
                {
                    success = SetDeviceGammaRamp(hDC, ref originalRamp);
                    ReleaseDC(IntPtr.Zero, hDC);
                }

                return success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 