using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brilliantech.Framwork.Utils.LogUtil;
using Brilliantech.Tsk.Client.WPFUI.Config;
using System.IO;

namespace Brilliantech.Tsk.Client.WPFUI
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            LoadDefaultSettings();
        }

        /// <summary>
        /// load default config
        /// </summary>
        private void LoadDefaultSettings()
        {
            try
            {
                RemoteServerIPTB.Text = TskBaseConfig.RemoteServerIP;
                RemoteServerPortTB.Text = TskBaseConfig.RemoteServerPort;
                DataFilePathTB.Text = TskBaseConfig.DataFilePath;
                MoveFilePathTB.Text = TskBaseConfig.MovedFilePath;
                ErrorFilePathTB.Text = TskBaseConfig.ErrorFilePath;
                for (int i = 0; i < DeleteFileAfterReadCB.Items.Count; i++)
                {
                    if ((DeleteFileAfterReadCB.Items[i] as ComboBoxItem).Tag.ToString().Equals(TskBaseConfig.DeleteFileAfterRead.ToString()))
                    {
                        DeleteFileAfterReadCB.SelectedIndex = i;
                        break;
                    }
                }
                ScanIntervalTB.Text = TskBaseConfig.ScanInterval.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                LogUtil.Logger.Error(e.Message);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(DataFilePathTB.Text))
            {
                TskBaseConfig.RemoteServerIP = RemoteServerIPTB.Text;
                TskBaseConfig.RemoteServerPort = RemoteServerPortTB.Text;

                TskBaseConfig.DataFilePath = DataFilePathTB.Text;
                TskBaseConfig.MovedFilePath = MoveFilePathTB.Text;
                TskBaseConfig.ErrorFilePath = ErrorFilePathTB.Text;
                TskBaseConfig.DeleteFileAfterRead = bool.Parse((DeleteFileAfterReadCB.SelectedItem as ComboBoxItem).Tag.ToString());
                int scanInterval = 2;
                if (int.TryParse(ScanIntervalTB.Text, out scanInterval)) {
                    TskBaseConfig.ScanInterval = scanInterval;
                }
                TskBaseConfig.Save();
                MessageBox.Show("TSK配置保存成功！", "保存成功", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else {
                MessageBox.Show("TSK数据文件路径不存在！请重新填写","保存失败",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}