using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Model;
using Calculator.Services;

namespace Calculator.ViewModel
{
    internal class CalculatorViewModel : BaseVM
    {
        private CalculatorMemory model;
        private bool is_any_input = false;
        public CalculatorMemory Model
        {
            set { Set(ref model, value); }
            get { return model; }
        }

        public string Last
        {
            set 
            { 
                model.Last = value;
                NotifyPropertyChanged(nameof(Last));
            }
            get { return model.Last; }
        }

        public string Current 
        {
            get 
            {
                return model.Current;
            }
            set
            {
                model.Current = value;
                NotifyPropertyChanged(nameof(Current));
            }
        }

        public ICommand InputNumCommand { get; }
        public ICommand RemoveDigitCommand { get; }
        public ICommand CalcCommand { get; }
        public ICommand CleanCommand { get; }
        public ICommand CECommand { get; }
        public CalculatorViewModel()
        {
            Model = new CalculatorMemory();
            InputNumCommand = new DelegateCommand(InputNumber);
            RemoveDigitCommand = new DelegateCommand(RemoveDigit);
            CalcCommand = new DelegateCommand(Calculate);
            CleanCommand = new DelegateCommand(Clean);
            CECommand = new DelegateCommand(CleanEntry);

        }

        private void Clean(object parameter)
        {
            Model.Clean();
            NotifyPropertyChanged(nameof(Last));
            NotifyPropertyChanged(nameof(Current));
        }

        private void CleanEntry(object parameter)
        {
            Model.CleanEntry();
            NotifyPropertyChanged(nameof(Current));
        }

        private void InputNumber(object parameter)
        {
            if (Model.CleanFlag)
            {
                Model.Clean();
                Model.CleanFlag = false;
                NotifyPropertyChanged(Last);
            }
            Current = InputService.AddSymToNum(Current, (string)parameter, Model.IsNeedToCleanInput);
            is_any_input = true;
            Model.IsNeedToCleanInput = false;
        }

        private void RemoveDigit(object parameter)
        {
            if (Model.CleanFlag)
            {
                Model.Clean();
                Model.CleanFlag = false;
                NotifyPropertyChanged(Last);
            }
            Current = InputService.RemoveDigit(Current);
        }

        private void Calculate(object parameter) 
        {
            if (!Model.CleanFlag)
            {
                Model.ChangeMemory((string)parameter, is_any_input);
                NotifyPropertyChanged(nameof(Current));
                NotifyPropertyChanged(nameof(Last));
                is_any_input = false;
            }
        }

    }
}
