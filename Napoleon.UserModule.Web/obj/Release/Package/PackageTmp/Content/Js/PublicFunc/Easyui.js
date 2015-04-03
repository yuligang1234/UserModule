
define(function (require, exports, module) {

    //创建新页面
    exports.CreateFrame = function (url, id) {
        var s = '<iframe id="' + id + '" scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;overflow-y: auto; "></iframe>';
        return s;
    };

    //新增选项卡
    exports.AddTabs = function (subtitle, url, icon, closable, id) {
        if (!$('#tabs').tabs('exists', subtitle)) {
            $('#tabs').tabs('add', {
                title: subtitle,
                content: exports.CreateFrame(url, id),
                closable: closable,
                icon: icon
            });
        } else {
            $('#tabs').tabs('select', subtitle);
        }
        exports.TabClose();
    };

    //关闭选项卡
    exports.TabClose = function () {
        /*双击关闭TAB选项卡*/
        $(".tabs-inner").dblclick(function () {
            var subtitle = $(this).children(".tabs-closable").text();
            $('#tabs').tabs('close', subtitle);
        });
        /*为选项卡绑定右键*/
        $(".tabs-inner").bind('contextmenu', function (e) {
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
            var subtitle = $(this).children(".tabs-closable").text();
            $('#mm').data("currtab", subtitle);
            $('#tabs').tabs('select', subtitle);
            return false;
        });
    };

    //右键事件
    exports.TabRightEvent = function () {
        //刷新
        $('#mm-tabupdate').click(function () {
            var currTab = $('#tabs').tabs('getSelected');
            var url = $(currTab.panel('options').content).attr('src');
            var id = $(currTab.panel('options').content).attr('id');; //获取id
            $('#tabs').tabs('update', {
                tab: currTab,
                options: {
                    content: exports.CreateFrame(url, id)
                }
            });
        });
        //关闭
        $('#mm-tabclose').click(function () {
            var currtabTitle = $('#mm').data("currtab");
            $('#tabs').tabs('close', currtabTitle);
        });
        // 关闭其他
        $('#closeother').click(function () {
            //所有所有tab对象
            var allTabs = $('#tabs').tabs('tabs');
            var currentTab = $('#tabs').tabs('getSelected');
            var currtabTitle = currentTab.panel('options').title;
            for (var i = (allTabs.length - 1) ; i >= 0; i--) {
                var tab = allTabs[i];
                var opt = tab.panel('options');
                //获取标题
                var title = opt.title;
                if (currtabTitle == title) {
                    continue;
                }
                //是否可关闭 ture:会显示一个关闭按钮，点击该按钮将关闭选项卡
                var closable = opt.closable;
                if (closable) {
                    $('#tabs').tabs('close', title);
                }

            }
        });
        // 全部关闭
        $('#mm-tabAllclose').click(function () {
            //所有所有tab对象
            var allTabs = $('#tabs').tabs('tabs');
            for (var i = (allTabs.length - 1) ; i >= 0; i--) {
                var tab = allTabs[i];
                var opt = tab.panel('options');
                //获取标题
                var title = opt.title;
                //是否可关闭 ture:会显示一个关闭按钮，点击该按钮将关闭选项卡
                var closable = opt.closable;
                if (closable) {
                    $('#tabs').tabs('close', title);
                }
            }
        });
        //退出
        $("#mm-exit").click(function () {
            $('#mm').menu('hide');
        });
    };

    //自定义验证
    exports.ValidateExtend = function () {
        $.extend($.fn.validatebox.defaults.rules, {
            //两个值判断是否相等
            equalTo: {
                validator: function (value, param) {
                    return $(param[0]).val() == value;
                },
                message: '两次输入密码不匹配'
            }
        });
    };

    //获取操作权限
    exports.LoadOperate = function (selector, id) {
        $.ajax({
            url: '/Ajax/GetOperate',
            async: false,
            data: { menuId: id },
            type: 'post',
            complete: function (result) {
                $(selector).append(result.responseText);
                $.parser.parse($(selector));
            }
        });
    };

    //加载树节点
    exports.LoadTree = function (selector, url, isChecked, isLines) {
        $(selector).tree({
            url: url,
            method: 'get',
            animate: true,
            checkbox: isChecked === undefined ? false : isChecked,
            lines: isLines === undefined ? false : isLines
        });
    };

    //加载菜单树节点
    exports.LoadMenuTree = function (selector) {
        $(selector).tree({
            url: '/Ajax/GetTree?randId=' + Math.random(),
            method: 'get',
            animate: true,
            onClick: function (node) {
                //不是父节点
                if (node.children == undefined) {
                    var id, title, url, icon;
                    id = node.id;
                    title = node.text;
                    url = node.url;
                    icon = node.iconCls;
                    exports.AddTabs(title, url, icon, true, id);
                }
            }
        });
    };

    //加载treegrid
    //gridColumns:[{filed:'123',title:'123',rowspan:1,colspan:1,width:100,align:'left',halign:'center'}]
    exports.LoadTreeGrid = function (selector, url, gridColumns, idField, treeField, title) {
        $(selector).treegrid({
            url: url,
            columns: [gridColumns],
            idField: idField,
            treeField: treeField,
            animate: true,
            title: title
        });
    };

    //重新加载treegrid
    //parameters:{id:1,name:123}
    exports.ReloadTreeGrid = function (selector, url, parameters) {
        $.getJSON(url, parameters, function (data) {
            $(selector).treegrid('loadData', data);
        });
    };

    //设置treegrid的宽高
    exports.ResizeTreeGrid = function (selector, height, width) {
        $(selector).treegrid('resize', {
            height: height,
            width: width
        });
    };

    //加载表格
    //gridColumns:[{filed:'123',title:'123',rowspan:1,colspan:1,width:100,align:'left',halign:'center'}]
    exports.LoadDataGrid = function (selector, url, gridColumns, title, isSingle, pageSize, pageList, dbClickFunc) {
        $(selector).datagrid({
            url: url,
            columns: [gridColumns],
            //idfield: 'Id', //绑定的主键
            singleSelect: isSingle === undefined ? true : isSingle, //单选
            title: title,
            pagination: true, //显示分页
            pageSize: pageSize === undefined ? 20 : pageSize, //每页的数量
            pageList: pageList === undefined ? [20, 40, 60, 80, 100] : pageList, //分页数选择
            rownumbers: true, //序号
            fitColumns: true, //配合Columns的Width属性，根据table的宽度，自动调整
            onDblClickRow: function () {
                if (dbClickFunc === undefined) {
                    return;
                }
                dbClickFunc();
            }
        });
    };

    //重新加载表格
    //parameters:{id:1,name:123}
    exports.ReloadDataGrid = function (selector, url, parameters) {
        $.getJSON(url, parameters, function (data) {
            $(selector).datagrid('loadData', data);
        });
    };

    //设置datagrid的宽高
    exports.ResizeDataGrid = function (selector, height, width) {
        $(selector).datagrid('resize', {
            height: height,
            width: width
        });
    };

    //显示小窗体
    //width:宽度,height:高度,isModal:是否显示遮罩效果,minimizable:是否显示最小化按钮,maximizable:是否显示最大化按钮
    exports.ShowWindow = function (selector, title, url, width, height, isModal, minimizable, maximizable) {
        var widths = width === undefined ? '600' : width;
        var heights = height === undefined ? '450' : height;
        $(selector).window({
            title: title,
            width: widths,
            height: heights,
            top: ($(window).height() - heights) / 2,
            left: ($(window).width() - widths) / 2,
            content: '<iframe scrolling="yes" frameborder="0"  src="' + url + '" style="width:100%;height:98%;"></iframe>',
            modal: isModal === undefined ? true : isModal,
            minimizable: minimizable === undefined ? false : minimizable,
            maximizable: maximizable === undefined ? false : maximizable,
            shadow: false,
            cache: false,
            closed: false,
            collapsible: false,
            resizable: false,
            loadingMessage: '正在加载数据，请稍等......'
        });
    };

    //在父页面显示小窗体
    exports.ShowParentWindow = function (selector, title, url, width, height, isModal, minimizable, maximizable) {
        var widths = width === undefined ? '600' : width;
        var heights = height === undefined ? '450' : height;
        parent.window.$(selector).window({
            title: title,
            width: widths,
            height: heights,
            top: ($(parent.window).height() - heights) / 2,
            left: ($(parent.window).width() - widths) / 2,
            content: '<iframe scrolling="yes" frameborder="0"  src="' + url + '" style="width:100%;height:98%;"></iframe>',
            modal: isModal === undefined ? true : isModal,
            minimizable: minimizable === undefined ? false : minimizable,
            maximizable: maximizable === undefined ? false : maximizable,
            shadow: false,
            cache: false,
            closed: false,
            collapsible: false,
            resizable: false,
            loadingMessage: '正在加载数据，请稍等......'
        });
    };

    //下拉框
    exports.LoadCombobox = function (selector, url, isEdit, panelHeight, selectFunc) {
        $(selector).combobox({
            url: url,
            valueField: 'id',
            textField: 'text',
            editable: isEdit === undefined ? false : isEdit,
            panelHeight: panelHeight === undefined ? 'auto' : panelHeight,
            onSelect: function (data) {
                if (selectFunc != undefined) {
                    selectFunc(data);
                }
            }
        });
    };

})