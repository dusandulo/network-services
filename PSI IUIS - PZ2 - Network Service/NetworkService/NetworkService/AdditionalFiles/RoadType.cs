using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.AdditionalFiles
{
    public class RoadType : ValidationBase
    {
        private string name;
        private string imgSrc;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    ImgSrc = ComboBoxItems.comboBoxTypes[value];
                    OnPropertyChanged("Name");
                }
            }
        }

        public string ImgSrc
        {
            get { return imgSrc; }
            set
            {
                if (imgSrc != value)
                {
                    imgSrc = value;
                    OnPropertyChanged("ImgSrc");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (this.Name == null)
            {
                this.ValidationErrors["Name"] = "Empty field";
            }
        }
    }
}
