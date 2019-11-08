using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;
using TestGenerator.View;

namespace TestGenerator.ViewModel
{
    public class StudProgVM : INotifyPropertyChanged
    {
        public StudProgVM(IRepo repo, int role)
        {
            _repo = repo;
            GetStudProg();
            if (role == 1)
            {
                DetailsVisible = Visibility.Visible;
            }
        }

        private readonly IRepo _repo;
        private Progress _selectProg;

        public Progress SelectProg
        {
            get => _selectProg;
            set
            {
                _selectProg = value;
                OnPropertyChanged("SelectProg");
            }
        }

        public ObservableCollection<Progress> Progress { get; set; } = new ObservableCollection<Progress>();
        public RelayCommand GraficCmd => new RelayCommand(Grafics);

        private void Grafics(object obj)
        {
            Grafic view = new Grafic();
            GraficVM graf = new GraficVM(_repo);
            view.DataContext = graf;
            view.ShowDialog();
        }

        private void GetStudProg()
        {
            var prog = _repo.GetStudProg();
            foreach (var pr in prog)
            {
                Progress.Add(pr);
            }
        }

        private Visibility detailsVisible = Visibility.Collapsed;
        public Visibility DetailsVisible
        {
            get => detailsVisible;
            set
            {
                detailsVisible = value;
                OnPropertyChanged("DetailsVisible");
            }
        }

        public RelayCommand DetailsCmd => new RelayCommand(Details,CanDetails);

        private bool CanDetails(object obj)
        {
            return SelectProg != null ? true : false;
        }

        private void Details(object obj)
        {
            var prog = _repo.GetStudProg(SelectProg.StudId, SelectProg.TestId);
            var details = new DetailsVM(SelectProg.Student,prog,_repo);
            var view = new DetaisV() { DataContext = details };
            view.ShowDialog();
        }

        public RelayCommand KoeffCmd => new RelayCommand(Koeff,CanDetails);

        private void Koeff(object obj)
        {
            var prog = _repo.GetStudProg(SelectProg.StudId, SelectProg.TestId);
            var koeff = new KoeffVM(prog,_repo);
            var view = new KoeffV() { DataContext = koeff };
            view.ShowDialog();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
