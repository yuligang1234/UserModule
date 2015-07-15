
define(function (require, exports, module) {

    //#region json序列化和反序列化    

    //将数组序列化为json格式
    exports.SerializeJson = function serializeJson(strs) {
        return JSON.stringify(strs);
    };

    exports.DeserializeJson = function deserializeJson(json) {
        return eval('(' + json + ')');
    };

    //#endregion
});