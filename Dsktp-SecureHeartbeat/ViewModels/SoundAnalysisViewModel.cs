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

        private ICommand _plotGraphCommand;

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
            //PlotModelFrequency = new PlotModel();
            PlotModelFiltered = new PlotModel();
            this.Items = new ObservableCollection<SoundAnalysisModel>();
            CreateModel();
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

        private void CreateModel()
        {

            var PlotModelFrequency = new PlotModel();
            PlotModelFrequency.Subtitle = "Frequency Spectrum";
            PlotModelFrequency.SubtitleFont = "Segoe UI";
            PlotModelFrequency.SubtitleFontSize = 22;

            var timeAxis = new LinearAxis(AxisPosition.Bottom, 0, 5) 
            { 
                MajorGridlineStyle = LineStyle.Solid, 
                MinorGridlineStyle = LineStyle.Dash, 
                MajorTickSize = 0.1, 
                Unit="Secs",
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 20
            };
            PlotModelFrequency.Axes.Add(timeAxis);

            var ferquencyAxis = new LinearAxis(AxisPosition.Left, 0, 8, "Frequency")
            {
                MajorGridlineStyle = LineStyle.Solid, 
                MinorGridlineStyle = LineStyle.Dash, 
                Unit = "kHz", 
                MajorTickSize = 0.1,
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 8
            };
            PlotModelFrequency.Axes.Add(ferquencyAxis);

            var magnitueAxis = new LinearColorAxis 
            {
                Position = AxisPosition.Right, 
                Minimum = 0, 
                Unit = "Dbs", 
                Title = "Magnitude",
                AbsoluteMinimum = 0
            };
            PlotModelFrequency.Axes.Add(magnitueAxis);

            PlotModelFiltered.Subtitle = "Noise Filter Applied";
            PlotModelFiltered.SubtitleFont = "Segoe UI";
            PlotModelFiltered.SubtitleFontSize = 22;

            var timeAxisF = new TimeSpanAxis(AxisPosition.Bottom, 0, 5, "Time", "s")
            {
                MajorGridlineStyle = LineStyle.Solid, 
                MinorGridlineStyle = LineStyle.Dash, 
                MajorTickSize = 0.1, 
                Unit="Secs",
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

            var x = new Random();

            //var frequencyGraph = new HeatMapSeries {Data = new double[833, 256]};
            //for (int i = 0; i < 833; i++)
            //{
            //    for (int j = 0; j < 256; j++)
            //    {
            //        frequencyGraph.Data[i, j] = x.NextDouble()*100;
            //    }
            //}

            var heatMapSeries1 = new HeatMapSeries();
            heatMapSeries1.Data = new Double[2, 3];
            heatMapSeries1.Data[0, 0] = 0;
            heatMapSeries1.Data[0, 1] = 0.2;
            heatMapSeries1.Data[0, 2] = 0.4;
            heatMapSeries1.Data[1, 0] = 0.1;
            heatMapSeries1.Data[1, 1] = 0.3;
            heatMapSeries1.Data[1, 2] = 0.2;
            PlotModelFrequency.Series.Add(heatMapSeries1);
            NotifyPropertyChanged("PlotModelFrequency");
            NotifyPropertyChanged("PlotModelFiltered");
            NotifyPropertyChanged("PlotGraphCommand");
            Console.WriteLine("Successfully executed the command");
        }

        public void PlotFFT()
        {
            var frequencyGraph = new HeatMapSeries();

            frequencyGraph.Interpolate = false;

            frequencyGraph.X0 = 0.1;
            frequencyGraph.X1 = 7.9;
            frequencyGraph.Y0 = 0.1;
            frequencyGraph.Y1 = 2.9;

            //frequencyGraph.Data = new Double[SaModel.SpectrumResults.GetLength(0), SaModel.SpectrumResults.GetLength(1)];

            //for (int i = 0; i <SaModel.SpectrumResults.GetLength(0); i++)
            //{
            //    for (int j = 0; j < SaModel.SpectrumResults.GetLength(1); j++)
            //    {
            //        frequencyGraph.Data[i, j] = SaModel.SpectrumResults[i, j];
            //    }
            //}

            //var newPlotModel = new PlotModel();
            var heatMapSeries1 = new HeatMapSeries();
            heatMapSeries1.Data = new Double[2, 3];
            heatMapSeries1.Data[0, 0] = 0;
            heatMapSeries1.Data[0, 1] = 0.2;
            heatMapSeries1.Data[0, 2] = 0.4;
            heatMapSeries1.Data[1, 0] = 0.1;
            heatMapSeries1.Data[1, 1] = 0.3;
            heatMapSeries1.Data[1, 2] = 0.2;
            PlotModelFrequency.Series.Add(heatMapSeries1);
            //PlotModelFrequency.Series.Add(frequencyGraph);

            NotifyPropertyChanged("PlotModelFrequency");
            NotifyPropertyChanged("PlotModelFiltered");
            NotifyPropertyChanged("PlotGraphCommand");
            //PlotModelFrequency.Render(null, 0, 0);
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
