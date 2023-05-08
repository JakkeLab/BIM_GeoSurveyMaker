using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using GeoSurveyRVT.Commands;
using GeoSurveyRVT.Model;
using GeoSurveyRVT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace GeoSurveyRVT.DockablePaneUI
{

    /// <summary>
    /// Interaction logic for MyDockablePane.xaml
    /// </summary>
    public partial class ShowBoringTable : Page, IDockablePaneProvider
    {
        private ExternalEvent externalEvent;
        public ShowBoringTable()
        {
            InitializeComponent();
            DataContext = BoringViewModel.Instance;
            externalEvent = ExternalEvent.Create(new CreateBoringFamily());
        }

        // IDockablePaneProvider abstrat method
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            // wpf object with pane's interface
            data.FrameworkElement = this as FrameworkElement;
            // initial state position
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };
        }

        //클릭할때 바로 행 선택
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = sender as DataGridCell;
            if (cell != null && cell.Column is DataGridCheckBoxColumn && !cell.IsEditing)
            {
                e.Handled = true;
                var cb = cell.Content as CheckBox;
                if (cb != null)
                {
                    cb.IsChecked = !cb.IsChecked;
                }
            }
        }

        //체크박스 처리
        //헤더에서 체크할경우
        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dgBorings.Items)
            {
                var row = dgBorings.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var cell = dgBorings.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    var cb = cell.Content as CheckBox;
                    if (cb != null && !cb.IsChecked.Value)
                    {
                        cb.IsChecked = true;
                    }
                }
            }
        }

        //헤더에서 체크해제할경우
        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dgBorings.Items)
            {
                var row = dgBorings.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var cell = dgBorings.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    var cb = cell.Content as CheckBox;
                    if (cb != null && cb.IsChecked.Value)
                    {
                        cb.IsChecked = false;
                    }
                }
            }
        }

        //상세보기 버튼
        private void btBoringDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgBoringDetail.Visibility == System.Windows.Visibility.Visible)
                dgBoringDetail.Visibility = System.Windows.Visibility.Hidden;
            else
                dgBoringDetail.Visibility = System.Windows.Visibility.Visible;
        }

        //상세보기 업데이트
        private void dgBorings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgBorings.SelectedItem is Boring selectedItem)
            {
                dgBoringDetail.UpdateDataGrid(selectedItem.GroundLayers);
            }
        }

        //ESC키 누를때 선택 해제
        private void dgBorings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                var dataGrid = sender as DataGrid;
                if(dataGrid != null)
                {
                    dataGrid.SelectedItem = null;
                    dgBoringDetail.ClearLayers();
                }
            }
        }

        //빈공간 클릭시 해제
        private void dgBorings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                var columnHeader = UIHelpers.TryFindFromPoint<DataGridColumnHeader>(dataGrid, e.GetPosition(dataGrid));
                if (columnHeader != null && columnHeader.Column.DisplayIndex == 0)
                {
                    // 첫번째 열의 헤더를 클릭한 경우, 이벤트 처리하지 않고 리턴.
                    return;
                }
                //그외 열선택
                var row = UIHelpers.TryFindFromPoint<DataGridRow>(dataGrid, e.GetPosition(dataGrid));
                if (row == null)
                {
                    dataGrid.SelectedItem = null;
                    dgBoringDetail.ClearLayers();
                    e.Handled = true;
                }
            }
        }

        //점 선택
        private void btnSetPosition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //클릭된 버튼 얻기
                Button clickedButton = sender as Button;

                //클릭한 버튼을 가지고 있는 데이터그리드 획득
                DataGridRow row = null;
                DependencyObject parent = VisualTreeHelper.GetParent(clickedButton);
                while(parent !=null && row == null)
                {
                    row = parent as DataGridRow;
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if(row != null)
                {
                    //rowIndex행, colIndex열의 값 찾기
                    int rowIndex = row.GetIndex();
                    int colIndex = dgBorings.Columns.FirstOrDefault(x => x.Header.ToString() == "이름").DisplayIndex;
                    int statusCol = dgBorings.Columns.FirstOrDefault(x => x.Header.ToString() == "Status").DisplayIndex;

                    //DataGrid의 ItemSource 가져오기
                    var data = dgBorings.ItemsSource as IList<Boring>;

                    //셀값 가져오기
                    var rowData = data[rowIndex];
                    var selectedBoringName = rowData.BoringName;

                    //점선택 시작
                    UIApplication uiApp = App.MainUIApplication;
                    var uiDoc = uiApp.ActiveUIDocument;
                    XYZ pickedPoint = uiDoc.Selection.PickPoint(ObjectSnapTypes.Intersections, "점을 선택하세요.");
                    if (pickedPoint != null)
                    {
                        //점 할당
                        var item = BoringViewModel.Instance.ImportedBoringSet.Borings.FirstOrDefault(x => x.BoringName == selectedBoringName);
                        item.BoringLocation.X = pickedPoint.X;
                        item.BoringLocation.Y = pickedPoint.Y;

                        //Status에 표시
                        data[rowIndex].SetPoint = true;
                        dgBorings.Items.Refresh();
                    }
                }

            }
            catch (Exception ex)
            {
                TaskDialog.Show("오류", $"Error \n{ex}");
            }
        }
        
        //시추공 일괄 생성
        private void btCreateBorings_Click(object sender, RoutedEventArgs e)
        {
            CreateBoringFamily.boringData = BoringViewModel.Instance.ImportedBoringSet.Borings[0];
            externalEvent.Raise();
            //foreach (var item in BoringViewModel.Instance.ImportedBoringSet.Borings)
            //{
            //    CreateBoringFamily.boringData = item;
            //    externalEvent.Raise();
            //}
        }
    }

    //클릭한 곳의 좌표 추적
    public static class UIHelpers
    {
        public static T TryFindFromPoint<T>(UIElement element, Point point) where T : DependencyObject
        {
            var hitTestResult = VisualTreeHelper.HitTest(element, point);
            if (hitTestResult == null) return null;

            var visual = hitTestResult.VisualHit;
            while (visual != null && !(visual is T))
                visual = VisualTreeHelper.GetParent(visual);

            return visual as T;
        }
    }
}
