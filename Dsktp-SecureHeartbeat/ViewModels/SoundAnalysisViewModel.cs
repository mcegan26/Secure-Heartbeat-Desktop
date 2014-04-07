using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.Commands;
using Dsktp_SecureHeartbeat.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace Dsktp_SecureHeartbeat.ViewModels
{
    public class SoundAnalysisViewModel : INotifyPropertyChanged
    {
        string fsGraphTitle = "Frequency Spectrum";
        string fsGraphSubtitle = "Graph Displaying Sound File In The Frequency Spectrum";

        string filGraphTitle = "Noise Filtered Spectrum";
        string filGraphSubtitle = "Graph Displaying Noise Filtered Sound File";

        private SoundAnalysisModel saModel;
        public SoundAnalysisModel SaModel
        {
            get { return saModel; }
            set { saModel = value; }
        }

        private PlotModel _plotModel;
        public PlotModel PlotModelFrequency
        {
            get
            {
                if (_plotModel == null)
                {
                    _plotModel = new PlotModel();
                }
                return _plotModel;
            }
            set
            {
                if (!Equals(value, _plotModel))
                {
                    _plotModel = value;
                    NotifyPropertyChanged("PlotModelFrequency");
                    Console.WriteLine("Successfully changed the plotmodel");
                }
            }
        }

        private PlotModel _plotModelFiltered;
        public PlotModel PlotModelFiltered
        {
            get
            {
                if (_plotModelFiltered == null)
                {
                    _plotModelFiltered = new PlotModel();
                }
                return _plotModelFiltered;
            }
            set
            {
                if (value != _plotModelFiltered)
                {
                    _plotModelFiltered = value;
                    NotifyPropertyChanged("PlotModelFiltered");
                }
            }
        }


        private ICommand _plotGraphCommand;
        public ICommand PlotGraghCommand
        {
            get
            {
                if (_plotGraphCommand == null)
                {
                    _plotGraphCommand = new PlotGraghCommand(this);
                    //_plotGraphCommand.RegisterViewModel(this);
                }
                return _plotGraphCommand;
            }
            set
            {
                if (value != _plotGraphCommand)
                {
                    _plotGraphCommand = value;
                    NotifyPropertyChanged("PlotGraphCommand");
                }
            }
        }


        public SoundAnalysisViewModel()
        {
            SaModel = new SoundAnalysisModel();
            this.Items = new ObservableCollection<SoundAnalysisModel>();
            PlotModelFrequency = CreateFrequencyPlot(fsGraphTitle, fsGraphSubtitle);
            PlotModelFiltered = CreateFrequencyPlot(filGraphTitle, filGraphSubtitle);
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<SoundAnalysisModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        //public string SampleProperty
        //{
        //    get
        //    {
        //        return _sampleProperty;
        //    }
        //    set
        //    {
        //        if (value != _sampleProperty)
        //        {
        //            _sampleProperty = value;
        //            NotifyPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        ///// <summary>
        ///// Sample property that returns a localized string
        ///// </summary>
        //public string LocalizedSampleProperty
        //{
        //    get
        //    {
        //        return AppResources.SampleProperty;
        //    }
        //}

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            this.Items.Add(SaModel);
            this.IsDataLoaded = true;
        }


        public void PlotFft()
        {
            var frequencyGraph = new HeatMapSeries();
            frequencyGraph.Interpolate = true;

            frequencyGraph.Data = new Double[SaModel.SpectrumResults.GetLength(0), SaModel.SpectrumResults.GetLength(1)];

            var soundTimeLength = SaModel.SpectrumResults.GetLength(0) * 0.005;
            var fsGraphTitle = "Frequency Spectrum";
            var fsGraphSubtitle = "Graph Displaying Sound File In The Frequency Spectrum";
            var newPlot = CreateFrequencyPlot(fsGraphTitle, fsGraphSubtitle, soundTimeLength);

            frequencyGraph.X0 = 0;
            frequencyGraph.X1 = soundTimeLength;
            frequencyGraph.Y0 = 0;
            frequencyGraph.Y1 = 8000;

            for (var i = 0; i < SaModel.SpectrumResults.GetLength(0); i++)
            {
                for (var j = 0; j < SaModel.SpectrumResults.GetLength(1); j++)
                {
                    frequencyGraph.Data[i, j] = SaModel.SpectrumResults[i, j];
                }
            }
            newPlot.Series.Add(frequencyGraph);

            // Atomically swap the entire reference to the new plot model
            PlotModelFrequency = newPlot;
            NotifyPropertyChanged("PlotModelFrequency");

        }

        private PlotModel CreateFrequencyPlot(string title, string subtitle, double soundTimeLength)
        {
            var newPlot = new PlotModel { Subtitle = subtitle, Title = title, TitleFont = "Segoe UI", TitleFontSize = 22 };
            var magnitudeAxis = new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Minimum = 0,
                Unit = "Dbs",
                Title = "Magnitude",
                AbsoluteMinimum = 0
            };
            newPlot.Axes.Add(magnitudeAxis);
            var timeAxis = new LinearAxis(AxisPosition.Bottom, "Time")
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                Unit = "Secs",
                AbsoluteMinimum = 0,
                AbsoluteMaximum = soundTimeLength
            };
            newPlot.Axes.Add(timeAxis);
            var ferquencyAxis = new LinearAxis(AxisPosition.Left, "Frequency")
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                Unit = "kHz",
                Minimum = 0,
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 8000
            };
            newPlot.Axes.Add(ferquencyAxis);

            return newPlot;
        }

        private PlotModel CreateFrequencyPlot(string title, string subtitle)
        {
            var newPlot = new PlotModel { Subtitle = subtitle, Title = title, TitleFont = "Segoe UI", TitleFontSize = 22};
            var magnitudeAxis = new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Minimum = 0,
                Unit = "Dbs", 
                Title = "Magnitude",
                AbsoluteMinimum = 0
            };
            newPlot.Axes.Add(magnitudeAxis);

            var timeAxis = new LinearAxis(AxisPosition.Bottom, "Time")
            {
                MajorGridlineStyle = LineStyle.Solid, 
                MinorGridlineStyle = LineStyle.Dash, 
                Unit="Secs",
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 20
            };
            newPlot.Axes.Add(timeAxis);

            var ferquencyAxis = new LinearAxis(AxisPosition.Left, "Frequency")
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                Unit = "kHz",
                Minimum = 0,
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 8000
            };
            newPlot.Axes.Add(ferquencyAxis);

            return newPlot;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
