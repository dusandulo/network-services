using NetworkService.AdditionalElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetworkService.Model
{

    public class Graph : BindableBase
    {
        private int cmId;
        private int cmValue;
        private double cmHeight;
        private string cmDate;
        private string cmTime;
        private string cmColor;

        public Graph()
        {

        }
        public Graph(int cmId, int cmValue, string cmDate, string cmTime)
        {
            CmId = cmId;
            CmValue = cmValue;
            CmDate = cmDate;
            CmTime = cmTime;
        }
        public int CmId
        {
            get
            {
                return cmId;
            }
            set
            {
                cmId = value;
                OnPropertyChanged("CmId");
            }
        }
        public double CmHeight
        {
            get
            {
                return cmHeight;
            }
            set
            {
                cmHeight = value;
                OnPropertyChanged("CmHeight");
            }
        }
        public int CmValue
        {
            get
            {
                return cmValue;
            }
            set
            {
                cmValue = value;
                double temp = CmValue;
                CmHeight = temp / 18500 * 200;
                if (CmValue < 7000 || CmValue > 15000)
                {
                    CmColor = "Red";
                }
                else
                {
                    CmColor = "Green";
                }
                OnPropertyChanged("CmValue");
                OnPropertyChanged("CmColor");
            }
        }
        public string CmDate
        {
            get
            {
                return cmDate;
            }
            set
            {
                cmDate = value;
                OnPropertyChanged("CmDate");
            }
        }
        public string CmTime
        {
            get
            {
                return cmTime;
            }
            set
            {
                cmTime = value;
                OnPropertyChanged("CmTime");
            }
        }
        public string CmColor
        {
            get
            {
                return cmColor;
            }
            set
            {
                cmColor = value;
                OnPropertyChanged("CmColor");
            }
        }
    }

}
