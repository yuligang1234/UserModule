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

});