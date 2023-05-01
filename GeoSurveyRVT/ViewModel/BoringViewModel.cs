using GeoSurveyRVT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeoSurveyRVT.ViewModel
{
    public class BoringViewModel
    {
		//뷰모델 싱글톤 적용
		private static readonly Lazy<BoringViewModel> _instance = new Lazy<BoringViewModel>(() => new BoringViewModel());
		public static BoringViewModel Instance
		{
			get { return _instance.Value; }
		}

		//뷰모델 초기화
		private BoringViewModel()
		{

		}

		private BoringSet _importedBoringSet = new BoringSet();
		public BoringSet ImportedBoringSet
		{
			get { return _importedBoringSet;}
			set { _importedBoringSet = value;}
		}
	}
}
