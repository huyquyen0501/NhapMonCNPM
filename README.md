Mỗi người nhận task nào trên jira thì tự assign mình vào, sau đấy tạo nhánh tên giống task đã nhận, khi code xong thì đẩy lên rồi merge vào nhánh qa rồi kéo code sang nhánh code review.

Tester tương tự test task trên cột code review sang testing, test không thấy lỗi thì kéo sang done, có bug thì note ở comment lại rồi kéo sang todo.

Hàng ngày pull code từ qa về tránh conflict.

Khi merge code thì merge vào nhánh qa, tester pull code từ nhánh qa về test.

link jira: https://bbqhotpot.atlassian.net/



# Important

Issues of this repository are tracked on https://github.com/aspnetboilerplate/aspnetboilerplate. Please create your issues on https://github.com/aspnetboilerplate/aspnetboilerplate/issues.

# Introduction

This is a template to create **ASP.NET Core MVC / Angular** based startup projects for [ASP.NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents). It has 2 different versions:

1. [ASP.NET Core MVC & jQuery](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core) (server rendered multi-page application).
2. [ASP.NET Core & Angular](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular) (single page application).
 
User Interface is based on [BSB Admin theme](https://github.com/gurayyarar/AdminBSBMaterialDesign).
 
# Download

Create & download your project from https://aspnetboilerplate.com/Templates

# Screenshots

#### Sample Dashboard Page
![](_screenshots/module-zero-core-template-ui-home.png)

#### User Creation Modal
![](_screenshots/module-zero-core-template-ui-user-create-modal.png)

#### Login Page

![](_screenshots/module-zero-core-template-ui-login.png)

# Documentation

* [ASP.NET Core MVC & jQuery version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core)
* [ASP.NET Core & Angular  version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular)

# License

[MIT](LICENSE).
