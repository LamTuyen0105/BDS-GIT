# Huong dan 
## Cách tạo database
- Kiểm tra tên server ở thư viện RealEstate.Data file appsettings
- Chọn Tools -> Nuget Package Manager -> Package Manager Console. Ở ô Default project chọn RealEstate.Data
- Add-Migration 'ten'
- Update-database
- Thêm csdl file đính kèm
## Viết Api
- Tạo class viewmodel: trong thư viện RealEstate.ViewModels
- Viết xử lý Service: tạo interface Service và gọi interface thông qua class Service trong thư viện RealEstate.Application
- Gọi Api: tạo controller trong thư viện RealEstate.BackendApi
- Lưu ý: Kiểm tra tên server ở thư viện RealEstate.BackendApi file appsettings.Dvelopment.json (thư mục con của appsettings.json).
		 Tạo xử lý Service phải khai báo trong Startup phương thức ConfigureServices
## Chạy Api
- Click chuột phải RealEstate.BackendApi chọn Set as Startup Project sau đó mới build
- Để nhập được dữ liệu phải chọn nút Try it out trong mỗi phương thức
- Hiện tại đã có tài khoản mẫu UserName: admin@gmail.com, Password: Admin@123)
- Đăng ký tài khoản mới (UserName phải đăng ký dạng email 1 email chỉ đăng ký đc 1 lần, Password phải ít nhất 6 ký tự phải có chữ hoa, chữ thường, số, ký tự đặc biệt)