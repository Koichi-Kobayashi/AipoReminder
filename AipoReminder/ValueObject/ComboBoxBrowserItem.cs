using System;
using System.Collections.Generic;
using System.Text;

namespace AipoReminder.ValueObject
{
    /// <summary>
    /// コンボボックスに追加する項目となるクラス
    /// </summary>
    class ComboBoxBrowserItem : System.IComparable,
    System.IComparable<ComboBoxBrowserItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public ComboBoxBrowserItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public ComboBoxBrowserItem(int id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        /// <summary>
        /// コンボボックスに表示される名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        // 自分自身がotherより小さいときはマイナスの数、大きいときはプラスの数、
        // 同じときは0を返す
        public int CompareTo(ComboBoxBrowserItem other)
        {
            // nullより大きい
            if (other == null)
                return 1;

            // Nameの比較結果をそのまま使用する
            return this.Name.CompareTo(other.Name);
        }

        public int CompareTo(object other)
        {
            // nullより大きい
            if (other == null)
                return 1;

            // 違う型とは比較できない
            if (this.GetType() != other.GetType())
                throw new ArgumentException();

            return this.CompareTo((ComboBoxBrowserItem)other);
        }
    }
}
