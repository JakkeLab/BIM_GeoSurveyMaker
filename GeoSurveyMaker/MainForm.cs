using GeoSurveyMaker.Models;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace GeoSurveyMaker
{
    public partial class MainForm : Form
    {
        //����� ����
        ViewModel viewModel = new ViewModel();
        FileModel fileModel;
        public MainForm()
        {
            InitializeComponent();
            viewModel.OnBoringsChanged += ViewModel_OnBoringsChanged;
            fileModel = new FileModel();
            this.Text = $"�� ���� ������ - SurveyMaker";
        }

        //�̺�Ʈ �۾�
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

                //���ο� �����϶�
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
                            MessageBox.Show($"���� : {ex}");
                        }
                    }
                    listBorings.Items.Add(boring.BoringName);
                    ClearTypedFields();
                    viewModel.AddBoring(boring);
                }

                //������ �̸��� ������ ���� ���� �����
                else
                {
                    if (MessageBox.Show($"���� {tbBoringName.Text} �� �̹� �����մϴ�\n ���� ����ڽ��ϱ�?", "���� �߰�",
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
                MessageBox.Show("���� �̸� �� ���� �׸��� �Է��� �ּ���");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBorings.SelectedItems != null)
            {
                if (MessageBox.Show("���� ������ �����Ͻðڽ��ϱ�?", "���� ����", MessageBoxButtons.YesNo) == DialogResult.Yes) ;
                {
                    ClearTypedFields();

                    //������ �׸� ����Ʈ�� ����
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

        //���� ������ �ҷ����ֱ�
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
                    //2���̻� ������ ���� ���� ����â ��� ���ϰ� ����
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
            tbBoringTop.Text = string.Empty;
            dgLayers.Rows.Clear();
        }

        public void ClearAllBorings()
        {
            viewModel.Borings.Clear();
        }

        
        //������ �׸� �ҷ�����
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

        //���� ����Ʈ ��ȭ ���� �� ���� ��ü �׸���, ���� ����Ʈ ���ΰ�ħ
        public void RefreshBoringView()
        {

            if(viewModel.Borings.Count != 0)
            {
                //���� ���̾ ���� ������ ���̾�� ����
                int maxCountOfLayer = viewModel.Borings.Select(x => x.GroundLayers.Count).Max();

                //������ �׸��� �ʱ�ȭ
                dgBorings.Rows.Clear();

                //���̾��� �÷��� �ִ� ��츸 ���� ���̾� �÷��� ����
                if (dgBorings.Columns.Count > 3)
                {
                    var indexRange = Enumerable.Range(0, dgBorings.Columns.Count - 2).Reverse();
                    foreach (var i in indexRange)
                    {
                        dgBorings.Columns.RemoveAt(i + 2);
                    }
                }

                //���̾� �� �÷� ����
                if (dgBorings.Columns.Count < 3)
                {
                    var column = dgBorings.Columns.Add("colTop", "���� ǥ��");
                    dgBorings.Columns[column].Width = 100;
                    for (int i = 0; i < maxCountOfLayer; i++)
                    {

                        dgBorings.Columns.Add($"colLayer{i + 1}", $"���̾�{i + 1}");

                        //�ʺ� ����
                        dgBorings.Columns[$"colLayer{i + 1}"].Width = 130;
                    }
                }

                //���� �߰�
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
                        string str = $"{boring.GroundLayers[i].LayerName} | ���� : {boring.GroundLayers[i].Depth.ToString("F2")}";
                        row.Add(str);
                    }
                    dgBorings.Rows.Add(row.ToArray());
                }
                
                //��������Ʈ �ʱ�ȭ
                listBorings.Items.Clear();
                foreach(var boring in viewModel.Borings)
                {
                    listBorings.Items.Add(boring.BoringName);
                }
            }
            else
            {
                //������ �׸��� �ʱ�ȭ
                dgBorings.Rows.Clear();

                //���̾��� �÷��� �ִ� ��츸 ���� ���̾� �÷��� ����
                if (dgBorings.Columns.Count > 3)
                {
                    var indexRange = Enumerable.Range(0, dgBorings.Columns.Count - 2).Reverse();
                    foreach (var i in indexRange)
                    {
                        dgBorings.Columns.RemoveAt(i + 2);
                    }
                }

                //��������Ʈ ����
                listBorings.Items.Clear();
            }
        }
        #endregion

        //�Է����Ѻ�
        //����ǥ�� ������ȣ, �Ҽ���, ���� ���� �Է� ����
        private void InputOnlyDouble_KeyPress(object sender, KeyPressEventArgs e)
        {
            // �Է� ���� ��Ģ ���� (��: ����, �Ҽ���, ��ȣ �� �齺���̽��� ���)
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        //���̾� ����ĭ �������� �Է¹���
        private void dgLayers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dgLayers.CurrentCell.OwningColumn.Name == "ColDepth")
            {
                if(e.Control is DataGridViewTextBoxEditingControl textBox)
                {
                    // KeyPress �̺�Ʈ �ڵ鷯 �߰�
                    textBox.KeyPress += InputOnlyDouble_KeyPress;
                }
            }
            else
            {
                if (e.Control is DataGridViewTextBoxEditingControl textBox)
                {
                    // KeyPress �̺�Ʈ �ڵ鷯 ����
                    textBox.KeyPress -= InputOnlyDouble_KeyPress;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("���α׷��� �����Ͻðڽ��ϱ�?", "���α׷� ����", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileModel.IsContentChanged)
            {
                if (MessageBox.Show("���� ���� ���� �� ���� ����ðڽ��ϱ�?", "�� ���� �ۼ�", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClearAllBorings();
                    RefreshBoringView();
                    fileModel = new FileModel();
                    this.Text = "�� ���� ������ - SurveyMaker";
                }
            }
            else
            {
                ClearAllBorings();
                RefreshBoringView();
                fileModel = new FileModel();
                this.Text = "�� ���� ������ - SurveyMaker";
            }
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileModel.IsContentChanged == true)
            {
                if (MessageBox.Show("���� ������ ������� �ʾҽ��ϴ�.\n�ҷ����ðڽ��ϱ�?", "���� �ҷ�����", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        //�ݱ��ư Ŭ���� ���� �̺�Ʈ ��Ȱ��
        private void CloseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formClosingArgs = new FormClosingEventArgs(CloseReason.UserClosing, false);
            MainForm_FormClosing(sender, formClosingArgs);
            if (!formClosingArgs.Cancel)
            {
                this.Close();
            }
        }

        #region xml ���� �޼ҵ�
        //XML �����ϱ�
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

        //XML ��������
        private XDocument XMLCreate()
        {
            if (viewModel.Borings.Count != 0)
            {
                //��������
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
                XElement xroot = new XElement("BoringSet");
                xmlDoc.Add(xroot);

                //Elements
                for (int i = 0; i < viewModel.Borings.Count; i++)
                {
                    var boring = viewModel.Borings[i];

                    //���߰� Id�κ� (�ε���, �����̸�, ǥ��) ����
                    XElement xBoring = new XElement("Boring", new XAttribute("Index", i + 1));
                    xBoring.Add(new XElement("Name", boring.BoringName));
                    xBoring.Add(new XElement("TopLevel", boring.TopLevel));

                    //���߰� ���̾� ��Ʈ ����
                    XElement xLayerSet = new XElement("Layers");

                    //���̾� ��Ʈ �� �� �߰�
                    for (int k = 0; k < boring.GroundLayers.Count; k++)
                    {
                        var layer = boring.GroundLayers[k];
                        XElement xLayers = new XElement($"Layer{k+1}");
                        xLayers.Add(new XElement("LayerType", layer.LayerName));
                        xLayers.Add(new XElement("LayerDepth", layer.Depth));
                        xLayerSet.Add(xLayers);
                    }

                    //���̾� ��Ʈ ���
                    xBoring.Add(xLayerSet);
                    xroot.Add(xBoring);
                }
                return xmlDoc;
            }
            else
            {
                MessageBox.Show("���� ������ �����ϴ�. ���� ������ �߰� �Ͽ� �ٽ� �õ��� �ּ���");
                return null;
            }
        }

        //XML ��������
        private void XMLSave(XDocument xDoc, string filePath)
        {
            xDoc.Save(filePath);
        }

        //XML ���� ����
        private object[] XMLRead()
        {
            OpenFileDialog openFileDialog = OpenFile();
            List<Boring> boringsResult = new List<Boring>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XDocument xDoc = XDocument.Load(openFileDialog.FileName);
                //��ü�� ��ȯ�ؼ� ����
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

        //�������� ���
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

        //���� ���� ���
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