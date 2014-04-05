AipoReminder  
  
グループウェアAipoの新着通知を自動で取得するアプリケーションです。  
  
使い方  
　１.動作環境として、.NET Framework2.0以上が必要になります。  
　２.Aipo4.0.4.0以上または5.0.0.0以上が同じネットワーク上にインストールされている必要があります。  
　　一部の機能は5.0.0.0以上で使用可能です。  
  
以下、Aipoの管理者向けの情報です。  
　次の情報を元にPostgreSQLの設定を変更することで、  
 本ソフトウェアが正常に動作するようになりますが、  
　セキュリティレベルが下がります。  
　使用者のセキュリティポリシーにそぐわない場合は、使用を中止して下さい。  
  
　３.ポートの設定について  
　　Aipoをインストールしたままの状態ではローカルホストのみのアクセスを許可しているため、  
　　AipoリマインダーをクライアントPCにインストールしても  
　　Aipoに接続出来ないというエラーが表示されますので、PostgreSQLが使用するポートを  
  開ける必要があります。  
  
　　　【Windows XP / Windows Server 2003等にAipoをインストールしている場合】  
　　　ファイアウォールが有効になっている場合、「例外タブ」で「ポートの追加」より、  
　　　5432ポートを開けるように設定して下さい。  
　　　名前は特に何でも構いませんが、分かり易いように、Aipo_PostgreSQLとし、  
　　　ポートは5432でTCPを選択して下さい。  
  
　　　セキュリティ対策ソフトでファイアウォールを構築している場合は、  
　　　そちらの設定が優先されますので、  
　　　セキュリティ対策ソフトの設定を行って下さい。  
  
　　　【LinuxにAipoをインストールしている場合】  
　　　vi /etc/sysconfig/iptables  
  
　　　COMMITの前に、  
　　　-A RH-Firewall-1-INPUT -m state --state NEW -m tcp -p tcp --dport 5432 -j ACCEPT  
　　　を追加し、iptables を再起動して下さい。  
　　　(RH-Firewall-1-INPUTはディストリビューションによって異なる可能性があります。)  
  
  ４.PostgreSQLの設定変更について(Windowsの場合)  
　　ここから先はPostgreSQLの知識がある方が行って下さい。  
  
　　AipoがEドライブにインストールされている場合は以下のように設定します。  
　　IPアドレスなどは環境に合わせて変更して下さい。  
  
    【postgresql.confの修正】  
      E:\aipo\dpl003\postgresql\data\postgresql.conf  
  
      49行目  
      #listen_addresses = 'localhost'  
      ↓  
      listen_addresses = '*'  
  
    【pg_hba.confの修正】  
      E:\aipo\dpl003\postgresql\data\pg_hba.conf  
      以下の設定を追加(192.168.24.0/24のネットワークの場合)  
  
      host    all         aipo_reminder         192.168.24.0/24       md5  
  
      【ロールの追加と権限の付与】  
      AipoがインストールされているPostgreSQLに対して以下のコマンドを実行して下さい。  
      (aipo_reminderの名前やパスワード(reminder)については、変更して頂いて構いませんが、  
      その場合は、Aipoリマインダーの「DB設定」のユーザIDやパスワードも合わせて修正して下さい。)  
  
      以下のコマンドを一気に実行するバッチを用意しています。  
      postgres_setting.batを実行するとコマンドを入力する手間が省けます。  
    【使い方】  
      バッチファイルをテキストエディタで開き、7行目のpsql実行ファイルのパスを環境に合わせて設定し、  
      Aipoがインストールされている環境にて実行して下さい。  
  
      一つ一つ実行する場合は以下の手順で行って下さい。  
      Windowsのコマンドプロンプトから実行する場合は、  
      E:\aipo\dpl003\postgresql\bin に移動したあと、以下のコマンドを実行して下さい。  
  
      psql -d org001 -U aipo_postgres  
  
      PostgreSQLに接続出来たら、以下のコマンドを順に実行して下さい。  
  
      【Aipo4～5の場合】  
        CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder';  
        GRANT SELECT ON eip_t_schedule_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_schedule TO aipo_reminder;  
        GRANT SELECT ON turbine_user TO aipo_reminder;  
        GRANT SELECT ON turbine_group TO aipo_reminder;  
        GRANT SELECT ON turbine_user_group_role TO aipo_reminder;  
        GRANT SELECT ON eip_t_whatsnew TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_entry TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_comment TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_topic TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_note TO aipo_reminder;  
        GRANT SELECT, INSERT, UPDATE ON eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system_map TO aipo_reminder;  
        GRANT UPDATE ON eip_t_ext_timecard_timecard_id_seq TO aipo_reminder;  
  
      【Aipo6,7の場合】  
        CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder';  
        GRANT SELECT ON eip_t_schedule_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_schedule TO aipo_reminder;  
        GRANT SELECT ON turbine_user TO aipo_reminder;  
        GRANT SELECT ON turbine_group TO aipo_reminder;  
        GRANT SELECT ON turbine_user_group_role TO aipo_reminder;  
        GRANT SELECT ON eip_t_whatsnew TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_entry TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_comment TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_topic TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_note TO aipo_reminder;  
        GRANT SELECT, INSERT, UPDATE ON eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system_map TO aipo_reminder;  
        GRANT UPDATE ON pk_eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON activity TO aipo_reminder;  
        GRANT SELECT ON activity_map TO aipo_reminder;  
  
      \q で切断します。  
  
      最後にAipoの再起動を行って下さい。  
  
  ５.PostgreSQLの設定変更について(Linuxの場合)  
