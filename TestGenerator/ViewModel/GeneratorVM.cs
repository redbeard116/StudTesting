using System.ComponentModel;
using TestGenerator.Command;
using TestGenerator.Interface;

namespace TestGenerator.ViewModel
{
    public class GeneratorVM : INotifyPropertyChanged
    {
        public GeneratorVM(IRepo repo)
        {
            _repo = repo;
        }

        private readonly IRepo _repo;

        public string Question { get; set; }
        public string Answer { get; set; }

        public RelayCommand CreateCmd => new RelayCommand(Create);

        private void Create(object obj)
        {
            _repo.GenerateTest(Question,Answer);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
