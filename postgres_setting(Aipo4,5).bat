@echo off
@setlocal

rem ------------------------------------------------------------------------
rem aipoをインストールした場所に合わせて下さい
rem psql実行ファイル
set psql=E:\aipo\dpl003\postgresql\bin\psql.exe
rem ------------------------------------------------------------------------

set psql=%psql% -d org001 -U aipo_postgres -q -t -d org001 -c 
rem ロールの検索結果出力先
set result=aipo_reminder_psql_result.txt
rem ロール追加コマンド
set role_add_cmd="CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder'; "
rem ロール権限付与コマンド
set role_cmd="GRANT SELECT ON eip_t_schedule_map TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_schedule_map TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_schedule TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON turbine_user TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON turbine_group TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON turbine_user_group_role TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_whatsnew TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_blog_entry TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_blog_comment TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_workflow_request_map TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_workflow_request TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_workflow_category TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_msgboard_topic TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_msgboard_category TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_msgboard_category_map TO aipo_reminder; 
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_note TO aipo_reminder;
set role_cmd=%role_cmd% GRANT SELECT, INSERT, UPDATE ON eip_t_ext_timecard TO aipo_reminder;
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_ext_timecard_system TO aipo_reminder;
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_ext_timecard_system_map TO aipo_reminder;
set role_cmd=%role_cmd% GRANT SELECT ON eip_t_ext_timecard_timecard_id_seq TO aipo_reminder; "

rem ロール検索
%psql% "SELECT * FROM pg_roles where rolname = 'aipo_reminder';" | findstr "aipo_reminder" > %result%
rem 検索結果を取得
for /f %%a in (%result%) do set role=%%a
rem ロール検索結果ファイルの削除
del %result%

if "%role%" == "aipo_reminder" (
    rem aipo_reminderが存在している場合は、テーブルにアクセス権限を付与する
    %psql% %role_cmd%
) else (
    rem aipo_reminderが存在しない場合は、ロールを追加してからテーブルにアクセス権限を付与する
    %psql% %role_add_cmd%
    %psql% %role_cmd%
)

pause;
