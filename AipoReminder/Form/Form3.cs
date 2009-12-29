using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AipoReminder.DataSet;
using AipoReminder.Model;
using AipoReminder.Utility;
using AipoReminder.ValueObject;

namespace AipoReminder
{
    public partial class Form3 : Form
    {
        // 
        public string GroupId { set; get; }

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Focus();
            setComboBoxGroup();
            setListViewUser("0");
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder userGroup = new StringBuilder();
            foreach (ListViewItemEx item in listViewUser.Items)
            {
                if (item.Checked)
                {
                    if (userGroup.Length != 0)
                    {
                        userGroup.Append(",");
                        userGroup.Append(item.Key);
                    }
                    else
                    {
                        userGroup.Append(item.Key);
                    }
                }
            }
            GroupId = userGroup.ToString();
            this.Close();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ユーザ一覧をListViewに設定
        /// </summary>
        private void setListViewUser(string group_id)
        {
            listViewUser.Items.Clear();

            // チェックボックスにチェックを入れるユーザIDをListに入れる
            List<string> list = new List<string>();
            list.AddRange(SettingManager.GroupUserId.Split(",".ToCharArray()));

            TurbineUserDataSet userList = this.GetTurbineGroupUserData(group_id);

            foreach(TurbineUserDataSet.turbine_group_userRow user in userList.turbine_group_user)
            {
                string[] name = { user.last_name + " " + user.first_name };
                ListViewItemEx item = new ListViewItemEx(name, user.user_id);

                // チェックボックスの設定
                if (list.Contains(user.user_id))
                {
                    item.Checked = true;
                }

                listViewUser.Items.Add(item);
            }
        }

        /// <summary>
        /// グループ一覧をComboBoxに設定
        /// </summary>
        private void setComboBoxGroup()
        {
            // 初期値を設定
            ComboBoxGroupItem defaultItem = new ComboBoxGroupItem(2, "（部署全体）");
            comboBoxGroup.Items.Add(defaultItem);
            comboBoxGroup.SelectedIndex = 0;

            TurbineGroupDataSet groupList = this.GetTurbineGroupData();
            foreach (TurbineGroupDataSet.turbine_groupRow group in groupList.turbine_group)
            {
                ComboBoxGroupItem item = new ComboBoxGroupItem(group.group_id, group.group_alias_name);
                comboBoxGroup.Items.Add(item);
            }
        }

        /// <summary>
        /// ユーザ情報取得
        /// </summary>
        /// <returns></returns>
        private TurbineUserDataSet GetTurbineGroupUserData(string group_id)
        {
            TurbineUserDataSet data = new TurbineUserDataSet();

            TurbineUserDataSet.search_turbine_groupRow paramRow = data.search_turbine_group.Newsearch_turbine_groupRow();

            paramRow.group_id = group_id;
            paramRow.not_disp_user_id = SettingManager.UserId;

            data.search_turbine_group.Rows.Add(paramRow);

            UserModel m = new UserModel();
            m.Execute(m.GetTurbineGroupUserInfo, data);

            return data;
        }

        /// <summary>
        /// グループ情報取得
        /// </summary>
        /// <returns></returns>
        private TurbineGroupDataSet GetTurbineGroupData()
        {
            TurbineGroupDataSet data = new TurbineGroupDataSet();

            TurbineGroupDataSet.search_turbine_groupRow paramRow = data.search_turbine_group.Newsearch_turbine_groupRow();

            paramRow.owner_id = "1";

            data.search_turbine_group.Rows.Add(paramRow);

            GroupModel m = new GroupModel();
            m.Execute(m.GetTurbineGroupInfo, data);

            return data;
        }

        /// <summary>
        /// 部署を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxGroupItem item = (ComboBoxGroupItem)comboBoxGroup.SelectedItem;
            setListViewUser(item.Id.ToString());
        } 

    }
}
