using NetworkService.AdditionalElements;
using NetworkService.AdditionalFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {

        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand ExitCommand { get; private set; }

        private NetworkEntitiesViewModel networkEntitiesViewModel = new NetworkEntitiesViewModel();
        private NetworkDisplayViewModel networkDisplayViewModel = new NetworkDisplayViewModel();
        private MeasurementGraphViewModel measurementGraphViewModel = new MeasurementGraphViewModel();

        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            createListener(); //Connecting with server
            NavCommand = new MyICommand<string>(OnNav);
            ExitCommand = new MyICommand(Exit);

            CurrentViewModel = networkEntitiesViewModel;
            networkEntitiesViewModel.Entities.CollectionChanged += this.OnCollectionChanged;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "NetworkEntities":
                    CurrentViewModel = networkEntitiesViewModel;
                    break;
                case "NetworkDisplay":
                    CurrentViewModel = networkDisplayViewModel;
                    break;
                case "MeasurementGraph":
                    CurrentViewModel = measurementGraphViewModel;
                    break;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Road newEntity in e.NewItems)
                {
                    if (!networkDisplayViewModel.EntitiesInList.Contains(newEntity))
                        networkDisplayViewModel.EntitiesInList.Add(newEntity);
                    if (!measurementGraphViewModel.Entities.Contains(newEntity))
                        measurementGraphViewModel.Entities.Add(newEntity);

                }
            }
            if (e.OldItems != null)
            {
                foreach (Road oldEntity in e.OldItems)
                {
                    if (networkDisplayViewModel.EntitiesInList.Contains(oldEntity))
                        networkDisplayViewModel.EntitiesInList.Remove(oldEntity);
                    else
                    {
                        int canvasIndex = networkDisplayViewModel.GetCanvasIndexForEntityId(oldEntity.Id);
                        networkDisplayViewModel.DeleteEntityFromCanvas(oldEntity);
                    }
                    if (measurementGraphViewModel.Entities.Contains(oldEntity))
                        measurementGraphViewModel.Entities.Remove(oldEntity);
                }
            }
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 12333);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Receiving a message
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        if (incomming.Equals("Need object count"))
                        {
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(networkEntitiesViewModel.Entities.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //################ IMPLEMENTACIJA ####################
                            Console.WriteLine(incomming);//Date Time Id Value

                            string incommingEntityId = incomming.Substring(0, incomming.IndexOf(':'));
                            double newValue = double.Parse(incomming.Substring(incomming.IndexOf(':') + 1));

                            for (int idx = 0; idx < networkEntitiesViewModel.Entities.Count; idx++)
                            {
                                string currentEntityId = $"Entitet_{idx}";
                                if (currentEntityId == incommingEntityId)
                                {
                                    networkEntitiesViewModel.Entities[idx].Value = newValue;
                                    using (StreamWriter writer = File.AppendText("log.txt"))
                                    {
                                        DateTime dateTime = DateTime.Now;
                                        writer.WriteLine($"{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}: {networkEntitiesViewModel.Entities[idx].Id}, {newValue}");
                                    }
                                    networkDisplayViewModel.UpdateEntityOnCanvas(networkEntitiesViewModel.Entities[idx]);
                                    measurementGraphViewModel.OnShow();

                                    break;
                                }
                            }

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
