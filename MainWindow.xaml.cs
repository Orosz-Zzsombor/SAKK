using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSakk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Jatszma> jatszmak;
        public MainWindow()
        {
            InitializeComponent();
            jatszmak = File.ReadAllLines("jatszmak.txt").Select(sor => new Jatszma(sor)).ToList();

            MessageBox.Show($"A huszárok ennyi mezőt haladtak: {jatszmak.Sum(jatszma => jatszma.HuszarokLepesszama) * 4}");
            MessageBox.Show($"A futók ennyiszer léptek: {jatszmak.Sum(jatszma => jatszma.TisztLepesszama('F'))}");
            MessageBox.Show($"Tobb mint 20 babu {jatszmak.Count(jatszma => jatszma.tobbMint)}");
            MessageBox.Show($"Vezerlepes {jatszmak.Sum(jatszma => jatszma.Vezerlepes())}");

        }
    }
}