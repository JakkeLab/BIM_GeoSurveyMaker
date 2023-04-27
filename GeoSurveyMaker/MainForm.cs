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

        //�̺�Ʈ ����
        private void ViewModel_OnBoringsChanged(object? sender, EventArgs e)
        {
            RefreshBoringView();
        }

        private void btnAddBoring_Click(object sender, EventArgs e)
        {
            if (tbBoringName.Text != null && dgBorings.Rows != null)
            {
                bool IsAlreadyContained = ViewModel.Borings.Find(x => x.BoringName == tbBoringName.Text) == null ? true : false;

                //���ο� �����϶�
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
                
                //������ �̸��� ������ ���� ��
                else
                {
                    if(MessageBox.Show($"���� {tbBoringName.Text} �� �̹� �����մϴ�\n ���� ����ڽ��ϱ�?", "���� ����", 
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
                MessageBox.Show("���� �̸� �� ���� �׸��� �Է��� �ּ���");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBorings.SelectedItem != null)
            {
                string boringName = (string)listBorings.SelectedItem;
                tbBoringName.Text = boringName;
                var selectedBoring = ViewModel.Borings.Find(x => x.BoringName == boringName);
                if (MessageBox.Show("���� ������ �����Ͻðڽ��ϱ�?", "���� ����", MessageBoxButtons.YesNo) == DialogResult.Yes);
                {
                    listBorings.Items.Remove(listBorings.SelectedItem);
                    ViewModel.Borings.Remove(selectedBoring);
                    ClearTypedFields();
                }
            }
        }

        //���� ������ �ҷ����ֱ�
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

        //�� �� Ŭ���� ��������
        private void listBorings_MouseDown(object sender, MouseEventArgs e)
        {
            int index = listBorings.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                listBorings.ClearSelected();
            }
        }

        #region Methods for manipulation
        //�Է�ĭ �ʱ�ȭ
        public void ClearTypedFields()
        {
            tbBoringName.Text = string.Empty;
            dgLayers.Rows.Clear();
        }
        
        //������ �׸� �ҷ�����
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

        //���� ����Ʈ ��ȭ ���� �� ���� ��ü �׸��� ���ΰ�ħ
        public void RefreshBoringView()
        {
            //���� ���̾ ���� ������ ���̾�� ����
            int maxCountOfLayer = ViewModel.Borings.Select(x => x.GroundLayers.Count).Max();

            //������ �׸��� �ʱ�ȭ
            dgBorings.Rows.Clear();

            //���̾����� �ش��ϴ� �÷����� ����
            var indexesToRemove = Enumerable.Range(2, dgBorings.Columns.Count - 1).Reverse();

            //�÷� �缳�� : �ִ� ���̾����ŭ Ȯ���ؾ� �� 
            if(dgBorings.Columns.Count - 2 < maxCountOfLayer)
            {
                foreach(int i in indexesToRemove)
                {
                    dgBorings.Columns.RemoveAt(i);
                }

                for (int i = 0; i < maxCountOfLayer; i++)
                {
                    dgBorings.Columns.Add($"ColLayer{i}", $"���̾�{i}");
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