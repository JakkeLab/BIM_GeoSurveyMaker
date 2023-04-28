using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyMaker.Models
{
    public class ViewModel
    {
		private List<Boring> _borings = new List<Boring>();

		public List<Boring> Borings
		{
			get { return _borings; }
		}
        //이벤트 핸들러 정의
        public event EventHandler OnBoringsChanged;


        //보링 데이터 CUD
        //Create
		public void AddBoring(Boring boring)
		{
            _borings.Add(boring);
            OnBoringsChanged?.Invoke(this, EventArgs.Empty);
        }

        //Delete
        public void RemoveBoring(Boring boring)
        {
            _borings.Remove(boring);
            OnBoringsChanged?.Invoke(this, EventArgs.Empty);
        }

        //Update
        public void UpdateBoring(int index, Boring newBoring)
        {
            _borings[index] = newBoring;
            OnBoringsChanged?.Invoke(this, EventArgs.Empty);
        }

        //Reset
        public void ResetBoring()
        {
            _borings.Clear();
            OnBoringsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
