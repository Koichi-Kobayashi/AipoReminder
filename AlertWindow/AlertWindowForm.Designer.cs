namespace Allison.AlertWindow
{
    partial class AlertWindowForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.TableLayout = new Allison.AlertWindow.Windows.Forms.TableLayoutPanelEx();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblLinkMessage = new System.Windows.Forms.LinkLabel();
            this.btnClose = new Allison.AlertWindow.Windows.Forms.ButtonEx();
            this.lblTitle = new System.Windows.Forms.Label();
            this.TableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Interval = 3000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // TableLayout
            // 
            this.TableLayout.AutoSize = true;
            this.TableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayout.BackColor = System.Drawing.Color.Transparent;
            this.TableLayout.ColumnCount = 4;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.TableLayout.Controls.Add(this.picIcon, 0, 0);
            this.TableLayout.Controls.Add(this.lblLinkMessage, 0, 1);
            this.TableLayout.Controls.Add(this.btnClose, 3, 0);
            this.TableLayout.Controls.Add(this.lblTitle, 1, 0);
            this.TableLayout.Location = new System.Drawing.Point(0, 0);
            this.TableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 3;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.TableLayout.Size = new System.Drawing.Size(169, 84);
            this.TableLayout.TabIndex = 0;
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(8, 8);
            this.picIcon.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            // 
            // lblLinkMessage
            // 
            this.lblLinkMessage.AutoSize = true;
            this.TableLayout.SetColumnSpan(this.lblLinkMessage, 2);
            this.lblLinkMessage.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.lblLinkMessage.Location = new System.Drawing.Point(16, 56);
            this.lblLinkMessage.Margin = new System.Windows.Forms.Padding(16);
            this.lblLinkMessage.Name = "lblLinkMessage";
            this.lblLinkMessage.Size = new System.Drawing.Size(113, 12);
            this.lblLinkMessage.TabIndex = 2;
            this.lblLinkMessage.Text = "LinkLabel1LinkLabel1";
            this.lblLinkMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(148, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(18, 18);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(48, 8);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(33, 12);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AlertWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 138);
            this.ControlBox = false;
            this.Controls.Add(this.TableLayout);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(180, 140);
            this.Name = "AlertWindowForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TableLayout.ResumeLayout(false);
            this.TableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Allison.AlertWindow.Windows.Forms.TableLayoutPanelEx TableLayout;
        internal System.Windows.Forms.PictureBox picIcon;
        internal System.Windows.Forms.Timer DisplayTimer;
        internal System.Windows.Forms.LinkLabel lblLinkMessage;
        internal Allison.AlertWindow.Windows.Forms.ButtonEx btnClose;
        internal System.Windows.Forms.Label lblTitle;
    }
}

