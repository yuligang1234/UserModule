
define(function (require, exports, module) {


    //#region 键盘按钮事件

    //禁用退格键返回上一页且输入框能正常使用
    exports.BackSapce = function (event) {
        var e = event || window.event || arguments.callee.caller.arguments[0];
        var d = e.srcElement || e.target;
        if (e && e.keyCode == 8) {
            return d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA' ? true : false;
        }
        return true;
    };

    //#endregion

});