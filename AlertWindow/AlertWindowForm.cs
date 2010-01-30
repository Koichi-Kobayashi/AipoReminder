using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Allison.AlertWindow
{
    internal partial class AlertWindowForm
    {

        #region P/Invoke

        [Flags()]
        private enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x1,
            AW_HOR_NEGATIVE = 0x2,
            AW_VER_POSITIVE = 0x4,
            AW_VER_NEGATIVE = 0x8,
            AW_CENTER = 0x10,
            AW_HIDE = 0x10000,
            AW_ACTIVATE = 0x20000,
            AW_SLIDE = 0x40000,
            AW_BLEND = 0x80000
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        private readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x1;
        private const UInt32 SWP_NOMOVE = 0x2;

        private const UInt32 SWP_NOACTIVATE = 0x10;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        #endregion

        #region コンストラクタ

        public AlertWindowForm()
        {
            InitializeComponent();
        }

        #endregion

        #region プロパティ

        public AnimationMode Animation { get; set; }

        public int Time { get; set; }

        public new Padding Margin { get; set; }

        public bool ShowCloseButton { get; set; }

        public Image AlertWindowBackgroundImage { get; set; }

        public ImageLayout AlertWindowBackgroundImageLayout { get; set; }

        public Color BackColor2 { get; set; }

        public LinearGradientMode GradientMode { get; set; }

        public Image AlertIcon { get; set; }

        public Cursor AlertCursor { get; set; }

        public string Title { get; set; }

        public Font TitleFont { get; set; }

        public Color TitleForeColor { get; set; }

        public string Message { get; set; }

        public Font MessageFont { get; set; }

        public Color MessageForeColor { get; set; }

        public LinkLabel.LinkCollection MessageLiks
        {
            get { return this.lblLinkMessage.Links; }
        }

        public string abc { get; set; }

        #endregion

        #region イベント

        // ×ボタンクリック
        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.CloseWindow();
        }

        private void DisplayTimer_Tick(object sender, System.EventArgs e)
        {
            // マウスがフォーム内の時は閉じないようにする
            if (this.Bounds.Contains(Control.MousePosition))
            {
                this.DisplayTimer.Interval = 500;
                return;
            }

            this.CloseWindow();
        }

        #endregion

        #region Public メソッド

        public new void Show()
        {
            // フォームと各コントロールの初期化
            this.InitializeForm();

            // フォームロードを行わせる（アニメーション中にコントロールのテキストが表示されないため）
            base.Opacity = 0;
            base.Show();
            base.Hide();
            base.Opacity = 1;

            // 表示位置を設定
            Rectangle wa = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(wa.Width - this.Width - this.Margin.Right, wa.Height - this.Height - this.Margin.Bottom);

            // アクティブにならないように最前面に表示
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
            if (this.Animation == AnimationMode.Slide)
            {
                AnimateWindow(this.Handle, 400, AnimateWindowFlags.AW_SLIDE | AnimateWindowFlags.AW_VER_NEGATIVE);
            }
            else if (this.Animation == AnimationMode.Blend)
            {
                AnimateWindow(this.Handle, 400, AnimateWindowFlags.AW_BLEND);
            }
            base.Show();

            this.DisplayTimer.Interval = this.Time;
            this.DisplayTimer.Start();
        }

        #endregion

        #region Protected メソッド

        // 背景を描画
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            using (LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColor2, this.GradientMode))
            {
                e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }
        }

        // 表示時アクティブにしないようにする
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        // マウスクリックでアクティブにしないようにする
        private const int WM_MOUSEACTIVATE = 0x21;

        private const int MA_NOACTIVATE = 3;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Private メソッド

        // フォームの初期設定
        private void InitializeForm()
        {
            // 背景画像
            this.TableLayout.BackgroundImage = this.AlertWindowBackgroundImage;
            this.TableLayout.BackgroundImageLayout = this.AlertWindowBackgroundImageLayout;

            // カーソル
            this.Cursor = this.AlertCursor;

            // アイコン
            if (this.AlertIcon == null)
            {
                this.picIcon.Hide();
                this.TableLayout.ColumnStyles[0].Width = 0;
            }
            else
            {
                this.TableLayout.ColumnStyles[0].Width = this.AlertIcon.Width + this.picIcon.Margin.Horizontal;
                this.picIcon.Image = this.AlertIcon;
                this.picIcon.Size = this.AlertIcon.Size;
            }

            // タイトル
            this.lblTitle.Font = this.TitleFont;
            this.lblTitle.ForeColor = this.TitleForeColor;
            this.lblTitle.Text = this.Title;
            if (this.lblTitle.Text == string.Empty)
            {
                this.lblTitle.Hide();

            }
            else if (this.AlertIcon == null)
            {
                this.TableLayout.SetColumn(this.lblTitle, 0);
                this.TableLayout.SetColumnSpan(this.lblTitle, 2);

            }
            else if (this.AlertIcon != null)
            {
                if (this.lblTitle.Height < this.picIcon.Height)
                {
                    int w = this.lblTitle.Width;
                    this.lblTitle.AutoSize = false;
                    this.lblTitle.Width = w;
                    this.lblTitle.Height = this.picIcon.Height;
                }
            }

            // メッセージ
            this.lblLinkMessage.Text = this.Message;
            this.lblLinkMessage.Font = this.MessageFont;
            this.lblLinkMessage.ForeColor = this.MessageForeColor;
            if (this.lblLinkMessage.Text == string.Empty)
            {
                this.lblLinkMessage.Hide();
                this.lblTitle.Margin = new Padding(8);
            }
            if ((this.AlertIcon == null && this.lblTitle.Text == string.Empty) == false)
            {
                if (this.ShowCloseButton == true)
                {
                    this.TableLayout.SetColumnSpan(this.lblLinkMessage, 4);
                }
            }

            // 閉じるボタン
            this.btnClose.Visible = this.ShowCloseButton;
            if (this.ShowCloseButton == true)
            {
                if (this.AlertIcon == null && this.lblTitle.Text == string.Empty)
                {
                    this.TableLayout.SetRow(this.btnClose, 1);
                }
            }
            else
            {
                this.TableLayout.ColumnStyles[3].Width = 0;
            }

            // フォームのサイズを調整
            if (this.TableLayout.Width < this.MinimumSize.Width)
            {
                this.TableLayout.ColumnStyles[2].Width = this.MinimumSize.Width - this.TableLayout.Width;
                this.TableLayout.ColumnStyles[1].SizeType = SizeType.Absolute;
                this.TableLayout.ColumnStyles[1].Width = this.MinimumSize.Width - this.TableLayout.Width;
            }
            if (this.TableLayout.Height < this.MinimumSize.Height)
            {
                this.TableLayout.RowStyles[2].Height = this.MinimumSize.Height - this.TableLayout.Height;
            }
            this.ClientSize = new Size(this.TableLayout.Width, this.TableLayout.Height);
        }

        // フォームを閉じる
        private void CloseWindow()
        {
            this.DisplayTimer.Stop();

            if (this.Animation == AnimationMode.Slide)
            {
                AnimateWindow(this.Handle, 400, AnimateWindowFlags.AW_HIDE | AnimateWindowFlags.AW_SLIDE | AnimateWindowFlags.AW_VER_POSITIVE);
            }
            else if (this.Animation == AnimationMode.Blend)
            {
                AnimateWindow(this.Handle, 400, AnimateWindowFlags.AW_HIDE | AnimateWindowFlags.AW_BLEND);
            }

            this.Close();
        }

        #endregion

    }
}
