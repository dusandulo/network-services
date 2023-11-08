using NetworkService.AdditionalElements;
using NetworkService.AdditionalFiles;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class MeasurementGraphViewModel : BindableBase
    {
        //collections
        public ObservableCollection<Graph> Graphs { get; set; }
        public List<string> ComboBoxItems { get; set; } = AdditionalFiles.ComboBoxItems.comboBoxTypes.Keys.ToList();

        //entities
        public List<Road> Entities { get; set; } = new List<Road>();

        private Road selectedEntity;
        public MyICommand ShowEntitiesCommand { get; set; }
        private string errorMessage;

        public MeasurementGraphViewModel()
        {
            ShowEntitiesCommand = new MyICommand(OnShow);

            Graphs = new ObservableCollection<Graph>();
            for (int i = 0; i <= 4; i++)
            {
                Graph x = new Graph();
                Graphs.Add(x);
            }

            if (Entities.Count > 0)
            {
                SelectedEntity = Entities.First();
                SetValuesToGraphs(LoadLastFiveUpdates());
            }
        }

        //selected item
        public Road SelectedEntity
        {
            get
            {
                return selectedEntity;
            }

            set
            {
                selectedEntity = value;
                OnPropertyChanged("SelectedEntity");
            }

        }
        //loading last five from log.txt
        private List<Graph> LoadLastFiveUpdates()
        {
            if (!File.Exists("log.txt"))
            {
                ErrorMessage = "Log file does not exist";
                return null;
            }

            string[] lines = File.ReadAllLines("log.txt");

            List<Graph> lastFiveUpdates = new List<Graph>();

            for (int i = lines.Count() - 1; i >= 0; i--)
            {
                string line = lines[i];

                string date = line.Substring(0, line.IndexOf(" "));
                line = line.Substring(line.IndexOf(" ") + 1);
                string time = line.Substring(0, line.IndexOf(" ") - 1);
                line = line.Substring(line.IndexOf(" ") + 1);
                int id = int.Parse(line.Substring(0, line.IndexOf(',')));
                string val = line.Substring(line.IndexOf(',') + 2);

                if ((id == SelectedEntity.Id) && (lastFiveUpdates.Count < 5))
                {
                    lastFiveUpdates.Add(new Graph(id, int.Parse(val), date, time));
                }
            }

            if(lastFiveUpdates.Count > 0)
            {
                return lastFiveUpdates;
            }
            else
            {
                return null;
            }

        }
        //set graph to default values
        private void SetDefaultGraphs()
        {
            for (int i = 0; i <= 4; i++)
            {
                Graphs[i].CmId = 0;
                Graphs[i].CmValue = 0;
                Graphs[i].CmDate = "Date";
                Graphs[i].CmTime = "Time";
            }
        }
        //set values to graph
        private void SetValuesToGraphs(List<Graph> bars)
        {
            if (bars != null)
            {
                for (int i = 0; i < bars.Count; i++)
                {
                    Graphs[i].CmId = bars[bars.Count - i - 1].CmId;
                    Graphs[i].CmValue = bars[bars.Count - i - 1].CmValue;
                    Graphs[i].CmDate = bars[bars.Count - i - 1].CmDate;
                    Graphs[i].CmTime = bars[bars.Count - i - 1].CmTime;
                }
            }
            else
            {
                SetDefaultGraphs();
            }
        }
        //error
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        //combobox item change
        public void OnShow()
        {
            if (SelectedEntity != null)
            {
                ErrorMessage = String.Empty;

                List<Graph> bars = LoadLastFiveUpdates();

                SetValuesToGraphs(bars);
            }
            else
            {
                errorMessage = "Choose entity";
            }
        }


    }
}
