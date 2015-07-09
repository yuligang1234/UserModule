define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");
    var pubJs = require("../PublicFunc/Index.js");

    //加载用户信息列表
    exports.LoadGrid = function (projectId) {
        var url, gridColumns, title;
        url = '/Rule/LoadRuleGrid?projectId=' + projectId;
        title = '角色列表';
        gridColumns = [
            { field: 'Name', title: '角色名称', halign: 'center', align: 'left', width: 180 },
            { field: 'Id', title: '角色节点', halign: 'center', align: 'center', width: 200 },
            { field: 'ParentId', title: '角色父节点', halign: 'center', align: 'center', width: 200 },
            { field: 'Sort', title: '顺序', halign: 'center', align: 'center', width: 100 },
            { field: 'Remark', title: '备注', halign: 'center', align: 'center', width: 200 }
        ];
        easyui.LoadTreeGrid("#gridTool", url, gridColumns, 'Id', 'Name', title);
    };

    //设置页面
    exports.LoadBody = function (bottom) {
        var height = document.documentElement.clientHeight;
        var width = document.documentElement.clientWidth;
        var bottomHeight = bottom === undefined ? 65 : bottom;
        $('#searchTool').css('width', width - "4" + "px");
        easyui.ResizeTreeGrid("#gridTool", height - bottomHeight, width);
    };

    //新增
    exports.FixedAdd = function (projectId) {
        var url = '/Rule/Add?projectId=' + projectId + '&randId=' + Math.random();
        easyui.ShowWindow('#myWindow', '新增角色', url, '700', '300');
    };

    //保存新增
    exports.SaveRule = function () {
        $('#AddRuleForm').form('submit', {
            url: '/Rule/SaveRule',
            success: function (data) {
                var json = pubJs.DeserializeJson(data);
                switch (data.substring(0, 4)) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        parent.window.$('#gridTool').treegrid('reload');
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
            var url = '/Rule/Edit?id=' + row.Id + '&randId=' + Math.random();
            easyui.ShowWindow('#myWindow', '角色编辑', url, '700', '300');
        }
    };

    //更新
    exports.UpdateRule = function () {
        $('#UpdateRuleForm').form('submit', {
            url: '/Rule/UpdateRule',
            success: function (data) {
                var json = pubJs.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        parent.window.$('#gridTool').treegrid('reload');
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
                    url: '/Rule/DeleteRule',
                    data: { id: row.Id },
                    type: 'post',
                    complete: function (data) {
                        var json = pubJs.DeserializeJson(data.responseText);
                        switch (json.Status) {
                            case "success":
                                parent.window.$.messager.alert('提示', json.Msg, 'info');
                                $('#gridTool').treegrid('reload');
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

    //角色权限
    exports.FixedRulePermit = function () {
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请选择需要操作的数据行！', 'info');
            return;
        }
        if (row.ParentId === "0") {
            parent.window.$.messager.alert('提示', '请不要操作父菜单！', 'info');
            return;
        }
        var url = '/RulePermit/Index?projectId=' + row.ProjectId + '&ruleId=' + row.Id + '&randId=' + Math.random();
        easyui.ShowWindow('#myWindow', '分配角色权限', url, '700', '420');
    };

});