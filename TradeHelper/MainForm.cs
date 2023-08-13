using System.Windows.Forms;
using TradeHelper.Misc;

namespace TradeHelper
{
    public partial class MainForm : Form
    {
        private bool _isWork;

        public MainForm()
        {
            InitializeComponent();
        }

        private void CheckConnectionBtn_Click(object sender, System.EventArgs e)
        {
            var isConnOk = TelegramWorker.CheckConnection();

            ConnectionIndicator.Text = isConnOk ? Resources.FormStrings.ConnectionIsOk : Resources.FormStrings.ConnectionFailed;
        }

        private void StartWorkingBtn_Click(object sender, System.EventArgs e)
        {
            if (!_isWork)
            {
                if (Settings.TelegramToken == null || Settings.LogfilePath == null)
                {
                    MessageBox.Show(Resources.FormStrings.AppConfigError, "HOW BORING AND SMOL", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                _isWork = true;

                TelegramWorker.StartWorking(Settings.TelegramToken);
                LogParser.StartForceUpdateFile();

                StartWorkingBtn.Text = Resources.FormStrings.StopWorking;
                ConnectionIndicator.Text = Resources.FormStrings.ConnectionIsOk;
                CheckConnectionBtn.Enabled = false;
            }
            else
            {
                _isWork = false;

                TelegramWorker.StopWorking();
                LogParser.StopForceUpdateFile();

                StartWorkingBtn.Text = Resources.FormStrings.StartWorking;
                ConnectionIndicator.Text = Resources.FormStrings.PressAnyButton;
                CheckConnectionBtn.Enabled = true;
            }
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.BalloonTipTitle = Text;
                notifyIcon1.BalloonTipText = Resources.FormStrings.Minimized;

                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;

            notifyIcon1.Visible = false;
        }

        private void MainForm_OnClose(object sender, System.EventArgs e)
        {
            TelegramWorker.StopWorking();
            LogParser.StopForceUpdateFile();
        }
    }
}
