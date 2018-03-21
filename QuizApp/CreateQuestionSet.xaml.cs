using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Web.Script.Serialization;
using System.IO;


namespace QuizApp
{
    public partial class CreateQuestionSet: Page
    {
        private QuestionsDeck questionsDeck;
        private Question question;

        public CreateQuestionSet()
        {
            InitializeComponent();
        }

        private void AddQuestionToDeck_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
