using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyRVT.Model
{
	//보링 세트
    public class BoringSet
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
	//개별 보링
    public class Boring
    {
		//보링 이름
		private string _boringName;

		public string BoringName
		{
			get { return _boringName; }
			set { _boringName = value; }
		}

		//보링 표고
		private double _topLevel;

		public double TopLevel
		{
			get { return _topLevel; }
			set { _topLevel = value; }
		}

		//지반층 정의
		//지반층 리스트

		private List<GroundLayer> _groundLayers = new List<GroundLayer>();

		//레이어 생성은 메소드를 통해서만 가능하도록 함 
		public IReadOnlyList<GroundLayer> GroundLayers
		{
			get { return _groundLayers.AsReadOnly(); }
		}

		//지반층 추가 메소드
		public void AddGroundLayer(string layerName, double depth)
		{
			int layerIndex = _groundLayers.Count + 1;
			//각 암질별 출현층(Top)은 메소드 내부적으로 정의
			double top = TopLevel - _groundLayers.Select(x => x.Depth).Sum();
            _groundLayers.Add(new GroundLayer(layerIndex, layerName, depth, top));
        }

		//지반층 편집 메소드
		public void EditGroundLayer(int layerIndex, string newLayerName, double newDepth)
		{
			if(layerIndex >= 1 && layerIndex <= _groundLayers.Count)
			{
				_groundLayers[layerIndex - 1].LayerName= newLayerName;
				_groundLayers[layerIndex - 1].Depth= newDepth;

				//표고 업데이트
				for(int i = 0; i< _groundLayers.Count; i++)
				{
					double currentTop = TopLevel - _groundLayers.Take(i).Select(x => x.Depth).Sum();
					_groundLayers[i].Top = currentTop;
				}
			}
		}

		//지반층 삭제 메소드
		public void RemoveGroundLayer(int layerIndex)
		{
            if (layerIndex >= 1 && layerIndex <= _groundLayers.Count)
			{
				_groundLayers.RemoveAt(layerIndex - 1);

                //표고 업데이트
                for (int i = 0; i < _groundLayers.Count; i++)
                {
                    double currentTop = TopLevel - _groundLayers.Take(i).Select(x => x.Depth).Sum();
                    _groundLayers[i].Top = currentTop;
                }
            }
        }
    }
	//지반층 클래스
	public class GroundLayer
	{
        //지반층 순서
        private int _layerIndex;

        public int LayerIndex
        {
            get { return _layerIndex; }
            set { _layerIndex = value; }
        }
        //지반층 이름
        private string _layerName;

		public string LayerName
		{
			get { return _layerName; }
			set { _layerName = value; }
		}

		//지반층 깊이
		private double _depth;
		
		public double Depth
		{
			get { return _depth; }
			set { _depth = value; }
		}

		//지반층 표고
		private double _top;

		public double Top
		{
			get { return _top; }
			set { _top = value; }
		}

		//지반층 초기화 부분
		public GroundLayer(int layerIndex, string layerName, double depth, double top)
		{
            _layerIndex = layerIndex;
            _layerName = layerName;
			_depth = depth;
			_top= top;
		}
	}
}
