AipoReminder

�O���[�v�E�F�AAipo�̐V���ʒm�������Ŏ擾����A�v���P�[�V�����ł��B

�g����
�@�P.������Ƃ��āA.NET Framework2.0�ȏオ�K�v�ɂȂ�܂��B
�@�Q.Aipo4.0.4.0�ȏ�܂���5.0.0.0�ȏオ�����l�b�g���[�N��ɃC���X�g�[������Ă���K�v������܂��B
�@�@�ꕔ�̋@�\��5.0.0.0�ȏ�Ŏg�p�\�ł��B

�ȉ��AAipo�̊Ǘ��Ҍ����̏��ł��B
�@���̏�������PostgreSQL�̐ݒ��ύX���邱�ƂŁA�{�\�t�g�E�F�A������ɓ��삷��悤�ɂȂ�܂����A
�@�Z�L�����e�B���x����������܂��B
�@�g�p�҂̃Z�L�����e�B�|���V�[�ɂ�����Ȃ��ꍇ�́A�g�p�𒆎~���ĉ������B

�@�R.�|�[�g�̐ݒ�ɂ���
�@�@Aipo���C���X�g�[�������܂܂̏�Ԃł̓��[�J���z�X�g�݂̂̃A�N�Z�X�������Ă��邽�߁A
�@�@Aipo���}�C���_�[���N���C�A���gPC�ɃC���X�g�[�����Ă�
�@�@Aipo�ɐڑ��o���Ȃ��Ƃ����G���[���\������܂��̂ŁAPostgreSQL���g�p����|�[�g���J����K�v������܂��B

�@�@�@�yWindows XP / Windows Server 2003����Aipo���C���X�g�[�����Ă���ꍇ�z
�@�@�@�t�@�C�A�E�H�[�����L���ɂȂ��Ă���ꍇ�A�u��O�^�u�v�Łu�|�[�g�̒ǉ��v���A
�@�@�@5432�|�[�g���J����悤�ɐݒ肵�ĉ������B
�@�@�@���O�͓��ɉ��ł��\���܂��񂪁A������Ղ��悤�ɁAAipo_PostgreSQL�Ƃ��A�|�[�g��5432��TCP��I�����ĉ������B

�@�@�@�Z�L�����e�B�΍�\�t�g�Ńt�@�C�A�E�H�[�����\�z���Ă���ꍇ�́A������̐ݒ肪�D�悳��܂��̂ŁA
�@�@�@�Z�L�����e�B�΍�\�t�g�̐ݒ���s���ĉ������B

�@�@�@�yLinux��Aipo���C���X�g�[�����Ă���ꍇ�z
�@�@�@vi /etc/sysconfig/iptables

�@�@�@COMMIT�̑O�ɁA
�@�@�@-A RH-Firewall-1-INPUT -m state --state NEW -m tcp -p tcp --dport 5432 -j ACCEPT
�@�@�@��ǉ����Aiptables ���ċN�����ĉ������B
�@�@�@(RH-Firewall-1-INPUT�̓f�B�X�g���r���[�V�����ɂ���ĈقȂ�\��������܂��B)

  �S.PostgreSQL�̐ݒ�ύX�ɂ���(Windows�̏ꍇ)
�@�@����������PostgreSQL�̒m������������s���ĉ������B

�@�@Aipo��E�h���C�u�ɃC���X�g�[������Ă���ꍇ�͈ȉ��̂悤�ɐݒ肵�܂��B
�@�@IP�A�h���X�Ȃǂ͊��ɍ��킹�ĕύX���ĉ������B

    �ypostgresql.conf�̏C���z
      E:\aipo\dpl003\postgresql\data\postgresql.conf

      49�s��
      #listen_addresses = 'localhost'
      ��
      listen_addresses = '*'

    �ypg_hba.conf�̏C���z
      E:\aipo\dpl003\postgresql\data\pg_hba.conf
      �ȉ��̐ݒ��ǉ�(192.168.24.0/24�̃l�b�g���[�N�̏ꍇ)

      host    all         aipo_reminder         192.168.24.0/24       md5

      �y���[���̒ǉ��ƌ����̕t�^�z
      Aipo���C���X�g�[������Ă���PostgreSQL�ɑ΂��Ĉȉ��̃R�}���h�����s���ĉ������B
      (aipo_reminder�̖��O��p�X���[�h(reminder)�ɂ��ẮA�ύX���Ē����č\���܂��񂪁A
      ���̏ꍇ�́AAipo���}�C���_�[�́uDB�ݒ�v�̃��[�UID��p�X���[�h�����킹�ďC�����ĉ������B)

      �ȉ��̃R�}���h����C�Ɏ��s����o�b�`��p�ӂ��Ă��܂��B
      postgres_setting.bat�����s����ƃR�}���h����͂����Ԃ��Ȃ��܂��B
    �y�g�����z
      �o�b�`�t�@�C�����e�L�X�g�G�f�B�^�ŊJ���A7�s�ڂ�psql���s�t�@�C���̃p�X�����ɍ��킹�Đݒ肵�A
      Aipo���C���X�g�[������Ă�����ɂĎ��s���ĉ������B

      �����s����ꍇ�͈ȉ��̎菇�ōs���ĉ������B
      Windows�̃R�}���h�v�����v�g������s����ꍇ�́A
      E:\aipo\dpl003\postgresql\bin �Ɉړ��������ƁA�ȉ��̃R�}���h�����s���ĉ������B

      psql -d org001 -U aipo_postgres

      PostgreSQL�ɐڑ��o������A�ȉ��̃R�}���h�����Ɏ��s���ĉ������B

      �yAipo4�`5�̏ꍇ�z
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

      �yAipo6,7�̏ꍇ�z
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

      \q �Őؒf���܂��B

      �Ō��Aipo�̍ċN�����s���ĉ������B

  �T.PostgreSQL�̐ݒ�ύX�ɂ���(Linux�̏ꍇ)
�@�@����������PostgreSQL�̒m������������s���ĉ������B

      Aipo�� /usr/local/aipo �ɃC���X�g�[������Ă���ꍇ�͈ȉ��̂悤�ɐݒ肵�܂��B
      IP�A�h���X�Ȃǂ͊��ɍ��킹�ĕύX���ĉ������B

      �ypostgresql.conf�̏C���z
      vi /usr/local/aipo/postgres/data/postgresql.conf

      49�s��
      #listen_addresses = 'localhost'
      ��
      listen_addresses = '*'

      �ypg_hba.conf�̏C���z
      vi /usr/local/aipo/postgres/data/pg_hba.conf
      �ȉ��̐ݒ��ǉ�(192.168.24.0/24�̃l�b�g���[�N�̏ꍇ)

      host    all         aipo_reminder         192.168.24.0/24       md5

      �y���[���̒ǉ��ƌ����̕t�^�z
      Aipo���C���X�g�[������Ă���PostgreSQL�ɑ΂��Ĉȉ��̃R�}���h�����s���ĉ������B
      (aipo_reminder�̖��O��p�X���[�h(reminder)�ɂ��ẮA�ύX���Ē����č\���܂��񂪁A
      ���̏ꍇ�́AAipo���}�C���_�[�́uDB�ݒ�v�̃��[�UID��p�X���[�h�����킹�ďC�����ĉ������B)

      Aipo���N������Ă��Ȃ��ꍇ�͈ȉ��̃R�}���h�ŋN�������ĉ������B
      /usr/local/aipo/bin/startup.sh

      �ȉ��̃R�}���h��aipo_postgres���[�U�ɂȂ�܂��B
      su - aipo_postgres

      PostgreSQL�ɐڑ����܂��B
      psql -d org001 -U aipo_postgres

      PostgreSQL�ɐڑ��o������A�ȉ��̃R�}���h�����Ɏ��s���ĉ������B

      �yAipo4�`5�̏ꍇ�z
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

      �yAipo6�̏ꍇ�z
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

      \q �Őؒf���܂��B

      �Ō��Aipo�̍ċN�����s���ĉ������B


#AlertWindow.dll�ɂ���
AlertWindow.dll��Youryella���񂪍쐬�������̂�Ǝ��ɃJ�X�^�}�C�Y���Ă��܂��B  
Youryella����Ɏg�p�̋��𒸂��Ă��܂��B  
�{�ƃT�C�g  
http://youryella.wankuma.com/Library/ClassLibrary/AlertWindow.aspx  

