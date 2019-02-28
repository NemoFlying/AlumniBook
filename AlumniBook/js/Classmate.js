

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
                    <td>`+ this.Id + `</td>
                    <td>`+ this.QqId + `</td>
                    <td>`+ this.RealName + `</td>
                    <td>`+ this.UserName + `</td>
                    <td>`+ (new Date(parseInt(this.RegistDate.replace(/\D/igm, "")))).toLocaleString() + `</td>
                </tr>
            `);
            });

        }
    });

    $.post("../ClassInfo/GetCurrentClassInfo", {}, function (reData) {
        //绑定现有的问题&答案
        $(".q1").val(reData.Data.ClassQustion[0].Question);
        $(".a1").val(reData.Data.ClassQustion[0].Answer);
        $(".q2").val(reData.Data.ClassQustion[0].Question);
        $(".a2").val(reData.Data.ClassQustion[0].Answer);
        $(".q3").val(reData.Data.ClassQustion[0].Question);
        $(".a3").val(reData.Data.ClassQustion[0].Answer);
    })
    $(".questionBtn").on("click", function () {
        $.ajax({
            dataType: "json",
            type: "post",
            url: "../ClassInfo/ClassInfoBaseUpdate",
            data: {
                qa: [{ Question: $(".q1").val(), Answer: $(".a1").val() },
                { Question: $(".q2").val(), Answer: $(".a2").val() },
                { Question: $(".q3").val(), Answer: $(".a3").val() }]
            },
            success: function (data) {
                console.log(data);
            }
        });
    });
    $(".noticeBtn").on("click", function () {
        var AddNoticeText = $(".AddNoticeText").val();
        $.ajax({
            url: "../ClassInfo/AddClassNotice",
            data: {
                Notice: AddNoticeText
            },
            type: "post",
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
                    alert(data.Msg + ",添加上失敗！");
                }
            }
        });
    });
    $(".searchBtn").on("click", function () {
        key = $("#keyWords").val();
        if (key == "") {
            alert("请输入关键字!");
            return false;
        }
        $.ajax({
            url: "../User/GetUserByKeyOrder?key="+key,
            success: function (reData) {
                console.log(reData);
                $(".userBody tbody").empty();
                $(reData).each(function () {
                    console.log(this);
                    $(".userBody tbody").append(`
                <tr>
                    <td>`+ this.Id + `</td>
                    <td>`+ this.QqId + `</td>
                    <td>`+ this.RealName + `</td>
                    <td>`+ this.UserName + `</td>
                    <td>`+ (new Date(parseInt(this.RegistDate.replace(/\D/igm, "")))).toLocaleString() + `</td>
                </tr>
            `);
                });
                //if (reData.Status = "ok") {
                //    $(".leftBottomDiv ul").empty();
                //    $(reData.Data).each(function () {
                //        $(".leftBottomDiv ul").append(`
                //    <li>
                //    <h3 title=`+ this.Id + `>` + this.Notice + `</h3>
                //    <p>`+ (new Date(parseInt(this.CreateDate.replace(/\D/igm, "")))).toLocaleString() + `</p>
                //    </li>
                //`);
                //    });

                //} else {
                //    alert(data.Msg + ",添加上失敗！");
                //}
            }
        });
            
    })
});
//Demo


layui.use('form', function () {
    var form = layui.form;
});



layui.use('table', function () {
    var table = layui.table;

});
