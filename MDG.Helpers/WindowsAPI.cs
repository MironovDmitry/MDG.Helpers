﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MDG.Helpers
{
    internal class WindowsAPI
    {

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {   
            int dx;
            int dy;
            uint mouseData;
            uint dwFlags;
            uint time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            internal ushort wVk;
            internal ushort wScan;
            internal uint dwFlags;
            internal uint time;
            internal IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUT
        {
            [FieldOffset(0)]
            internal int type;
            [FieldOffset(4)] //*
            internal MOUSEINPUT mi;
            [FieldOffset(4)] //*
            internal KEYBDINPUT ki;
            [FieldOffset(4)] //*
            internal HARDWAREINPUT hi;
        } 
        #endregion



        /// <summary>
        /// 
        /// </summary>
        internal const uint WM_KEYDOWN = 0x100;

        /// <summary>
        /// 
        /// </summary>
        internal const uint WM_KEYUP = 0x101;

        /// <summary>
        /// 
        /// </summary>
        internal const uint WM_LBUTTONDOWN = 0x201;

        /// <summary>
        /// 
        /// </summary>
        internal const uint WM_LBUTTONUP = 0x202;

        internal const uint WM_CHAR = 0x102;

        /// <summary>
        /// 
        /// </summary>
        internal const int MK_LBUTTON = 0x01;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_RETURN = 0x0d;

        internal const int VK_ESCAPE = 0x1b;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_TAB = 0x09;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_LEFT = 0x25;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_UP = 0x26;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_RIGHT = 0x27;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_DOWN = 0x28;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_F5 = 0x74;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_F6 = 0x75;

        /// <summary>
        /// 
        /// </summary>
        internal const int VK_F7 = 0x76;

        /// <summary>
        /// The GetForegroundWindow function returns a handle to the foreground window.
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        internal static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          [Out()] byte[] lpBuffer,
          int dwSize,
          out int lpNumberOfBytesRead
         );

        internal static void SwitchWindow(IntPtr windowHandle)
        {
            if (GetForegroundWindow() == windowHandle)
                return;

            IntPtr foregroundWindowHandle = GetForegroundWindow();
            uint currentThreadId = GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out temp);
            AttachThreadInput(currentThreadId, foregroundThreadId, true);
            SetForegroundWindow(windowHandle);
            AttachThreadInput(currentThreadId, foregroundThreadId, false);

            while (GetForegroundWindow() != windowHandle)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        internal static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern byte VkKeyScan(char ch);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static IntPtr FindWindow(string name)
        {
            Process[] procs = Process.GetProcesses();

            foreach (Process proc in procs)
            {
                if (proc.MainWindowTitle == name)
                {
                    return proc.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        internal static int MakeLong(int low, int high)
        {
            return (high << 16) | (low & 0xffff);
        }

        [DllImport("User32.dll")]
        internal static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMessageExtraInfo();
        
        internal const int INPUT_MOUSE = 0;
        internal const int INPUT_KEYBOARD = 1;
        internal const int INPUT_HARDWARE = 2;
        internal const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        internal const uint KEYEVENTF_KEYUP = 0x0002;
        internal const uint KEYEVENTF_UNICODE = 0x0004;
        internal const uint KEYEVENTF_SCANCODE = 0x0008;
        internal const uint XBUTTON1 = 0x0001;
        internal const uint XBUTTON2 = 0x0002;
        internal const uint MOUSEEVENTF_MOVE = 0x0001;
        internal const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const uint MOUSEEVENTF_LEFTUP = 0x0004;
        internal const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        internal const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        internal const uint MOUSEEVENTF_XDOWN = 0x0080;
        internal const uint MOUSEEVENTF_XUP = 0x0100;
        internal const uint MOUSEEVENTF_WHEEL = 0x0800;
        internal const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        internal const uint MOUSEEVENTF_ABSOLUTE = 0x8000;



        public static IntPtr GetWindowHandle(string windowName)
        {
            return FindWindow(windowName);
        }

        public static void PressKey(char ch, bool press)
        {
            byte vk = WindowsAPI.VkKeyScan(ch);
            ushort scanCode = (ushort)WindowsAPI.MapVirtualKey(vk, 0);

            if (press)
                KeyDown(scanCode);
            else
                KeyUp(scanCode);
        }

        public static void SendString(IntPtr destinationWindowHandle, string stringToSend)
        {
            List<INPUT> inputs = new List<INPUT>();

        }

        private static void KeyDown(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
            inputs[0].ki.dwFlags = 0;
            inputs[0].ki.wScan = (ushort)(scanCode & 0xff);

            uint intReturn = WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

        private static void KeyUp(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
            inputs[0].ki.wScan = scanCode;
            inputs[0].ki.dwFlags = WindowsAPI.KEYEVENTF_KEYUP;
            uint intReturn = WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

        
    }
    
}