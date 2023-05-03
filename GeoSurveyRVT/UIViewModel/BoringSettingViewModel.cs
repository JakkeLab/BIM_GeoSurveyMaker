using GeoSurveyRVT.RibbonUIForm.BoringSetting;
using GeoSurveyRVT.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GeoSurveyRVT.UIViewModel
{
    public class BoringSettingViewModel : INotifyPropertyChanged
    {

        private static readonly Lazy<BoringSettingViewModel> _instance = new Lazy<BoringSettingViewModel>(() => new BoringSettingViewModel());
        public static BoringSettingViewModel Instance
        {
            get { return _instance.Value; }
        }

        private static ObservableCollection<string> _layerNames = new ObservableCollection<string>();

		public ObservableCollection<string> LayerNames
		{
			get { return _layerNames; }
		}

		public void SaveLayerNames(ObservableCollection<string> LayerNames)
		{
			_layerNames.Clear();
            _layerNames = LayerNames;
        }

		private ObservableCollection<Color> _layerColors = new ObservableCollection<Color>();

        public ObservableCollection<Color> LayerColors
		{
			get { return _layerColors; }
		}

		public void SaveLayerColors(ObservableCollection<Color> LayerColors)
		{
			_layerColors.Clear();
			_layerColors = LayerColors;
		}

		//세팅값 저장소 초기화
		public BoringSettingViewModel()
		{

		}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
