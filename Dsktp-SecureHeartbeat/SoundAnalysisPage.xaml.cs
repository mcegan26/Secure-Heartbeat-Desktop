using System.Windows.Controls;

namespace Dsktp_SecureHeartbeat
{
    /// <summary>
    /// Interaction logic for SoundAnalysisPage.xaml
    /// </summary>
    public partial class SoundAnalysisPage : Page
    {
        public SoundAnalysisPage()
        {
            InitializeComponent();
            DataContext = App.SoundAnalysisvm;
        }
    }
}
