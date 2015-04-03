
define(function (require, exports, module) {

    //#region 获取Url参数的方法

    exports.GetUrlParameter = function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        if (name === "backUrl") {//返回链接的正则表达式
            reg = new RegExp("(^|&)" + name + "=([^]*)(|$)");
        }
        var url = decodeURI(window.location.search);
        var r = url.substr(1).match(reg); //匹配目标参数
        if (name === "?") {
            return url;
        }
        else if (name === "" || name === undefined || name === null) {
            return decodeURI(window.location.href);
        }
        if (r != null) {
            //在IE9及以下的浏览器中会匹配不到正则表达式
            if (r[2] === "" && name === "backUrl") {
                return unescape(r[0].substr(9));
            }
            return unescape(r[2]);
        }
        return null; //返回参数值
    };

    //#endregion

    //#region 正则判断

    //判断
    var bool = function (value, reg) {
        if (reg.test(value)) {
            return true;
        }
        return false;
    };

    //判空
    exports.RegIsNull = function isNull(value) {
        var reg = /^\s*$/;
        return bool(value, reg);
    };

    //判断是否都是数字
    exports.RegIsNumber = function isNumber(value) {
        var reg = /^([0-9.]+)*$/;
        return bool(value, reg);
    };

    //判断是否都是中文
    exports.RegIsChinese = function isChinese(value) {
        var reg = /^([\u4E00-\u9FA5])*$/;
        return bool(value, reg);
    };

    //#endregion

    //#region json序列化和反序列化    

    //将数组序列化为json格式
    exports.SerializeJson = function serializeJson(strs) {
        return JSON.stringify(strs);
    };

    exports.DeserializeJson = function deserializeJson(json) {
        return eval('(' + json + ')');
    };

    //#endregion

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

    //#region 字符串操作

    //判断当前字符串是否以str开始
    exports.StartWidth = function (value, str) {
        //slice比indexof方法在处理长字符串时效率高
        return value.slice(0, str.length) == str;
    };

    //判断当前字符串是否以str结尾
    exports.EndWidth = function (value, str) {
        return value.slice(-str.length) == str;
    };

    //替换字符串
    exports.ReplaceJs = function (value, oldStr, newStr) {
        var reg = new RegExp(oldStr, "g");
        var newValue = value.replace(reg, newStr);
        return newValue;
    };

    //#endregion

});