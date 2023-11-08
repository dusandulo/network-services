using NetworkService.AdditionalElements;
using NetworkService.AdditionalFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        //list for canvas
        public BindingList<Road> EntitiesInList { get; set; }

        //collections
        public ObservableCollection<Canvas> CanvasCollection { get; set; }
        public ObservableCollection<Brush> BorderBrushCollection { get; set; }

        private Road selectedEntity;

        //common canvas objects
        private Road draggedItem = null;
        private bool dragging = false;
        public int draggingSourceIndex = -1;

        //commands
        public MyICommand<object> DropEntityOnCanvas { get; set; }
        public MyICommand<object> LeftMouseButtonDownOnCanvas { get; set; }
        public MyICommand MouseLeftButtonUp { get; set; }
        public MyICommand<object> SelectionChanged { get; set; }
        public MyICommand<object> OslobodiCanvas { get; set; }
        public MyICommand<object> OslobodiCanvasSve { get; set; }
        public MyICommand<object> RightMouseButtonDownOnCanvas { get; set; }

        private int sourceCanvasIndex = -1;


        public NetworkDisplayViewModel()
        {
            EntitiesInList = new BindingList<Road>();

            CanvasCollection = new ObservableCollection<Canvas>();
            for (int i = 0; i < 12; i++)
            {
                CanvasCollection.Add(new Canvas()
                {
                    Background = Brushes.LightGray,
                    AllowDrop = true
                });
            }

            BorderBrushCollection = new ObservableCollection<Brush>();
            for (int i = 0; i < 12; i++)
            {
                BorderBrushCollection.Add(Brushes.DarkGray);
            }

            DropEntityOnCanvas = new MyICommand<object>(OnDrop);
            LeftMouseButtonDownOnCanvas = new MyICommand<object>(OnLeftMouseButtonDown);
            MouseLeftButtonUp = new MyICommand(OnMouseLeftButtonUp);
            SelectionChanged = new MyICommand<object>(OnSelectionChanged);
            OslobodiCanvas = new MyICommand<object>(OnOslobodiCanvas);
            OslobodiCanvasSve = new MyICommand<object>(OnOslobodiCanvasSve);
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
        //ondrop
        private void OnDrop(object parameter)
        {
            if (draggedItem != null)
            {
                int index = Convert.ToInt32(parameter);

                if (CanvasCollection[index].Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(draggedItem.Type.ImgSrc, UriKind.RelativeOrAbsolute);
                    logo.EndInit();

                    CanvasCollection[index].Background = new ImageBrush(logo);
                    CanvasCollection[index].Resources.Add("taken", true);
                    CanvasCollection[index].Resources.Add("data", draggedItem);
                    BorderBrushCollection[index] = (draggedItem.IsValueValidForType()) ? Brushes.Green : Brushes.Red;

                    if (draggingSourceIndex != -1)
                    {
                        CanvasCollection[draggingSourceIndex].Background = Brushes.LightGray;
                        CanvasCollection[draggingSourceIndex].Resources.Remove("taken");
                        CanvasCollection[draggingSourceIndex].Resources.Remove("data");
                        BorderBrushCollection[draggingSourceIndex] = Brushes.DarkGray;

                        if (sourceCanvasIndex != -1)
                        {
                            sourceCanvasIndex = -1;
                        }

                        draggingSourceIndex = -1;
                    }

                    if (EntitiesInList.Contains(draggedItem))
                    {
                        EntitiesInList.Remove(draggedItem);
                    }
                }
            }
        }
        //left mouse click
        private void OnLeftMouseButtonDown(object parameter)
        {
            if (!dragging)
            {
                int index = Convert.ToInt32(parameter);

                if (CanvasCollection[index].Resources["taken"] != null)
                {
                    dragging = true;
                    draggedItem = (Road)(CanvasCollection[index].Resources["data"]);
                    draggingSourceIndex = index;
                    DragDrop.DoDragDrop(CanvasCollection[index], draggedItem, DragDropEffects.Move);
                }
            }
        }
        //left mouse release
        private void OnMouseLeftButtonUp()
        {
            draggedItem = null;
            SelectedEntity = null;
            dragging = false;
            draggingSourceIndex = -1;
        }
        //items selection in list
        private void OnSelectionChanged(object parameter)
        {
            if (!dragging)
            {
                dragging = true;
                draggedItem = SelectedEntity;
                DragDrop.DoDragDrop((ListView)parameter, draggedItem, DragDropEffects.Move);
            }
        }
        //release canvas item from frame
        private void OnOslobodiCanvas(object parameter)
        {
            int index = Convert.ToInt32(parameter);

            if (CanvasCollection[index].Resources["taken"] != null)
            {
                if (sourceCanvasIndex != -1)
                {
                    sourceCanvasIndex = -1;
                }

                EntitiesInList.Add((Road)CanvasCollection[index].Resources["data"]);
                CanvasCollection[index].Background = Brushes.LightGray;
                CanvasCollection[index].Resources.Remove("taken");
                CanvasCollection[index].Resources.Remove("data");
                BorderBrushCollection[index] = Brushes.DarkGray;
            }
        }
        //release all taken items
        private void OnOslobodiCanvasSve(object parameter)
        {
            for(int i = 0; i < 12; i++)
            {
                if (CanvasCollection[i].Resources["taken"] != null)
                {
                    if (sourceCanvasIndex != -1)
                    {
                        sourceCanvasIndex = -1;
                    }

                    EntitiesInList.Add((Road)CanvasCollection[i].Resources["data"]);
                    CanvasCollection[i].Background = Brushes.LightGray;
                    CanvasCollection[i].Resources.Remove("taken");
                    CanvasCollection[i].Resources.Remove("data");
                    BorderBrushCollection[i] = Brushes.DarkGray;

                }
            }
        }
        //delete item from canvas
        public void DeleteEntityFromCanvas(Road entity)
        {
            int canvasIndex = GetCanvasIndexForEntityId(entity.Id);

            if (canvasIndex != -1)
            {
                CanvasCollection[canvasIndex].Background = Brushes.LightGray;
                CanvasCollection[canvasIndex].Resources.Remove("taken");
                CanvasCollection[canvasIndex].Resources.Remove("data");
                BorderBrushCollection[canvasIndex] = Brushes.DarkGray;

            }
        }
        //index of item
        public int GetCanvasIndexForEntityId(int entityId)
        {
            for (int i = 0; i < CanvasCollection.Count; i++)
            {
                Road entity = (CanvasCollection[i].Resources["data"]) as Road;

                if ((entity != null) && (entity.Id == entityId))
                {
                    return i;
                }
            }
            return -1;
        }
        //update canvas item (from value 7000 or 15000)
        public void UpdateEntityOnCanvas(Road entity)
        {
            int canvasIndex = GetCanvasIndexForEntityId(entity.Id);

            if (canvasIndex != -1)
            {
                if (entity.IsValueValidForType())
                {
                    BorderBrushCollection[canvasIndex] = Brushes.Green;
                }
                else
                {
                    BorderBrushCollection[canvasIndex] = Brushes.Red;
                }
            }
        }

    }
}
