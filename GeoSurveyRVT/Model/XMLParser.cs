using Autodesk.Revit.DB;
using GeoSurveyRVT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GeoSurveyRVT.Model.XMLParser
{
    public class XMLParser
    {

        #region xml 관련 메소드
        //XML 문서 열기
        public List<Boring> XMLRead()
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
            return boringsResult;
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
