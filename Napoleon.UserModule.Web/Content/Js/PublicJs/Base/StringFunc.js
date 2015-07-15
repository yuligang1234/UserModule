
define(function (require, exports, module) {

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