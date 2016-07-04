using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Local
#pragma warning disable 414
#pragma warning disable 169

namespace PixelMagic.Helpers
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public static class Mouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(SystemMetric smIndex);

        private static int CalculateAbsoluteCoordinateX(int x)
        {
            return x*65536/GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        private static int CalculateAbsoluteCoordinateY(int y)
        {
            return y*65536/GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        public static void LeftClick(int x, int y)
        {
            var mouseInput = new INPUT {type = SendInputEventType.InputMouse};
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)] public MouseInputData mi;

            [FieldOffset(0)] public readonly KEYBDINPUT ki;

            [FieldOffset(0)] public readonly HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public readonly ushort wVk;
            public readonly ushort wScan;
            public readonly uint dwFlags;
            public readonly uint time;
            public readonly IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public readonly int uMsg;
            public readonly short wParamL;
            public readonly short wParamH;
        }

        private struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [Flags]
        private enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        private enum SendInputEventType
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        private enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1
        }
    }
}