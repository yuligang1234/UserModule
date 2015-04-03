define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");

    //加载用户信息列表
    exports.LoadTrees = function (projectId, ruleId) {
        var url;
        url = '/RulePermit/LoadTree?projectId=' + projectId + '&ruleId=' + ruleId;
        easyui.LoadTree('#treeTool', url, true, true);
    };

    //设置页面
    exports.LoadBody = function () {
        var width = document.documentElement.clientWidth;
        $('#searchTool').css('width', width - "22" + "px");
        $('#treeTool').css('width', width - "22" + "px");
    };

    //保存
    exports.SaveRule = function (projectId, ruleId) {
        var arrays, json = "";
        var nodes = $('#treeTool').tree('getChecked', ['checked', 'indeterminate']);
        //拼接数据成json
        for (var i = 0; i < nodes.length; i++) {
            arrays = new Object();
            /*if (nodes[i].parentId === "0") {
                arrays.RuleId = ruleId;
                arrays.MenuId = nodes[i].id;
                arrays.OperationId = nodes[i].parentId;
                arrays.ProjectId = projectId;
            }*/
            if (nodes[i].children === undefined) {
                arrays.MenuId = nodes[i].parentId;
                arrays.OperationId = nodes[i].id;
            } else {
                arrays.MenuId = nodes[i].id;
                arrays.OperationId = nodes[i].parentId;
            }
            arrays.RuleId = ruleId;
            arrays.ProjectId = projectId;
            json = json + JSON.stringify(arrays) + ",";
        }
        json = "[" + json.substring(0, json.length - 1) + "]";
        $.ajax({
            url: '/RulePermit/SaveRule',
            data: { json: encodeURI(json) },
            type: 'post',
            complete: function (data) {
                switch (data.responseText.substring(0, 4)) {
                    case "更新成功":
                        parent.window.$.messager.confirm('提示', "更新成功，是否重新登陆系统！", function (r) {
                            if (r) {
                                $.ajax({
                                    url: '/Login/LoginOut',
                                    complete: function () {
                                        top.window.location.href = '/Login/Index';
                                    }
                                });
                            }
                        });
                        break;
                    case "更新失败":
                        parent.window.$.messager.alert('提示', data.responseText, 'info');
                        break;
                    default:
                        parent.window.$.messager.alert('提示', "更新失败！", 'info');
                        break;
                }
            }
        });
    };

});