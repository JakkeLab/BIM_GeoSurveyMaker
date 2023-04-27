using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyMaker.Models
{
    public static class ViewModel
    {
		private static List<Boring> _borings = new List<Boring>();

		public static List<Boring> Borings
		{
			get { return _borings; }
			set 
			{ 
				_borings = value;

				//이벤트 발생
                OnBoringsChanged?.Invoke(null, EventArgs.Empty);
            }
		}
		
		//이벤트 핸들러 정의
		public static event EventHandler OnBoringsChanged;
	}
}
