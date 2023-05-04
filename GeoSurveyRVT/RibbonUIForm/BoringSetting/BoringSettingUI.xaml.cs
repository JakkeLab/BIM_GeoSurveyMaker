using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WinForm = System.Windows.Forms;
using System.Windows.Media;
using GeoSurveyRVT.ViewModel;
using GeoSurveyRVT.UIViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using GeoSurveyRVT.Model;

namespace GeoSurveyRVT.RibbonUIForm.BoringSetting
{
    public partial class BoringSettingUI : Window
    {
        public bool ToSaveValues;
        public BoringSettingUI()
        {
            InitializeComponent();
            
            //처음 켤때
            if(BoringSettingViewModel.Instance.LayerInfos.Count == 0)
            {
                ObservableCollection<LayerInfo> layerInfos = new ObservableCollection<LayerInfo>();
                foreach(var name in BoringViewModel.Instance.LayerNames)
                {
                    LayerInfo layerInfo = new LayerInfo();
                    layerInfo.LayerName = name;
                    layerInfo.LayerColor = System.Windows.Media.Color.FromRgb(0, 0, 0);
                    layerInfos.Add(layerInfo);
                }
                BoringSettingViewModel.Instance.SaveLayerInfo(layerInfos);
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
                //클릭한 버 튼의 색상 바꾸기
                clickedButton.Background = new SolidColorBrush(applyColor);
            }
        }

        //닫을때 취소버튼 누른지 아닌지 확인하기
        private void IsClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ToSaveValues)
            {
                //레이어 이름 불러오기
                List<string> layerNameList = dgBoringSettings.Items.OfType<LayerInfo>().Select(x => x.LayerName).ToList();
                //레이어 색상 불러오기
                ObservableCollection<System.Windows.Media.Color> layerColorList = new ObservableCollection<System.Windows.Media.Color>();
                //헤더가 "색상"인 열에 생성된 버튼에서 색을 가져온 후 layerColorList에 할당

                for (int i = 0; i < dgBoringSettings.Items.Count; i++)
                {
                    DataGridRow row = (DataGridRow)dgBoringSettings.ItemContainerGenerator.ContainerFromIndex(i);
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                        DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1);
                        ContentPresenter contentPresenter = cell.Content as ContentPresenter;
                        Button colorButton = (Button)contentPresenter.ContentTemplate.FindName("btnSetColor", contentPresenter);
                        SolidColorBrush buttonBackground = (SolidColorBrush)colorButton.Background;
                        System.Windows.Media.Color color = new Color();
                        if (buttonBackground != null)
                        {
                            color = buttonBackground.Color;
                        }
                        else
                        {
                            color = Color.FromRgb(0,0,0);
                        }
                        layerColorList.Add(color);
                    }
                }

                ObservableCollection<LayerInfo> layerInfos = new ObservableCollection<LayerInfo>();
                for (int i = 0; i < dgBoringSettings.Items.Count; i++)
                {
                    LayerInfo layerInfo = new LayerInfo();

                    //레이어 이름 설정
                    layerInfo.LayerName = layerNameList[i];
                    layerInfo.LayerColor = layerColorList[i];
                    layerInfos.Add(layerInfo);
                }

                //레이어 설정값 저장하기
                BoringSettingViewModel.Instance.SaveLayerInfo(layerInfos);
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

        // GetVisualChild helper method
        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
