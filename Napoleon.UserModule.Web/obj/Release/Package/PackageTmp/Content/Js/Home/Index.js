define(function (require, exports, module) {

    var easyui = require('../PublicFunc/Easyui.js');

    //退出
    exports.LoginOut = function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {
            if (r) {
                $.ajax({
                    url: '/Login/LoginOut',
                    complete: function () {
                        top.window.location.href = '/Login/Index';
                    }
                });
            }
        });
    };

    //修改密码
    exports.ChangePassWord = function () {
        easyui.ShowWindow('#myWindow', '修改密码', '/Home/ChangePw', '420', '320');
    };

    //保存密码
    exports.SavePw = function () {
        $('#pwForm').form('submit', {
            url: '/User/SaveUser',
            success: function (data) {
                switch (data.substring(0, 4)) {
                    case "修改成功":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', data, 'info', function () {
                            $.ajax({
                                url: '/Login/LoginOut',
                                complete: function () {
                                    top.window.location.href = '/Login/Index';
                                }
                            });
                        });
                        break;
                    case "修改失败":
                        parent.window.$.messager.alert('提示', data, 'info');
                        break;
                    case "<!DO":
                        document.write(data);
                        break;
                }
            }
        });
    };

});