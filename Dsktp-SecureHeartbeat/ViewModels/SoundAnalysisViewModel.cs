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
            PlotModelFrequency = new PlotModel();
            PlotModelFiltered = new PlotModel();
            CreateModelFrequency();
            CreateModelFiltered();
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

        private void CreateModelFrequency()
        {

            //PlotModelFrequency.Subtitle = "Frequency Spectrum";
            //PlotModelFrequency.SubtitleFont = "Segoe UI";
            //PlotModelFrequency.SubtitleFontSize = 22;

            //var timeAxis = new LinearAxis(AxisPosition.Bottom, 0, 5) 
            //{ 
            //    MajorGridlineStyle = LineStyle.Solid, 
            //    MinorGridlineStyle = LineStyle.Dash, 
            //    MajorTickSize = 0.1, 
            //    Unit="Secs",
            //    AbsoluteMinimum = 0,
            //    AbsoluteMaximum = 20
            //};
            //PlotModelFrequency.Axes.Add(timeAxis);

            //var ferquencyAxis = new LinearAxis(AxisPosition.Left, 0, 8, "Frequency")
            //{
            //    MajorGridlineStyle = LineStyle.Solid, 
            //    MinorGridlineStyle = LineStyle.Dash, 
            //    Unit = "kHz", 
            //    MajorTickSize = 0.1,
            //    AbsoluteMinimum = 0,
            //    AbsoluteMaximum = 8
            //};
            //PlotModelFrequency.Axes.Add(ferquencyAxis);

            //var magnitueAxis = new LinearColorAxis 
            //{
            //    Position = AxisPosition.Right, 
            //    Minimum = 0, 
            //    Unit = "Dbs", 
            //    Title = "Magnitude",
            //    AbsoluteMinimum = 0
            //};
            //PlotModelFrequency.Axes.Add(magnitueAxis); 
            var fsGraphTitle = "Frequency Spectrum";
            var fsGraphSubtitle = "Graph Displaying Sound File In The Frequency Spectrum";
            PlotModelFrequency = CreateFrequencyPlot(fsGraphTitle, fsGraphSubtitle);

        }

        public void CreateModelFiltered()
        {
            PlotModelFiltered.Subtitle = "Noise Filter Applied";
            PlotModelFiltered.SubtitleFont = "Segoe UI";
            PlotModelFiltered.SubtitleFontSize = 22;

            var timeAxisF = new LinearAxis(AxisPosition.Bottom, 0, 5)
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                MajorTickSize = 0.1,
                Unit = "Secs",
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 20
            };
            PlotModelFiltered.Axes.Add(timeAxisF);

            var frequencyAxisF = new LinearAxis(AxisPosition.Left, 0, 8, "Frequency")
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                ShowMinorTicks = true,
                Unit = "kHz",
                MajorTickSize = 0.1,
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 8
            };
            PlotModelFiltered.Axes.Add(frequencyAxisF);

            var magnitueAxisF = new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Minimum = 0,
                Unit = "Dbs",
                Title = "Magnitude",
                AbsoluteMinimum = 0
            };
            PlotModelFiltered.Axes.Add(magnitueAxisF);

        }

        public void PlotFFT()
        {
            var fsGraphTitle = "Frequency Spectrum";
            var fsGraphSubtitle = "Graph Displaying Sound File In The Frequency Spectrum";
            var newPlot = CreateFrequencyPlot(fsGraphTitle, fsGraphSubtitle);

            /*var heatMapSeries1 = new HeatMapSeries();
            heatMapSeries1.X0 = 0.5;
            heatMapSeries1.X1 = 1.5;
            heatMapSeries1.Y0 = 0.5;
            heatMapSeries1.Y1 = 2.5;
            heatMapSeries1.LabelFontSize = 0.2;
            heatMapSeries1.Data = new Double[2, 3];
            heatMapSeries1.Data[0, 0] = 0;
            heatMapSeries1.Data[0, 1] = 0.2;
            heatMapSeries1.Data[0, 2] = 0.4;
            heatMapSeries1.Data[1, 0] = 0.1;
            heatMapSeries1.Data[1, 1] = 0.3;
            heatMapSeries1.Data[1, 2] = 0.2;
            newPlot.Series.Add(heatMapSeries1);*/

            var frequencyGraph = new HeatMapSeries();

            //frequencyGraph.Interpolate = false;

            frequencyGraph.Data = new Double[SaModel.SpectrumResults.GetLength(0), SaModel.SpectrumResults.GetLength(1)];

            frequencyGraph.X0 = 0;
            frequencyGraph.X1 = SaModel.SpectrumResults.GetLength(0) * 0.005;
            frequencyGraph.Y0 = 0;
            frequencyGraph.Y1 = 8000;

            for (int i = 0; i < SaModel.SpectrumResults.GetLength(0); i++)
            {
                for (int j = 0; j < SaModel.SpectrumResults.GetLength(1); j++)
                {
                    frequencyGraph.Data[i, j] = SaModel.SpectrumResults[i, j];
                }
            }
            newPlot.Series.Add(frequencyGraph);

            // Atomically swap the entire reference to the new plot model
            PlotModelFrequency = newPlot;

            NotifyPropertyChanged("PlotModelFrequency");

        }

        public PlotModel CreateFrequencyPlot(String title, String subtitle)
        {
            var newPlot = new PlotModel { Subtitle = subtitle, Title = title, TitleFont = "Segoe UI", TitleFontSize = 22};
            var magnitudeAxis = new LinearColorAxis
            {
                HighColor = OxyColors.Gray,
                LowColor = OxyColors.Black,
                Position = AxisPosition.Right,
                Minimum = 0,
                Unit = "Dbs", 
                Title = "Magnitude",
                AbsoluteMinimum = 0
            };
            newPlot.Axes.Add(magnitudeAxis);
            var timeAxis = new LinearAxis(AxisPosition.Bottom, "Time")
            {
                IsZoomEnabled = false,
                MajorGridlineStyle = LineStyle.Solid, 
                MinorGridlineStyle = LineStyle.Dash, 
                Unit="Secs",
                //AbsoluteMinimum = 0,
                //AbsoluteMaximum = 20
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
                //AbsoluteMinimum = 0,
                //AbsoluteMaximum = 8
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

        //public event PropertyChangedEventHandler PropertyChanged;

        ////[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
