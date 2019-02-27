

$(function () {
    var bodyH = $(window).height();
    console.log(bodyH);
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.1) + "px");
    $(".body").css({
        "height": (bodyH * 0.9) + "px", "margin-top": (bodyH * 0.1) + "px"
    });

    $(".offBtn").on("click", function () {

        window.location.href = "../home/logon";
    });
    $(".homeBtn").on("click", function () {

        window.location.href = "../home/home";
    });
    $(".MessageBoardBtn").on("click", function () {

        window.location.href = "../home/MessageBoard";
    });
    $(".PhotoAlbumBtn").on("click", function () {
        window.location.href = "../home/imglist";
    });
});
//Demo
layui.use('form', function () {
    var form = layui.form;

    //监听提交
    form.on('submit(formDemo)', function (data) {
        layer.msg(JSON.stringify(data.field));
        return false;
    });
});



layui.use('table', function () {
    var table = layui.table;

});

//用户管理翻页控制
//layui.use('laypage', function () {
//    var laypage = layui.laypage;

//    //执行一个laypage实例
//    laypage.render({
//        elem: 'test1' //注意，这里的 test1 是 ID，不用加 # 号
//        , count: 50 //数据总数，从服务端得到
//    });
//});




//function getListData() {
//    $.ajax({
//        type: 'POST',
//        url: "", // ajax请求路径
//        data: {
//            page: currPage, //当前页数
//            rows: 10
//        },
//        dataType: 'json',
//        success: function (data) {
//            data = JSON.parse(data);
//            pageCount = data.result.pageCount;
//            shuju(data.result.dataList);
//            paged(data);
//        }
//    });
//};

//function paged(data) {
//    layui.use(['laypage', 'layer'], function () {
//        var laypage = layui.laypage
//            , layer = layui.layer;
//        var nums = 10; //每页出现数量

//        laypage({
//            cont: 'split' // 容器id
//            , pages: pageCount //总页数
//            , curr: currPage
//            , jump: function (obj, first) {
//                currPage = obj.curr;  //这里是后台返回给前端的当前页数
//                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr  ajax 再次请求
//                    getListData(currPage);
//                }
//            }
//        });
//    });
//}