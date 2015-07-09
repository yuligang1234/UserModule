define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");
    var pubJs = require("../PublicFunc/Index.js");

    //加载用户信息列表
    exports.LoadTrees = function (projectId, ruleId) {
        var url;
        url = '/RulePermit/LoadTree?projectId=' + projectId + '&ruleId=' + ruleId + '&randId=' + Math.random();
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
                var jsons = pubJs.DeserializeJson(data.responseText);
                switch (jsons.Status) {
                    case "success":
                        parent.window.$.messager.confirm('提示', jsons.Msg, function (r) {
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
                    default:
                        parent.window.$.messager.alert('提示', jsons.Msg, 'info');
                        break;
                }
            }
        });
    };

});