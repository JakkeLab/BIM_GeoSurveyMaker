using GeoSurveyRVT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyRVT.Status
{
    public static class Container
    {
		private static BoringSet _boringSetContainer = new BoringSet();

		public static BoringSet BoringSetContainer
		{
			get { return _boringSetContainer; }
			set { _boringSetContainer = value; }
		}
	}
}
