using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

public partial class TestAlertWindowForm : Form
{
    public TestAlertWindowForm()
    {
        InitializeComponent();

        propertyGrid1.SelectedObject = this.alertWindow1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.alertWindow1.Show();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        using (Allison.AlertWindow.AlertWindow aw = new Allison.AlertWindow.AlertWindow())
        {
            aw.Show("メッセージを表示");
        }
    }
}
