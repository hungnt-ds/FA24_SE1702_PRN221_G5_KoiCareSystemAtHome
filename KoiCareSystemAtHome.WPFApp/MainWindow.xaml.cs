using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KoiCareSystemAtHome.Data;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service;

namespace KoiCareSystemAtHome.WPFApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private KoiFishService _koiFishService;
    private UserService _userService;
    private PondService _pondService;

    public MainWindow()
    {
        InitializeComponent();
        _koiFishService ??= new KoiFishService();
        _userService ??= new UserService();
        _pondService ??= new PondService();
        LoadData();
    }

    private async void LoadData()
    {
        // Load Pond Data
        var pondResult = (await _pondService.GetAll()).Data as List<Pond>;

        cboPondId.ItemsSource = pondResult;
        cboPondId.DisplayMemberPath = "PondName";  // Cột hiển thị trong ComboBox
        cboPondId.SelectedValuePath = "PondId";    // Giá trị thực tế khi chọn

        // Load User Data
        var userResult = (await _userService.GetAll()).Data as List<User>;

        cboUserId.ItemsSource = userResult;
        cboUserId.DisplayMemberPath = "Email";  // Cột hiển thị trong ComboBox
        cboUserId.SelectedValuePath = "Id";    // Giá trị thực tế khi chọn

        // Load Koi Fish Data vào DataGrid
        var koiFishResult = (await _koiFishService.GetAll()).Data as List<KoiFish>;

        KoiFishDataGrid.ItemsSource = koiFishResult;
    }

    private async void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        // Lấy giá trị từ 3 ô input
        string fishName = FishNameSearchBox.Text.Trim();
        string pondName = PondNameSearchBox.Text.Trim();
        string userName = EmailSearchBox.Text.Trim();

        // Gọi service để tìm kiếm
        var searchResult = (await _koiFishService.SearchKoiFishAsync(fishName, pondName, userName)).Data as List<KoiFish>;

        KoiFishDataGrid.ItemsSource = searchResult;

    }

    //private async void SaveButton_Click(object sender, RoutedEventArgs e)
    //{
    //    // Tạo đối tượng koiFish từ dữ liệu của các TextBox và ComboBox
    //    // Chuyển đổi Size và Weight từ string sang decimal?
    //    decimal? size = null;
    //    decimal? weight = null;

    //    if (decimal.TryParse(txtSize.Text, out var parsedSize))
    //    {
    //        size = parsedSize;
    //    }

    //    if (decimal.TryParse(txtWeight.Text, out var parsedWeight))
    //    {
    //        weight = parsedWeight;
    //    }

    //    var koiFish = new KoiFish
    //    {
    //        FishName = txtFishName.Text,
    //        PondId = (long)cboPondId.SelectedValue,  
    //        UserId = (long)cboUserId.SelectedValue,  
    //        ImageUrl = txtImageUrl.Text,
    //        BodyShape = txtBodyShape.Text,
    //        Age = int.Parse(txtAge.Text),
    //        Size = size,
    //        Weight = weight,
    //        Gender = txtGender.Text,
    //        Breed = txtBreed.Text,
    //        Origin = txtOrigin.Text,
    //        Price = decimal.Parse(txtPrice.Text),
    //        CreatedAt = DateTime.Now,
    //        UpdatedAt = DateTime.Now,
    //    };

    //    // Gọi service để lưu dữ liệu
    //    var result = await _koiFishService.Update(koiFish);

    //    if (result.Status == 1)
    //    {
    //        MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    //        // Sau khi lưu thành công có thể thực hiện refresh lại DataGrid
    //        LoadData();
    //    }
    //    else
    //    {
    //        MessageBox.Show("Lưu dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
    //    }
    //}

    //private async void SaveButton_Click(object sender, RoutedEventArgs e)
    //{
    //    // Lấy dòng được chọn từ DataGrid (nếu đang chọn)
    //    var selectedKoiFish = KoiFishDataGrid.SelectedItem as KoiFish;

    //    if (selectedKoiFish != null)
    //    {
    //        // Cập nhật các giá trị từ các input
    //        selectedKoiFish.FishName = txtFishName.Text;
    //        selectedKoiFish.ImageUrl = txtImageUrl.Text;
    //        selectedKoiFish.BodyShape = txtBodyShape.Text;
    //        selectedKoiFish.Age = int.Parse(txtAge.Text);
    //        selectedKoiFish.Size = decimal.Parse(txtSize.Text);
    //        selectedKoiFish.Weight = decimal.Parse(txtWeight.Text);
    //        selectedKoiFish.Gender = txtGender.Text;
    //        selectedKoiFish.Breed = txtBreed.Text;
    //        selectedKoiFish.Origin = txtOrigin.Text;
    //        selectedKoiFish.Price = decimal.Parse(txtPrice.Text);

    //        // Cập nhật UserId và PondId từ ComboBox
    //        selectedKoiFish.UserId = (long)cboUserId.SelectedValue;
    //        selectedKoiFish.PondId = (long)cboPondId.SelectedValue;

    //        // Gọi service để cập nhật dữ liệu
    //        var result = await _koiFishService.Update(selectedKoiFish);
    //        var result2 = await _koiFishService.Save(selectedKoiFish);

    //        if (result.Status == 1)
    //        {
    //            MessageBox.Show("Update successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    //        }
    //        else
    //        {
    //            MessageBox.Show($"Update failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    //        }

    //        // Tải lại dữ liệu vào DataGrid sau khi cập nhật
    //        KoiFishDataGrid.ItemsSource = (await _koiFishService.GetAll()).Data as List<KoiFish>;
    //    }
    //    else
    //    {
    //        MessageBox.Show("Please select a fish to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    //    }
    //}

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Kiểm tra xem có dòng nào được chọn trong DataGrid hay không
        var selectedKoiFish = KoiFishDataGrid.SelectedItem as KoiFish;

        if (selectedKoiFish == null)
        {
            // Nếu không có dòng nào được chọn -> Thực hiện thêm mới
            var newKoiFish = new KoiFish
            {
                FishName = txtFishName.Text,
                ImageUrl = txtImageUrl.Text,
                BodyShape = txtBodyShape.Text,
                Age = int.Parse(txtAge.Text),
                Size = decimal.Parse(txtSize.Text),
                Weight = decimal.Parse(txtWeight.Text),
                Gender = txtGender.Text,
                Breed = txtBreed.Text,
                Origin = txtOrigin.Text,
                Price = decimal.Parse(txtPrice.Text),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = (long)cboUserId.SelectedValue,
                PondId = (long)cboPondId.SelectedValue
            };

            // Gọi service để thêm dữ liệu mới
            var result = await _koiFishService.Save(newKoiFish);

            if (result.Status == 1)
            {
                MessageBox.Show("Data added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Failed to add data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            // Xử lý cập nhật dữ liệu
            try
            {
                // Lấy entity từ service (cập nhật entity từ database)
                var koiFishToUpdate = (await _koiFishService.GetById(selectedKoiFish.FishId)).Data as KoiFish;

                // Cập nhật thuộc tính của entity
                koiFishToUpdate.FishName = txtFishName.Text;
                koiFishToUpdate.ImageUrl = txtImageUrl.Text;
                koiFishToUpdate.BodyShape = txtBodyShape.Text;
                koiFishToUpdate.Age = int.Parse(txtAge.Text);
                koiFishToUpdate.Size = decimal.Parse(txtSize.Text);
                koiFishToUpdate.Weight = decimal.Parse(txtWeight.Text);
                koiFishToUpdate.Gender = txtGender.Text;
                koiFishToUpdate.Breed = txtBreed.Text;
                koiFishToUpdate.Origin = txtOrigin.Text;
                koiFishToUpdate.Price = decimal.Parse(txtPrice.Text);
                koiFishToUpdate.UserId = (long)cboUserId.SelectedValue;
                koiFishToUpdate.PondId = (long)cboPondId.SelectedValue;

                // Gọi service để cập nhật dữ liệu
                var result = await _koiFishService.Save(koiFishToUpdate);

                if (result.Status == 1)
                {
                    MessageBox.Show("Data updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to update data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Tải lại dữ liệu vào DataGrid sau khi thêm hoặc cập nhật
        KoiFishDataGrid.ItemsSource = (await _koiFishService.GetAll()).Data as List<KoiFish>;
    }



    private async Task KoiFishDataGrid_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
    {
        //// Lấy dòng được chọn từ DataGrid
        //var selectedKoiFish = KoiFishDataGrid.SelectedItem as KoiFish;

        //if (selectedKoiFish != null)
        //{
        //    // Gán giá trị lên các TextBox và ComboBox
        //    txtFishName.Text = selectedKoiFish.FishName;
        //    txtImageUrl.Text = selectedKoiFish.ImageUrl;
        //    txtBodyShape.Text = selectedKoiFish.BodyShape;
        //    txtAge.Text = selectedKoiFish.Age.ToString();
        //    txtSize.Text = selectedKoiFish.Size.ToString();
        //    txtWeight.Text = selectedKoiFish.Weight.ToString();
        //    txtGender.Text = selectedKoiFish.Gender;
        //    txtBreed.Text = selectedKoiFish.Breed;
        //    txtOrigin.Text = selectedKoiFish.Origin;
        //    txtPrice.Text = selectedKoiFish.Price.ToString();

        //    // Gán giá trị vào ComboBox (User và Pond)
        //    cboUserId.SelectedValue = selectedKoiFish.UserId;
        //    cboPondId.SelectedValue = selectedKoiFish.PondId;
        //}

        DataGrid grd = sender as DataGrid;
        if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
        {
            var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
            if (row != null)
            {
                var koiFish = row.Item as KoiFish;
                if (koiFish != null)
                {
                    var koiFishResult = await _koiFishService.GetById(koiFish.FishId);
                    if (koiFishResult.Status > 0 && koiFishResult.Data != null)
                    {
                        koiFish = koiFishResult.Data as KoiFish;
                        //Gán giá trị lên các TextBox và ComboBox
                        txtFishName.Text = koiFish.FishName;
                        txtImageUrl.Text = koiFish.ImageUrl;
                        txtBodyShape.Text = koiFish.BodyShape;
                        txtAge.Text = koiFish.Age.ToString();
                        txtSize.Text = koiFish.Size.ToString();
                        txtWeight.Text = koiFish.Weight.ToString();
                        txtGender.Text = koiFish.Gender;
                        txtBreed.Text = koiFish.Breed;
                        txtOrigin.Text = koiFish.Origin;
                        txtPrice.Text = koiFish.Price.ToString();

                        // Gán giá trị vào ComboBox (User và Pond)
                        cboUserId.SelectedValue = koiFish.UserId;
                        cboPondId.SelectedValue = koiFish.PondId;
                    }
                }
            }
        }
    }

    private async void KoiFishDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DataGrid grd = sender as DataGrid;
        if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
        {
            var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
            if (row != null)
            {
                var koiFish = row.Item as KoiFish;
                if (koiFish != null)
                {
                    var koiFishResult = await _koiFishService.GetById(koiFish.FishId);
                    if (koiFishResult.Status > 0 && koiFishResult.Data != null)
                    {
                        koiFish = koiFishResult.Data as KoiFish;
                        //Gán giá trị lên các TextBox và ComboBox
                        txtFishName.Text = koiFish.FishName;
                        txtImageUrl.Text = koiFish.ImageUrl;
                        txtBodyShape.Text = koiFish.BodyShape;
                        txtAge.Text = koiFish.Age.ToString();
                        txtSize.Text = koiFish.Size.ToString();
                        txtWeight.Text = koiFish.Weight.ToString();
                        txtGender.Text = koiFish.Gender;
                        txtBreed.Text = koiFish.Breed;
                        txtOrigin.Text = koiFish.Origin;
                        txtPrice.Text = koiFish.Price.ToString();

                        // Gán giá trị vào ComboBox (User và Pond)
                        cboUserId.SelectedValue = koiFish.UserId;
                        cboPondId.SelectedValue = koiFish.PondId;
                    }
                }
            }
        }
    }


    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        // Xóa chọn dòng trong DataGrid
        KoiFishDataGrid.SelectedItem = null;

        // Làm trống tất cả các ô input
        txtFishName.Text = string.Empty;
        txtImageUrl.Text = string.Empty;
        txtBodyShape.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtSize.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtGender.Text = string.Empty;
        txtBreed.Text = string.Empty;
        txtOrigin.Text = string.Empty;
        txtPrice.Text = string.Empty;

        // Đặt ComboBox về trạng thái mặc định
        cboUserId.SelectedIndex = -1;
        cboPondId.SelectedIndex = -1;
    }

    private void Reset()
    {
        // Xóa chọn dòng trong DataGrid
        KoiFishDataGrid.SelectedItem = null;

        // Làm trống tất cả các ô input
        txtFishName.Text = string.Empty;
        txtImageUrl.Text = string.Empty;
        txtBodyShape.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtSize.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtGender.Text = string.Empty;
        txtBreed.Text = string.Empty;
        txtOrigin.Text = string.Empty;
        txtPrice.Text = string.Empty;

        // Đặt ComboBox về trạng thái mặc định
        cboUserId.SelectedIndex = -1;
        cboPondId.SelectedIndex = -1;
    }
}