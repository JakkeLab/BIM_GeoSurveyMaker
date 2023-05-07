using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyMaker.Models
{
    public class FileModel
    {
		private string _fileName;

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		private bool _isContentChanged = false;

		public bool IsContentChanged
		{
			get { return _isContentChanged; }
			set { _isContentChanged = value; }
		}

		private bool _isNewFile = true;

		public bool IsNewFile
		{
			get { return _isNewFile; }
			set { _isNewFile = value; }
		}
	}
}
