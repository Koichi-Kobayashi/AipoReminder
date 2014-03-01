using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;

namespace WinFramework.Utility
{
    #region "COM Interop"

    /// <summary>
    /// ShellLink コクラス 
    /// </summary>
    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    [ClassInterface(ClassInterfaceType.None)]
    internal class ShellLinkObject { }

    #region "Unicode環境用"

    /// <summary>
    /// IShellLinkWインターフェイス
    /// </summary>
    [ComImport]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLinkW
    {
        void GetPath
            (
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cch,
            [MarshalAs(UnmanagedType.Struct)] ref WIN32_FIND_DATAW pfd,
            uint fFlags
            );

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cch);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cch);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cch);

        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        void GetHotkey(out ushort pwHotkey);

        void SetHotkey(ushort wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation
            (
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
            int cch,
            out int piIcon
            );

        void SetIconLocation
            (
            [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
            int iIcon
            );

        void SetRelativePath
            (
            [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
            uint dwReserved
            );

        void Resolve
            (
            IntPtr hwnd,
            uint fFlags
            );

        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

    /// <summary>
    /// WIN32_FIND_DATAW 構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    internal struct WIN32_FIND_DATAW
    {
        public const int MAX_PATH = 260;

        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string cFileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;
    }

    #endregion

    #region "ANSI環境用"

    /// <summary>
    /// IShellLinkAインターフェイス
    /// </summary>
    [ComImport]
    [Guid("000214EE-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLinkA
    {
        void GetPath
            (
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
            int cch,
            [MarshalAs(UnmanagedType.Struct)] ref WIN32_FIND_DATAA pfd,
            uint fFlags
            );

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszName, int cch);

        void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);

        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir, int cch);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);

        void GetArguments([Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs, int cch);

        void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);

        void GetHotkey(out ushort pwHotkey);

        void SetHotkey(ushort wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation
            (
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath,
            int cch,
            out int piIcon
            );

        void SetIconLocation
            (
            [MarshalAs(UnmanagedType.LPStr)] string pszIconPath,
            int iIcon
            );

        void SetRelativePath
            (
            [MarshalAs(UnmanagedType.LPStr)] string pszPathRel,
            uint dwReserved
            );

        void Resolve
            (
            IntPtr hwnd,
            uint fFlags
            );

        void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);
    }

    /// <summary>
    /// WIN32_FIND_DATAA 構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    internal struct WIN32_FIND_DATAA
    {
        public const int MAX_PATH = 260;

        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string cFileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;
    }

    #endregion

    #endregion

    /// <summary>
    /// ショートカットに関する処理を行うためのクラスです。
    /// </summary>
    public sealed class ShellLinkUtility : IDisposable
    {
        // IShellLinkインターフェイス
        private IShellLinkW shellLinkW;
        private IShellLinkA shellLinkA;

        // カレントファイル
        private string currentFile;

        // 実行環境
        private bool isUnicodeEnvironment;

        // 各種定数
        internal const int MAX_PATH = 260;

        internal const uint SLGP_SHORTPATH = 0x0001; // 短い形式(8.3形式)のファイル名を取得する
        internal const uint SLGP_UNCPRIORITY = 0x0002; // UNCパス名を取得する
        internal const uint SLGP_RAWPATH = 0x0004; // 環境変数などが変換されていないパス名を取得する

        #region "[型] ShellLinkDisplayMode列挙型"

        /// <summary>
        /// 実行時のウィンドウの表示方法を表す列挙型です。
        /// </summary>
        public enum ShellLinkDisplayMode : int
        {
            /// <summary>通常の大きさのウィンドウで起動します。</summary>
            Normal = 1,

            /// <summary>最大化された状態で起動します。</summary>
            Maximized = 3,

            /// <summary>最小化された状態で起動します。</summary>
            Minimized = 7,
        }

        #endregion

        #region "[型] ShellLinkResolveFlags列挙型"

        /// <summary></summary>
        [Flags]
        public enum ShellLinkResolveFlags : int
        {
            /// <summary></summary>
            SLR_ANY_MATCH = 0x2,

            /// <summary></summary>
            SLR_INVOKE_MSI = 0x80,

            /// <summary></summary>
            SLR_NOLINKINFO = 0x40,

            /// <summary></summary>
            SLR_NO_UI = 0x1,

            /// <summary></summary>
            SLR_NO_UI_WITH_MSG_PUMP = 0x101,

            /// <summary></summary>
            SLR_NOUPDATE = 0x8,

            /// <summary></summary>
            SLR_NOSEARCH = 0x10,

            /// <summary></summary>
            SLR_NOTRACK = 0x20,

            /// <summary></summary>
            SLR_UPDATE = 0x4
        }

        #endregion

        #region "コンストラクション・デストラクション"

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <exception cref="COMException">IShellLinkインターフェイスを取得できませんでした。</exception>
        public ShellLinkUtility()
        {
            currentFile = "";

            shellLinkW = null;
            shellLinkA = null;

            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    // Unicode環境
                    shellLinkW = (IShellLinkW)(new ShellLinkObject());

                    isUnicodeEnvironment = true;
                }
                else
                {
                    // Ansi環境
                    shellLinkA = (IShellLinkA)(new ShellLinkObject());

                    isUnicodeEnvironment = false;
                }
            }
            catch
            {
                throw new COMException("IShellLinkインターフェイスを取得できませんでした。");
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="linkFile">ショートカットファイル</param>
        public ShellLinkUtility(string linkFile)
            : this()
        {
            Load(linkFile);
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~ShellLinkUtility()
        {
            Dispose();
        }

        /// <summary>
        /// このインスタンスが使用しているリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            if (shellLinkW != null)
            {
                Marshal.ReleaseComObject(shellLinkW);
                shellLinkW = null;
            }

            if (shellLinkA != null)
            {
                Marshal.ReleaseComObject(shellLinkA);
                shellLinkA = null;
            }
        }

        #endregion

        #region "プロパティ"

        /// <summary>
        /// カレントファイル。
        /// </summary>
        public string CurrentFile
        {
            get { return currentFile; }
        }

        /// <summary>
        /// ショートカットのリンク先。
        /// </summary>
        public string TargetPath
        {
            get
            {
                StringBuilder targetPath = new StringBuilder(MAX_PATH, MAX_PATH);

                if (isUnicodeEnvironment)
                {
                    WIN32_FIND_DATAW data = new WIN32_FIND_DATAW();

                    shellLinkW.GetPath(targetPath, targetPath.Capacity, ref data, SLGP_UNCPRIORITY);
                }
                else
                {
                    WIN32_FIND_DATAA data = new WIN32_FIND_DATAA();

                    shellLinkA.GetPath(targetPath, targetPath.Capacity, ref data, SLGP_UNCPRIORITY);
                }

                return targetPath.ToString();
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetPath(value);
                }
                else
                {
                    shellLinkA.SetPath(value);
                }
            }
        }

        /// <summary>
        /// 作業ディレクトリ。
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                StringBuilder workingDirectory = new StringBuilder(MAX_PATH, MAX_PATH);

                if (isUnicodeEnvironment)
                {
                    shellLinkW.GetWorkingDirectory(workingDirectory, workingDirectory.Capacity);
                }
                else
                {
                    shellLinkA.GetWorkingDirectory(workingDirectory, workingDirectory.Capacity);
                }

                return workingDirectory.ToString();
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetWorkingDirectory(value);
                }
                else
                {
                    shellLinkA.SetWorkingDirectory(value);
                }
            }
        }

        /// <summary>
        /// コマンドライン引数。
        /// </summary>
        public string Arguments
        {
            get
            {
                StringBuilder arguments = new StringBuilder(MAX_PATH, MAX_PATH);

                if (isUnicodeEnvironment)
                {
                    shellLinkW.GetArguments(arguments, arguments.Capacity);
                }
                else
                {
                    shellLinkA.GetArguments(arguments, arguments.Capacity);
                }

                return arguments.ToString();
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetArguments(value);
                }
                else
                {
                    shellLinkA.SetArguments(value);
                }
            }
        }

        /// <summary>
        /// ショートカットの説明。
        /// </summary>
        public string Description
        {
            get
            {
                StringBuilder description = new StringBuilder(MAX_PATH, MAX_PATH);

                if (isUnicodeEnvironment)
                {
                    shellLinkW.GetDescription(description, description.Capacity);
                }
                else
                {
                    shellLinkA.GetDescription(description, description.Capacity);
                }

                return description.ToString();
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetDescription(value);
                }
                else
                {
                    shellLinkA.SetDescription(value);
                }
            }
        }

        /// <summary>
        /// アイコンのファイル。
        /// </summary>
        public string IconFile
        {
            get
            {
                int iconIndex = 0;
                string iconFile = "";

                GetIconLocation(out iconFile, out iconIndex);

                return iconFile;
            }
            set
            {
                int iconIndex = 0;
                string iconFile = "";

                GetIconLocation(out iconFile, out iconIndex);

                SetIconLocation(value, iconIndex);
            }
        }

        /// <summary>
        /// アイコンのインデックス。
        /// </summary>
        public int IconIndex
        {
            get
            {
                int iconIndex = 0;
                string iconPath = "";

                GetIconLocation(out iconPath, out iconIndex);

                return iconIndex;
            }
            set
            {
                int iconIndex = 0;
                string iconPath = "";

                GetIconLocation(out iconPath, out iconIndex);

                SetIconLocation(iconPath, value);
            }
        }

        /// <summary>
        /// アイコンのファイルとインデックスを取得する
        /// </summary>
        /// <param name="iconFile">アイコンのファイル</param>
        /// <param name="iconIndex">アイコンのインデックス</param>
        private void GetIconLocation(out string iconFile, out int iconIndex)
        {
            StringBuilder iconFileBuffer = new StringBuilder(MAX_PATH, MAX_PATH);

            if (isUnicodeEnvironment)
            {
                shellLinkW.GetIconLocation(iconFileBuffer, iconFileBuffer.Capacity, out iconIndex);
            }
            else
            {
                shellLinkA.GetIconLocation(iconFileBuffer, iconFileBuffer.Capacity, out iconIndex);
            }

            iconFile = iconFileBuffer.ToString();
        }

        /// <summary>
        /// アイコンのファイルとインデックスを設定する
        /// </summary>
        /// <param name="iconFile">アイコンのファイル</param>
        /// <param name="iconIndex">アイコンのインデックス</param>
        private void SetIconLocation(string iconFile, int iconIndex)
        {
            if (isUnicodeEnvironment)
            {
                shellLinkW.SetIconLocation(iconFile, iconIndex);
            }
            else
            {
                shellLinkA.SetIconLocation(iconFile, iconIndex);
            }
        }

        /// <summary>
        /// 実行時のウィンドウの大きさ。
        /// </summary>
        public ShellLinkDisplayMode DisplayMode
        {
            get
            {
                int showCmd = 0;

                if (isUnicodeEnvironment)
                {
                    shellLinkW.GetShowCmd(out showCmd);
                }
                else
                {
                    shellLinkA.GetShowCmd(out showCmd);
                }

                return (ShellLinkDisplayMode)showCmd;
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetShowCmd((int)value);
                }
                else
                {
                    shellLinkA.SetShowCmd((int)value);
                }
            }
        }

        /// <summary>
        /// ホットキー。
        /// </summary>
        public Keys HotKey
        {
            get
            {
                ushort hotKey = 0;

                if (isUnicodeEnvironment)
                {
                    shellLinkW.GetHotkey(out hotKey);
                }
                else
                {
                    shellLinkA.GetHotkey(out hotKey);
                }

                return (Keys)hotKey;
            }
            set
            {
                if (isUnicodeEnvironment)
                {
                    shellLinkW.SetHotkey((ushort)value);
                }
                else
                {
                    shellLinkA.SetHotkey((ushort)value);
                }
            }
        }

        #endregion

        #region "保存と読み込み"

        /// <summary>
        /// IShellLinkインターフェイスからキャストされたIPersistFileインターフェイスを取得します。
        /// </summary>
        /// <returns>IPersistFileインターフェイス。　取得できなかった場合はnull。</returns>
        private IPersistFile GetIPersistFile()
        {
            if (isUnicodeEnvironment)
            {
                return shellLinkW as IPersistFile;
            }
            else
            {
                return shellLinkA as IPersistFile;
            }
        }

        /// <summary>
        /// カレントファイルにショートカットを保存します。
        /// </summary>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Save()
        {
            Save(currentFile);
        }

        /// <summary>
        /// 指定したファイルにショートカットを保存します。
        /// </summary>
        /// <param name="linkFile">ショートカットを保存するファイル</param>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Save(string linkFile)
        {
            // IPersistFileインターフェイスを取得して保存
            IPersistFile persistFile = GetIPersistFile();

            if (persistFile == null) throw new COMException("IPersistFileインターフェイスを取得できませんでした。");

            persistFile.Save(linkFile, true);

            // カレントファイルを保存
            currentFile = linkFile;
        }

        /// <summary>
        /// 指定したファイルからショートカットを読み込みます。
        /// </summary>
        /// <param name="linkFile">ショートカットを読み込むファイル</param>
        /// <exception cref="FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Load(string linkFile)
        {
            Load(linkFile, IntPtr.Zero, ShellLinkResolveFlags.SLR_ANY_MATCH | ShellLinkResolveFlags.SLR_NO_UI, 1);
        }

        /// <summary>
        /// 指定したファイルからショートカットを読み込みます。
        /// </summary>
        /// <param name="linkFile">ショートカットを読み込むファイル</param>
        /// <param name="hWnd">このコードを呼び出したオーナーのウィンドウハンドル</param>
        /// <param name="resolveFlags">ショートカット情報の解決に関する動作を表すフラグ</param>
        /// <exception cref="FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Load(string linkFile, IntPtr hWnd, ShellLinkResolveFlags resolveFlags)
        {
            Load(linkFile, hWnd, resolveFlags, 1);
        }

        /// <summary>
        /// 指定したファイルからショートカットを読み込みます。
        /// </summary>
        /// <param name="linkFile">ショートカットを読み込むファイル</param>
        /// <param name="hWnd">このコードを呼び出したオーナーのウィンドウハンドル</param>
        /// <param name="resolveFlags">ショートカット情報の解決に関する動作を表すフラグ</param>
        /// <param name="timeOut">SLR_NO_UIを指定したときのタイムアウト値(ミリ秒)</param>
        /// <exception cref="FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Load(string linkFile, IntPtr hWnd, ShellLinkResolveFlags resolveFlags, TimeSpan timeOut)
        {
            Load(linkFile, hWnd, resolveFlags, (int)timeOut.TotalMilliseconds);
        }

        /// <summary>
        /// 指定したファイルからショートカットを読み込みます。
        /// </summary>
        /// <param name="linkFile">ショートカットを読み込むファイル</param>
        /// <param name="hWnd">このコードを呼び出したオーナーのウィンドウハンドル</param>
        /// <param name="resolveFlags">ショートカット情報の解決に関する動作を表すフラグ</param>
        /// <param name="timeOutMilliseconds">SLR_NO_UIを指定したときのタイムアウト値(ミリ秒)</param>
        /// <exception cref="FileNotFoundException">ファイルが見つかりません。</exception>
        /// <exception cref="COMException">IPersistFileインターフェイスを取得できませんでした。</exception>
        public void Load(string linkFile, IntPtr hWnd, ShellLinkResolveFlags resolveFlags, int timeOutMilliseconds)
        {
            if (!File.Exists(linkFile)) throw new FileNotFoundException("ファイルが見つかりません。", linkFile);

            // IPersistFileインターフェイスを取得
            IPersistFile persistFile = GetIPersistFile();

            if (persistFile == null) throw new COMException("IPersistFileインターフェイスを取得できませんでした。");

            // 読み込み
            persistFile.Load(linkFile, 0x00000000);

            // フラグを処理
            uint flags = (uint)resolveFlags;

            if ((resolveFlags & ShellLinkResolveFlags.SLR_NO_UI) == ShellLinkResolveFlags.SLR_NO_UI)
            {
                flags |= (uint)(timeOutMilliseconds << 16);
            }

            // ショートカットに関する情報を読み込む
            if (isUnicodeEnvironment)
            {
                shellLinkW.Resolve(hWnd, flags);
            }
            else
            {
                shellLinkA.Resolve(hWnd, flags);
            }

            // カレントファイルを指定
            currentFile = linkFile;
        }

        #endregion
    }
}
