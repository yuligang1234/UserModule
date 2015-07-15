define(function (require, exports, module) {

    var easyui = require("../PublicJs/Frame/Easyui.js");
    var serialize = require("../PublicJs/Format/SerializeFunc.js");

    //加载用户信息列表
    exports.LoadGrid = function (projectId) {
        var url, gridColumns, title;
        url = '/Menu/LoadMenuGrid?projectId=' + projectId;
        title = '菜单列表';
        gridColumns = [
            { field: 'Name', title: '菜单名称', halign: 'center', align: 'left', width: 180 },
            { field: 'Id', title: '菜单节点', halign: 'center', align: 'center', width: 200 },
            { field: 'ParentId', title: '菜单父节点', halign: 'center', align: 'center', width: 200 },
            { field: 'Url', title: '链接地址', halign: 'center', align: 'center', width: 100 },
            { field: 'Icon', title: '菜单图标', halign: 'center', align: 'center', width: 100 },
            { field: 'Sort', title: '顺序', halign: 'center', align: 'center', width: 100 },
            { field: 'Remark', title: '备注', halign: 'center', align: 'center', width: 100 }
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
        var url = '/Menu/Add?projectId=' + projectId + '&randId=' + Math.random();
        easyui.ShowWindow('#myWindow', '新增菜单', url, '700', '400');
    };

    //保存新增
    exports.SaveMenu = function () {
        $('#AddMenuForm').form('submit', {
            url: '/Menu/SaveMenu',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
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

    //编辑
    exports.FixedEdit = function () {
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要编辑的数据行！', 'info');
        } else {
            var url = '/Menu/Edit?id=' + row.Id + '&randId=' + Math.random();
            easyui.ShowWindow('#myWindow', '菜单编辑', url, '700', '400');
        }
    };

    //更新
    exports.UpdateMenu = function () {
        $('#UpdateMenuForm').form('submit', {
            url: '/Menu/UpdateMenu',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
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
                    url: '/Menu/DeleteMenu',
                    data: { id: row.Id },
                    type: 'post',
                    complete: function (data) {
                        var json = serialize.DeserializeJson(data.responseText);
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

    //选择图片
    exports.CheckImg = function () {
        easyui.ShowWindow('#myWindow', '选择图片', '/Icons/MenuIcon', '440', '300');
    };

});