using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AipoReminder.ValueObject
{
    /// <summary>
    /// コンボボックスに追加する項目となるクラス
    /// </summary>
    class ComboBoxGroupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ComboBoxGroupItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// コンボボックスに表示される名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    
    }
}
