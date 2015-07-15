define(function (require, exports, module) {
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
});