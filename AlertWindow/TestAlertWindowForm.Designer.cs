partial class TestAlertWindowForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.alertWindow1 = new Allison.AlertWindow.AlertWindow();
        this.SuspendLayout();
        // 
        // propertyGrid1
        // 
        this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.propertyGrid1.Location = new System.Drawing.Point(12, 44);
        this.propertyGrid1.Name = "propertyGrid1";
        this.propertyGrid1.Size = new System.Drawing.Size(483, 491);
        this.propertyGrid1.TabIndex = 0;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(12, 15);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(153, 23);
        this.button1.TabIndex = 1;
        this.button1.Text = "PropertyGrid の設定で表示";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(171, 15);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(104, 23);
        this.button2.TabIndex = 2;
        this.button2.Text = "メッセージを表示";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(281, 15);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(104, 23);
        this.button3.TabIndex = 3;
        this.button3.Text = "タイトルを表示";
        this.button3.UseVisualStyleBackColor = true;
        // 
        // button4
        // 
        this.button4.Location = new System.Drawing.Point(391, 15);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(104, 23);
        this.button4.TabIndex = 4;
        this.button4.Text = "閉じるボタンを表示";
        this.button4.UseVisualStyleBackColor = true;
        // 
        // TestAlertWindowForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(507, 547);
        this.Controls.Add(this.button4);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.propertyGrid1);
        this.Name = "TestAlertWindowForm";
        this.Text = "TestAlertWindow";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private Allison.AlertWindow.AlertWindow alertWindow1;
}