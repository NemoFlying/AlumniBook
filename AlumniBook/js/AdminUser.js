

$(function () {
    var bodyH = $(window).height();
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

    //初始化用戶數據
    $.ajax({
        dataType: "json",
        url: "../User/GetUserIndexInfo",
        data: {
        },
        success: function (data) {
            //console.log(data);

            $(".classHomeImg").append(`
                <img style='' src="http://q1.qlogo.cn/g?b=qq&nk=`+ data.UserInfo.QqId + `&s=140" alt="Alternate Text" />
            `);
            $(".userNames").text("" + data.UserInfo.UserName + "");

            //var formatTime1 = convertTime(this.Notice, "yyyy-MM-dd hh:mm:ss");//2015-07-11 14:12:29
            //$("#div1").text(formatTime1);
            //var formatTime2 = convertTime(dt, "yyyy年MM月dd日 hh时mm分ss秒");//2015年07月11日 14时12分29秒
            //$("#div2").text(formatTime2);
            $(".rightBottomL img").attr("src", "" + data.AlumCoverImgUrl + "");

            $(data.Notices).each(function () {
                //console.log(this)
                $(".leftBottomDiv ul").append(`
                    <li>
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                    </li>
                `);
            });
            $(data.Classmate).each(function () {
                $(".StudentsInformation ul").append(`
                    <li>
                        <p>
                            <img class='ClassmateImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ this.QqId + `&s=40" alt="">
                            <span>`+ this.UserName + `</span>
                            <span>`+ this.QqId + `</span>
                        </p>
                    </li>

                `);
            });


            $(data.Bbs).each(function () {
                $(".MessageBoard ul").append(`
                    <li title='`+ this.Id + `'>
                        <p>
                            <img class='MessageBoardImg' src="http://q1.qlogo.cn/g?b=qq&nk=`+ this.QqId + `&s=40" alt="Alternate Text" />
                            <span>`+ this.createUserName + `</span>
                            <p class='Msg'>`+ this.Msg + `</p>
                            <span class='MsgTimes'>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</span>
                        </p>
                    </li>
                `);
            });


        }

    });

    $.ajax({
        dataType: "json",
        url: "../User/GetAllClassUser",
        data: {
        },
        success: function (reData) {
            //console.log(reData);
            $(reData).each(function () {
                console.log(this);
                $(".userBody tbody").append(`
                <tr>
                    <td>`+this.Id+`</td>
                    <td>`+this.QqId+`</td>
                    <td>`+ this.RealName +`</td>
                    <td>`+ this.UserName +`</td>
                    <td>`+ (new Date(parseInt(this.RegistDate.replace(/\D/igm, "")))).toLocaleString()+`</td>
                    <td><button class="layui-btn layui-btn-sm layui-btn-radius layui-btn-danger delUser">删除</button></td>
                </tr>
            `);
            });


            //删除
            $(".delUser").on("click", function () {
                var DelId= $(this).parents("tr").find("td:first").text();
                console.log(DelId);
                $.ajax({
                    dataType: "json",
                    url: "../User/DeleteUserById",
                    data: {
                        userId: DelId
                    },
                    success: function (reData) {
                        console.log(reData);
                        if (reData.Status == "ERR") {
                            alert(reData.Msg + "删除失败！");
                        } else {
                            $.ajax({
                                dataType: "json",
                                url: "../User/GetAllClassUser",
                                data: {
                                },
                                success: function (reData) {
                                    //console.log(reData);
                                    $(".userBody tbody").empty();
                                    $(reData).each(function () {
                                        
                                        $(".userBody tbody").append(`
                                            <tr>
                                                <td>`+ this.Id + `</td>
                                                <td>`+ this.QqId + `</td>
                                                <td>`+ this.RealName + `</td>
                                                <td>`+ this.UserName + `</td>
                                                <td>`+ (new Date(parseInt(this.RegistDate.replace(/\D/igm, "")))).toLocaleString() + `</td>
                                                <td><button class="layui-btn layui-btn-sm layui-btn-radius layui-btn-danger delUser">删除</button></td>
                                            </tr>
                                        `);
                                    });
                                },
                            });
                        }
                        
                    }
                });
            });

        }
    });

    $(".noticeBtn").on("click", function () {
        var AddNoticeText = $(".AddNoticeText").val();
        console.log(AddNoticeText)
        $.ajax({
            url: "../ClassInfo/AddClassNotice",
            data: {
                Notice: AddNoticeText
            },
            type:"post",
            success: function (reData) {
                console.log(reData);
                if (reData.Status = "ok") {
                    $(".leftBottomDiv ul").empty();
                    $(reData.Data).each(function () {
                        $(".leftBottomDiv ul").append(`
                    <li>
                    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                    </li>
                `);
                    });

                } else {
                    alert(data.Msg+",添加上失敗！");
                }
            }
        });
    });
});
//Demo


layui.use('form', function () {
    var form = layui.form;

    //监听提交
    //form.on('submit(formDemo)', function (data) {
    //    layer.msg(JSON.stringify(data.field));
    //    return false;
    //});
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