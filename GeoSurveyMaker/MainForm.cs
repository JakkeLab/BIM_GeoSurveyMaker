using GeoSurveyMaker.Models;

namespace GeoSurveyMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ViewModel.OnBoringsChanged += ViewModel_OnBoringsChanged;
        }

        //이벤트 구독
        private void ViewModel_OnBoringsChanged(object? sender, EventArgs e)
        {
            RefreshBoringView();
        }

        private void btnAddBoring_Click(object sender, EventArgs e)
        {
            if (tbBoringName.Text != null && dgBorings.Rows != null)
            {
                bool IsAlreadyContained = ViewModel.Borings.Find(x => x.BoringName == tbBoringName.Text) == null ? true : false;

                //새로운 보링일때
                if(IsAlreadyContained)
                {
                    Boring boring = new Boring();
                    boring.BoringName = tbBoringName.Text;
                    for (int i = 0; i < dgLayers.Rows.Count - 1; i++)
                    {
                        var layer = dgLayers.Rows[i];
                        string layerName = layer.Cells[0].Value.ToString();
                        double layerDepth = Double.Parse(layer.Cells[1].Value.ToString());
                        boring.AddGroundLayer(layerName, layerDepth);
                    }
                    listBorings.Items.Add(boring.BoringName);
                    ClearTypedFields();
                    Models.ViewModel.Borings.Add(boring);
                }
                
                //동일한 이름의 보링이 있을 때
                else
                {
                    if(MessageBox.Show($"보링 {tbBoringName.Text} 가 이미 존재합니다\n 덮어 씌우겠습니까?", "보링 저장", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK);
                    {
                        int oldIndex = ViewModel.Borings.FindIndex(x => x.BoringName == tbBoringName.Text);
                        Boring newBoring = new Boring();
                        newBoring.BoringName = tbBoringName.Text;
                        for (int i = 0; i < dgLayers.Rows.Count - 1; i++)
                        {
                            var layer = dgLayers.Rows[i];
                            string layerName = layer.Cells[0].Value.ToString();
                            double layerDepth = Double.Parse(layer.Cells[1].Value.ToString());
                            newBoring.AddGroundLayer(layerName, layerDepth);
                        }
                        ViewModel.Borings.RemoveAt(oldIndex);
                        ViewModel.Borings.Insert(oldIndex, newBoring);
                        ClearTypedFields();
                        listBorings.ClearSelected();
                    }
                }

            }
            else
            {
                MessageBox.Show("보링 이름 및 층별 항목을 입력해 주세요");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBorings.SelectedItem != null)
            {
                string boringName = (string)listBorings.SelectedItem;
                tbBoringName.Text = boringName;
                var selectedBoring = ViewModel.Borings.Find(x => x.BoringName == boringName);
                if (MessageBox.Show("보링 정보를 삭제하시겠습니까?", "보링 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes);
                {
                    listBorings.Items.Remove(listBorings.SelectedItem);
                    ViewModel.Borings.Remove(selectedBoring);
                    ClearTypedFields();
                }
            }
        }

        //보링 아이템 불러와주기
        private void listBorings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBorings.SelectedItem != null)
            {
                string boringName = (string)listBorings.SelectedItem;
                tbBoringName.Text = boringName;
                btnRemove.Enabled = true;
                LoadSelectedBoringData(boringName);
            }
            else
            {
                btnRemove.Enabled = false;
                ClearTypedFields();
            }
        }

        //빈 곳 클릭시 선택해제
        private void listBorings_MouseDown(object sender, MouseEventArgs e)
        {
            int index = listBorings.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                listBorings.ClearSelected();
            }
        }

        #region Methods for manipulation
        //입력칸 초기화
        public void ClearTypedFields()
        {
            tbBoringName.Text = string.Empty;
            dgLayers.Rows.Clear();
        }
        
        //선택한 항목 불러오기
        public void LoadSelectedBoringData(string BoringName)
        {
            dgLayers.Rows.Clear();
            Boring boring = ViewModel.Borings.Find(x => x.BoringName == BoringName);
            tbBoringName.Text = boring.BoringName;
            for (int i = 0; i < boring.GroundLayers.Count; i++)
            {
                var layer = boring.GroundLayers[i];
                dgLayers.Rows.Add(new object[] { layer.LayerName, layer.Depth });
            }
        }

        //보링 리스트 변화 있을 시 보링 전체 그리드 새로고침
        public void RefreshBoringView()
        {
            //가장 레이어가 많은 보링의 레이어수 선택
            int maxCountOfLayer = ViewModel.Borings.Select(x => x.GroundLayers.Count).Max();

            //데이터 그리드 초기화
            dgBorings.Rows.Clear();

            //레이어층에 해당하는 컬럼범위 선언
            var indexesToRemove = Enumerable.Range(2, dgBorings.Columns.Count - 1).Reverse();

            //컬럼 재설정 : 최대 레이어수만큼 확장해야 함 
            if(dgBorings.Columns.Count - 2 < maxCountOfLayer)
            {
                foreach(int i in indexesToRemove)
                {
                    dgBorings.Columns.RemoveAt(i);
                }

                for (int i = 0; i < maxCountOfLayer; i++)
                {
                    dgBorings.Columns.Add($"ColLayer{i}", $"레이어{i}");
                    dgBorings.Columns[dgBorings.Columns.Count - 1].Width = 120;
                }
            }
            foreach(var boring in ViewModel.Borings)
            {
                dgBorings.Rows.Add();
            }
        }

        public object[] RowData(Boring boring)
        {
            int layerCount = boring.GroundLayers.Count;
            List<object> rows = new List<object>();
            rows.Add(boring.BoringName);
            foreach(var )
        }
        #endregion
    }
}