
namespace AipoReminder.Constants
{
    class DBConstants
    {
        public const string PUBLIC_FLAG_OPEN = "O";
        public const string PUBLIC_FLAG_CLOSED = "C";

        /* ポートレットタイプ */
        public static int WHATS_NEW_TYPE_BLOG_ENTRY = 1;        // ブログ
        public static int WHATS_NEW_TYPE_BLOG_COMMENT = 2;      // ブログコメント
        public static int WHATS_NEW_TYPE_WORKFLOW_REQUEST = 3;  // ワークフロー
        public static int WHATS_NEW_TYPE_MSGBOARD_TOPIC = 4;    // 掲示板
        public static int WHATS_NEW_TYPE_NOTE = 5;              // 伝言メモ
        public static int WHATS_NEW_TYPE_SCHEDULE = 6;          // スケジュール
    }
}