　　ここから先はPostgreSQLの知識がある方が行って下さい。  
  
      Aipoが /usr/local/aipo にインストールされている場合は以下のように設定します。  
      IPアドレスなどは環境に合わせて変更して下さい。  
  
      【postgresql.confの修正】  
      vi /usr/local/aipo/postgres/data/postgresql.conf  
  
      49行目  
      #listen_addresses = 'localhost'  
      ↓  
      listen_addresses = '*'  
  
      【pg_hba.confの修正】  
      vi /usr/local/aipo/postgres/data/pg_hba.conf  
      以下の設定を追加(192.168.24.0/24のネットワークの場合)  
  
      host    all         aipo_reminder         192.168.24.0/24       md5  
  
      【ロールの追加と権限の付与】  
      AipoがインストールされているPostgreSQLに対して以下のコマンドを実行して下さい。  
      (aipo_reminderの名前やパスワード(reminder)については、変更して頂いて構いませんが、  
      その場合は、Aipoリマインダーの「DB設定」のユーザIDやパスワードも合わせて修正して下さい。)  
  
      Aipoが起動されていない場合は以下のコマンドで起動させて下さい。  
      /usr/local/aipo/bin/startup.sh  
  
      以下のコマンドでaipo_postgresユーザになります。  
      su - aipo_postgres  
  
      PostgreSQLに接続します。  
      psql -d org001 -U aipo_postgres  
  
      PostgreSQLに接続出来たら、以下のコマンドを順に実行して下さい。  
  
      【Aipo4～5の場合】  
        CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder';  
        GRANT SELECT ON eip_t_schedule_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_schedule TO aipo_reminder;  
        GRANT SELECT ON turbine_user TO aipo_reminder;  
        GRANT SELECT ON turbine_group TO aipo_reminder;  
        GRANT SELECT ON turbine_user_group_role TO aipo_reminder;  
        GRANT SELECT ON eip_t_whatsnew TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_entry TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_comment TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_topic TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_note TO aipo_reminder;  
        GRANT SELECT, INSERT, UPDATE ON eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system_map TO aipo_reminder;  
        GRANT UPDATE ON eip_t_ext_timecard_timecard_id_seq TO aipo_reminder;  
  
      【Aipo6,7の場合】  
        CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder';  
        GRANT SELECT ON eip_t_schedule_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_schedule TO aipo_reminder;  
        GRANT SELECT ON turbine_user TO aipo_reminder;  
        GRANT SELECT ON turbine_group TO aipo_reminder;  
        GRANT SELECT ON turbine_user_group_role TO aipo_reminder;  
        GRANT SELECT ON eip_t_whatsnew TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_entry TO aipo_reminder;  
        GRANT SELECT ON eip_t_blog_comment TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_request TO aipo_reminder;  
        GRANT SELECT ON eip_t_workflow_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_topic TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category TO aipo_reminder;  
        GRANT SELECT ON eip_t_msgboard_category_map TO aipo_reminder;  
        GRANT SELECT ON eip_t_note TO aipo_reminder;  
        GRANT SELECT, INSERT, UPDATE ON eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system TO aipo_reminder;  
        GRANT SELECT ON eip_t_ext_timecard_system_map TO aipo_reminder;  
        GRANT UPDATE ON pk_eip_t_ext_timecard TO aipo_reminder;  
        GRANT SELECT ON activity TO aipo_reminder;  
        GRANT SELECT ON activity_map TO aipo_reminder;  
  
      \q で切断します。  
  
      最後にAipoの再起動を行って下さい。  
  
  
#AlertWindow.dllについて  
AlertWindow.dllはYouryellaさんが作成したものを独自にカスタマイズしています。    
Youryellaさんに使用の許可を頂いています。    
本家サイト    
http://youryella.wankuma.com/Library/ClassLibrary/AlertWindow.aspx    
  
  