using System;
using System.Windows.Input;
using Dsktp_SecureHeartbeat.ViewModels;


namespace Dsktp_SecureHeartbeat.Commands
{
    public class PlotGraghCommand : ICommand
    {

        private FFTFunction fftHelper;
        private SoundAnalysisViewModel _soundAnalysisvm;


        /// <summary>
        /// The event used to trigger a refrehs on the UI when the status of the CanExecute command has changed
        /// Note - Currently this is never called by us, ie the state never changes
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };

        public PlotGraghCommand(SoundAnalysisViewModel savm)
        {
            //SoundAnalysisModel soundWaveToPlot = currentsoundWaveSpectrum;
            _soundAnalysisvm = savm;
            fftHelper = new FFTFunction();
        }

        //public void RegisterViewModel(SoundAnalysisViewModel savm)
        //{
        //    _soundAnalysisvm = savm;
        //}

        public bool CanExecute(object parameter)
        {
            // Return true as the button can always be executed
            return true;
        }

        public void Execute(object parameter)
        {
            if (_soundAnalysisvm.SaModel.SpectrumResults == null)
            {
                _soundAnalysisvm.SaModel.SpectrumResults =
                    fftHelper.PerformFftFunction(_soundAnalysisvm.SaModel.SoundFileName);

                _soundAnalysisvm.SaModel.FilteredSpectrumResults =
                    _soundAnalysisvm.SaModel.SpectrumResults.Clone() as float[,];

                fftHelper.PerformWienerFilter(_soundAnalysisvm.SaModel.FilteredSpectrumResults);
            }
     
            _soundAnalysisvm.PlotFft();
        }

    }
}
