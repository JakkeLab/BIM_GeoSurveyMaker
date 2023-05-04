using GeoSurveyRVT.Model;
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


        private ObservableCollection<LayerInfo> _layerInfos = new ObservableCollection<LayerInfo>();

        public ObservableCollection<LayerInfo> LayerInfos
        {
            get { return _layerInfos; }
        }


		public void SaveLayerInfo(ObservableCollection<LayerInfo> layerInfos)
		{
            _layerInfos.Clear();
            foreach (var layerInfo in layerInfos)
            {
                _layerInfos.Add(layerInfo);
            }
        }

		//세팅값 저장소 초기화
		public BoringSettingViewModel()
		{

		}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
