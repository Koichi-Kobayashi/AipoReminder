using System.Windows.Forms;

namespace AipoReminder.ValueObject
{
    /// <summary>
    /// 非表示のキー項目を持つListViewItemクラス
    /// </summary>
    class ListViewItemEx : ListViewItem
    {
        public string Key { set; get; }

        public ListViewItemEx(string[] subItems, string key) : base(subItems)
        {
            Key = key;
        }
    }
}
