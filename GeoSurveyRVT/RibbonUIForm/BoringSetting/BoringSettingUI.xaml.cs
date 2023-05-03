using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WinForm = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using GeoSurveyRVT.ViewModel;
using GeoSurveyRVT.UIViewModel;
using System.Collections.ObjectModel;

namespace GeoSurveyRVT.RibbonUIForm.BoringSetting
{
    public partial class BoringSettingUI : Window
    {
        public bool ToSaveValues;
        public BoringSettingUI()
        {
            InitializeComponent();
            
            //처음 켤때
            if(BoringSettingViewModel.Instance.LayerNames.Count == 0)
            {
                BoringSettingViewModel.Instance.SaveLayerNames(BoringViewModel.Instance.LayerNames);
                ObservableCollection<System.Windows.Media.Color> Colors = new ObservableCollection<System.Windows.Media.Color>();
                for (int i = 0; i < BoringViewModel.Instance.LayerNames.Count; i++)
                {
                    var color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                    Colors.Add(color);
                }
            }
            DataContext = BoringSettingViewModel.Instance;
            ClosedByOkBtn += ClosedByOkBtn_Process;
            ClosedByCancelBtn += ClosedByCancelBtn_Process;
        }

        //색상 설정
        private void btnSetColor_Click(object sender, RoutedEventArgs e)
        {
            //클릭된 버튼 찾기
            Button clickedButton = sender as Button;
            WinForm.ColorDialog dlg = new WinForm.ColorDialog();
            if (dlg.ShowDialog() == WinForm.DialogResult.OK)
            {
                var pickedColor = dlg.Color;
                var applyColor = System.Windows.Media.Color.FromRgb(pickedColor.R, pickedColor.G, pickedColor.B);
                //클릭한 버튼의 색상 바꾸기
                clickedButton.Background = new SolidColorBrush(applyColor);
            }
        }

        //닫을때 취소버튼 누른지 아닌지 확인하기
        private void IsClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ToSaveValues)
            {
                //레이어 설정값 불러오기
                List<string> layerNameList = dgBoringSettings.Items.OfType<string>().ToList();
                ObservableCollection<string> layerNames = new ObservableCollection<string>();
                foreach(string name in layerNameList)
                {
                    layerNames.Add(name);
                }
                var buttons = dgBoringSettings.Items.OfType<Button>().ToList();
                ObservableCollection<System.Windows.Media.Color> layerColorList = new ObservableCollection<System.Windows.Media.Color>();
                foreach(var button in buttons)
                {
                    layerColorList.Add(button.Background);
                }

                //레이어 설정값 저장하기
                BoringSettingViewModel.Instance.SaveLayerNames(layerNames);
                BoringSettingViewModel.Instance.SaveLayerColors(layerColorList);
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            ClosedByOkBtn?.Invoke(sender, e);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosedByCancelBtn?.Invoke(sender, e);
        }

        //확인 버튼으로 종료시
        public event EventHandler ClosedByOkBtn;    
        public void ClosedByOkBtn_Process(object sender, EventArgs e)
        {
            ToSaveValues = true;
            wdBoringSetting.Close();
        }

        //확인 버튼으로 종료시
        public event EventHandler ClosedByCancelBtn;    
        public void ClosedByCancelBtn_Process(object sender, EventArgs e)
        {
            ToSaveValues = false;
            wdBoringSetting.Close();
        }
    }
}
