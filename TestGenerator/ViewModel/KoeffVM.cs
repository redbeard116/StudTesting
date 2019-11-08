using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;
using WpfCharts;

namespace TestGenerator.ViewModel
{
    public class KoeffVM
    {
        private readonly IRepo _repo;
        public double A
        {
            get; set;
        }
        public double B
        {
            get; set;
        }
        public double C
        {
            get; set;
        }
        public double D
        {
            get; set;
        }
        public double F
        {
            get; set;
        }
        public string[] Axes { get; set; }
        public ObservableCollection<ChartLine> Lines { get; set; }

        public KoeffVM(StudentProgress progress,
                       IRepo repo)
        {
            _repo = repo;
            GetKoeff(progress);
            Paint();
        }

        private void Paint()
        {
            Axes = new[] { "A", "B", "C", "D", "E"};
            Lines = new ObservableCollection<ChartLine> {new ChartLine
            {
                 LineColor = Colors.Red,
                 FillColor = Color.FromArgb(128, 255, 0, 0),
                 LineThickness = 2,
                 PointDataSource = new List<double>{A,B,C,D,F}
            }};
        }

        private void GetKoeff(StudentProgress progress)
        {
            var test = _repo.GetTests(progress.TestId);
            var studKoeffs = new List<bool>();
            var testKoeffs = new List<double>();
            foreach (var koeff in progress.Koeff.Split(','))
            {
                if (!string.IsNullOrEmpty(koeff))
                {
                    studKoeffs.Add(bool.Parse(koeff));
                }
            }
            foreach (var koeff in test.Koeff.Split(','))
            {
                if (!string.IsNullOrEmpty(koeff))
                {
                    var num = koeff.Replace('.', ',');
                    testKoeffs.Add(double.Parse(num));
                }
            }
            var list = new List<double>();
            foreach (var koeff in studKoeffs)
            {
                if (koeff)
                {
                    foreach (var koef in testKoeffs)
                    {
                        list.Add(koef);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        list.Add(0);
                    }
                }
            }
            var a = new List<double>();
            var b = new List<double>();
            var c = new List<double>();
            var d = new List<double>();
            var f = new List<double>();

            for (int i = 0; i < testKoeffs.Count; i++)
            {
                var lists = list.Skip(i).Take(5).Sum();
                for (int j = 0; j < 5; j++)
                {
                    switch (i)
                    {
                        case 0:
                            a.Add(list[i]);
                            break;
                        case 1:
                            b.Add(list[i]);
                            break;
                        case 2:
                            c.Add(list[i]);
                            break;
                        case 3:
                            d.Add(list[i]);
                            break;
                        case 4:
                            f.Add(list[i]);
                            break;
                    }
                }
            }
            A = a.Sum() / a.Count;
            B = b.Sum() / b.Count;
            C = c.Sum() / c.Count;
            D = d.Sum() / d.Count;
            F = f.Sum() / f.Count;
        }

        public RelayCommand CloseCmd => new RelayCommand(Close);

        private void Close(object obj)
        {
            if (obj is Window view)
            {
                view.Close();
            }
        }
    }
}
