using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Allison.AlertWindow
{
    [DefaultEvent("Click")]
    public class AlertWindow : System.ComponentModel.Component
    {

        #region フィールド

        private LinkLabel _DummyLinkLable;

        /// <summary>
        /// AlertWindow本体
        /// </summary>
        private AlertWindowForm awf;

        /// <summary>
        /// AlertWindowを閉じたかどうか
        /// </summary>
        private bool isClosed;

        #endregion

        #region コンストラクタ

        public AlertWindow()
        {
            this.Animation = AnimationMode.Slide;
            this.Time = 4000;
            this.Margin = new Padding(10);
            this.MinimumSize = new Size(180, 140);

            this.BackColor = Color.White;
            this.BackColor2 = Color.FromArgb(229, 231, 240);
            this.GradientMode = LinearGradientMode.Vertical;
            this.BackgroundImage = null;
            this.BackgroundImageLayout = ImageLayout.None;
            this.Cursor = Cursors.Default;

            this.Icon = AlertIcons.None;
            this.IconSize = new Size(32, 32);
            this.CustomIcon = null;

            this.Title = string.Empty;
            this.TitleFont = new Font("MS UI Gothic", 9, FontStyle.Bold);
            this.TitleForeColor = Color.Black;

            this.Message = string.Empty;
            this.MessageFont = new Font("MS UI Gothic", 9, FontStyle.Regular);
            this.MessageForeColor = Color.Black;
            this._DummyLinkLable = new LinkLabel();
            this._MessageLinks = new LinkLabel.LinkCollection(this._DummyLinkLable);
            this._MessageLinks.Clear();
            this.MessageActiveLinkColor = Color.Red;
            this.MessageLinkColor = Color.Blue;
        }

        #endregion

        #region プロパティ

        /// <summary>
        /// アラートウィンドウの表示アニメーションを取得または設定します。
        /// </summary>
        [Category("動作")]
        [DefaultValue(typeof(AnimationMode), "Slide")]
        [Description("アラートウィンドウの表示アニメーションを取得または設定します。")]
        public AnimationMode Animation { get; set; }

        /// <summary>
        /// アラートウィンドウの表示時間 (ミリ秒単位) を取得または設定します。
        /// </summary>
        [Category("動作")]
        [DefaultValue(4000)]
        [Description("アラートウィンドウの表示時間 (ミリ秒単位) を取得または設定します。")]
        public int Time { get; set; }

        /// <summary>
        /// 閉じるボタンを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(false)]
        [Description("閉じるボタンを表示するかどうかを示す値を取得または設定します。")]
        public bool ShowCloseButton { get; set; }

        /// <summary>
        /// アラートウィンドウと作業領域との余白間のスペースを指定します。
        /// </summary>
        [Category("配置")]
        [DefaultValue(typeof(Padding), "10,10,10,10")]
        [Description("アラートウィンドウと作業領域との余白間のスペースを指定します。")]
        public Padding Margin { get; set; }
        
        /// <summary>
        /// アラートウィンドウの最小サイズを取得または設定します。
        /// </summary>
        [Category("配置")]
        [DefaultValue(typeof(Size), "180,140")]
        [Description("アラートウィンドウの最小サイズを取得または設定します。")]
        public Size MinimumSize { get; set; }

        /// <summary>
        /// アラートウィンドウに表示される背景色を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "White")]
        [Description("アラートウィンドウに表示される背景色を取得または設定します。")]
        public Color BackColor { get; set; }

        /// <summary>
        /// アラートウィンドウに表示される背景のグラデーションの終了色を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "229, 231, 240")]
        [Description("アラートウィンドウに表示される背景のグラデーションの終了色を取得または設定します。")]
        public Color BackColor2 { get; set; }

        /// <summary>
        /// アラートウィンドウの背景色のグラデーションの方向です。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(LinearGradientMode), "Vertical")]
        [Description("アラートウィンドウの背景色のグラデーションの方向です。")]
        public LinearGradientMode GradientMode { get; set; }

        /// <summary>
        /// アラートウィンドウに表示される背景イメージを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Image), "")]
        [Description("アラートウィンドウに表示される背景イメージを取得または設定します。")]
        public Image BackgroundImage { get; set; }

        /// <summary>
        /// アラートウィンドウに表示される背景イメージ レイアウトを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(ImageLayout), "None")]
        [Description("アラートウィンドウに表示される背景イメージ レイアウトを取得または設定します。")]
        public ImageLayout BackgroundImageLayout { get; set; }

        /// <summary>
        /// マウスカーソルがアラートウィンドウの上にあるときに表示されるカーソルを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Cursor), "Default")]
        [Description("マウスカーソルがアラートウィンドウの上にあるときに表示されるカーソルを取得または設定します。")]
        public Cursor Cursor { get; set; }

        /// <summary>
        /// アラートウィンドウに表示されるアイコンを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(AlertIcons), "None")]
        [Description("アラートウィンドウに表示されるアイコンを取得または設定します。")]
        public AlertIcons Icon { get; set; }

        /// <summary>
        /// アラートウィンドウに表示されるアイコンのサイズを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Size), "32, 32")]
        [Description("アラートウィンドウに表示されるアイコンのサイズを取得または設定します。")]
        public Size IconSize { get; set; }

        /// <summary>
        /// アラートウィンドウに表示される独自のアイコンを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Image), "")]
        [Description("アラートウィンドウに表示される独自のアイコンを取得または設定します。")]
        public Image CustomIcon { get; set; }

        /// <summary>
        /// アラートウィンドウのタイトルバーに表示されるテキストを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue("")]
        [Description("アラートウィンドウのタイトルバーに表示されるテキストを取得または設定します。")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Title { get; set; }

        /// <summary>
        /// アラートウィンドウのタイトルバーに表示されるテキストのフォントを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Font), "MS UI Gothic,9pt,style=Bold")]
        [Description("アラートウィンドウのタイトルバーに表示されるテキストのフォントを取得または設定します。")]
        public Font TitleFont { get; set; }

        /// <summary>
        /// アラートウィンドウのタイトルバーに表示されるテキストの前景色または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("アラートウィンドウのタイトルバーに表示されるテキストの前景色または設定します。")]
        public Color TitleForeColor { get; set; }

        /// <summary>
        /// アラートウィンドウに表示されるテキストを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue("")]
        [Description("アラートウィンドウに表示されるテキストを取得または設定します。")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Message { get; set; }

        /// <summary>
        /// アラートウィンドウに表示されるテキストのフォントを取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Font), "MS UI Gothic,9pt,style=Regular")]
        [Description("アラートウィンドウに表示されるテキストのフォントを取得または設定します。")]
        public Font MessageFont { get; set; }

        /// <summary>
        /// アラートウィンドウに表示されるテキストの前景色を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("アラートウィンドウに表示されるテキストの前景色を取得または設定します。")]
        public Color MessageForeColor { get; set; }

        private LinkArea _MessageLinkArea;
        /// <summary>
        /// メッセージ内でリンクとして処理される領域を取得または設定します。
        /// </summary>
        [Category("動作")]
        [DefaultValue(typeof(LinkArea), "0,0")]
        [Description("メッセージ内でリンクとして処理される領域を取得または設定します。")]
        public LinkArea MessageLinkArea
        {
            get { return this._MessageLinkArea; }
            set
            {
                this._MessageLinkArea = value;

                this._MessageLinks.Clear();
                this._MessageLinks.Add(value.Start, value.Length);
            }
        }

        private LinkLabel.LinkCollection _MessageLinks;
        /// <summary>
        /// メッセージ内でリンクとして処理される領域を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [Category("動作")]
        [DefaultValue(typeof(LinkLabel.LinkCollection), "")]
        [Description("メッセージ内でリンクとして処理される領域を取得または設定します。")]
        public LinkLabel.LinkCollection MessageLiks
        {
            get { return this._MessageLinks; }
        }

        /// <summary>
        /// 通常のリンクを表示するために使用する色を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "Blue")]
        [Description("通常のリンクを表示するために使用する色を取得または設定します。")]
        public Color MessageLinkColor { get; set; }

        /// <summary>
        /// アクティブなリンクを表示するために使用する色を取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(typeof(Color), "Red")]
        [Description("アクティブなリンクを表示するために使用する色を取得または設定します。")]
        public Color MessageActiveLinkColor { get; set; }

        #endregion

        #region イベント

        /// <summary>
        /// アラートウィンドウ内のハイパーリンクがクリックされたときに発生します。
        /// </summary>
        public event LinkLabelLinkClickedEventHandler LinkClicked;
        private void lblLinkMessage_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (LinkClicked != null)
            {
                LinkClicked(this, e);
            }
            Console.WriteLine(e.Link.Name);
        }

        /// <summary>
        /// アラートウィンドウがクリックされたときに発生します。
        /// </summary>
        public event EventHandler Click;
        private void AlertWindow_Click(System.Object sender, System.EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        #endregion

        #region Public メソッド

        /// <summary>
        /// アラートウィンドウを表示する
        /// </summary>
        public void Show()
        {
            this.Show(this.Message, this.Title, this.Icon);
        }

        /// <summary>
        /// アラートウィンドウを表示する
        /// </summary>
        /// <param name="message">アラートウィンドウに表示するテキスト</param>
        public void Show(string message)
        {
            this.Show(message, this.Title, this.Icon);
        }

        /// <summary>
        /// アラートウィンドウを表示する
        /// </summary>
        /// <param name="message">アラートウィンドウに表示するテキスト</param>
        /// <param name="title">アラートウィンドウのタイトルバーに表示するテキスト</param>
        public void Show(string message, string title)
        {
            this.Show(message, title, this.Icon);
        }

        /// <summary>
        /// アラートウィンドウを表示する
        /// </summary>
        /// <param name="message">アラートウィンドウに表示するテキスト</param>
        /// <param name="title">アラートウィンドウのタイトルバーに表示するテキスト</param>
        /// <param name="icon">アラートウィンドウに表示するアイコン</param>
        public void Show(string message, string title, AlertIcons icon)
        {
            awf = new AlertWindowForm();
            isClosed = false;

            // アニメーション
            awf.Animation = this.Animation;
            awf.Time = this.Time;

            // 余白
            awf.MinimumSize = this.MinimumSize;
            awf.Margin = this.Margin;

            // 外観
            awf.ShowCloseButton = this.ShowCloseButton;
            awf.BackColor = this.BackColor;
            awf.BackColor2 = this.BackColor2;
            awf.GradientMode = this.GradientMode;
            awf.AlertWindowBackgroundImage = this.BackgroundImage;
            awf.AlertWindowBackgroundImageLayout = this.BackgroundImageLayout;
            awf.AlertCursor = this.Cursor;
            this.AddHandlerClick(awf);
            awf.btnClose.Click -= AlertWindow_Click;

            // アイコン設定
            switch (icon)
            {
                case AlertIcons.None:
                    break;
                case AlertIcons.Information:
                    awf.AlertIcon = SystemIcons.Information.ToBitmap();
                    break;
                case AlertIcons.Question:
                    awf.AlertIcon = SystemIcons.Question.ToBitmap();
                    break;
                case AlertIcons.Warning:
                    awf.AlertIcon = SystemIcons.Warning.ToBitmap();
                    break;
                case AlertIcons.Error:
                    awf.AlertIcon = SystemIcons.Error.ToBitmap();
                    break;
                case AlertIcons.Custom:
                    awf.AlertIcon = this.CustomIcon;
                    break;
            }
            // アイコンサイズを調整
            if (awf.AlertIcon != null)
            {
                Bitmap b = new Bitmap(this.IconSize.Width, this.IconSize.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawImage(awf.AlertIcon, 0, 0, this.IconSize.Width, this.IconSize.Height);
                }
                awf.AlertIcon = b;
            }

            // タイトル設定
            awf.Title = title;
            awf.TitleFont = this.TitleFont;
            awf.TitleForeColor = this.TitleForeColor;

            // メッセージ設定
            awf.Message = message;
            awf.MessageFont = this.MessageFont;
            awf.MessageForeColor = this.MessageForeColor;
            // リンク設定
            foreach (LinkLabel.Link l in this.MessageLiks)
            {
                awf.MessageLiks.Add(l);
            }
            awf.lblLinkMessage.LinkClicked += lblLinkMessage_LinkClicked;

            // アラートウィンドウを表示
            awf.Show();
        }

        /// <summary>
        /// アラートウィンドウを閉る
        /// </summary>
        public void Close()
        {
            if (awf != null)
            {
                if (!isClosed)
                {
                    awf.CloseWindow();
                    isClosed = true;
                }
            }
        }

        /// <summary>
        /// アラートウィンドウを表示しているかどうかを表す
        /// </summary>
        /// <returns>true:表示、false:非表示</returns>
        public bool isShow()
        {
            if (awf != null)
            {
                return awf.isShow();
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Protected メソッド

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    this._DummyLinkLable.Dispose();
                    if (this.BackgroundImage != null)
                    {
                        this.BackgroundImage.Dispose();
                    }
                    if (this.CustomIcon != null)
                    {
                        this.CustomIcon.Dispose();
                    }
                    if (this.TitleFont != null)
                    {
                        this.TitleFont.Dispose();
                    }
                    if (this.MessageFont != null)
                    {
                        this.MessageFont.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Private メソッド

        /// <summary>
        /// 指定されたコントロール配下の全てのクリックイベントを関連付けます。
        /// </summary>
        /// <param name="container">親コントロール</param>
        private void AddHandlerClick(Control container)
        {
            container.Click += AlertWindow_Click;

            if (container.HasChildren)
            {
                foreach (Control c in container.Controls)
                {
                    this.AddHandlerClick(c);
                }
            }
        }

        #endregion
    }
}
