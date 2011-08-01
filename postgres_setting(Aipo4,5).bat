@echo off
@setlocal

rem ------------------------------------------------------------------------
rem aipo���C���X�g�[�������ꏊ�ɍ��킹�ĉ�����
rem psql���s�t�@�C��
set psql=E:\aipo\dpl003\postgresql\bin\psql.exe
rem ------------------------------------------------------------------------

set psql=%psql% -d org001 -U aipo_postgres -q -t -d org001 -c 
rem ���[���̌������ʏo�͐�
set result=aipo_reminder_psql_result.txt
rem ���[���ǉ��R�}���h
set role_add_cmd="CREATE ROLE aipo_reminder WITH LOGIN PASSWORD 'reminder'; "
rem ���[�������t�^�R�}���h
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

rem ���[������
%psql% "SELECT * FROM pg_roles where rolname = 'aipo_reminder';" | findstr "aipo_reminder" > %result%
rem �������ʂ��擾
for /f %%a in (%result%) do set role=%%a
rem ���[���������ʃt�@�C���̍폜
del %result%

if "%role%" == "aipo_reminder" (
    rem aipo_reminder�����݂��Ă���ꍇ�́A�e�[�u���ɃA�N�Z�X������t�^����
    %psql% %role_cmd%
) else (
    rem aipo_reminder�����݂��Ȃ��ꍇ�́A���[����ǉ����Ă���e�[�u���ɃA�N�Z�X������t�^����
    %psql% %role_add_cmd%
    %psql% %role_cmd%
)

pause;
