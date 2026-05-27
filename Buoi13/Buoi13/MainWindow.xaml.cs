using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Thêm 2 thư viện này để làm việc với Crystal Reports
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Buoi13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                string reportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyReport.rpt");
                report.Load(reportPath);
                ConnectionInfo connectionInfo = new ConnectionInfo();
                connectionInfo.ServerName = "localhost";            
                connectionInfo.DatabaseName = "QuanLySinhVien";      
                connectionInfo.IntegratedSecurity = true;           
                Tables tables = report.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ViewerCore.ReportSource = report;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải báo cáo: " + ex.Message, "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}