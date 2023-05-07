using GeoSurveyMaker.Models;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace GeoSurveyMaker
{
    public partial class MainForm : Form
    {
        //저장소 생성
        ViewModel viewModel = new ViewModel();
        FileModel fileModel;
        public MainForm()
        {
            InitializeComponent();
            viewModel.OnBoringsChanged += ViewModel_OnBoringsChanged;
            fileModel = new FileModel();
            this.Text = $"새 보링 데이터 - SurveyMaker";
        }

        //이벤트 작업
        private void ViewModel_OnBoringsChanged(object sender, EventArgs e)
        {
            RefreshBoringView();
            fileModel.IsContentChanged = true;
            this.Text = $"*{this.Text}";
        }

        private void btnAddBoring_Click(object sender, EventArgs e)
        {
            if (tbBoringName.Text != null && dgBorings.Rows != null)
            {
                bool IsAlreadyContained = viewModel.Borings.Find(x => x.BoringName == tbBoringName.Text) == null ? true : false;

                //새로운 보링일때
                if(IsAlreadyContained)
                {
                    Boring boring = new Boring();
                    boring.BoringName = tbBoringName.Text;
                    boring.TopLevel = Double.Parse(tbBoringTop.Text);
                    for (int i = 0; i < dgLayers.Rows.Count - 1; i++)
                    {
                        var layer = dgLayers.Rows[i];
                        string layerName = layer.Cells[0].Value.ToString();
                        double layerDepth;
                        try
                        {
                            Double.TryParse(layer.Cells[1].Value.ToString(), out layerDepth);
                            boring.AddGroundLayer(layerName, layerDepth);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show($"오류 : {ex}");
                        }
                    }
                    listBorings.Items.Add(boring.BoringName);
                    ClearTypedFields();
                    viewModel.AddBoring(boring);
                }

                //동일한 이름의 보링이 있을 때는 덮어쓰기
                else
                {
                    if (MessageBox.Show($"보링 {tbBoringName.Text} 가 이미 존재합니다\n 덮어 씌우겠습니까?", "보링 추가",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int oldIndex = viewModel.Borings.FindIndex(x => x.BoringName == tbBoringName.Text);
                        Boring newBoring = new Boring();
                        newBoring.BoringName = tbBoringName.Text;
                        newBoring.TopLevel = Double.Parse(tbBoringTop.Text);
                        for (int i = 0; i < dgLayers.Rows.Count - 1; i++)
                        {
                            var layer = dgLayers.Rows[i];
                            string layerName = layer.Cells[0].Value.ToString();
                            double layerDepth = Double.Parse(layer.Cells[1].Value.ToString());
                            newBoring.AddGroundLayer(layerName, layerDepth);
                        }
                        viewModel.UpdateBoring(oldIndex, newBoring);
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
            if (listBorings.SelectedItems != null)
            {
                if (MessageBox.Show("보링 정보를 삭제하시겠습니까?", "보링 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes) ;
                {
                    ClearTypedFields();

                    //삭제할 항목 리스트에 저장
                    List<string> itemsToRemove = new List<string>();
                    foreach (var selectedItem in listBorings.SelectedItems)
                    {
                        itemsToRemove.Add((string)selectedItem);
                    }

                    foreach (var itemName in itemsToRemove)
                    {
                        var selectedBoring = viewModel.Borings.Find(x => x.BoringName == itemName);

                        listBorings.Items.Remove(itemName);
                        viewModel.RemoveBoring(selectedBoring);
                    }
                }
            }
        }

        //보링 아이템 불러와주기
        private void listBorings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBorings.SelectedItem != null)
            {
                if(listBorings.SelectedItems.Count == 1)
                {
                    string boringName = (string)listBorings.SelectedItem;
                    tbBoringName.Text = boringName;
                    btnRemove.Enabled = true;
                    LoadSelectedBoringData(boringName);
                }
                else
                {
                    //2개이상 선택한 경우는 보링 편집창 사용 못하게 설정
                    tbBoringName.Enabled = false;
                    tbBoringTop.Enabled = false;
                    dgLayers.Enabled = false;
                    btnRemove.Enabled = true;
                    ClearTypedFields();
                }
            }
            else
            {
                tbBoringName.Enabled = true;
                tbBoringTop.Enabled = true;
                dgLayers.Enabled = true;
                btnRemove.Enabled = false;
                ClearTypedFields();
            }

            tbBoringName.Enabled = true;
            tbBoringTop.Enabled = true;
            dgLayers.Enabled = true;
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
            tbBoringTop.Text = string.Empty;
            dgLayers.Rows.Clear();
        }

        public void ClearAllBorings()
        {
            viewModel.Borings.Clear();
        }

        
        //선택한 항목 불러오기
        public void LoadSelectedBoringData(string BoringName)
        {
            dgLayers.Rows.Clear();
            Boring boring = viewModel.Borings.Find(x => x.BoringName == BoringName);
            tbBoringName.Text = boring.BoringName;
            tbBoringTop.Text = boring.TopLevel.ToString();
            for (int i = 0; i < boring.GroundLayers.Count; i++)
            {
                var layer = boring.GroundLayers[i];
                dgLayers.Rows.Add(new object[] { layer.LayerName, layer.Depth });
            }
        }

        //보링 리스트 변화 있을 시 보링 전체 그리드, 보링 리스트 새로고침
        public void RefreshBoringView()
        {

            if(viewModel.Borings.Count != 0)
            {
                //가장 레이어가 많은 보링의 레이어수 선택
                int maxCountOfLayer = viewModel.Borings.Select(x => x.GroundLayers.Count).Max();

                //데이터 그리드 초기화
                dgBorings.Rows.Clear();

                //레이어층 컬럼이 있는 경우만 기존 레이어 컬럼들 삭제
                if (dgBorings.Columns.Count > 3)
                {
                    var indexRange = Enumerable.Range(0, dgBorings.Columns.Count - 2).Reverse();
                    foreach (var i in indexRange)
                    {
                        dgBorings.Columns.RemoveAt(i + 2);
                    }
                }

                //레이어 층 컬럼 생성
                if (dgBorings.Columns.Count < 3)
                {
                    var column = dgBorings.Columns.Add("colTop", "지반 표고");
                    dgBorings.Columns[column].Width = 100;
                    for (int i = 0; i < maxCountOfLayer; i++)
                    {

                        dgBorings.Columns.Add($"colLayer{i + 1}", $"레이어{i + 1}");

                        //너비 지정
                        dgBorings.Columns[$"colLayer{i + 1}"].Width = 130;
                    }
                }

                //보링 추가
                foreach (var boring in viewModel.Borings)
                {
                    List<object> row = new List<object>
                {
                    dgBorings.Rows.Count + 1,
                    boring.BoringName,
                    boring.TopLevel
                };

                    for (int i = 0; i < boring.GroundLayers.Count; i++)
                    {
                        string str = $"{boring.GroundLayers[i].LayerName} | 깊이 : {boring.GroundLayers[i].Depth.ToString("F2")}";
                        row.Add(str);
                    }
                    dgBorings.Rows.Add(row.ToArray());
                }
                
                //보링리스트 초기화
                listBorings.Items.Clear();
                foreach(var boring in viewModel.Borings)
                {
                    listBorings.Items.Add(boring.BoringName);
                }
            }
            else
            {
                //데이터 그리드 초기화
                dgBorings.Rows.Clear();

                //레이어층 컬럼이 있는 경우만 기존 레이어 컬럼들 삭제
                if (dgBorings.Columns.Count > 3)
                {
                    var indexRange = Enumerable.Range(0, dgBorings.Columns.Count - 2).Reverse();
                    foreach (var i in indexRange)
                    {
                        dgBorings.Columns.RemoveAt(i + 2);
                    }
                }

                //보링리스트 비우기
                listBorings.Items.Clear();
            }
        }
        #endregion

        //입력제한부
        //지반표고 음수부호, 소수점, 숫자 제외 입력 방지
        private void InputOnlyDouble_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력 제한 규칙 적용 (예: 숫자, 소수점, 부호 및 백스페이스만 허용)
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        //레이어 깊이칸 숫자제외 입력방지
        private void dgLayers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dgLayers.CurrentCell.OwningColumn.Name == "ColDepth")
            {
                if(e.Control is DataGridViewTextBoxEditingControl textBox)
                {
                    // KeyPress 이벤트 핸들러 추가
                    textBox.KeyPress += InputOnlyDouble_KeyPress;
                }
            }
            else
            {
                if (e.Control is DataGridViewTextBoxEditingControl textBox)
                {
                    // KeyPress 이벤트 핸들러 제거
                    textBox.KeyPress -= InputOnlyDouble_KeyPress;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("프로그램을 종료하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileModel.IsContentChanged)
            {
                if (MessageBox.Show("기존 보링 삭제 후 새로 만드시겠습니까?", "새 파일 작성", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClearAllBorings();
                    RefreshBoringView();
                    fileModel = new FileModel();
                    this.Text = "새 보링 데이터 - SurveyMaker";
                }
            }
            else
            {
                ClearAllBorings();
                RefreshBoringView();
                fileModel = new FileModel();
                this.Text = "새 보링 데이터 - SurveyMaker";
            }
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileModel.IsContentChanged == true)
            {
                if (MessageBox.Show("기존 파일이 저장되지 않았습니다.\n불러오시겠습니까?", "파일 불러오기", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    object[] xmlData = XMLRead();
                    List<Boring> loadedBorings = xmlData[1] as List<Boring>;
                    viewModel.ResetBoring();
                    foreach (var loadedBoring in loadedBorings)
                    {
                        viewModel.AddBoring(loadedBoring);
                    }
                    fileModel.FileName = xmlData[0] as string;
                    fileModel.IsNewFile = false;
                    fileModel.IsContentChanged = false;
                    this.Text = $"{Path.GetFileName(fileModel.FileName)} - SurveyMaker";
                }
            }
            else
            {
                object[] xmlData = XMLRead();
                if (xmlData[0] != string.Empty)
                {
                    List<Boring> loadedBorings = xmlData[1] as List<Boring>;
                    viewModel.ResetBoring();
                    foreach (var loadedBoring in loadedBorings)
                    {
                        viewModel.AddBoring(loadedBoring);
                    }
                    fileModel.FileName = xmlData[0] as string;
                    fileModel.IsNewFile = false;
                    fileModel.IsContentChanged = false;
                    this.Text = $"{Path.GetFileName(fileModel.FileName)} - SurveyMaker";
                }
            }
        }

        private void exportFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var xmlDoc = XMLCreate();
            if(xmlDoc == null)
            {
                return;
            }
            string fileName = SaveXML(xmlDoc);
            if (fileName != null)
            {
                fileModel.FileName = fileName;
                fileModel.IsContentChanged = false;
                fileModel.IsNewFile = false;
                this.Text = $"{Path.GetFileName(fileName)} - SurveyMaker";
            }
        }

        //닫기버튼 클릭시 종료 이벤트 재활용
        private void CloseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formClosingArgs = new FormClosingEventArgs(CloseReason.UserClosing, false);
            MainForm_FormClosing(sender, formClosingArgs);
            if (!formClosingArgs.Cancel)
            {
                this.Close();
            }
        }

        #region xml 관련 메소드
        //XML 저장하기
        private string SaveXML(XDocument xDoc)
        {
            if(XMLCreate() != null)
            {
                if (fileModel.IsNewFile)
                {
                    SaveFileDialog saveFileDialog = SaveFile();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        XMLSave(xDoc, saveFileDialog.FileName);
                        return saveFileDialog.FileName;
                    }
                    else
                    {
                        return fileModel.FileName;
                    }
                }
                else
                {
                    XMLSave(xDoc, fileModel.FileName);
                    return fileModel.FileName;
                }
            }
            else
            {
                return null;
            }
        }

        //XML 문서생성
        private XDocument XMLCreate()
        {
            if (viewModel.Borings.Count != 0)
            {
                //문서생성
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
                XElement xroot = new XElement("BoringSet");
                xmlDoc.Add(xroot);

                //Elements
                for (int i = 0; i < viewModel.Borings.Count; i++)
                {
                    var boring = viewModel.Borings[i];

                    //시추공 Id부분 (인덱스, 보링이름, 표고) 정의
                    XElement xBoring = new XElement("Boring", new XAttribute("Index", i + 1));
                    xBoring.Add(new XElement("Name", boring.BoringName));
                    xBoring.Add(new XElement("TopLevel", boring.TopLevel));

                    //시추공 레이어 세트 정의
                    XElement xLayerSet = new XElement("Layers");

                    //레이어 세트 각 층 추가
                    for (int k = 0; k < boring.GroundLayers.Count; k++)
                    {
                        var layer = boring.GroundLayers[k];
                        XElement xLayers = new XElement($"Layer{k+1}");
                        xLayers.Add(new XElement("LayerType", layer.LayerName));
                        xLayers.Add(new XElement("LayerDepth", layer.Depth));
                        xLayerSet.Add(xLayers);
                    }

                    //레이어 세트 담기
                    xBoring.Add(xLayerSet);
                    xroot.Add(xBoring);
                }
                return xmlDoc;
            }
            else
            {
                MessageBox.Show("보링 정보가 없습니다. 보링 정보를 추가 하여 다시 시도해 주세요");
                return null;
            }
        }

        //XML 문서서장
        private void XMLSave(XDocument xDoc, string filePath)
        {
            xDoc.Save(filePath);
        }

        //XML 문서 열기
        private object[] XMLRead()
        {
            OpenFileDialog openFileDialog = OpenFile();
            List<Boring> boringsResult = new List<Boring>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XDocument xDoc = XDocument.Load(openFileDialog.FileName);
                //객체로 변환해서 저장
                var borings = from b in xDoc.Descendants("Boring")
                              select new
                              {
                                  BoringElement = b,
                                  BoringObject = new Boring
                                  {
                                      BoringName = b.Element("Name").Value,
                                      TopLevel = Double.Parse(b.Element("TopLevel").Value)
                                  }
                              };

                foreach (var boring in borings)
                {
                    int layerIndex = 1;
                    XElement layerElement;

                    while ((layerElement = boring.BoringElement.Element("Layers").Element($"Layer{layerIndex}")) != null)
                    {
                        string layerName = layerElement.Element("LayerType").Value;
                        double layerDepth = double.Parse(layerElement.Element("LayerDepth").Value);

                        boring.BoringObject.AddGroundLayer(layerName, layerDepth);
                        layerIndex++;
                    }

                    boringsResult.Add(boring.BoringObject);
                }
            }
            
;           object[] result = new object[] { (string)openFileDialog.FileName, (List<Boring>)boringsResult};

            return result;
        }

        //파일저장 기능
        private SaveFileDialog SaveFile()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckFileExists = false;
            saveDialog.AddExtension = true;
            saveDialog.ValidateNames = true;

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string filepath = Path.GetDirectoryName(path);

            saveDialog.InitialDirectory = filepath;

            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML (*.xml) | *.xml";
            saveDialog.FileName = "Boring".ToString();

            return saveDialog;
        }

        //파일 열기 기능
        private OpenFileDialog OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string filepath = Path.GetDirectoryName(path);

            openFileDialog.InitialDirectory = filepath;
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "XML (*.xml) | *.xml";

            return openFileDialog;
        }
        #endregion
    }
}