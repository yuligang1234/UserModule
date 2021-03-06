﻿
define(function (require, exports, module) {

    var easyui = require("../PublicJs/Frame/Easyui.js");
    var serialize = require("../PublicJs/Format/SerializeFunc.js");

    //加载权限
    exports.LoadOperate = function (selector) {
        var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");
        easyui.LoadOperate(selector, iframeid);
    };

    //加载用户信息列表
    exports.LoadGrid = function () {
        var url, gridColumns, title;
        url = '/Project/LoadProjectGrid';
        title = '系统列表';
        gridColumns = [
            { field: 'ProjectId', title: '系统代码', halign: 'center', align: 'center', width: 100 },
            { field: 'ProjectName', title: '系统名称', halign: 'center', align: 'center', width: 100 },
            { field: 'Remark', title: '备注', halign: 'center', align: 'center', width: 300 },
            { field: 'Operator', title: '操作者', halign: 'center', align: 'center', width: 100 }
        ];
        easyui.LoadPageDataGrid("#gridTool", url, gridColumns, title, true, undefined, undefined, undefined);
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
        var projectId = $('#projectId').val();
        var projectName = $('#projectName').val();
        var parameters = { projectId: projectId, projectName: projectName, rows: 20, page: 1 };
        easyui.ReloadDataGrid("#gridTool", parameters);
    };

    //新增
    exports.FixedAdd = function () {
        easyui.ShowParentWindow('#myWindow', '新增系统', '/Project/Add', '420', '300');
    };

    //保存新增
    exports.SaveAdd = function () {
        $('#AddProjectForm').form('submit', {
            url: '/Project/SaveAdd',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
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
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要编辑的数据行！', 'info');
        } else {
            var url = '/Project/Edit?projectId=' + row.ProjectId + '&randId=' + Math.random();
            easyui.ShowParentWindow('#myWindow', '系统编辑', url, '420', '340');
        }
    };

    //更新
    exports.UpdateProject = function () {
        $('#UpdateProjectForm').form('submit', {
            url: '/Project/UpdateProject',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
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
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请选择需要删除的数据！', 'info');
            return;
        }
        parent.window.$.messager.confirm('提示', '确定是否删除！', function (result) {
            if (result) {
                $.ajax({
                    url: '/Project/DeleteProject',
                    data: { projectId: row.ProjectId },
                    type: 'post',
                    complete: function (data) {
                        var json = serialize.DeserializeJson(data.responseText);
                        switch (json.Status) {
                            case "success":
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                $('#gridTool').datagrid('reload');
                                break;
                            default :
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                break;
                        }
                    }
                });
            }
        });

    };

    //菜单
    exports.FixedMenu = function () {
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要操作菜单的数据行！', 'info');
        } else {
            var url = '/Menu/Index?projectId=' + row.ProjectId + '&randId=' + Math.random();
            easyui.ShowParentWindow('#myWindow', '菜单列表', url, '1000', '500');
        }
    };

    //角色
    exports.FixedRule = function () {
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要操作角色的数据行！', 'info');
        } else {
            var url = '/Rule/Index?projectId=' + row.ProjectId + '&randId=' + Math.random();
            easyui.ShowParentWindow('#myWindow', '角色列表', url, '1000', '500');
        }
    };


});