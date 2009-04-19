
namespace AipoReminder.ValueObject
{
    /// <summary>
    /// コンボボックスに追加する項目となるクラス
    /// </summary>
    public class ComboBoxItem
    {
        private string m_id = "";
        private string m_name = "";
        private string m_password = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">表示名称</param>
        public ComboBoxItem(string id, string name, string password)
        {
            m_id = id;
            m_name = name;
            m_password = password;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// 表示名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password
        {
            get
            {
                return m_password;
            }
        }

        /// <summary>
        /// コンボボックスに表示される
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_name;
        }
    }
}
