// �������� ���������� ������������ WebApplicationBuilder, ������� �������� ��������� ����������������, ��������������, �������, ���������� �������� ����������
var builder = WebApplication.CreateBuilder(args);

// ��������������� ����������� ����� � ������������ � ������� WebApplicationBuilder � OS
builder.Services.AddControllersWithViews();

// ����� ������ Build() � ���������� ������������, ����� ������� ��������� WebApplication
var app = builder.Build();

// ���������� �������������� �� � ���-����������, ����� ������� ��������;



// ������������� �������� ����� ����������;
//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//MapDefaultControllerRoute(). // ������������� ������� ����� �������� ������������� �����

// ����� ������ Run() � ���-����������, ����� ��������� ������ � �������� ������� �������
app.Run();
