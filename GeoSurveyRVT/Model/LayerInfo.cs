using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GeoSurveyRVT.Model
{
    public class LayerInfo : INotifyPropertyChanged
    {
        private string _layerName;
        private Color _color;

        public string LayerName
        {
            get { return _layerName; }
            set
            {
                _layerName = value;
                OnPropertyChanged(nameof(LayerName));
            }
        }

        public Color LayerColor
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(LayerColor));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
