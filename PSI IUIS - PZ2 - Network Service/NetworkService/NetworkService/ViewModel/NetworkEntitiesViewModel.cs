using MindFusion.UI.Wpf;
using NetworkService.AdditionalElements;
using NetworkService.AdditionalFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public KeyboardLayout kbLayout { get; set; }

        //messages
        public const string ID_NEGATIVE = "ID negative";
        public const string ID_NOT_INT = "ID not integer";
        public const string ID_REQ = "ID required";
        public const string FIELDS_EMPTY = "Fields empty";

        //undo stack
        private Stack<List<Road>> undoStack = new Stack<List<Road>>();
        private Stack<List<Road>> redoStack = new Stack<List<Road>>();
        private Stack<List<Road>> historyStack = new Stack<List<Road>>();

        public List<string> ComboBoxItems { get; set; } = AdditionalFiles.ComboBoxItems.comboBoxTypes.Keys.ToList();

        //collections
        public ObservableCollection<Road> EntitiesToShow { get; set; }
        public ObservableCollection<Road> Entities { get; set; }
        public ObservableCollection<Road> FilteredEntities { get; set; }

        //commands
        public MyICommand UndoCommand { get; set; }
        public MyICommand AddEntityCommand { get; set; }
        public MyICommand DeleteEntityCommand { get; set; }
        public MyICommand FilterEntityCommand { get; set; }
        public MyICommand ResetFilterCommand { get; set; }
        public MyICommand<TextBox> KeyBoardFocusedCommand { get; set; }
        public MyICommand CloseKeyboard { get; private set; }
       

        private Road currentEntity = new Road();
        private RoadType currentEntityType = new RoadType();

        private Road selectedEntity; //selected

        private string selectedEntityTypeToFilter;
        private bool isGreaterThanRadioButtonSelected;
        private bool isLessThanRadioButtonSelected;
        private bool isEqualsThanRadioButtonSelected;
        private string selectedIdMarginToFilterText;
        private string filterErrorMessage;

        //counters
        private int iACount = 0;
        private int iBCount = 0;

        public NetworkEntitiesViewModel()
        {
            Entities = new ObservableCollection<Road>();
            EntitiesToShow = Entities;


            AddEntityCommand = new MyICommand(OnAdd);
            DeleteEntityCommand = new MyICommand(OnDelete, CanDelete);
            FilterEntityCommand = new MyICommand(OnFilter);
            ResetFilterCommand = new MyICommand(OnResetFilter);
            UndoCommand = new MyICommand(OnUndo);
            KeyBoardFocusedCommand = new MyICommand<TextBox>(OnOpenKeyBoard);
            CloseKeyboard = new MyICommand(OnCloseKeyboard);
        }
        //keyboard
        private Visibility keyboardVisible = Visibility.Collapsed;
        public Visibility KeyboardVisible
        {
            get => keyboardVisible;
            set
            {
                if (value != keyboardVisible)
                {
                    keyboardVisible = value;
                    OnPropertyChanged("KeyboardVisible");
                }
            }
        }
        private Visibility keyboardNotVisible = Visibility.Visible;
        public Visibility KeyboardNotVisible
        {
            get => keyboardNotVisible;
            set
            {
                if (value != keyboardNotVisible)
                {
                    keyboardNotVisible = value;
                    OnPropertyChanged("KeyboardNotVisible");
                }
            }
        }
        //closing keyboard
        private void OnCloseKeyboard()
        {
            KeyboardNotVisible = Visibility.Visible;
            KeyboardVisible = Visibility.Collapsed;
        }
        //opening keyboard
        private void OnOpenKeyBoard(TextBox tb)
        {
            KeyboardVisible = Visibility.Visible;
            KeyboardNotVisible = Visibility.Collapsed;
        }
        //undo
        private void OnUndo()
        {

            if (undoStack.Count > 0)
            {
                List<Road> previousSnapshot = undoStack.Pop();
                List<Road> newSnapshot = previousSnapshot.Select(entity => new Road
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = new RoadType
                    {
                        Name = entity.Type.Name,
                        ImgSrc = entity.Type.ImgSrc
                    },
                    Value = entity.Value
                }).ToList();

                Entities.Clear();
                foreach (Road entity in newSnapshot)
                {
                    Entities.Add(entity);
                }

                redoStack.Push(previousSnapshot);

                EntitiesToShow = Entities;
                OnPropertyChanged("EntitiesToShow");
            }
        }
        //selected entity
        public Road SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                selectedEntity = value;
                DeleteEntityCommand.RaiseCanExecuteChanged();
            }
        }
        //selected filter type
        public string SelectedEntityTypeToFilter
        {
            get { return selectedEntityTypeToFilter; }
            set
            {
                selectedEntityTypeToFilter = value;
                OnPropertyChanged("SelectedEntityTypeToFilter");
            }
        }
        //greater radioButton
        public bool IsGreaterThanRadioButtonSelected
        {
            get { return isGreaterThanRadioButtonSelected; }
            set
            {
                isGreaterThanRadioButtonSelected = value;
                OnPropertyChanged("IsGreaterThanRadioButtonSelected");
            }
        }
        //less radioButton
        public bool IsLessThanRadioButtonSelected
        {
            get { return isLessThanRadioButtonSelected; }
            set
            {
                isLessThanRadioButtonSelected = value;
                OnPropertyChanged("IsLessThanRadioButtonSelected");
            }
        }
        //equals radioButton
        public bool IsEqualsThanRadioButtonSelected
        {
            get { return isEqualsThanRadioButtonSelected; }
            set
            {
                isEqualsThanRadioButtonSelected = value;
                OnPropertyChanged("IsEqualsThanRadioButtonSelected");
            }
        }
        //id filter
        public string SelectedIdMarginToFilterText
        {
            get { return selectedIdMarginToFilterText; }
            set
            {
                selectedIdMarginToFilterText = value;
                OnPropertyChanged("SelectedIdMarginToFilterText");
            }
        }
        // FILTER
        private void OnFilter()
        {
            FilterErrorMessage = String.Empty;

            if (SelectedEntityTypeToFilter == null &&
                !IsGreaterThanRadioButtonSelected &&
                !IsLessThanRadioButtonSelected &&
                !IsEqualsThanRadioButtonSelected &&
                string.IsNullOrWhiteSpace(SelectedIdMarginToFilterText))
            {
                FilterErrorMessage = FIELDS_EMPTY;
                return;
            }

            List<Road> filteredList = new List<Road>();

            foreach (Road entity in Entities)
            {
                filteredList.Add(entity);
            }

            if (SelectedEntityTypeToFilter != null)
            {
                List<Road> entitiesToDelete = new List<Road>();
                for (int i = 0; i < filteredList.Count; i++)
                {
                    if (filteredList[i].Type.Name != SelectedEntityTypeToFilter)
                    {
                        Console.WriteLine($"Mathicng index : {i}");
                        entitiesToDelete.Add(filteredList[i]);
                    }
                }

                foreach (Road x in entitiesToDelete)
                {
                    filteredList.Remove(x);
                }
            }

            if (IsGreaterThanRadioButtonSelected)
            {
                List<Road> entitiesToDelete = new List<Road>();

                if (string.IsNullOrWhiteSpace(SelectedIdMarginToFilterText))
                {
                    FilterErrorMessage = ID_REQ;
                }
                else
                {
                    int selectedId;
                    bool parsingSuccessful = int.TryParse(SelectedIdMarginToFilterText, out selectedId);

                    if (parsingSuccessful)
                    {
                        FilterErrorMessage = String.Empty;
                        if (selectedId >= 0)
                        {
                            FilterErrorMessage = String.Empty;
                            for (int i = 0; i < filteredList.Count; i++)
                            {
                                if (filteredList[i].Id <= selectedId)
                                {
                                    entitiesToDelete.Add(filteredList[i]);
                                }
                            }

                            foreach (Road entity in entitiesToDelete)
                            {
                                filteredList.Remove(entity);
                            }
                        }
                        else
                        {
                            FilterErrorMessage = ID_NEGATIVE;
                        }
                    }
                    else
                    {
                        FilterErrorMessage = ID_NOT_INT;
                    }
                }
            }
            else if (IsLessThanRadioButtonSelected)
            {
                List<Road> entitiesToDelete = new List<Road>();

                if (string.IsNullOrWhiteSpace(SelectedIdMarginToFilterText))
                {
                    FilterErrorMessage = ID_REQ;
                }
                else
                {
                    int selectedId;
                    bool parsingSuccessful = int.TryParse(SelectedIdMarginToFilterText, out selectedId);

                    if (parsingSuccessful)
                    {
                        FilterErrorMessage = String.Empty;

                        if (selectedId >= 0)
                        {
                            FilterErrorMessage = String.Empty;

                            for (int i = 0; i < filteredList.Count; i++)
                            {
                                if (filteredList[i].Id >= selectedId)
                                {
                                    entitiesToDelete.Add(filteredList[i]);
                                }
                            }

                            foreach (Road entity in entitiesToDelete)
                            {
                                filteredList.Remove(entity);
                            }
                        }
                        else
                        {
                            FilterErrorMessage = ID_NEGATIVE;
                        }
                    }
                    else
                    {
                        FilterErrorMessage = ID_NOT_INT;
                    }
                }
            }
            else if (IsEqualsThanRadioButtonSelected)
            {

                List<Road> entitiesToDelete = new List<Road>();

                if (string.IsNullOrWhiteSpace(SelectedIdMarginToFilterText))
                {
                    FilterErrorMessage = ID_REQ;
                }
                else
                {
                    int selectedId;
                    bool parsingSuccessful = int.TryParse(SelectedIdMarginToFilterText, out selectedId);

                    if (parsingSuccessful)
                    {
                        FilterErrorMessage = String.Empty;

                        if (selectedId >= 0)
                        {
                            FilterErrorMessage = String.Empty;

                            for (int i = 0; i < filteredList.Count; i++)
                            {
                                if (filteredList[i].Id > selectedId)
                                {
                                    entitiesToDelete.Add(filteredList[i]);
                                }
                                else if (filteredList[i].Id < selectedId)
                                {
                                    entitiesToDelete.Add(filteredList[i]);
                                }
                            }

                            foreach (Road entity in entitiesToDelete)
                            {
                                filteredList.Remove(entity);
                            }
                        }
                        else
                        {
                            FilterErrorMessage = ID_NEGATIVE;
                        }
                    }
                    else
                    {
                        FilterErrorMessage = ID_NOT_INT;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SelectedIdMarginToFilterText))
                {
                    FilterErrorMessage = "Choose \"<\" , \">\" or \"=\" ";
                }
            }

            if (filteredList.Count > 0)
            {
                FilteredEntities = new ObservableCollection<Road>();

                foreach (Road x in filteredList)
                {
                    FilteredEntities.Add(x);
                }

                EntitiesToShow = FilteredEntities;
                OnPropertyChanged("EntitiesToShow");
            }
            else
            {
                FilterErrorMessage = "No results";
                EntitiesToShow = Entities;
                OnPropertyChanged("EntitiesToShow");
            }
        }

        //reset filter
        private void OnResetFilter()
        {
            SelectedEntityTypeToFilter = null;
            IsGreaterThanRadioButtonSelected = false;
            IsLessThanRadioButtonSelected = false;
            SelectedIdMarginToFilterText = String.Empty;
            FilterErrorMessage = String.Empty;

            EntitiesToShow = Entities;
            FilteredEntities = new ObservableCollection<Road>();
            OnPropertyChanged("EntitiesToShow");

        }
        //filter errror
        public string FilterErrorMessage
        {
            get { return filterErrorMessage; }
            set
            {
                filterErrorMessage = value;
                OnPropertyChanged("FilterErrorMessage");
            }
        }
        //button delete visib
        private bool CanDelete()
        {
            return SelectedEntity != null;
        }

        //delete button
        private void OnDelete()
        {
            switch (SelectedEntity.Type.Name)
            {
                case "iA":
                    iACount--;
                    MessageBox.Show("Entity : \"" + SelectedEntity.Name + "\" was deleted", "Deleted", MessageBoxButton.OK);
                    break;
                case "iB":
                    iBCount--;
                    MessageBox.Show("Entity : \"" + SelectedEntity.Name + "\" was deleted", "Deleted", MessageBoxButton.OK);
                    break;
            }

            Entities.Remove(SelectedEntity);
            List<Road> currentSnapshot = Entities.Select(entity =>
            {
                return new Road()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = new RoadType()
                    {
                        Name = entity.Type.Name,
                        ImgSrc = entity.Type.ImgSrc
                    },
                    Value = entity.Value
                };
            }).ToList();
            historyStack.Push(currentSnapshot);

        }
        //current
        public Road CurrentEntity
        {
            get { return currentEntity; }
            set
            {
                currentEntity = value;
                OnPropertyChanged("CurrentEntity");
            }
        }
        //current type
        public RoadType CurrentEntityType
        {
            get { return currentEntityType; }
            set
            {
                currentEntityType = value;
                OnPropertyChanged("CurrentEntityType");
            }
        }
        //add f
        public void OnAdd()
        {
            try
            {
                int parsedId;
                bool canParse = int.TryParse(CurrentEntity.TextId, out parsedId);
                bool idAlreadyExists = false;

                if (canParse)
                {
                    foreach (Road x in Entities)
                    {
                        if (x.Id == parsedId)
                        {
                            idAlreadyExists = true;
                            break;
                        }
                    }
                }

                CurrentEntity.DoesIdAlreadyExist = idAlreadyExists;

                CurrentEntity.Validate();
                CurrentEntityType.Validate();

                if (CurrentEntity.IsValid)
                {
                    Entities.Add(new Road()
                    {
                        Id = int.Parse(CurrentEntity.TextId),
                        Name = CurrentEntity.Name,
                        Type = new RoadType
                        {
                            Name = CurrentEntityType.Name,
                            ImgSrc = CurrentEntityType.ImgSrc
                        },
                        Value = 0
                    });

                    switch (CurrentEntityType.Name)
                    {
                        case "iA":
                            iACount++;
                            break;
                        case "iB":
                            iBCount++;
                            break;
                    }
                    CurrentEntity.TextId = String.Empty;
                    CurrentEntity.Name = String.Empty;
                    MessageBox.Show("Added", "New entities", MessageBoxButton.OK);
                    //undo on add
                    List<Road> currentSnapshot = Entities.Select(entity =>
                    {
                        return new Road()
                        {
                            Id = entity.Id,
                            Name = entity.Name,
                            Type = new RoadType()
                            {
                                Name = entity.Type.Name,
                                ImgSrc = entity.Type.ImgSrc
                            },
                            Value = entity.Value
                        };
                    }).ToList();
                    historyStack.Push(currentSnapshot);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.Message}");
            }
        }
    }
}
