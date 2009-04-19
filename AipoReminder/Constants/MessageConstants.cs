
namespace AipoReminder.Constants
{
    public static class MessageConstants
    {
        // 項目名
        public const string ITEM_LOGIN = "ログイン名";
        public const string ITEM_PASSWORD = "パスワード";
        public const string ITEM_URL = "URL";
        public const string ITEM_DB_SERVER_IP = "サーバIP";
        public const string ITEM_DB_PORT = "ポート";
        public const string ITEM_DB_USER_ID = "ユーザID";
        public const string ITEM_DB_PASSWORD = "パスワード";
        public const string ITEM_DB_NAME = "データベース";

        // エラーメッセージ
        public const string MSG_CAPTION_001 = "アカウント設定エラー";
        public const string MSG_CAPTION_002 = "DB設定エラー";
        public const string INFO_ACCOUNT_SETTING_OK = "アカウント情報を保存しました。";
        public const string INFO_WHATSNEW_SETTING_OK = "お知らせ設定を保存しました。";
        public const string ERR_PASSWORD = "パスワードが間違っています。";
        public const string ERR_DB_ACCESS = "Aipoに接続できませんでした。";

        // 必須
        public const string CHK_MUST = "{0}を入力して下さい。";

        // 文字数
        public const string CHK_HALF_LENGTH = "{0}は半角{1}文字で入力して下さい。";

        // 文字種
        public const string CHK_HALF_ALPHA_NUM = "{0}は半角英数字で入力して下さい。";
        public const string CHK_URL = "{0}は「http」で始まる文字列を入力して下さい。";

        // 伝言メモ
        public const string SUBJECT_TYPE1 = "再度電話します。";
        public const string SUBJECT_TYPE2 = "折返しお電話ください。";
        public const string SUBJECT_TYPE3 = "連絡があったことをお伝えください。";
        public const string SUBJECT_TYPE4 = "伝言をお願いします。";
    }
}
