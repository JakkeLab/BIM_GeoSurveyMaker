using GeoSurveyRVT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeoSurveyRVT.ViewModel
{
    public class BoringViewModel : INotifyPropertyChanged
    {
		//뷰모델 싱글톤 적용
		private static readonly Lazy<BoringViewModel> _instance = new Lazy<BoringViewModel>(() => new BoringViewModel());
		public static BoringViewModel Instance
		{
			get { return _instance.Value; }
		}
		

		private BoringSet _importedBoringSet = new BoringSet();
		public BoringSet ImportedBoringSet
		{
			get { return _importedBoringSet;}
			set { _importedBoringSet = value;}
		}


        private ObservableCollection<string> _layerNames = new ObservableCollection<string>();
        public ObservableCollection<string> LayerNames
        {
            get { return _layerNames; }
            set { _layerNames = value; }
        }

        private BoringViewModel()
        {
            
        }

        public void LoadBoringNames()
        {
            // 여기에서 Borings를 가져오는 코드 구현
            // ...

            // Borings에서 중복되지 않은 레이어 이름을 찾아서 LayerNames에 저장
            var layerNames = ImportedBoringSet.Borings.SelectMany(boring => boring.GroundLayers).Select(x => x.LayerName).Distinct();
            LayerNames = new ObservableCollection<string>(layerNames);
        }

        // INotifyPropertyChanged 인터페이스 구현
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
