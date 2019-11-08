using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using TestGenerator.Interface;
using TestGenerator.Model;

namespace TestGenerator.ViewModel
{
    public class GraficVM
    {
        public GraficVM(IRepo repo)
        {
            _repo = repo;
            var prog = _repo.GetStudProg();
            Progress = prog;
            List<Progress> labels = new List<Model.Progress>();
            foreach (var p in prog)
            {
                labels.Add(p);
            }
            Labels = new string[labels.Count];
            for (int i = 0; i < labels.Count; i++)
            {
                Labels[i] = labels[i].Student;
            }
            foreach (var label in labels)
            {
                SeriesCollection.Add(new ColumnSeries { Values = new LiveCharts.ChartValues<ObservableValue>
                                                                            { new ObservableValue(label.Mark)},
                Title = label.Student,});
            }
            Formatter = value => value + "";
        }
        private readonly IRepo _repo;
        private List<Progress> Progress = new List<Progress>();

        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
