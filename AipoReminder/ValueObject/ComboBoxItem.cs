﻿
namespace AipoReminder.ValueObject
{
    /// <summary>
    /// コンボボックスに追加する項目となるクラス
    /// </summary>
    public class ComboBoxItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">表示名称</param>
        public ComboBoxItem(string id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
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
