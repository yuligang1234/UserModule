
define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");
    var pubJs = require("../PublicFunc/Index.js");

    //加载权限
    exports.LoadOperate = function (selector) {
        var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");
        easyui.LoadOperate(selector, iframeid);
    };

    //加载用户信息列表
    exports.LoadGrid = function () {
        var url, gridColumns, title;
        url = '/User/LoadDataGrid';
        title = '用户列表';
        gridColumns = [
            { field: 'ck', checkbox: true },
            { field: 'UserName', title: '用户账号', halign: 'center', align: 'center', width: 100 },
            { field: 'RealName', title: '真实姓名', halign: 'center', align: 'center', width: 100 },
            { field: 'MobilePhone', title: '联系电话', halign: 'center', align: 'center', width: 100 },
            { field: 'IsUse', title: '是否启用', halign: 'center', align: 'center', width: 100 },
            { field: 'UserAddress', title: '地址', halign: 'center', align: 'center', width: 200 },
            { field: 'Remark', title: '备注', halign: 'center', align: 'center', width: 100 }
        ];
        easyui.LoadDataGrid("#gridTool", url, gridColumns, title, false, undefined, undefined, undefined);
        //easyui.LoadComboGrid('#mobilePhone', '/User/LoadDataGrids', 800, "Id", "RealName", 'get', columns, true);
    };

    //设置页面
    exports.LoadBody = function (bottom) {
        var height = document.documentElement.clientHeight;
        var width = document.documentElement.clientWidth;
        var bottomHeight = bottom === undefined ? 65 : bottom;
        $('#searchTool').css('width', width - "4" + "px");
        easyui.ResizeDataGrid("#gridTool", height - bottomHeight, width);
    };

    //查询
    exports.FixedSearch = function () {
        var userName = $('#userName').val();
        var realName = $('#realName').val();
        var mobilePhone = $('#mobilePhone').val();
        var parameters = { userName: userName, realName: realName, mobilePhone: mobilePhone, rows: 20, page: 1 };
        easyui.ReloadDataGrid("#gridTool", parameters);
    };

    //详情页面
    exports.FixedInfo = function () {
        var row = $('#gridTool').datagrid('getChecked');
        if (row.length === 0) {
            parent.window.$.messager.alert('提示', '请先选择需要查看详情的数据行！', 'info');
        } else {
            if (row.length > 1) {
                parent.window.$.messager.alert('提示', '只能查看一条数据！', 'info');
            } else {
                var url = '/User/ViewInfo?id=' + row[0].Id + '&randId=' + Math.random();
                easyui.ShowParentWindow('#myWindow', '用户详情', url, '720', '340');
            }
        }
    };

    //新增
    exports.FixedAdd = function () {
        easyui.ShowParentWindow('#myWindow', '新增用户3', '/User/Add', '720', '380');
    };

    //保存新增
    exports.SaveAdd = function () {
        $('#addUserForm').form('submit', {
            url: '/User/SaveAdd',
            success: function (data) {
                var json = pubJs.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').datagrid('reload');//标签页里获取iframe
                        break;
                    default:
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        break;
                }
            }
        });
    };

    //编辑
    exports.FixedEdit = function () {
        var row = $('#gridTool').datagrid('getChecked');
        if (row.length === 0) {
            parent.window.$.messager.alert('提示', '请先选择需要编辑的数据行！', 'info');
        } else {
            if (row.length > 1) {
                parent.window.$.messager.alert('提示', '只能编辑一条数据！', 'info');
            } else {
                var url = '/User/Edit?id=' + row[0].Id + '&randId=' + Math.random();
                easyui.ShowParentWindow('#myWindow', '用户编辑', url, '720', '380');
            }
        }
    };

    //更新
    exports.UpdateUser = function () {
        $('#updateUserForm').form('submit', {
            url: '/User/UpdateUser',
            success: function (data) {
                var json = pubJs.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').datagrid('reload');//标签页里获取iframe
                        break;
                    default:
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        break;
                }
            }
        });
    };

    //删除
    exports.FixedDelete = function () {
        var ids = new Array(), row = $('#gridTool').datagrid('getChecked');
        if (row.length === 0) {
            parent.window.$.messager.alert('提示', '请选择需要删除的数据！', 'info');
            return;
        }
        for (var i = 0; i < row.length; i++) {
            ids[i] = row[i].Id;
        }
        parent.window.$.messager.confirm('提示', '确定是否删除！', function (result) {
            if (result) {
                $.ajax({
                    url: '/User/DeleteUser',
                    data: { id: ids.toString() },
                    type: 'post',
                    complete: function (data) {
                        var json = pubJs.DeserializeJson(data.responseText);
                        switch (json.Status) {
                            case "success":
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                $('#gridTool').datagrid('reload');
                                break;
                            default:
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                break;
                        }
                    }
                });
            }
        });

    };

    //密码初始化
    exports.FixedPassWord = function () {
        var ids = new Array(), row = $('#gridTool').datagrid('getChecked');
        if (row.length === 0) {
            parent.window.$.messager.alert('提示', '请选择需要初始化密码的用户！', 'info');
            return;
        }
        for (var i = 0; i < row.length; i++) {
            ids[i] = row[i].Id;
        }
        parent.window.$.messager.confirm('提示', '确定是否初始化密码！', function (result) {
            if (result) {
                $.ajax({
                    url: '/User/UpdatePassWord',
                    data: { ids: ids.toString() },
                    type: 'post',
                    complete: function (data) {
                        var json = pubJs.DeserializeJson(data.responseText);
                        switch (json.Status) {
                            case "success":
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                break;
                            default:
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                break;
                        }
                    }
                });
            }
        });
    };

    //分配权限
    exports.FixedPermit = function () {
        var row = $('#gridTool').datagrid('getChecked');
        if (row.length === 0) {
            parent.window.$.messager.alert('提示', '请先选择需要分配权限的用户！', 'info');
        } else {
            if (row.length > 1) {
                parent.window.$.messager.alert('提示', '只能选择一个用户！', 'info');
            } else {
                var url = '/User/Permit?userId=' + row[0].Id + '&randId=' + Math.random();
                easyui.ShowParentWindow('#myWindow', '分配权限', url, '720', '340');
            }
        }
    };

})